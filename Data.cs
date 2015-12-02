using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace AutoLabel
{
    class Data
    {
        public static List<Label> Labels = new List<Label>();

        public static List<string> Types = new List<string>();
        public static List<string> Weights = new List<string>();
        public static List<string> Quantitys = new List<string>();
        public static List<string> Colors = new List<string>();
        public static List<string> Materials = new List<string>();
        public static List<string> Limits = new List<string>();

        public static PrinterSettings printersettings;// = new PrinterSettings();

        /// <summary>
        /// Загрузка последних данных в случае сбоя программы, или инициализация данных по умолчанию
        /// </summary>
        public static void Load()
        {
            //Сами лейблы
            for (int i = 0; i < 6; i++)
                Labels.Add(new Label(i));
            //Так же списки выбора
            Types.Add("Bericap");
            Types.Add("Другой");
            Weights.Add("25,5");
            Weights.Add("0");
            Quantitys.Add("10896");
            Quantitys.Add("0");
            Colors.Add("БЕЛЫЙ");
            Colors.Add("НЕ БЕЛЫЙ");
            Materials.Add("ПОЛИЭФ");
            Materials.Add("ЧУГУН");
            Limits.Add("24 месяца");
            Limits.Add("1 день");
        }
        

        public static void Save()
        {
            //Здесь будет сохранение
        }

        public static void PrintSetup()
        {
            PrintDialog diag = new PrintDialog();
            if (diag.ShowDialog() == DialogResult.Cancel) return;
            printersettings = diag.PrinterSettings;
            printersettings.DefaultPageSettings.Landscape = true;   //Задаём альбомную ориентацию

            //Тут настройки надо сохранить в файл
        }

        public static bool PrintSelected()
        {
            return printersettings != null;
        }
    }
}
