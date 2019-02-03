using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PowerLine
{
    public partial class SetLine : Form
    {

        public SetLine(Line line)
        {
            InitializeComponent();
            Result = line;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = button1.BackColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                button1.BackColor = cd.Color;
            }
        }


        public Line Result;


        private void button2_Click(object sender, EventArgs e)
        {
            Result.ColorIndex = button1.BackColor.ToArgb();
            Result.Width = (int)numericUpDown1.Value;
            Result.Dash = checkBox1.Checked;
            Result.Name = textBox1.Text;
            Result.Description = textBox2.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') button2_Click(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
