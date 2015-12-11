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
            Types.Add("Bericap");
            Types.Add("Другой");
            Weights.Add("25,5");
            Weights.Add("0");
            Quantitys.Add("10896");
            Quantitys.Add("0");
            Colors.Add("Белый");
            Colors.Add("Не белый");
            Materials.Add("Полиэф");
            Materials.Add("Чугун");
            Limits.Add("24 месяца");
            Limits.Add("1 день");
            AntiTypes.Add("");
            AntiTypes.Add("АД");
            AntiCounts.Add("000");
            AntiCounts.Add("999");
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
