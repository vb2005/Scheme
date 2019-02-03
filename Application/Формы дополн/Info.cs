using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PowerLine
{
    public partial class Info : UserControl
    {
        public Info()
        {
            InitializeComponent();
        }

        public void Update(string caption, string text){
            label1.Text = caption;
            label2.Text = text;
        }
    }
}
