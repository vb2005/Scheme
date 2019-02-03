using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PowerLine.Классы;
using System.Net.Sockets;

namespace PowerLine
{
    static class Program
    {
        //public static UDPServer udp;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //udp = new UDPServer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
