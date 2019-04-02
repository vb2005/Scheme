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
    public partial class Recovery : Form
    {
        public string Result { get; set; }
        public Recovery()
        {
            InitializeComponent();
            try
            {
                string[] files = System.IO.Directory.GetFiles(Application.StartupPath + "\\Recovery");
                foreach (var i in files)
                {
                    listBox1.Items.Add(i.Substring(i.LastIndexOf('\\') + 1));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удаётся найти резервные копии!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Result = listBox1.SelectedItem.ToString();
                if (MessageBox.Show("Вы действительно хотите восстановить старую схему? Все изменения этого дня будут утеряны", "Восстановление", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
