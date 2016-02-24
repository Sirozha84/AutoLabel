﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace AutoLabel
{
    class Data
    {
        /// <summary>
        /// Количество ТПА
        /// </summary>
        public const int TPACount = 8;
        /// <summary>
        /// Максимальное количество этикеток за раз
        /// </summary>
        public const int MaxLabels = 9;
        /// <summary>
        /// Максимальное количество преформ в малом коробе
        /// </summary>
        public const int PreformsInLittleBox = 1920;
        /// <summary>
        /// Сбрасывать ли счётчик при заступании новой
        /// </summary>
        const bool ResetOnChangeShift = true;
        /// <summary>
        /// Количество последних запоминаемых смен
        /// </summary>
        const int ShiftMemory = 7;
        /// <summary>
        /// Названия смен
        /// </summary>
        public static string[] Shifts = { "Смена 1", "Смена 2", "Смена 3", "Смена 4" };
        /// <summary>
        /// Программа запущена на машине, false - если десктопная версия
        /// </summary>
        public static bool IsMachine = false;
        /// <summary>
        /// Проверка привязки к ТПА
        /// </summary>
        public static bool AccessControl = true;
        /// <summary>
        /// Файл принтера
        /// </summary>
        static string FilePrinter = "Printer.txt";

        public static string Shift;
        public static string[] LogName = new string[ShiftMemory];
        //Списки пользователей и лейблов
        public static List<User> Users = new List<User>();
        public static List<Label> Labels = new List<Label>();
        //Списки выпадающих меню
        public static List<string> Types = new List<string>();
        public static List<string> Weights0 = new List<string>();
        public static List<string> Weights1 = new List<string>();
        public static List<string> Quantitys0 = new List<string>();
        public static List<string> Quantitys1 = new List<string>();
        public static List<string> Colors0 = new List<string>();
        public static List<string> Colors1 = new List<string>();
        public static List<string> Materials = new List<string>();
        public static List<string> Limits = new List<string>();
        public static List<string> Antistatics = new List<string>();
        public static List<string> Colorants = new List<string>();

        public static PrinterSettings printersettings;// = new PrinterSettings();

        //Инициализация первоначальных данных
        public static void Init()
        {
            //Режим ПК
            if (!IsMachine)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                      "\\SG\\Autolabel\\";
                Directory.CreateDirectory(folder);
                FilePrinter = folder + "Printer.txt";
            }
            //Принтер
            try
            {
                StreamReader file = File.OpenText(FilePrinter);
                printersettings = new PrinterSettings();
                printersettings.PrinterName = file.ReadLine();
                file.Dispose();
            }
            catch
            {
                //Применяем настройки принтера по умолчанию
                printersettings = null;
            }
            //Загружаем выпадающие списки
            ListLoad(Types, "Types");
            ListLoad(Weights0, "Weights0");
            ListLoad(Weights1, "Weights1");
            ListLoad(Quantitys0, "Quantitys0");
            ListLoad(Quantitys1, "Quantitys1");
            ListLoad(Colors0, "Colors0");
            ListLoad(Colors1, "Colors1");
            ListLoad(Materials, "Materials");
            ListLoad(Limits, "Limits");
            ListLoad(Antistatics, "Antistatics");
            ListLoad(Colorants, "Colorants");
        }

        static void ListLoad(List<string> list, string filename)
        {
            //89-158  89-97
            try
            {
                StreamReader file = File.OpenText("Lists\\" + filename + ".txt");
                while (!file.EndOfStream)
                    list.Add(file.ReadLine());
            }
            catch { }
        }

        /// <summary>
        /// Загрузка последних данных в случае сбоя программы, или инициализация данных по умолчанию
        /// </summary>
        public static void Load()
        {
            //Смена
            LoadShift();
            //Лейблы
            Labels.Clear();
            Labels.Add(new Label("Husky №1", 0));
            Labels.Add(new Label("Netstal №2", 0));
            Labels.Add(new Label("Netstal №3", 0));
            Labels.Add(new Label("Netstal №4", 0));
            Labels.Add(new Label("Netstal №5", 0));
            Labels.Add(new Label("Netstal №6", 0));
            Labels.Add(new Label("C1", 1));
            Labels.Add(new Label("C2", 1));
        }

        /// <summary>
        /// Загрузка текущей смены
        /// </summary>
        static void LoadShift()
        {
            //Заполняем пустотой на случай если в файле нет информации
            for (int i = 0; i < ShiftMemory; i++)
                LogName[i] = "Пусто";
            try
            {
                StreamReader file = File.OpenText("Shift.txt");
                Shift = file.ReadLine();
                for (int i = 0; i < ShiftMemory; i++)
                    LogName[i] = file.ReadLine();
                file.Dispose();
            }
            catch
            {
                Shift = Shifts[0];
                ShiftChange(Shift); //Если файла небыло, сохраним его
            }
        }

        /// <summary>
        /// Загрузка пользователей
        /// </summary>
        public static void UsersLoad()
        {
            Users.Clear();
            try
            {
                StreamReader file = File.OpenText("Users.txt");
                AccessControl = file.ReadLine() == "AccessControl ON";
                while (!file.EndOfStream)
                {
                    file.ReadLine();
                    Users.Add(new User(file.ReadLine(), file.ReadLine(), file.ReadLine(), file.ReadLine()));
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
                file.Write("AccessControl ");
                if (AccessControl) file.WriteLine("ON"); else file.WriteLine("OFF");
                foreach (User u in Users)
                {
                    file.WriteLine("--------------------");
                    file.WriteLine(u.Name);
                    file.WriteLine(u.Code);
                    file.WriteLine(u.Rule);
                    string a = "";
                    foreach (bool b in u.TPAAccess)
                        if (b) a += "1"; else a += "0";
                    file.WriteLine(a);
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
                StreamWriter file = File.CreateText(FilePrinter);
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
            if (Shift == shift) return;
            Shift = shift;
            //Сдвигаем коллекцию последних смен
            for (int i = ShiftMemory - 1; i > 0; i--)
                LogName[i] = LogName[i - 1];
            //Изменяем файл журнала
            LogName[0] = (DateTime.Now.ToString("yyyy.MM.dd - ") + Shift);
            //Сохраняем текущую смену в файл
            try
            {
                StreamWriter file = File.CreateText("Shift.txt");
                file.WriteLine(Shift);
                for (int i = 0; i < ShiftMemory; i++)
                    file.WriteLine(LogName[i]);
                file.Dispose();
            }
            catch
            {
                Error("Не удалось сохранить настройку смены.");
            }
            //Label.NewLog();
            Directory.CreateDirectory("Logs");
            //Обнуляем счётчики коробов
            if (ResetOnChangeShift)
                foreach (Label l in Labels)
                {
                    l.CurrentNum = 1;
                    l.Save();
                }
        }

        /// <summary>
        /// Сообщение об ошибке :-(
        /// </summary>
        /// <param name="message"></param>
        static void Error(string message)
        {
            MessageBox.Show(message + "\nДля решение проблемы вызовите системного администратора",
                "Случилось что-то плохое");
            Log.Write("Ошибка: "+ message);
        }

        /// <summary>
        /// Запрос ключа
        /// </summary>
        /// <param name="Minimum">Минимально необходимый уровень доступа</param>
        /// <returns></returns>
        public static byte GetKey(int Minimum)
        {
            UsersLoad();
            if (IsMachine)
            {
                //Сначала проверим, есть ли в списке хоть один админ
                if (Users.Find(u => (u.Rule == 255 & u.Code != "")) == null) return 255;
                //Админы есть, значит просим прислонить ключ
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
                        if (user.Code != "")
                        {
                            FormError err = new FormError("Недостаточно прав");
                            err.ShowDialog();
                        }
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

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="Rule"></param>
        public static void AddNewUser(string Rule)
        {
            string name = "";
            string code = "";
            if (IsMachine)
            {
                FormKeyboardLetter keyboard = new FormKeyboardLetter("Введите имя пользователя");
                if (keyboard.ShowDialog() == DialogResult.Cancel) return;
                //Добавление нового пользователя
                name = keyboard.Str;
                //Добавление ключа (пока только на машине)
                FormKey key = new FormKey();
                key.ShowDialog();
                code = key.Code;
            }
            else
            {
                FormInput input = new FormInput("Введите имя пользователя");
                if (input.ShowDialog() == DialogResult.Cancel) return;
                name = input.Str;
            }
            Users.Add(new User(name, code, Rule, "000000"));
            Users.Sort((a,b) => a.Name.CompareTo(b.Name));
        }

        /// <summary>
        /// Проверка доступности ТПА данному пользователю
        /// </summary>
        /// <param name="name">Имя проверяемого</param>
        /// <param name="tpa">Номер ТПА</param>
        /// <returns></returns>
        public static bool AccessTest(string name, int tpa)
        {
            if (!AccessControl) return true;
            User u = Users.Find(us => us.Name == name);
            return u.TPAAccess[tpa];
        }

        /// <summary>
        /// Пользователь админ?
        /// </summary>
        /// <param name="u">Пользователь</param>
        /// <returns></returns>
        static string UserIsAdmin(User u)
        {
            if (u.Rule == 255) return "Админ";
            return "";
        }

        /// <summary>
        /// Пользователь с ключём?
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        static string UserWidthKey(User u)
        {
            if (u.Code != "") return "Есть";
            return "";
        }

        /// <summary>
        /// Заполнение таблицы пользователей
        /// </summary>
        /// <param name="list">Ссылка на ListView</param>
        public static void UserListDraw(ListView list)
        {
            list.Items.Clear();
            foreach (User u in Users)
            {
                ListViewItem it = new ListViewItem(u.Name);
                it.SubItems.Add(UserIsAdmin(u));
                it.SubItems.Add(UserWidthKey(u));
                it.SubItems.Add(u.StringWidthTPA());
                list.Items.Add(it);
            }
        }

        /// <summary>
        /// Предоставление текущей даты в человечьем виде
        /// </summary>
        /// <returns></returns>
        public static string DateToString()
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("dd ");
            switch (now.Month)
            {
                case 1: date += "янв"; break;
                case 2: date += "фев"; break;
                case 3: date += "мар"; break;
                case 4: date += "апр"; break;
                case 5: date += "май"; break;
                case 6: date += "июн"; break;
                case 7: date += "июл"; break;
                case 8: date += "авг"; break;
                case 9: date += "сен"; break;
                case 10: date += "окт"; break;
                case 11: date += "ноя"; break;
                case 12: date += "дек"; break;
            }
            return date + now.ToString(" yy");
        }

        /// <summary>
        /// Задание цвета для кнопки
        /// </summary>
        /// <param name="but">Кнопку</param>
        /// <param name="lab">Лейбл</param>
        public static void SetColor(Button but, int tpa)
        {
            but.Visible = Labels[tpa].PartNum != "";
            switch (Labels[tpa].PColor)
            {
                case "Бесцветный":
                    but.BackColor = Color.FromArgb(64, 64, 64);
                    but.ForeColor = Color.FromArgb(192, 192, 192);
                    break;
                case "Бесцветный (М)":
                    but.BackColor = Color.FromArgb(128, 128, 128);
                    but.ForeColor = Color.FromArgb(192, 192, 192);
                    break;
                case "Белый":
                    but.BackColor = Color.FromArgb(255, 255, 255);
                    but.ForeColor = Color.FromArgb(192, 192, 192);
                    break;
                case "Оранжевый":
                    but.BackColor = Color.FromArgb(255, 128, 0);
                    but.ForeColor = Color.FromArgb(255, 178, 0);
                    break;
                case "Зелёный":
                    but.BackColor = Color.FromArgb(0, 128, 0);
                    but.ForeColor = Color.FromArgb(0, 255, 0);
                    break;
                case "Золотой":
                    but.BackColor = Color.FromArgb(192, 128, 0);
                    but.ForeColor = Color.FromArgb(255, 178, 0);
                    break;
                case "Синий":
                    but.BackColor = Color.FromArgb(0, 0, 128);
                    but.ForeColor = Color.FromArgb(0, 128, 255);
                    break;
                case "Бирюзовый":
                    but.BackColor = Color.FromArgb(0, 128, 128);
                    but.ForeColor = Color.FromArgb(0, 255, 255);
                    break;
                case "Красный":
                    but.BackColor = Color.FromArgb(128, 0, 0);
                    but.ForeColor = Color.FromArgb(255, 64, 64);
                    break;
                case "Коричневый":
                    but.BackColor = Color.FromArgb(64, 32, 0);
                    but.ForeColor = Color.FromArgb(128, 64, 0);
                    break;
                case "Фиолетовый":
                    but.BackColor = Color.FromArgb(128, 0, 192);
                    but.ForeColor = Color.FromArgb(192, 0, 255);
                    break;
                case "Чёрный":
                    but.BackColor = Color.FromArgb(0, 0, 0);
                    but.ForeColor = Color.FromArgb(64, 64, 64);
                    break;
                default:
                    but.Visible = false;
                    break;
            }
        }
    }
}
