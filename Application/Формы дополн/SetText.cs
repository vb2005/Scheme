using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PowerLine
{
    public partial class SetText : Form
    {

        public SetText(TextField st)
        {
            InitializeComponent();
            Result = st;
        }



        public TextField Result;



        private void button2_Click(object sender, EventArgs e)
        {
            Result.Description = textBox2.Text;
            Result.Size = (int)numericUpDown1.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

    }
}
