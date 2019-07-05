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
        /// Набор городов, доступный для просмотра
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
        /// Группа в которой отображаются города
        /// </summary>
        public string Group { get; set; }

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
                instance = ReadBase(Properties.Settings.Default["Path"] + "local2.db");
            return instance;
        }

        /// <summary>
        /// Internal. Сохранение изменений в БД
        /// </summary>
        /// <param name="db"></param>
        public static void SaveInstance(DataBase db)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(Properties.Settings.Default["Path"] + "local2.db", FileMode.Create))
            {
                formatter.Serialize(fs,db);
            }
        }

        /// <summary>
        /// Чтение базы данных с сервера
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataBase ReadBase(string path)
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
                        List<string> names = new List<string>();
                        Dictionary<string, ToolStripMenuItem> dic = new Dictionary<string, ToolStripMenuItem>();
                        item.City.ForEach(a => names.Add(a.Group));
                        foreach (var x in names.Distinct().OrderBy(x=>x))
                        {
                            ToolStripMenuItem m = new ToolStripMenuItem(x);
                            m.Tag = m;
                            dic.Add(x, m);
                            ti.DropDownItems.Add(m);
                        }

                        foreach (var item2 in item.City)
                        {
                            ToolStripMenuItem m = new ToolStripMenuItem((item2.ReadOnly ? "* " : "") + item2.Name);
                            m.Tag = item2.File;
                            m.Click += new EventHandler((o, e) =>
                            {
                                if (System.IO.File.Exists(Properties.Settings.Default["Path"] + m.Tag.ToString()))
                                {
                                    string s = item2.File;
                                    Properties.Settings.Default["Name"] = m.Tag.ToString();
                                    Form1.Instance.Blocked = (m.Text.Contains("*"));
                                    Form1.Instance.ReadFromServer(true);
                                    Form1.Instance.UpdateLines(true, true);
                                    Properties.Settings.Default.Save();
                                }
                                else {
                                    MessageBox.Show("Указанная схема не найдена! Свяжитесь с администратором программы для устранения ошибки!","Ошбика!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                }
                            });
                            var mm = dic[item2.Group];
                            mm.DropDownItems.Add(m);
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
            //db2 = new DataBase();
            AddCity(db2);

         /*   var ss0 = new City() { Name = "Михайлов", File = "Михайлов.sch" };
            var ss1 = new City() { Name = "Сасово", File = "Сасово.sch" };
            var ss2 = new City() { Name = "Зелёная", File = "Зелёная2.sch" };
            var ss3 = new City() { Name = "Октябрьский", File = "Октябрьский.sch" };
            var ss4 = new City() { Name = "Первомайский", File = "Первомайский.sch" };
            var ss5 = new City() { Name = "Михайлов", File = "Михайлов.sch" };
            var ss6 = new City() { Name = "Рыбное", File = "Рыбное.sch" };
            var uu = new User() { Name = "Иванюк", City = new List<City> { ss0, ss1, ss2, ss3, ss4, ss5, ss6 } };
            uu.City.ForEach(o => o.ReadOnly = true);
            db2.Users.Add(uu);*/
            DataBase.SaveInstance(db2);
        }

        public static void AddCity(DataBase db2) {
            List<City> d = new List<City>();

            // 0-3 Михайлов          OK
            d.Add(new City() { Name = "Михайлов", File = "Михайлов.sch", Group = "Михайловский филиал" });
            d.Add(new City() { Name = "Зелёная", File = "Зелёная2.sch", Group = "Михайловский филиал" });
            d.Add(new City() { Name = "Октябрьский", File = "Октябрьский.sch", Group = "Михайловский филиал" });
            d.Add(new City() { Name = "Первомайский", File = "Первомайский.sch", Group = "Михайловский филиал" });

            // 4-9 Сасово            OK?
            d.Add(new City() { Name = "Сасово", File = "Сасово.sch", Group = "Сасовский филиал" });
            d.Add(new City() { Name = "Сасовский район", File = "Сасово2.sch", Group = "Сасовский филиал" });
            d.Add(new City() { Name = "Шацк", File = "Шацк.sch", Group = "Сасовский филиал" });
            d.Add(new City() { Name = "Чучково", File = "Чучково.sch", Group = "Сасовский филиал" });
            d.Add(new City() { Name = "Пителино", File = "Пителино.sch", Group = "Сасовский филиал" });
            d.Add(new City() { Name = "Ермишь", File = "Ермишь.sch", Group = "Сасовский филиал" });

            // 10 Рыбное             OK?
            d.Add(new City() { Name = "Рыбное", File = "Рыбное.sch", Group = "Рыбновский филиал" });

            // 11-12 Спасск          OK?
            d.Add(new City() { Name = "Спасск-Рязанский", File = "Спасск.sch", Group = "Спасский филиал" });
            d.Add(new City() { Name = "Старожилово", File = "Старожилово.sch", Group = "Спасский филиал" });

            // 13-14 Ал. Неский      OK Требцютчся изменения
            d.Add(new City() { Name = "Ал. Несвкий", File = "Невский.sch", Group = "Новодеревенский филиал" });
            d.Add(new City() { Name = "Ухолово", File = "Ухолово.sch", Group = "Новодеревенский филиал" });

            // 15-16 Касимов
            d.Add(new City() { Name = "Касимов", File = "Касимов.sch", Group = "Касимовский филиал" });
            d.Add(new City() { Name = "Первомайский", File = "Первомайский2.sch", Group = "Касимовский филиал" });

            // 17-18 Клепики
            d.Add(new City() { Name = "Спас-Клепики", File = "Клепики.sch", Group = "Клепиковский филиал" });
            d.Add(new City() { Name = "Тума", File = "Тума.sch", Group = "Клепиковский филиал" });

            // 19 Путятино
            d.Add(new City() { Name = "Путятино", File = "Путятино.sch", Group = "Путятинский филиал" });

            // 20-21 Скопин        ОК требуются правки
            d.Add(new City() { Name = "Скопин", File = "Скопин.sch", Group = "Скопинский филиал" });
            d.Add(new City() { Name = "Милославское", File = "Милославское.sch", Group = "Скопинский филиал" });

            // 22 Рязань
            d.Add(new City() { Name = "Рязань", File = "Рязань.sch", Group = "Рязанский филиал" });

            // 23-24 Ряжск
            d.Add(new City() { Name = "Ряжск", File = "Ряжск.sch", Group = "Ряжский филиал" });
            d.Add(new City() { Name = "Сараи", File = "Сараи.sch", Group = "Ряжский филиал" });


            List<User> usr = new List<User>();

            d.ForEach(o => o.ReadOnly = true);
            usr.Add(new User() { Name = "Иванюк14", City = d });

            /*
            d.ForEach(o => o.ReadOnly = false);
            usr.Add(new User() { Name = "vb2005", City = d });

            usr.Add(new User() { Name = "Устинов13", City = new List<City>() { d[0], d[1], d[2], d[3] } });
            usr.Add(new User() { Name = "СасовоДЧ", City = new List<City>()  { d[4], d[5], d[6], d[7], d[8], d[9] } });
            usr.Add(new User() { Name = "Рыбное21", City = new List<City>()  { d[10]} });
            usr.Add(new User() { Name = "Спасск44", City = new List<City>()  { d[11], d[12] } });
            usr.Add(new User() { Name = "Невский95", City = new List<City>() { d[13], d[14] } });
            usr.Add(new User() { Name = "Касимов16", City = new List<City>() { d[15], d[16] } });
            usr.Add(new User() { Name = "Клепики81", City = new List<City>() { d[17], d[18] } });

            usr.Add(new User() { Name = "Путятино42", City = new List<City>() { d[19]} });
            usr.Add(new User() { Name = "Скопин18", City = new List<City>() { d[20], d[21]} });

            usr.Add(new User() { Name = "Рязань99", City = new List<City>() { d[22] } });
            usr.Add(new User() { Name = "Ряжск74", City = new List<City>() { d[23], d[24] } });
            */
            db2.Users.AddRange(usr);
        }
    }
}
