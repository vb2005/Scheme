using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
namespace PowerLine
{
    /// <summary>
    /// Класс содержит описание поведённой линии на поле
    /// </summary>
    [Serializable]
    public class Line
    {
        /// <summary>
        /// Название линии
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Задана ли линия штрихами
        /// </summary>
        public bool Dash { get; set; }

        /// <summary>
        /// Ширина линии
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Цвет линии
        /// </summary>
        public int ColorIndex { get; set; }

        /// <summary>
        /// Начальная точка линии
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Конечная точка линии
        /// </summary>
        public Point EndPoint { get; set; }

        /// <summary>
        /// Требуется ли обновление элемнета
        /// </summary>
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// Метод получения линии по её свойствам
        /// </summary>
        /// <returns>Возвращает линию с её характеристиками</returns>
        public LineShape GetGraphics(MouseEventHandler CLICK, MouseEventHandler MOVE, int mode = 0)
        {
            LineShape l = new LineShape();
            l.BorderColor = Color.FromArgb(ColorIndex);
            l.BorderWidth = Width;
            l.BorderStyle = (Dash) ? DashStyle.Dash : DashStyle.Solid;
            l.X1 = StartPoint.X;
            l.Y1 = StartPoint.Y;
            l.X2 = EndPoint.X;
            l.Y2 = EndPoint.Y;
            l.Tag = this;
            l.Enabled = (mode != 0);
            l.MouseMove += MOVE;
            l.MouseClick += CLICK;
            return l;
        }

        public static Point getNear(Point p) {
            int X = (int)Math.Round(p.X / (double)Form1.Instance.GridSize) * Form1.Instance.GridSize;
            int Y = (int)Math.Round(p.Y / (double)Form1.Instance.GridSize) * Form1.Instance.GridSize;
            return new Point(X, Y);
        }

        /// <summary>
        /// Возвращает полную независимую копию объекта
        /// </summary>
        /// <returns></returns>
        public Line Clone()
        {
            return new Line()
            {
                ColorIndex = ColorIndex,
                Dash = Dash,
                Description = Description,
                EndPoint = new Point(EndPoint.X, EndPoint.Y),
                Name = Name,
                NeedUpdate = true,
                StartPoint = new Point(StartPoint.X, StartPoint.Y),
                Width = Width
            };
        }

    }
}
