using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Modbus_Poll_CS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new ComPort());
        }
    }
}