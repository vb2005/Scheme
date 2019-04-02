using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace PowerLine
{
    [Serializable]
    public class Properties_
    {
        public List<Line> Lines { get; set; }
        public List<Station> Stations { get; set; }
        public List<TextField> Texts { get; set; }
        public string hash;

        public Properties_() {
            Lines = new List<Line>();
            Stations = new List<Station>();
            Texts = new List<TextField>();
            hash = "0";
        }

        /// <summary>
        /// Чтение файла со схемой линий
        /// </summary>
        /// <param name="path">Путь к файлу *.shm</param>
        /// <returns>Возвращает набор данных</returns>
        public static Properties_ Read(string path)
        {


            XmlSerializer formatter = new XmlSerializer(typeof(Properties_));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                var t = (Properties_)formatter.Deserialize(fs);
                _instance = t;
                return t;
            }

        }

        /// <summary>
        /// Сохранение линий в файл
        /// </summary>
        /// <param name="path">Путь к сохраняемому файлу</param>
        /// <param name="obj">Экземпляр линий</param>
        public static void Save(string path, Properties_ obj)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Properties_));
            using (FileStream fs = new FileStream(path, FileMode.Create))
                formatter.Serialize(fs, obj);
        }

        public static void Send() {
            XmlSerializer formatter = new XmlSerializer(typeof(Properties_));
            MemoryStream fs = new MemoryStream();

                formatter.Serialize(fs, GetInstance());

            var Client = new UdpClient();
            var RequestData = fs.GetBuffer();
            var ServerEp = new IPEndPoint(IPAddress.Any, 0);

            Client.EnableBroadcast = true;

            try
            {
                string[] adr = System.IO.File.ReadAllLines("IP.txt");
                foreach (var i in adr)
                {
                    Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Parse(i), 8787));
                }
            }
            catch (Exception)
            {
                Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8787));
            }
        }


        public static void Update() {
            Random r = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
            GetInstance().hash = r.Next().ToString() + r.Next().ToString();
        }

        public static Properties_ _instance;
        
        public static Properties_ GetInstance() {
            if (_instance != null) return _instance;
            try
            {
                _instance = Read("Autosave.sch");
            }
            catch (Exception)
            {
                _instance = new Properties_();
            }
            return _instance;
        }


    }
}
