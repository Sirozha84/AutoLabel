﻿using System;
using System.Windows.Forms;

namespace AutoLabel
{
    static class Program
    {
        public static string Version = "1.2.0 (11.03.2016)";
        public static string Patch = Environment.CurrentDirectory + "\\";
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            if (param.Length > 0) Data.IsMachine = true;
            if (Data.IsMachine)
                Net.Log("Запуск программы в режиме терминала");
            else
                Net.Log("Запуск программы в режиме ПК");
            Net.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            Net.Log("Завершение работы");
        }

        public static void About()
        {
            MessageBox.Show("AutoLabel\n" +
                            "Версия: " + Version + "\n" +
                            "Автор: Сергей Гордеев\n" +
                            "Телефон техподдержки: +7 (965) 917-31-43\n" +
                            "Сайт автора: http://www.sg-software.ru",
                            "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
