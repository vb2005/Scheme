using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Xml.Serialization;

namespace PowerLine.Классы
{
    public class UDPServer
    {
        public System.Threading.Thread tr;
        public static UdpClient Server = new UdpClient(8787);

        public UDPServer() {
            tr = new System.Threading.Thread(new System.Threading.ThreadStart(WaitData));
            tr.Start();
        }


        public static void WaitData()
        {

            while (true)
            {
                var ClientEp = new IPEndPoint(IPAddress.Any, 0);
                var ClientRequestData = Server.Receive(ref ClientEp);

                MemoryStream ms = new MemoryStream();
                ms.Write(ClientRequestData, 0, ClientRequestData.Length);
                ms.Position = 0;
                XmlSerializer formatter = new XmlSerializer(typeof(Properties_));

                Properties_ rs = (Properties_)formatter.Deserialize(ms);

                var rd = Properties_.GetInstance();

                if (rd.hash!=rs.hash)
                Form1.Instance.Invoke(Form1.Instance.ds, new object[] { ClientEp.Address.ToString(), rs });
            }
        }


    }
}
