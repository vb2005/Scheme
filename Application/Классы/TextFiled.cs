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
    public class TextField
    {
        /// <summary>
        /// Описание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Расположение объекта
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Размер шрифта
        /// </summary>
        public int Size { get; set; }


        /// <summary>
        /// Требуется ли обновление элемнета
        /// </summary>
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// Метод получения объекта по её свойствам
        /// </summary>
        /// <returns>Возвращает линию с её характеристиками</returns>
        public Label GetGraphics(MouseEventHandler CLICK, MouseEventHandler MOVE)
        {
            Label pb = new Label();
            pb.BackColor = Color.Transparent ;
            pb.ForeColor = Color.White;
            if (Description.Contains("АС"))
            pb.ForeColor = Color.Green;
            pb.AutoSize = false;
            pb.Height = Description.Split('\n').Length * Size *2;
            pb.Width = 10;
            foreach (var a in Description.Split('\n'))
                pb.Width = Math.Max(pb.Width, a.Length * Size);
            pb.TextAlign = ContentAlignment.MiddleCenter;
            pb.Location = new Point(Location.X - pb.Width / 2, Location.Y - pb.Height / 2);
            if (Size == 0) Size = 10;
            pb.Font = new Font(pb.Font.FontFamily, Size, FontStyle.Regular);
            pb.MouseClick += CLICK;
            pb.MouseMove += MOVE;
            pb.Text = Description;
            pb.Tag = this;
            return pb;
        }

        /// <summary>
        /// Возвращает полную независимую копию объекта
        /// </summary>
        /// <returns></returns>
        public TextField Clone()
        {
            return new TextField()
            {
                Location = new Point(Location.X, Location.Y),
                Description = Description,
                Size = Size,
                NeedUpdate = true
            };
        }
    }
}
