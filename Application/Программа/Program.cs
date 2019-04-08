using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Sockets;

namespace PowerLine
{
    static class Program
    {
        /// <summary>
        /// Да, я знаю, что так делают только п*****, ну а какие варианты?
        /// Огород городить? Или обойтись 2мя строчками?
        /// </summary>
        public static Form1 FORM1;

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
            FORM1 = new Form1();
            Application.Run(FORM1);
        }
    }
}
