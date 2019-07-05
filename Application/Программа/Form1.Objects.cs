using System;
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
        /// <summary>
        /// Процедура щелчка по Station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Click2(object sender, MouseEventArgs e)
        {
            Station tag;
            MouseEventArgs args;
            Point location = this.panel1.Location;
            int X = (e.X + ((RectangleShape)sender).Left) - (((RectangleShape)sender).Width / 2);
            int Y = (e.Y + ((RectangleShape)sender).Top) - (((RectangleShape)sender).Height / 2);
            RectangleShape shape = (RectangleShape)sender;
            int max = 100000;
            for (int i = 0; i < this.shapeContainer2.Shapes.Count; i++)
            {
                if (this.shapeContainer2.Shapes.get_Item(i).GetType() == typeof(RectangleShape))
                {
                    int left = ((RectangleShape)this.shapeContainer2.Shapes.get_Item(i)).Left;
                    int top = ((RectangleShape)this.shapeContainer2.Shapes.get_Item(i)).Top;
                    int len = (int)Math.Sqrt(Math.Pow((double)(X - left), 2.0) + Math.Pow((double)(Y - top), 2.0));
                    if (max > len)
                    {
                        max = len;
                        shape = (RectangleShape)this.shapeContainer2.Shapes.get_Item(i);
                    }
                }
            }
            var sender1 = sender;
            sender = shape;

            // Режим пользователя
            if (this.UserMode)
            {
                if (Blocked) return;
                if (e.Button == MouseButtons.Right)
                {
                    tag = (Station)((RectangleShape)sender).Tag;
                    if ((tag.Type > StationType.Station14) & (tag.Type < StationType.Station24))
                    {
                        this.Stations.Remove(tag);
                    }
                    this.UpdateLines(false, true);
                    Properties_._instance.Date = DateTime.Now;
                }
                if (e.Button == MouseButtons.Left)
                {
                    tag = (Station)((RectangleShape)sender).Tag;
                    tag.State = !tag.State;
                    tag.NeedUpdate = true;
                    this.UpdateLines(false, false);
                    Properties_._instance.Date = DateTime.Now;
                }
                if (e.Button == MouseButtons.Middle)
                {
                    args = new MouseEventArgs(MouseButtons.Middle, 1, (e.X + ((RectangleShape)sender1).Left) - 30, (e.Y + ((RectangleShape)sender1).Top) - 30, 0);
                    this.CLICK(null, args);
                    Properties_._instance.Date = DateTime.Now;
                }
            }

            // Режим администратора
            else if (this._mode == 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    tag = (Station)((RectangleShape)sender).Tag;
                    this.Stations.Remove(tag);
                    this.UpdateLines(false, true);
                    Properties_._instance.Date = DateTime.Now;
                }
                if (e.Button == MouseButtons.Middle)
                {
                    new CorrectPosition((Station)((RectangleShape)sender).Tag).ShowDialog();
                    Properties_._instance.Date = DateTime.Now;
                }
                if (e.Button == MouseButtons.Left)
                {
                    args = new MouseEventArgs(MouseButtons.Left, 1, (e.X + ((RectangleShape)sender1).Left) - 0x25, (e.Y + ((RectangleShape)sender1).Top) - 0x25, 0);
                    this.CLICK(null, args);
                    Properties_._instance.Date = DateTime.Now;
                }
            }
            if ((this._mode == 2) && !this.UserMode)
            {
                tag = (Station)((RectangleShape)sender).Tag;
                if (tag.Type != StationType.Station10)
                {
                    tag.State = !tag.State;
                }
                else
                {
                    tag.Angle = 180f + tag.Angle;
                }
                tag.NeedUpdate = true;
                this.UpdateLines(false, false);
                Properties_._instance.Date = DateTime.Now;
            }
            this.panel1.Location = location;
        }



        /// <summary>
        /// Процедура щелчка по Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Click3(object sender, MouseEventArgs e)
        {
            Point location = this.panel1.Location;
            if (!UserMode)
            {
                // Удаление
                if ((_mode == 1) && (e.Button == MouseButtons.Right))
                {
                    TextField tag = (TextField)((Label)sender).Tag;
                    Texts.Remove(tag);
                    UpdateLines(false, true);
                    Properties_._instance.Date = DateTime.Now;
                }
                // изменение позиции
                if ((_mode == 1) && (e.Button == MouseButtons.Middle))
                {
                    new CorrectPosition((TextField)((Label)sender).Tag).ShowDialog();
                }
            }
            panel1.Location = location;
        }



        /// <summary>
        /// Процедура щелчка по линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Click1(object sender, MouseEventArgs e)
        {
            Point point2;
            LineShape shape;
            Station station;
            Point location = this.panel1.Location;
            if (this.UserMode)
            {
                if (Blocked) return;
                if (e.Button == MouseButtons.Middle)
                {
                    point2 = e.Location;
                    shape = (LineShape)sender;
                    point2.X += Math.Min(shape.StartPoint.X, shape.EndPoint.X) - 0x25;
                    point2.Y += Math.Min(shape.StartPoint.Y, shape.EndPoint.Y) - 0x25;
                    station = new Station();
                    if (new SetStation(station, 1).ShowDialog() == DialogResult.OK)
                    {
                        if (station.Angle == 0f)
                        {
                            if (shape.StartPoint.X == shape.EndPoint.X)
                            {
                                station.Angle = 90f;
                            }
                            else
                            {
                                station.Angle = (float)((Math.Atan(((double)(shape.StartPoint.Y - shape.EndPoint.Y)) / ((double)(shape.StartPoint.X - shape.EndPoint.X))) * 180.0) / 3.1415926535897931);
                            }
                        }
                        station.Location = point2;
                        // Для больших табличек делаем смещение
                        if (((int)station.Type >= 18) && ((int)station.Type <= 21))
                            station.Location = new Point(point2.X + 10, point2.Y);
                        station.NeedUpdate = true;
                        this.Stations.Add(station);
                        this.UpdateLines(false, false);
                        Properties_._instance.Date = DateTime.Now;
                    }
                }
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    Line tag = (Line)((LineShape)sender).Tag;
                    this.Lines.Remove(tag);
                    this.UpdateLines(false, true);
                    Properties_._instance.Date = DateTime.Now;
                }
                if ((e.Button == MouseButtons.Left) && (this._mode == 1))
                {
                    point2 = e.Location;
                    shape = (LineShape)sender;
                    point2.X += Math.Min(shape.StartPoint.X, shape.EndPoint.X) - 0x25;
                    point2.Y += Math.Min(shape.StartPoint.Y, shape.EndPoint.Y) - 0x25;
                    station = new Station();
                    if (new SetStation(station, 0).ShowDialog() == DialogResult.OK)
                    {
                        if (station.Angle == 0f)
                        {
                            if (shape.StartPoint.X == shape.EndPoint.X)
                            {
                                station.Angle = 90f;
                            }
                            else
                            {
                                station.Angle = (float)((Math.Atan(((double)(shape.StartPoint.Y - shape.EndPoint.Y)) / ((double)(shape.StartPoint.X - shape.EndPoint.X))) * 180.0) / 3.1415926535897931);
                            }
                        }
                        station.Location = point2;
                        // Для больших табличек делаем смещение
                        if (((int)station.Type >= 18) && ((int)station.Type <= 21))
                            station.Location = new Point(point2.X + 10, point2.Y);
                        station.NeedUpdate = true;
                        this.Stations.Add(station);
                        this.UpdateLines(false, false);
                        Properties_._instance.Date = DateTime.Now;
                    }
                }
            }
            this.panel1.Location = location;
        }

        #region Deprecated

        /// <summary>
        /// Процедура движения по линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Move1(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// ПРоцедура движения по Station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Move2(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Процедура движения по Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Move3(object sender, MouseEventArgs e)
        {

        }
        #endregion
    }
}
