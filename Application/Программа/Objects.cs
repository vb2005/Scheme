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
        /// <summary>
        /// Процедура щелчка по Station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Click2(object sender, MouseEventArgs e)
        {
            // В первой части определяем объект по которому произошёл щелчок.
            // Если объект иной - пождменяем SENDER

            int X = e.X + ((RectangleShape)sender).Left - ((RectangleShape)sender).Width / 2;
            int Y = e.Y + ((RectangleShape)sender).Top - ((RectangleShape)sender).Height / 2;

            RectangleShape s1 = (RectangleShape)sender;
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


            // Режим удаления, корректировки состояния, добавления нового компонента
            if (_mode == 1)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    Station l = (Station)((RectangleShape)sender).Tag;
                    Stations.Remove(l);
                    UpdateLines(false, true);
                }

                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    new CorrectPosition((Station)((RectangleShape)sender).Tag).ShowDialog();
                }

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    MouseEventArgs args = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, e.X + ((RectangleShape)sender).Bounds.Left - 37, e.Y + ((RectangleShape)sender).Bounds.Top - 37, 0);
                    CLICK(null, args);
                }
            }

            // Режим изменения состояний
            if (_mode == 2)
            {
                Station l = (Station)((RectangleShape)sender).Tag;

                // TODO: Сделано для поворота трасформаторов. Убрать потом
                if (l.Type != StationType.Station10)
                    l.State = !l.State;
                else
                    l.Angle = 180 + l.Angle;

                l.NeedUpdate = true;
                UpdateLines(false, false);
            }
        }



        /// <summary>
        /// Процедура щелчка по Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Click3(object sender, MouseEventArgs e)
        {
            // Удаление компонента
            if (_mode == 1)
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    TextField l = (TextField)((Label)sender).Tag;
                    Texts.Remove(l);
                    UpdateLines(false, true);
                }

            // Изменение расположения
            if (_mode==1)
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    new CorrectPosition((TextField)((Label)sender).Tag).ShowDialog();
                }
        }



        /// <summary>
        /// Процедура щелчка по линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Click1(object sender, MouseEventArgs e)
        {
            // Удлание линии
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Line l = (Line)((LineShape)sender).Tag;
                Lines.Remove(l);
                UpdateLines(false, true);
            }

            // Добавление на неё контрола
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (_mode == 1)
                {
                    // Добавляем новый контрол
                    Point p = e.Location;
                    LineShape ln = (LineShape)sender;
                    p.X += Math.Min(ln.StartPoint.X, ln.EndPoint.X) - 37;
                    p.Y += Math.Min(ln.StartPoint.Y, ln.EndPoint.Y) - 37;

                    Station s = new Station();
                    if (new SetStation(s).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (s.Angle == 0)
                            if (ln.StartPoint.X == ln.EndPoint.X)
                                s.Angle = 90;
                            else
                                s.Angle = (float)(Math.Atan((double)(ln.StartPoint.Y - ln.EndPoint.Y) / (double)(ln.StartPoint.X - ln.EndPoint.X)) * 180f / Math.PI);
                        s.Location = p;
                        s.NeedUpdate = true;
                        Stations.Add(s);
                        UpdateLines(false, false);
                    }
                }
            }
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
