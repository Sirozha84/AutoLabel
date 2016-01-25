using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLabel
{
    static class Program
    {
        public static string Version = "1.0 Beta 7 (25.01.2016)";

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            if (param.Length > 0) Data.IsMachine = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        public static void About()
        {
            MessageBox.Show("AutoLabel\n" +
                            "Версия: " + Program.Version + "\n" +
                            "Автор: Сергей Гордеев\n" +
                            "Телефон техподдержки: +7 (965) 917-31-43\n" +
                            "Сайт автора: http://www.sg-software.ru",
                            "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
