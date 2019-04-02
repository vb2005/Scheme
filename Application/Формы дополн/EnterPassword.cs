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
    public partial class EnterPassword : Form
    {
        public EnterPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass2 = (13 + 1300).ToString() + "pass";
            string pass1 = (DateTime.Now.Day + 11).ToString();

            if (textBox1.Text == pass2) 
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else 
                if (textBox1.Text.Contains(pass1)) 
                    DialogResult = System.Windows.Forms.DialogResult.OK; 
            else 
                DialogResult = System.Windows.Forms.DialogResult.Cancel;

            if (DialogResult == System.Windows.Forms.DialogResult.Cancel)
                MessageBox.Show("Пароль не верный!", "Пароль администратора", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button1_Click(null, null);
        }
    }
}
