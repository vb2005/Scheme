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
    public partial class ZoomForm : Form
    {
        public ZoomForm(Bitmap src)
        {
            InitializeComponent();
            Update(src);
        }

        private void Click(object sender, MouseEventArgs e)
        {
            // Mode 2 - Change value of stations

            Click2(sender, e);
        }

        public void Update(Bitmap src) {
            Bitmap x4 = new Bitmap(src, src.Width * 2, src.Height * 2);
            pictureBox1.Image = x4;
        }

        public event MouseEventHandler Click2;
    }
}
