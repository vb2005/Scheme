using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using PowerLine.Классы;
using System.Net;
using PowerLine.Формы_дополн;
namespace PowerLine
{
    public partial class Form1 : Form
    {
        // Точки для привязки объектов и линий
     //   public List<Point> Locations;

        // Текущий инстанс формы
        public static Form1 Instance;

        // Вектор станций
        public List<Station> Stations;

        // Вектор текстовых полей
        public List<TextField> Texts;

        // Список выделенных объектов
        public List<Object> Selection;
        public Point SelectionStart;

        // Величина шага сетки
        public int GridSize = 5;

        // Форма вывода масштабированного объекта
        private ZoomForm fz;

        public Form1()
        {
            InitializeComponent();

            fz = new ZoomForm(new Bitmap(1,1));
            fz.Click2 += new MouseEventHandler(fz_Click2);

            Instance = this;
            ds = new DataRecievedHandler(DataRecieved);
            Link(false);
            LoadWeather();

        }

        /// <summary>
        /// Грузим данные из Properties
        /// </summary>
        /// <param name="fromFile">Имя файла</param>
        public void Link(bool fromFile) {
            Properties_ pr = Properties_.GetInstance();
            Lines = pr.Lines;
            Stations = pr.Stations;
            Texts = pr.Texts;
            UpdateLines(fromFile, true);
        }

        /// <summary>
        /// Грущим погоду текущую
        /// </summary>
        public void LoadWeather() {
            try
            {
                Bitmap bmp = new Bitmap((new WebClient()).OpenRead("https://info.weather.yandex.net/" + Properties_.GetInstance().City + "/4_white.ru.png?domain=ru"));
               // pictureBox1.Image = bmp;
            }
            catch (Exception ex)
            {
            }
        }


    
        #region Чтение, сохранение и печать схемы линий

            private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Вы хотите сохранить старую схему?", "Сохранение документа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            сохранитьToolStripMenuItem_Click(null, null);
                        Lines.Clear();
                        Stations.Clear();
                        Texts.Clear();
                        UpdateLines(false,true);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Невозможно сохранить файл по данному пути!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Lines.Clear();
                    Stations.Clear();
                    Texts.Clear();
                    UpdateLines(false,true);
                }
            }
            private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Properties_ p = new Properties_() { Lines = this.Lines, Stations = this.Stations, Texts = this.Texts };
                        Properties_.Save(saveFileDialog1.FileName, p);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно сохранить файл по данному пути!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Properties_ p = Properties_.Read(openFileDialog1.FileName);
                        Lines = p.Lines;
                        Stations = p.Stations;
                        Texts = p.Texts;
                        UpdateLines(false,true);
                        LoadWeather();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно открыть файл по данному пути!", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            private void выходToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.Close();
            }
            private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PrintDocument doc = new PrintDocument();
                    doc.PrinterSettings = pd.PrinterSettings;
                    doc.PrintPage += (oo, ee) =>
                    {
                        menuStrip1.Visible = false;
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        this.BackColor = Color.White;

                        var bitmap = new Bitmap(this.Width, this.Height);
                        this.DrawToBitmap(bitmap, new Rectangle(new Point(), this.Size));
                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        ee.Graphics.DrawImage(bitmap, 0, 0, ee.PageBounds.Width, ee.PageBounds.Height);
                        //bitmap.Save(@"C:\temp\123.bmp");

                        this.BackColor = Color.FromArgb(240, 240, 240);
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                        menuStrip1.Visible = true;
                    };
                    doc.Print();
                }
            }
            private void toolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Properties_ p = new Properties_() { Lines = this.Lines, Stations = this.Stations, Texts = this.Texts };
                Properties_.Save("Autosave.sch", p);
            }
        #endregion

        #region Управление режимом работы
        int _mode = 0;
        private void ModeEvt(object sender, EventArgs e)
        {
            if (sender == редактированиеСхемыToolStripMenuItem) _mode = 0;
            if (sender == редактирвованиеОбъектовToolStripMenuItem) _mode = 1;
            if (sender == режимПросмотраToolStripMenuItem) _mode = 2;
            if (sender == режимПереносаToolStripMenuItem) _mode = 4;

            switch (_mode)
            {
                case 1:
                    редактированиеСхемыToolStripMenuItem.Checked = false;
                    редактирвованиеОбъектовToolStripMenuItem.Checked = true;
                    режимПросмотраToolStripMenuItem.Checked = false;
                    режимПереносаToolStripMenuItem.Checked = false;
                    break;
                case 2:
                    редактированиеСхемыToolStripMenuItem.Checked = false;
                    редактирвованиеОбъектовToolStripMenuItem.Checked = false;
                    режимПереносаToolStripMenuItem.Checked = false;
                    режимПросмотраToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    редактированиеСхемыToolStripMenuItem.Checked = false;
                    редактирвованиеОбъектовToolStripMenuItem.Checked = false;
                    режимПереносаToolStripMenuItem.Checked = true;
                    режимПросмотраToolStripMenuItem.Checked = false;
                    break;
                default:
                    редактированиеСхемыToolStripMenuItem.Checked = true;
                    редактирвованиеОбъектовToolStripMenuItem.Checked = false;
                    режимПереносаToolStripMenuItem.Checked = false;
                    режимПросмотраToolStripMenuItem.Checked = false;
                    break;
            }
            UpdateLines(true, true);
        }
        #endregion




        #region Net&Pro

        private void обновитьСхемыНаВсехУстройствахToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  Properties_.Send();
        }

        public void DataRecieved(string IP, Properties_ pr)
        {
            if (MessageBox.Show("Получены новые данные с устройства: " + IP + ". Выполнить обновление?", "Обновление данных", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Properties_._instance = pr;
                Form1.Instance.Link(false);
            }
        }

        public delegate void DataRecievedHandler(string IP, Properties_ pr);
        public DataRecievedHandler ds;


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Сохранить изменения?", "Сохранение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                Properties_ p = new Properties_() { Lines = this.Lines, Stations = this.Stations, Texts = this.Texts };
                Properties_.Save("Autosave.sch", p);
            }
            //UDPServer.Server.Close();
            //Program.udp.tr.Abort();

        }

        private void отобажатьОбъектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            отобажатьОбъектыToolStripMenuItem.Checked = !отобажатьОбъектыToolStripMenuItem.Checked;
            UpdateLines(false,true);
        }

        private void сделатьВесьПунктирДругимЦветомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var l in Lines)
            {
                if (l.Dash) {
                    l.ColorIndex = Color.Green.ToArgb();
                }
            }
            UpdateLines(false,true);
        }

        private void сделатьВсеЛинии10КВToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var l in Lines)
            {
                if (!l.Dash)
                    if (l.ColorIndex==Color.White.ToArgb())
                {
           //         l.ColorIndex = Color.FromArgb(100, 0, 100).ToArgb();
              //      l.ColorIndex = Color.FromArgb(200, 150, 100).ToArgb();
                    l.ColorIndex = Color.FromArgb(200, 100, 200).ToArgb();
                //    l.ColorIndex = Color.FromArgb(130, 100, 50).ToArgb();

                }
            }
            /*
            foreach (var i in Lines)
            {
                i.StartPoint = new Point(i.StartPoint.X + 100, i.StartPoint.Y);
                i.EndPoint = new Point(i.EndPoint.X + 100, i.EndPoint.Y);
            }

            foreach (var i in Stations)
            {
                i.Location = new Point(i.Location.X + 100, i.Location.Y);
            }

            foreach (var i in Texts)
            {
                i.Location = new Point(i.Location.X + 100, i.Location.Y);

            }
             * 
             * */
            UpdateLines(false,true);

        }

        #endregion

        private void использоватьСеткуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            использоватьСеткуToolStripMenuItem.Checked = !использоватьСеткуToolStripMenuItem.Checked;
            if (использоватьСеткуToolStripMenuItem.Checked) GridSize = 10; else GridSize = 1;
            UpdateLines(false, false);
        }



        public void DrawText() {
            SizeF textImageSize = panel1.CreateGraphics().MeasureString("ТЕСТ 112345678", this.Font);
            //создаем ихзображение
            Bitmap str = new Bitmap((int)textImageSize.Width + 10, (int)textImageSize.Height + 5);
            Graphics strgr = Graphics.FromImage(str);
            //рисуем строку
            strgr.DrawString("ТЕСТ 112345678", this.Font, Brushes.Black, 1, 1);
            strgr.Save();
            //а потом поворачиваем на 270 градусов (так чтобы текст читался снизу вверх)
            str.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //и рисуем на основно изображении
            panel1.CreateGraphics().DrawImage(str, 0, 600, str.Width, str.Height);
        }

        private void увеличитьМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
                gr.CopyFromScreen(panel1.PointToScreen(Point.Empty), Point.Empty, panel1.Size);

            fz.Update(bitmap);
            fz.ShowDialog();
        }

        void fz_Click2(object sender, MouseEventArgs e)
        {
            if (e.X > 0)
            {

                int X = e.X / 4;
                int Y = e.Y / 4;
                RectangleShape s1 = new RectangleShape();
                int max = 100000;
                for (int i = 0; i < shapeContainer2.Shapes.Count; i++)
                {
                    if (shapeContainer2.Shapes.get_Item(i).GetType() == typeof(RectangleShape))
                    {
                        int _x = ((RectangleShape)shapeContainer2.Shapes.get_Item(i)).Left;
                        int _y = ((RectangleShape)shapeContainer2.Shapes.get_Item(i)).Top;

                        int _max = (int)Math.Sqrt(Math.Pow(X - _x, 2) + Math.Pow(Y - _y, 2));
                        if (max > _max)
                        {
                            max = _max;
                            s1 = (RectangleShape)shapeContainer2.Shapes.get_Item(i);
                        }
                    }
                }

                Station l = (Station)((RectangleShape)s1).Tag;
                l.State = !l.State;
                l.NeedUpdate = true;
                UpdateLines(false, false);

                fz.Opacity = 0;
                Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
                using (Graphics gr = Graphics.FromImage(bitmap))
                    gr.CopyFromScreen(panel1.PointToScreen(Point.Empty), Point.Empty, panel1.Size);
                fz.Update(bitmap);
                fz.Opacity = 100;
                fz_Click2(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, -10, 0, 0));
            }
        }

        private void привязатьВсеЛинииКСеткеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var l in Lines)
            {
                if (l.EndPoint.X % 5 != 0) { l.EndPoint = new Point((int)Math.Round(l.EndPoint.X / 5d) * 5, l.EndPoint.Y); }
                if (l.EndPoint.Y % 5 != 0) { l.EndPoint = new Point(l.EndPoint.X, (int)Math.Round(l.EndPoint.Y / 5d) * 5); }

                if (l.StartPoint.X % 5 != 0) { l.StartPoint = new Point((int)Math.Round(l.StartPoint.X / 5d) * 5, l.StartPoint.Y); }
                if (l.StartPoint.Y % 5 != 0) { l.StartPoint = new Point(l.StartPoint.X, (int)Math.Round(l.StartPoint.Y / 5d) * 5); }

            }

            UpdateLines(true, true);

        }



       
    }


}
