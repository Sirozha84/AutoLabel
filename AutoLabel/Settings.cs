using System;
using System.IO;
using System.Drawing.Printing;

namespace AutoLabel
{
    class Settings
    {
        public static string server;
        public static string printer;
        static string fileName;

        /// <summary>
        /// Инициализация параметров
        /// </summary>
        public static void Init()
        {
            fileName = Environment.SpecialFolder.LocalApplicationData + "AutoLabel\\Settings.ini";
            Directory.CreateDirectory(Environment.SpecialFolder.LocalApplicationData + "AutoLabel");
            server = "";
            printer = null;
            try
            {
                using (StreamReader file = File.OpenText(fileName))
                    while (!file.EndOfStream)
                    {
                        string s = file.ReadLine();
                        string[] st = s.Split('=');
                        if (st[0] == "Server") server = st[1];
                        if (st[0] == "Printer") printer = st[1];
                    }
            }
            catch { }
            if (printer != "")
            {
                Data.printersettings = new PrinterSettings();
                Data.printersettings.PrinterName = printer;
            }
        }

        /// <summary>
        /// Сохранение параметров
        /// </summary>
        public static void Save()
        {
            try
            {
                using (StreamWriter file = File.CreateText(fileName))
                {
                    file.WriteLine("Server=" + server);
                    file.WriteLine("Printer=" + printer);
                }
            }
            catch { }
        }
       /* public static void PrintSetup()
        {
            PrintDialog diag = new PrintDialog();
            if (diag.ShowDialog() == DialogResult.Cancel) return;
            printer = diag.PrinterSettings;
            printer.DefaultPageSettings.Landscape = true;   //Задаём альбомную ориентацию
            //Сохраняем настройку принтера в файл
        }*/
    }
}
