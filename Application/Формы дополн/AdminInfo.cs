using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerLine
{
    public partial class AdminInfo : Form
    {
        public AdminInfo()
        {
            InitializeComponent();

                textBox1.Text = Properties.Settings.Default["Name"].ToString();
                textBox2.Text = Properties.Settings.Default["Path"].ToString();
                textBox3.Text = Properties.Settings.Default["Weather"].ToString();
                textBox4.Text = Properties.Settings.Default["Username"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Name"] = textBox1.Text;
            Properties.Settings.Default["Path"] = textBox2.Text;
            Properties.Settings.Default["Weather"] = textBox3.Text;
            Properties.Settings.Default["Username"] = textBox4.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
