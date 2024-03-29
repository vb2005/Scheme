﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Net;
using PowerLine.Формы_дополн;
namespace PowerLine
{
    public partial class Form1 : Form
    {
        /*TODO: 
         * Сделать обновление схем
         * Проверить работу режима №2
         * 
         */

        //Точки для привязки объектов и линий
        public List<Point> Locations;

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

        // Режим пользователя
        // TODO: В режиме отладки делаем FALSE
        public bool UserMode = true;

        // Режим блокировки действий
        // TODO: В режиме отладки делаем FALSE
        public bool Blocked = false;

        // Ok
        public Form1()
        {
            InitializeComponent();
         
            fz = new ZoomForm(new Bitmap(1,1));
            fz.Click2 += new MouseEventHandler(fz_Click2);
            DataBase.AddCity(выбратьГородToolStripMenuItem, Properties.Settings.Default["Username"].ToString());
            Instance = this;
            Link(false);
            LoadWeather();
            ReadFromServer(true);
         //  DataBase.InternalCode();
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
                string city = Properties.Settings.Default["Weather"].ToString();
                if (city != "")
                {
                    Bitmap bmp = new Bitmap((new WebClient()).OpenRead("https://info.weather.yandex.net/" + city + "/4_white.ru.png?domain=ru"));
                    pictureBox1.Image = bmp;
                    pictureBox1.Visible = true;
                    pictureBox1.BringToFront();
                }
                else {
                    pictureBox1.Visible = false;
                }
            }
            catch (Exception)
            {
                pictureBox1.Visible = false;
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
        int _mode = 1;
        private void ModeEvt(object sender, EventArgs e)
        {
            if (!UserMode)
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
        }
        #endregion




        #region Net&Pro

      


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
            //        l.ColorIndex = Color.FromArgb(200, 150, 100).ToArgb();
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


        private void административныеНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AdminInfo().ShowDialog();
            LoadWeather();
        }

        private void перейтиВРежимКорректировкиСхемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserMode)
                if (new EnterPassword().ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    UserMode = false;
                else
                    UserMode = true;
            else
                UserMode = true;

            AdminModeRules();
        }

        //Ok
        private void AdminModeRules() {
            перейтиВРежимКорректировкиСхемToolStripMenuItem.Checked = !UserMode;
            _mode = 1;
            proToolStripMenuItem.Visible = !UserMode;
            режимToolStripMenuItem.Visible = !UserMode;
            toolStripMenuItem2.Visible = UserMode;
            файлToolStripMenuItem1.Visible = !UserMode;
            UpdateLines(true, true);
        }

        // Обновление схемы
        private void timer2_Tick(object sender, EventArgs e)
        {
            // TODO >>
            ReadFromServer(false);
           // SendToServer();
        }

        // Обновление схемы
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // TODO >>
            SendToServer();
        }

        public void ReadFromServer(bool isNewFile)
        {
            try
            {

            var pr = Properties_.ReadNew(
                Properties.Settings.Default["Path"] + 
                Properties.Settings.Default["Name"].ToString());

            // В том случае, если сведения на сервере новее текущих, то обновляем текущие
            if ((Properties_.GetInstance().Date < pr.Date)|isNewFile)
            {
                Properties_._instance = pr;
                Lines = pr.Lines;
                Stations = pr.Stations;
                Texts = pr.Texts;
                UpdateLines(true, true);
            }
            else if (Properties_._instance.Date > pr.Date)
            {
                Properties_.Save(Properties.Settings.Default["Path"] +
                    Properties.Settings.Default["Name"].ToString(), Properties_.GetInstance());
            }

            string RecoverName1 = Application.StartupPath + "\\Recovery\\" + "Восстановление " + Properties.Settings.Default["Name"].ToString() + " от " + DateTime.Now.ToString("d.MMM yyyy") + ".sch";
            string RecoverName2 = Application.StartupPath + "\\Recovery\\" + "Текущая схема.sch";
            if (!System.IO.File.Exists(RecoverName1))
                Properties_.Save(RecoverName1, Properties_._instance);

            Properties_.Save(RecoverName2, Properties_._instance);
            последнееОбновлениеToolStripMenuItem.ForeColor = Color.Green;
            последнееОбновлениеToolStripMenuItem.Text = "Обновлено в " + DateTime.Now.ToShortTimeString();

            }
            catch (Exception)
            {
                последнееОбновлениеToolStripMenuItem.ForeColor = Color.Firebrick;
                последнееОбновлениеToolStripMenuItem.Text = "Нет доступа к сети!";
            }
        }

        public void SendToServer() {
            try
            {
                Properties_ p = Properties_._instance;// new Properties_() { Lines = this.Lines, Stations = this.Stations, Texts = this.Texts };

                string RecoverName1 = Application.StartupPath + "\\Recovery\\" + "Восстановление от " + DateTime.Now.ToString("d.MMM yyyy") + ".sch";
                string RecoverName2 = Application.StartupPath + "\\Recovery\\" + "Текущая схема.sch";
                if (!System.IO.File.Exists(RecoverName1))
                    Properties_.Save(RecoverName2, p);
                Properties_.Save(RecoverName2, p);
                Properties_.Save(Properties.Settings.Default["Path"] + Properties.Settings.Default["Name"].ToString(), p);
                последнееОбновлениеToolStripMenuItem.ForeColor = Color.Green;
                последнееОбновлениеToolStripMenuItem.Text = "Обновлено в " + DateTime.Now.ToShortTimeString();

            }
            catch (Exception)
            {
                последнееОбновлениеToolStripMenuItem.ForeColor = Color.Firebrick;
                последнееОбновлениеToolStripMenuItem.Text = "Нет доступа к сети!";

            }
        }

        private void восстановитьСхемуИзРезервнойКопииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recovery rec = new Recovery();
            if (rec.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Properties_ p = Properties_.Read(Application.StartupPath + "\\Recovery\\" + rec.Result);
                Lines = p.Lines;
                Stations = p.Stations;
                Texts = p.Texts;
                UpdateLines(false, true);
                Properties_._instance.Date = DateTime.Now;
                LoadWeather();

            }
        }

        private void увеличитьМасштабToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
                gr.CopyFromScreen(panel1.PointToScreen(Point.Empty), Point.Empty, panel1.Size);

            fz.Update(bitmap);
            fz.ShowDialog();
        }

        private void Form_Scroll(object sender, ScrollEventArgs e)
        {
            menuStrip1.Location = new Point(0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void поднятьВсёНа50ПиксToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var l in Lines)
            {
                l.StartPoint = new Point(l.StartPoint.X+20, l.StartPoint.Y);
                l.EndPoint = new Point(l.EndPoint.X + 20, l.EndPoint.Y);
            }
            foreach (var s in Stations)
            {
                s.Location = new Point(s.Location.X + 20, s.Location.Y);
            }
            foreach (var s in Texts)
            {
                s.Location = new Point(s.Location.X + 20, s.Location.Y);
            }
            UpdateLines(false, true);
        }
    }


}
