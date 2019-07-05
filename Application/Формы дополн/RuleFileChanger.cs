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
    public partial class RuleFileChanger : Form
    {
        DataBase DB;

        public RuleFileChanger()
        {
            InitializeComponent();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DB = DataBase.ReadBase(openFileDialog1.FileName);

                // Получаем список городов
                List<City> list = new List<City>();
                foreach (var i in DB.Users)
                    list.AddRange(i.City);
                list = list.Distinct().ToList();


            }
            else {
                DB = new DataBase();
            }
        }


    }
}
