using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PowerLine
{
    public partial class SetStation : Form
    {
        /// <summary>
        /// Тип возвращаемого объекта
        /// </summary>
        public Station Result;

        private bool x1 = false;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="st"></param>
        public SetStation(Station st)
        {
            InitializeComponent();
            Result = st;
            comboBox1.SelectedIndex = 1;
            listView1.Items.RemoveAt(0);
            listView1.SelectedIndices.Add(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClickOK();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


        // Deprecated
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') ClickOK();
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (x1)         
                if (listView1.SelectedIndices.Count > 0)            
                    ClickOK();
            x1 = true;
        }

        private void ClickOK() {
            Result.Description = textBox2.Text;
            Result.Type = (StationType)listView1.SelectedIndices[0] ;
            Result.Angle = (int)numericUpDown1.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
