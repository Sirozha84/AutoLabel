using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace AutoLabel
{
    class Data
    {
        /// <summary>
        /// Использовать ли ключи в данной сборке
        /// </summary>
        public static bool UseKeys = false;
        public static string Shift;
        public static string LogName = "DefaultLog.csv";
        //Списки пользователей и лейблов
        public static List<User> Users = new List<User>();
        public static List<Label> Labels = new List<Label>();
        //Списки выпадающих меню
        public static List<string> Types = new List<string>();
        public static List<string> Weights = new List<string>();
        public static List<string> Quantitys = new List<string>();
        public static List<string> Colors = new List<string>();
        public static List<string> Materials = new List<string>();
        public static List<string> Limits = new List<string>();
        public static List<string> AntiTypes = new List<string>();
        public static List<string> AntiCounts = new List<string>();

        public static PrinterSettings printersettings;// = new PrinterSettings();

        /// <summary>
        /// Загрузка последних данных в случае сбоя программы, или инициализация данных по умолчанию
        /// </summary>
        public static void Load()
        {
            //Смена
            LoadShift();
            //Пользователи
            LoadUsers();
            //Лейблы
            for (int i = 0; i < 6; i++)
                Labels.Add(new Label(i)); //Конструктор сам, либо создаст пустой, либо загрузит с диска

            //Так же списки выбора
            Types.Add("PCO");
            Types.Add("BPF");
            Types.Add("PCO 1881");
            Types.Add("PCO/BPF");
            Types.Add("Bericap");
            Types.Add("Semi PCO");
            Types.Add("DIN");
            Weights.Add("15,3");
            Weights.Add("16,8");
            Weights.Add("18,9");
            Weights.Add("20,6");
            Weights.Add("20,7");
            Weights.Add("21");
            Weights.Add("23");
            Weights.Add("24,5");
            Weights.Add("25,5");
            Weights.Add("26");
            Weights.Add("27");
            Weights.Add("29");
            Weights.Add("30");
            Weights.Add("30,7");
            Weights.Add("31,7");
            Weights.Add("33,7");
            Weights.Add("34,5");
            Weights.Add("36");
            Weights.Add("37,7");
            Weights.Add("38");
            Weights.Add("39,5");
            Weights.Add("40");
            Weights.Add("41");
            Weights.Add("42");
            Weights.Add("44");
            Weights.Add("45,7");
            Weights.Add("46");
            Weights.Add("48");
            Weights.Add("50,5");
            Weights.Add("51");
            Weights.Add("54,5");
            Quantitys.Add("720");
            Quantitys.Add("784");
            Quantitys.Add("792");
            Quantitys.Add("816");
            Quantitys.Add("864");
            Quantitys.Add("896");
            Quantitys.Add("912");
            Quantitys.Add("1008");
            Quantitys.Add("1104");
            Quantitys.Add("1200");
            Quantitys.Add("1440");
            Quantitys.Add("1920");
            Quantitys.Add("5808");
            Quantitys.Add("6096");
            Quantitys.Add("6440");
            Quantitys.Add("6672");
            Quantitys.Add("6720");
            Quantitys.Add("7104");
            Quantitys.Add("7112");
            Quantitys.Add("7200");
            Quantitys.Add("7296");
            Quantitys.Add("7440");
            Quantitys.Add("7680");
            Quantitys.Add("8400");
            Quantitys.Add("9408");
            Quantitys.Add("9264");
            Quantitys.Add("10080");
            Quantitys.Add("10896");
            Quantitys.Add("12000");
            Quantitys.Add("13104");
            Quantitys.Add("16608");
            Colors.Add("Бесцветный");
            Colors.Add("Матовый");
            Colors.Add("Белый");
            Colors.Add("Оранжевый");
            Colors.Add("Зелёный");
            Colors.Add("Синий");
            Colors.Add("Бирюзовый");
            Colors.Add("Красный");
            Colors.Add("Коричневый");
            Colors.Add("Чёрный");
            Materials.Add("Полиэф");
            Materials.Add("Распэт");
            Materials.Add("Texpet");
            Materials.Add("Jade");
            Materials.Add("WNKI");
            Limits.Add("24 месяца");
            AntiTypes.Add("");
            AntiTypes.Add("АД");
            AntiCounts.Add("000");
            AntiCounts.Add("001");
            AntiCounts.Add("106");
            AntiCounts.Add("107");
            AntiCounts.Add("109");
            AntiCounts.Add("111");
            AntiCounts.Add("211");
            AntiCounts.Add("214");
            AntiCounts.Add("308");
            AntiCounts.Add("312");
            AntiCounts.Add("314");
            AntiCounts.Add("315");
            AntiCounts.Add("316");
            AntiCounts.Add("318");
            AntiCounts.Add("320");
            AntiCounts.Add("323");
            AntiCounts.Add("402");
            AntiCounts.Add("403");
            AntiCounts.Add("501");
            AntiCounts.Add("502");
            //Принтер
            try
            {
                StreamReader file = File.OpenText("Printer.txt");
                printersettings = new PrinterSettings();
                printersettings.PrinterName = file.ReadLine();
                printersettings.DefaultPageSettings.Landscape = true;
                file.Dispose();
            }
            catch
            {
                //Применяем настройки принтера по умолчанию
                printersettings = null;
            }
        }

        /// <summary>
        /// Загрузка текущей смены
        /// </summary>
        static void LoadShift()
        {
            try
            {
                StreamReader file = File.OpenText("Shift.txt");
                Shift = file.ReadLine();
                LogName = file.ReadLine();
                file.Dispose();
            }
            catch
            {
                Shift = "Смена 1";
            }
        }

        /// <summary>
        /// Загрузка пользователей
        /// </summary>
        public static void LoadUsers()
        {
            Users.Clear();
            try
            {
                StreamReader file = File.OpenText("Users.txt");
                while (!file.EndOfStream)
                {
                    Users.Add(new User(file.ReadLine(), file.ReadLine(), file.ReadLine()));
                }
                file.Dispose();
            }
            catch { } //нишмагла...
        }

        /// <summary>
        /// Сохранение пользователей
        /// </summary>
        public static void SaveUsers()
        {
            try
            {
                StreamWriter file = File.CreateText("Users.txt");
                foreach (User u in Users)
                {
                    file.WriteLine(u.Name);
                    file.WriteLine(u.Code);
                    file.WriteLine(u.Rule);
                }
                file.Dispose();
            }
            catch
            {
                Error("Не удалось сохранить список пользователей");
            }
        }

        public static void PrintSetup()
        {
            PrintDialog diag = new PrintDialog();
            if (diag.ShowDialog() == DialogResult.Cancel) return;
            printersettings = diag.PrinterSettings;
            printersettings.DefaultPageSettings.Landscape = true;   //Задаём альбомную ориентацию
            //Сохраняем настройку принтера в файл
            try
            {
                StreamWriter file = File.CreateText("Printer.txt");
                file.Write(printersettings.PrinterName);
                file.Dispose();
            }
            catch
            {
                Error("Не удалось сохранить настройку принтера.");
            }
        }

        /// <summary>
        /// Возвращает true, если принтер выбран
        /// </summary>
        /// <returns></returns>
        public static bool PrintSelected()
        {
            return printersettings != null;
        }

        /// <summary>
        /// Выбор новой смены
        /// </summary>
        /// <param name="shift"></param>
        public static void ShiftChange(string shift)
        {
            Shift = shift;
            //Изменяем файл журнала
            LogName = (DateTime.Now.ToString("yyyy.MM.dd - ") + Shift + ".csv");
            //Сохраняем текущую смену в файл
            try
            {
                StreamWriter file = File.CreateText("Shift.txt");
                file.WriteLine(Shift);
                file.WriteLine(LogName);
                file.Dispose();
            }
            catch
            {
                Error("Не удалось сохранить настройку смены.");
            }
            Label.NewLog();
        }

        /// <summary>
        /// Сообщение об ошибке :-(
        /// </summary>
        /// <param name="message"></param>
        static void Error(string message)
        {
            MessageBox.Show(message + "\nДля решение проблемы вызовите системного администратора",
                "Случилось что-то плохое");
            //Ох, надеюсь мне не придётся увидеть ни одной из этих надписей...
        }

        /// <summary>
        /// Запрос ключа
        /// </summary>
        /// <param name="Minimum">Минимально необходимый уровень доступа</param>
        /// <returns></returns>
        public static byte GetKey(int Minimum)
        {
            if (UseKeys)
            {
                FormKey key = new FormKey();
                key.ShowDialog();
                //Ищем ключ в базе
                User user = Users.Find(u => u.Code == key.Code);
                if (user != null)
                {
                    if (user.Rule >= Minimum)
                    {
                        //Прав достаточно
                        return user.Rule;
                    }
                    else
                    {
                        //Прав недостаточно
                        FormError err = new FormError("Недостаточно прав");
                        err.ShowDialog();
                        return 0;
                    }
                }
                else
                {
                    if (key.Code != "")
                    {
                        FormError err = new FormError();
                        err.ShowDialog();
                    }
                    return 0; //значит, что нифига не нашли 
                }
            }
            //Если ключи не используются, выдаём полные права без вопросов
            return 255;
        }
    }
}
