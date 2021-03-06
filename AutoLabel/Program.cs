﻿using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace AutoLabel
{
    static class Program
    {
        public const string Version = "4.2 (17.04.2021)";
        /// <summary>
        /// Версия для проверки совместимости с сервером
        /// </summary>
        public const string VersionForComp = "4.0";
        public static string[] enLines;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            //Не позволяем запускать приложение повторно
            //Возможно неуловимая ошибка из-за этого и появляется
            int count = 0;
            foreach (Process pr in Process.GetProcesses())
                if (pr.ProcessName == "AutoLabel") count++;
            if (count > 1)
            {
                MessageBox.Show("Приложение уже запущено");
                return;
            }

            //Инициализируем настройки, если их нет - вызываем мастер настроек
            Settings.Init();
            Application.EnableVisualStyles();
            if (Settings.server == "")
            {
                FormStartWizzard form = new FormStartWizzard();
                if (form.ShowDialog() == DialogResult.Cancel) return;
            }

            //Выбираем режим работы приложения
            if (param.Length > 0) Data.isTerminal = true;
            if (Net.Test())
            {
                if (Data.isTerminal)
                    Net.Log("Запуск программы в режиме терминала");
                else
                    Net.Log("Запуск программы в режиме ПК");
            }

            enLines = param;
            //Application.SetCompatibleTextRenderingDefault(false);
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
