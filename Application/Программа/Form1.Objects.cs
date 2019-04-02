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
            int num = (e.X + ((RectangleShape)sender).Left) - (((RectangleShape)sender).Width / 2);
            int num2 = (e.Y + ((RectangleShape)sender).Top) - (((RectangleShape)sender).Height / 2);
            RectangleShape shape = (RectangleShape)sender;
            int num3 = 0x186a0;
            for (int i = 0; i < this.shapeContainer2.Shapes.Count; i++)
            {
                if (this.shapeContainer2.Shapes.get_Item(i).GetType() == typeof(RectangleShape))
                {
                    int left = ((RectangleShape)this.shapeContainer2.Shapes.get_Item(i)).Left;
                    int top = ((RectangleShape)this.shapeContainer2.Shapes.get_Item(i)).Top;
                    int num7 = (int)Math.Sqrt(Math.Pow((double)(num - left), 2.0) + Math.Pow((double)(num2 - top), 2.0));
                    if (num3 > num7)
                    {
                        num3 = num7;
                        shape = (RectangleShape)this.shapeContainer2.Shapes.get_Item(i);
                    }
                }
            }
            sender = shape;
            if (this.UserMode)
            {
                if (e.Button == MouseButtons.Right)
                {
                    tag = (Station)((RectangleShape)sender).Tag;
                    if ((tag.Type > StationType.Station14) & (tag.Type < StationType.Station24))
                    {
                        this.Stations.Remove(tag);
                    }
                    this.UpdateLines(false, true);
                }
                if (e.Button == MouseButtons.Left)
                {
                    tag = (Station)((RectangleShape)sender).Tag;
                    tag.State = !tag.State;
                    tag.NeedUpdate = true;
                    this.UpdateLines(false, false);
                }
                if (e.Button == MouseButtons.Middle)
                {
                    args = new MouseEventArgs(MouseButtons.Middle, 1, (e.X + ((RectangleShape)sender).Bounds.Left) - 0x25, (e.Y + ((RectangleShape)sender).Bounds.Top) - 0x25, 0);
                    this.CLICK(null, args);
                }
            }
            else if (this._mode == 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    tag = (Station)((RectangleShape)sender).Tag;
                    this.Stations.Remove(tag);
                    this.UpdateLines(false, true);
                }
                if (e.Button == MouseButtons.Middle)
                {
                    new CorrectPosition((Station)((RectangleShape)sender).Tag).ShowDialog();
                }
                if (e.Button == MouseButtons.Left)
                {
                    args = new MouseEventArgs(MouseButtons.Left, 1, (e.X + ((RectangleShape)sender).Bounds.Left) - 0x25, (e.Y + ((RectangleShape)sender).Bounds.Top) - 0x25, 0);
                    this.CLICK(null, args);
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
            if (!this.UserMode)
            {
                // Удлаение
                if ((this._mode == 1) && (e.Button == MouseButtons.Right))
                {
                    TextField tag = (TextField)((Label)sender).Tag;
                    this.Texts.Remove(tag);
                    this.UpdateLines(false, true);
                }
                // изменение позиции
                if ((this._mode == 1) && (e.Button == MouseButtons.Middle))
                {
                    new CorrectPosition((TextField)((Label)sender).Tag).ShowDialog();
                }
            }
            this.panel1.Location = location;
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
                        station.NeedUpdate = true;
                        this.Stations.Add(station);
                        this.UpdateLines(false, false);
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
                        station.NeedUpdate = true;
                        this.Stations.Add(station);
                        this.UpdateLines(false, false);
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
