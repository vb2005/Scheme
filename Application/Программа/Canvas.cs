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
            if (_mode == 5)
            {
                _mode = 4;
                UpdateLines(false, true);
                return;
            }

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
                    }
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

        private Point FindNear(Point p)
        {
          //  List<Point> lp = Locations.FindAll(o => (Math.Sqrt(Math.Pow(p.X - o.X, 2) + Math.Pow(p.Y - o.Y, 2)) < 10));
          //  if (lp.Count == 0) return p;
            return p;// lp[0];
        }

        private void MOVE(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.None)
            {
                if ((_mode == 0) | (_mode == 4))
                    if (_f)
                    {
                        Point m = Line.getNear(new Point(e.X, e.Y));
                        ls.X2 = m.X;
                        ls.Y2 = m.Y;
                    }

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

        private void DOWN(object sender, MouseEventArgs e)
        {
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
        #endregion

        public void UpdateLines(bool File, bool Full)
        {
            if (!File)
                Properties_.Update();

            for (int i = 0; i < panel1.Controls.Count; i++)
                if (panel1.Controls[i].GetType() != typeof(ShapeContainer))
                    if (panel1.Controls[i].GetType() != typeof(Info))
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
                    if (_mode != 5)
                        s.NeedUpdate = false;
                    var t = s.GetGraphics(Click1, Move1, _mode);
                    shapeContainer2.Shapes.Add(t);
                    t.BringToFront();
                }
            }

            foreach (var s in Texts)
            //   if (Full | s.NeedUpdate)
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

            if (отобажатьОбъектыToolStripMenuItem.Checked)
                foreach (var s in Stations)
                    if (Full | s.NeedUpdate)
                    {
                        if (_mode != 5)
                            s.NeedUpdate = false;
                        var t = s.GetGraphics(Click2, Move2);
                        shapeContainer2.Shapes.Add(t);
                        t.BringToFront();
                    }



            //Locations = new List<Point>();
            //Lines.ForEach(o => { Locations.Add(o.StartPoint); Locations.Add(o.EndPoint); });

            shapeContainer2.BringToFront();
            //   shapeContainer2.PerformLayout();
            //   shapeContainer2.Refresh();
            GC.Collect();
        }


        private void CLICK(object sender, MouseEventArgs e)
        {
            if (sender == null)
            {
                // Добавляем новый контрол
                Point p = Line.getNear(e.Location);
                Station s = new Station();
                if (new SetStation(s).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    s.Location = p;
                    s.NeedUpdate = true;
                    Stations.Add(s);
                    UpdateLines(false, false);

                }
            }
            else
                if (_mode == 1)
                {
                    // Добавляем новый контрол
                    Point p = Line.getNear(e.Location);
                    TextField s = new TextField();
                    if (new SetText(s).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        s.Location = p;
                        Texts.Add(s);
                        s.NeedUpdate = true;
                        UpdateLines(false, false);

                    }
                }


        }
    }
}
