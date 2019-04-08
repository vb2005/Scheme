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

namespace PowerLine
{
    public partial class Form1 : Form
    {
        #region Рисование новых ЛЭП
        Point _p;
        bool _f;
        LineShape ls;
        List<Line> Lines;

        private void UP(object sender, MouseEventArgs e)
        {
            Point location = this.panel1.Location;
            if (UserMode)
            {

            }
            else
            {
                // В режиме переноса на отжатую кнопку мыши выполняем начало переноса
                if (_mode == 5)
                {
                    _mode = 4;
                    UpdateLines(false, true);
                    return;
                }

                // В режиме рискования линий - завершаем рисование линий
                if (_mode == 0)
                    if (_f)
                        if (Math.Pow(_p.X - e.X, 2) + Math.Pow(_p.Y - e.Y, 2) > 30)
                        {
                            Point ee = e.Location;
                            _p = Line.getNear(FindNear(_p));
                            ee = Line.getNear(FindNear(ee));
                            if (Math.Abs(_p.X - ee.X) < 8) ee.X = _p.X;
                            if (Math.Abs(_p.Y - ee.Y) < 8) ee.Y = _p.Y;
                            _f = false;
                            Line l = new Line();
                            l.StartPoint = FindNear(_p);
                            l.EndPoint = FindNear(ee);
                            SetLine sl = new SetLine(l);
                            if (sl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                Lines.Add(l);

                            UpdateLines(false, true);
                            Properties_._instance.Date = DateTime.Now;
                        }

                // В режиме переноса закрепляем перенос
                if (_mode == 4)
                {
                    // ee _p
                    int minX, minY, maxX, maxY;
                    minX = Math.Min(e.Location.X, _p.X);
                    minY = Math.Min(e.Location.Y, _p.Y);
                    maxX = Math.Max(e.Location.X, _p.X);
                    maxY = Math.Max(e.Location.Y, _p.Y);
                    Selection = new List<object>();

                    List<Line> _tmp1 = new List<Line>();
                    List<Station> _tmp2 = new List<Station>();
                    List<TextField> _tmp3 = new List<TextField>();


                    foreach (var n in Lines)
                        if ((n.StartPoint.X > minX) | (n.EndPoint.X > minX))
                            if ((n.StartPoint.Y > minY) | (n.EndPoint.Y > minY))
                                if ((n.StartPoint.X < maxX) | (n.EndPoint.X < maxX))
                                    if ((n.StartPoint.Y < maxY) | (n.EndPoint.Y < maxY))
                                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                        {
                                            Line m = n.Clone();
                                            _tmp1.Add(m);
                                            Selection.Add(m);
                                        }
                                        else
                                            Selection.Add(n);
                    Lines.AddRange(_tmp1);


                    foreach (var n in Stations)
                        if ((n.Location.X > minX) & (n.Location.Y > minY) & (n.Location.X < maxX) & (n.Location.Y < maxY))
                            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            {
                                Station m = n.Clone();
                                _tmp2.Add(m);
                                Selection.Add(m);
                            }
                            else
                                Selection.Add(n);
                    Stations.AddRange(_tmp2);

                    foreach (var n in Texts)
                        if ((n.Location.X > minX) & (n.Location.Y > minY) & (n.Location.X < maxX) & (n.Location.Y < maxY))
                            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            {
                                TextField m = n.Clone();
                                _tmp3.Add(m);
                                Selection.Add(m);
                            }
                            else
                                Selection.Add(n);
                    Texts.AddRange(_tmp3);

                    if (Selection.Count > 0)
                    {
                        SelectionStart = e.Location;
                        _mode = 5;
                    }
                }


            }
            this.panel1.Location = location;
        }

        private Point FindNear(Point p)
        {
          //  List<Point> lp = Locations.FindAll(o => (Math.Sqrt(Math.Pow(p.X - o.X, 2) + Math.Pow(p.Y - o.Y, 2)) < 10));
          //  if (lp.Count == 0) return p;
            return p;// lp[0];
        }

        private void MOVE(object sender, MouseEventArgs e)
        {
            Point location = panel1.Location;
            if (UserMode)
            {

            }
            else
            {
                if (e.Button != System.Windows.Forms.MouseButtons.None)
                {
                    // Рисуем линию для переноса и ривания линий
                    if ((_mode == 0) | (_mode == 4))
                        if (_f)
                        {
                            Point m = Line.getNear(new Point(e.X, e.Y));
                            ls.X2 = m.X;
                            ls.Y2 = m.Y;
                        }

                    // На режиме №5 выполняем перенос элементов
                    if (_mode == 5)
                    {
                        foreach (var u in Selection)
                        {
                            if (u.GetType() == typeof(Station))
                            {
                                Station uu = (Station)u;
                                uu.NeedUpdate = true;
                                uu.Location = new Point(uu.Location.X - (SelectionStart.X - e.X), uu.Location.Y - (SelectionStart.Y - e.Y));
                            }

                            if (u.GetType() == typeof(TextField))
                            {
                                TextField uu = (TextField)u;
                                uu.NeedUpdate = true;
                                uu.Location = new Point(uu.Location.X - (SelectionStart.X - e.X), uu.Location.Y - (SelectionStart.Y - e.Y));
                            }

                            if (u.GetType() == typeof(Line))
                            {
                                Line uu = (Line)u;
                                uu.NeedUpdate = true;
                                uu.StartPoint = new Point(uu.StartPoint.X - (SelectionStart.X - e.X), uu.StartPoint.Y - (SelectionStart.Y - e.Y));
                                uu.EndPoint = new Point(uu.EndPoint.X - (SelectionStart.X - e.X), uu.EndPoint.Y - (SelectionStart.Y - e.Y));
                            }
                        }

                        SelectionStart = e.Location;
                        UpdateLines(false, false);
                    }

                    info1.Visible = false;
                }
            }
            this.panel1.Location = location;
        }

        private void DOWN(object sender, MouseEventArgs e)
        {
            Point location = panel1.Location;
            if (UserMode) { 
            
            }
            else
            {
                // Нажата кнопка мыши - начинаем перенос(рисование)
                if ((_mode == 0) | (_mode == 4))
                {
                    _f = true;
                    _p = Line.getNear(e.Location);
                    ls = new LineShape(_p.X, _p.Y, _p.X, _p.Y);
                    ls.Enabled = false;
                    ls.BorderColor = Color.Firebrick;
                    shapeContainer2.Shapes.Add(ls);
                }
            }
            panel1.Location = location;
        }
        #endregion

        public void UpdateLines(bool File, bool Full)
        {
            this.Text = Properties_._instance.Date.ToString();
            Rectangle RectV = new Rectangle(0, 0, 5000, 5000);

            Point location = this.panel1.Location;
            if (!File)
                Properties_.Update();

            for (int i = 0; i < panel1.Controls.Count; i++)
                if (panel1.Controls[i].GetType() != typeof(ShapeContainer))
                        if (!Full)
                            // if (panel1.Controls[i].Tag.GetType() == typeof(TextField))
                            if (((TextField)panel1.Controls[i].Tag).NeedUpdate)
                            {
                                var n = panel1.Controls[i];
                                panel1.Controls.RemoveAt(i--);
                                n.Dispose();
                            }
                            else
                            { }
                        else
                        {
                            var n = panel1.Controls[i ];
                            panel1.Controls.RemoveAt(i--);
                            n.Dispose();
                        }

            for (int i = 0; i < shapeContainer2.Shapes.Count; i++)
                if (!Full)
                {
                    var tmp = shapeContainer2.Shapes.get_Item(i);
                    if (tmp.GetType() == typeof(RectangleShape))
                        if (((Station)((RectangleShape)tmp).Tag).NeedUpdate)
                        {
                            shapeContainer2.Shapes.RemoveAt(i--);
                        }

                    if (tmp.GetType() == typeof(LineShape))
                        if (((LineShape)tmp).Tag != null)
                            if (((Line)((LineShape)tmp).Tag).NeedUpdate)
                                shapeContainer2.Shapes.RemoveAt(i--);
                            else
                            { }
                        else
                            shapeContainer2.Shapes.RemoveAt(i--);
                }
                else
                {
                    shapeContainer2.Shapes.Clear();
                }

            foreach (var s in Lines)
            {
                if (Full | s.NeedUpdate)
                {
                    if (RectV.Contains(s.StartPoint) | (RectV.Contains(s.EndPoint)))
                    {
                        if (_mode != 5)
                            s.NeedUpdate = false;
                        var t = s.GetGraphics(Click1, Move1, _mode);
                        shapeContainer2.Shapes.Add(t);
                        t.BringToFront();
                    }
                }
            }

            foreach (var s in Texts)
            //   if (Full | s.NeedUpdate)
            {
                if (RectV.Contains(s.Location))
                {
                    if (_mode != 5)
                        s.NeedUpdate = false;
                    var t = s.GetGraphics(Click3, Move3);
                    if (_mode != 5)
                    {
                        panel1.Controls.Add(t);
                        t.BringToFront();
                    }
                }
            }

            if (отобажатьОбъектыToolStripMenuItem.Checked)
                foreach (var s in Stations)
                    if (RectV.Contains(s.Location))
                    {
                        if (Full | s.NeedUpdate)
                        {
                            if (_mode != 5)
                                s.NeedUpdate = false;
                            var t = s.GetGraphics(Click2, Move2);
                            shapeContainer2.Shapes.Add(t);
                            t.BringToFront();
                        }
                    }



            //Locations = new List<Point>();
            //Lines.ForEach(o => { Locations.Add(o.StartPoint); Locations.Add(o.EndPoint); });
            
            this.shapeContainer2.BringToFront();
            GC.Collect();
            this.panel1.Location = location;
        }

        //ok
        private void CLICK(object sender, MouseEventArgs e)
        {
            Point point2;
            Station station;
            Point location = this.panel1.Location;
            
            if (this.UserMode)
            {
                if (Blocked) return;
                if (e.Button == MouseButtons.Middle)
                {
                    point2 = new Point(e.Location.X , e.Location.Y );//= Line.getNear();
                    station = new Station();
                    if (new SetStation(station, 1).ShowDialog() == DialogResult.OK)
                    {
                        station.Location = point2;
                        station.NeedUpdate = true;
                        this.Stations.Add(station);
                        this.UpdateLines(false, false);
                        Properties_._instance.Date = DateTime.Now;
                    }
                }
            }
            else if (sender == null)
            {
                point2 = Line.getNear(e.Location);
                station = new Station();
                if (new SetStation(station, 0).ShowDialog() == DialogResult.OK)
                {
                    station.Location = point2;
                    station.NeedUpdate = true;
                    this.Stations.Add(station);
                    this.UpdateLines(false, false);
                    Properties_._instance.Date = DateTime.Now;
                }
            }
            else if (this._mode == 1)
            {
                point2 = Line.getNear(e.Location);
                TextField st = new TextField();
                if (new SetText(st).ShowDialog() == DialogResult.OK)
                {
                    st.Location = point2;
                    this.Texts.Add(st);
                    st.NeedUpdate = true;
                    this.UpdateLines(false, false);
                    Properties_._instance.Date = DateTime.Now;
                }
            }
            this.panel1.Location = location;
        }
    }
}
