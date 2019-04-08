using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace PowerLine
{
    [Serializable]
    public class User
    {
        /// <summary>
        /// Имя данного пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Набор городов, доступный для простомотра
        /// </summary>
        public List<City> City { get; set; }
    }

    [Serializable]
    public class City {
        /// <summary>
        /// Имя посёлка, доступного для просмотра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Достуен ли город для изменения
        /// </summary>
        public bool ReadOnly { get; set; }
    }

    [Serializable]
    public class DataBase {

        /// <summary>
        /// Список доступных городов
        /// Хранится на сервере, доступен в режиме ReadOnly
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Оно, Singleton-база данных
        /// </summary>
        public static DataBase instance;

        /// <summary>
        /// Случай 1го запуска.
        /// Считаем его Deprecated
        /// </summary>
        public DataBase() {
            Users = new List<User>();
        }

        /// <summary>
        /// Загружаем файл по адресу [Main Directory]/local.db
        /// </summary>
        /// <returns></returns>
        public static DataBase GetInstance()
        {
            if (instance == null)
                instance = ReadBase(Properties.Settings.Default["Path"] + "local.db");
            return instance;
        }

        /// <summary>
        /// Internal. Сохранение изменений в БД
        /// </summary>
        /// <param name="db"></param>
        public static void SaveInstance(DataBase db)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(Properties.Settings.Default["Path"] + "local.db", FileMode.Create))
            {
                formatter.Serialize(fs,db);
            }
        }

        /// <summary>
        /// Чтение базы данных с сервера
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static DataBase ReadBase(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                DataBase obj = (DataBase)formatter.Deserialize(fs);
                return obj;
            }
        }

        /// <summary>
        /// Заполнение менюшки
        /// </summary>
        /// <param name="ti"></param>
        /// <param name="username"></param>
        public static void AddCity(ToolStripMenuItem ti, string username)
        {
            try
            {
                ti.DropDownItems.Clear();
                foreach (var item in GetInstance().Users)
                {
                    if (username == item.Name)
                    {
                        foreach (var item2 in item.City)
                        {
                            ToolStripMenuItem m = new ToolStripMenuItem((item2.ReadOnly ? "* " : "") + item2.Name);
                            m.Tag = item2.File;
                            m.Click += new EventHandler((o, e) =>
                            {
                                string s = item2.File;
                                Properties.Settings.Default["Name"] = m.Tag.ToString();
                                Form1.Instance.Blocked = (m.Text.Contains("*"));
                                Form1.Instance.ReadFromServer(true);
                                Form1.Instance.UpdateLines(true, true);
                                Properties.Settings.Default.Save();
                            });
                            ti.DropDownItems.Add(m);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Подключение к сети Интернет ограничено! Убедитесь в корретности настроек программы и Интернет-подключения и перезапустите программу!");
            }
        }

        /// <summary>
        /// Свой модуль для добавления пользователей
        /// </summary>
        public static void InternalCode()
        {
            var db2 = DataBase.GetInstance();
            var ss0 = new City() { Name = "Михайлов", File = "Михайлов.sch" };
            var ss1 = new City() { Name = "Сасово", File = "Сасово.sch" };
            var ss2 = new City() { Name = "Зелёная", File = "Зелёная2.sch" };
            var ss3 = new City() { Name = "Октябрьский", File = "Октябрьский.sch" };
            var ss4 = new City() { Name = "Первомайский", File = "Первомайский.sch" };
            var ss5 = new City() { Name = "Михайлов", File = "Михайлов.sch" };
            var ss6 = new City() { Name = "Рыбное", File = "Рыбное.sch" };
            var uu = new User() { Name = "Иванюк", City = new List<City> { ss0, ss1, ss2, ss3, ss4, ss5, ss6 } };
            uu.City.ForEach(o => o.ReadOnly = true);
            db2.Users.Add(uu);
            DataBase.SaveInstance(db2);
        }
    }
}
