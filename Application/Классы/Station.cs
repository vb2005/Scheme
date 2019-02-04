using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic.PowerPacks;

namespace PowerLine
{
    /// <summary>
    /// Класс, описывающий стационартный объект на линии
    /// </summary>
    [Serializable]
    public class Station
    {
        public int Size = 75;

        /// <summary>
        /// Тип объекта
        /// </summary>
        public StationType Type { get; set; }

        /// <summary>
        /// Включён или выключен
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Описание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Расположение объекта
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Угол наклона объекта
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Требуется ли обновление элемнета
        /// </summary>
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// Метод получения объекта по её свойствам
        /// </summary>
        /// <returns>Возвращает линию с её характеристиками</returns>
        public RectangleShape GetGraphics(MouseEventHandler CLICK, MouseEventHandler MOVE)
        {
            RectangleShape pb = new RectangleShape();
            pb.BackColor = Color.Transparent;
            pb.Height = Size;//pb.BackgroundImage.Height;
            pb.Width = Size;//pb.BackgroundImage.Width;



            pb.Location = Location;
            pb.MouseClick += CLICK;
            pb.MouseMove += MOVE;
            pb.BackgroundImageLayout = ImageLayout.Stretch;
            pb.BorderStyle = DashStyle.Custom;
            pb.BorderColor = Color.Transparent;
            pb.Tag = this;
            
            switch (Type)
            {
                    // МТП Своя
                case StationType.Station1:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x1, Angle, true);
                    break;

                    // МТП Чужая
                case StationType.Station2:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x2, Angle, true);
                    break;

                    // ТП Своя
                case StationType.Station3:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x3, Angle, true);
                    break;

                    // ТП Чужая
                case StationType.Station4:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x4, Angle, true);
                    break;

                    // Земля
                case StationType.Station5:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x10, Angle, true);
                    break;

                    // ЗТП своя
                case StationType.Station6:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x5, -Angle, true);
                    break;

                    // ЗТП чужая
                case StationType.Station7:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x6, -Angle, true);
                    break;

                    // Каб. вставка
                case StationType.Station8:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x7, Angle, true);
                    break;

                    // Каб. вставка
                case StationType.Station9:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x8, Angle, true);
                    break;

                    // Трансформатор Белый
                case StationType.Station10:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.x9, Angle, true);
                    break;


                case StationType.Station11:
                    if (State)
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.s1, Angle, true);
                    else
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.d1, Angle, true);
                    break;
                case StationType.Station12:
                    if (State)
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.s2, Angle, true);
                    else
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.d2, Angle, true);
                    break;
                case StationType.Station13:
                    if (State)
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.s3, Angle, true);
                    else
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.d3, Angle, true);
                    break;
                case StationType.Station14:
                    if (State)
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.s4, Angle, true);
                    else
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.d4, Angle, true);
                    break;


                case StationType.Station15:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.a2, Angle, false);
                    break;
                case StationType.Station16:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.a1, Angle, false);
                    break;
                case StationType.Station17:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.a3, Angle, false);
                    break;
                case StationType.Station18:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.a8, Angle, false);
                    break;
                case StationType.Station19:
                    pb.BackgroundImage = PowerLine.Properties.Resources.a4;
                    pb.Height = 40;
                    pb.Width = 125;
                    pb.Location = new Point(pb.Location.X - 50, pb.Location.Y + 10);
                    break;
                case StationType.Station20:
                    pb.BackgroundImage = PowerLine.Properties.Resources.a5;
                    pb.Height = 40;
                    pb.Width = 125;
                    pb.Location = new Point(pb.Location.X - 50, pb.Location.Y + 10);
                    break;
                case StationType.Station21:
                    pb.BackgroundImage = PowerLine.Properties.Resources.a6;
                    pb.Height = 40;
                    pb.Width = 125;
                    pb.Location = new Point(pb.Location.X - 50, pb.Location.Y+10);
                    break;
                case StationType.Station22:
                    pb.BackgroundImage = PowerLine.Properties.Resources.a7;
                    pb.Height = 40;
                    pb.Width = 125;
                    pb.Location = new Point(pb.Location.X - 50, pb.Location.Y + 10);
                    break;
                    // Code reserved for next element
                case StationType.Station23:
                    if (State)
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.s5, Angle+90, true);
                    else
                        pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.d5, Angle+90, true);
                    break;

                case StationType.Station24:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t2, Angle, true);
                    break;

                case StationType.Station25:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t3, Angle, true);
                    break;

                case StationType.Station26:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t4, Angle, true);
                    break;

                case StationType.Station27:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t5, Angle, true);
                    break;

                case StationType.Station28:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t6, Angle, true);
                    break;

                case StationType.Station29:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t7, Angle, true);
                    break;

                case StationType.Station30:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.l1, Angle, true);
                    break;

                case StationType.Station31:
                    pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.l2, Angle, true);
                    break;


             //   case StationType.Station30:
             //       pb.BackgroundImage = RotateImage(PowerLine.Properties.Resources.t8, Angle, true);
             //       break;
                default:
                    break;
            }
         

            return pb;
        }

        /// <summary>
        /// поворачивает изображение по часовой стрелке или против часовой стрелки
        /// </summary>
        /// <param name="img">изображение</param>
        /// <param name="rotationAngle">угол (в градусах).
        /// Положительные числа - по часовой стрелке
        /// отрицательные - против часовой стрелки
        /// </param>
        /// <returns></returns>
        public Image RotateImage(Image img, float rotationAngle, bool rotate)
        {
          
            img = new Bitmap(img, new System.Drawing.Size(Size, Size));
            Bitmap bmp = new Bitmap(img.Width * 2, img.Height * 2);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            if (rotate) gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 4, -(float)bmp.Height / 4);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));
            gfx.Dispose();
            return bmp;
        }

        /// <summary>
        /// Возвращает полную независимую копию объекта
        /// </summary>
        /// <returns></returns>
        public Station Clone() {
            return new Station()
            {
                State = State,
                Angle = Angle,
                Description = Description,
                Location = new Point(Location.X, Location.Y),
                NeedUpdate = true,
                Type = Type
            };
        }
    }

    /// <summary>
    /// Типы возможных стационарных объектов
    /// </summary>
    [Serializable]
    public enum StationType
    {
        // МТП
        Station1 = 0, 
        Station2 = 1,
        // КТП
        Station3 = 2, 
        Station4 = 3, 
        // Земля
        Station5 = 4,
        // ЗТП
        Station6 = 5, 
        Station7 = 6, 
        // В
        Station8 = 7, 
        Station9 = 8, 
        // Трансформатор
        Station10 = 9, 
        // ЛР
        Station11 = 10,
        // МВ
        Station12 = 11, 
        // МВ 2
        Station13 = 12,
        // ВН
        Station14 = 13, 
        // Заземление 2
        Station15 = 14,
        // Работа
        Station16 = 15, 
        // Молния
        Station17 = 16, 
        // РЗА
        Station18 = 17,
        // Табличка 1
        Station19 = 18,
        // Табличка 2
        Station20 = 19,
        // Табличка 3
        Station21 = 20,
        // Табличка 4
        Station22 = 21,
        // Резерв
        Station23 = 22,

        // T-1
        Station24 = 23,
        // T-2
        Station25 = 24,
        // T-3
        Station26 = 25,
        // T-4
        Station27 = 26,
        // T-5
        Station28 = 27,
        // T-6
        Station29 = 28,
        // T-7
        Station30 = 29,

        // Line 1
        Station31 = 30,
        // Line 2
        Station32 = 31,

    }
}
