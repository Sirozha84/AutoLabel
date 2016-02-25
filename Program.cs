using System;
using System.Windows.Forms;

namespace AutoLabel
{
    static class Program
    {
        public static string Version = "1.1.4 (25.02.2016)";

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            if (param.Length > 0) Data.IsMachine = true;
            if (Data.IsMachine)
                Log.Write("Запуск программы в режиме терминала");
            else
                Log.Write("Запуск программы в режиме ПК");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            Log.Write("Выход из программы");
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
