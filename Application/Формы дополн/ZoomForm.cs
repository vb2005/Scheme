using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace PowerLine.Формы_дополн
{
    public partial class ZoomForm : Form
    {
        // Делегат на щелчок по ZoomForm
        public event MouseEventHandler Click2;

        // По отображению формы запрос на её отрисовку и всё
        public ZoomForm(Bitmap src)
        {
            InitializeComponent();
            Update(src);
        }

        // Обновление содержимого формы. 
        public void Update(Bitmap src) {
            // TODO: Либо в 2 раза меньше, либо в 2 раза больше
            Bitmap x4 = new Bitmap(src, src.Width * 2, src.Height * 2);
            pictureBox1.Image = x4;
        }

        // Вызываем подписное событие. См. ниже
        private void Click1(object sender, MouseEventArgs e)
        {
            Click2(sender, e);
        }
    }
}

namespace PowerLine
{
    public partial class Form1 : Form
    {
        // Обработка щелчка по полю фантома
        void fz_Click2(object sender, MouseEventArgs e)
        {
            // Назначение доподлино не помню. Скорее всего BugFix
            if ((e.X > 0) & (e.Y > 0) & !Blocked)
            {
                // Рассмотрим 2 случая: Рядом есть элемент с коротым играемся. 
                // Рядом - это в 40 px от места щелчка
                int X = e.X / 2 - 30;
                int Y = e.Y / 2 - 40;

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
                sender = s1;
                Station l = (Station)((RectangleShape)sender).Tag;

                // К этому моменту мы получили MAX и ближайший элемент. Действуем!

                // ЛЕВО - смена состояния и обновление компонентов
                if ((max < 40) && (e.Button == System.Windows.Forms.MouseButtons.Left))
                {
                    l.State = !l.State;
                    l.NeedUpdate = true;
                    UpdateLines(false, false);
                    Properties_._instance.Date = DateTime.Now;
                }

                // ПРАВО - удлание компонента и обновление остального
                if ((max < 40) && (e.Button == System.Windows.Forms.MouseButtons.Right))
                {
                    if ((l.Type > StationType.Station14) & (l.Type < StationType.Station24))
                    {
                        this.Stations.Remove(l);
                    }
                    l.NeedUpdate = true;
                    UpdateLines(false, false);
                    Properties_._instance.Date = DateTime.Now;
                }

                // МИДЛ - добавление компонента и обновление формы
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    var args = new MouseEventArgs(MouseButtons.Middle, 1, X + 30, Y + 10, 0);
                    CLICK(null, args);
                    l.NeedUpdate = true;
                    UpdateLines(false, false);
                    Properties_._instance.Date = DateTime.Now;
                }

                Application.Idle += Application_Idle;
            }
        }

        // Отрисовка формы с её компонентами в фантом
        void Application_Idle(object sender, EventArgs e)
        {
            fz.Opacity = 0;
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
                gr.CopyFromScreen(panel1.PointToScreen(Point.Empty), Point.Empty, panel1.Size);
            fz.Update(bitmap);
            fz.Opacity = 100;
            Application.Idle -= Application_Idle;
        }
        }
    }

