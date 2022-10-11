using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrintCopySys
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string path = Application.ExecutablePath;
            //RegistryKey rk = Registry.LocalMachine;
            //RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            //rk2.SetValue("JcShutdown", path);
            //rk2.Close();
            //rk.Close();

            Application.Run(new PrintCopyForm());
        }
    }
}
