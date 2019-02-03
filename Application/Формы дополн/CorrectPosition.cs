using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerLine.Формы_дополн
{
    public partial class CorrectPosition : Form
    {
        public CorrectPosition(Station s)
        {
            InitializeComponent();
            this.s = s;
        }

        public CorrectPosition(TextField t)
        {
            InitializeComponent();
            this.t = t;
        }

        Station s;
        TextField t;
        int Step = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                s.Location = new Point(s.Location.X, s.Location.Y - Step);
                s.NeedUpdate = true;
            }
            if (t != null)
            {
                t.Location = new Point(t.Location.X, t.Location.Y - Step);
                t.NeedUpdate = true;
            }
            Form1.Instance.UpdateLines(false, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                s.Location = new Point(s.Location.X - Step, s.Location.Y);
                s.NeedUpdate = true;
            }
            if (t != null)
            {
                t.Location = new Point(t.Location.X - Step, t.Location.Y);
                t.NeedUpdate = true;
            }
            Form1.Instance.UpdateLines(false, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                s.Location = new Point(s.Location.X + Step, s.Location.Y);
                s.NeedUpdate = true;
            }
            if (t != null)
            {
                t.Location = new Point(t.Location.X + Step, t.Location.Y);
                t.NeedUpdate = true;
            }
            Form1.Instance.UpdateLines(false, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (s != null)
            {
                s.Location = new Point(s.Location.X, s.Location.Y + Step);
                s.NeedUpdate = true;
            }
            if (t != null)
            {
                t.Location = new Point(t.Location.X, t.Location.Y + Step);
                t.NeedUpdate = true;
            }
            Form1.Instance.UpdateLines(false, false);
        }
    }
}
