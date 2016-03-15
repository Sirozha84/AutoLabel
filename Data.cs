using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Sockets;

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
        static string FilePrinter;

        //Списки пользователей и лейблов
        public static List<User> Users = new List<User>();
        public static List<Label> Labels = new List<Label>();
        //Списки выпадающих меню
        public static List<string> Types0 = new List<string>();
        public static List<string> Types1 = new List<string>();
        public static List<string> Weights0 = new List<string>();
        public static List<string> Weights1 = new List<string>();
        public static List<string> Quantitys0 = new List<string>();
        public static List<string> Quantitys1 = new List<string>();
        public static List<string> Colors0 = new List<string>();
        public static List<string> Colors1 = new List<string>();
        public static List<string> Materials0 = new List<string>();
        public static List<string> Materials1 = new List<string>();
        public static List<string> Limits = new List<string>();
        public static List<string> Antistatics0 = new List<string>();
        public static List<string> Antistatics1 = new List<string>();
        public static List<string> Colorants0 = new List<string>();
        public static List<string> Colorants1 = new List<string>();

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
            else
                FilePrinter = Program.Patch + "Printer.txt";
            //Принтер
            try
            {
                StreamReader file = File.OpenText(FilePrinter);
                printersettings = new PrinterSettings();
                printersettings.PrinterName = file.ReadLine();
                file.Close();
            }
            catch
            {
                //Применяем настройки принтера по умолчанию
                printersettings = null;
            }
            
        }

        /// <summary>
        /// Загрузка выпадающих списков
        /// </summary>
        public static void ListsLoad()
        {
            ListLoad(Types0, "Types0");
            ListLoad(Types1, "Types1");
            ListLoad(Weights0, "Weights0");
            ListLoad(Weights1, "Weights1");
            ListLoad(Quantitys0, "Quantitys0");
            ListLoad(Quantitys1, "Quantitys1");
            ListLoad(Colors0, "Colors0");
            ListLoad(Colors1, "Colors1");
            ListLoad(Materials0, "Materials0");
            ListLoad(Materials1, "Materials1");
            ListLoad(Limits, "Limits");
            ListLoad(Antistatics0, "Antistatics0");
            ListLoad(Antistatics1, "Antistatics1");
            ListLoad(Colorants0, "Colorants0");
            ListLoad(Colorants1, "Colorants1");
        }

        static void ListLoad(List<string> list, string filename)
        {
            //89-158  89-97
            try
            {
                StreamReader file = File.OpenText(Program.Patch + "Lists\\" + filename + ".txt");
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
            Shift.Load();
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
        /// Загрузка пользователей
        /// </summary>
        public static void UsersLoad()
        {
            Users.Clear();
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("UsersRead");
                        int c = reader.ReadInt32()-1;
                        AccessControl = reader.ReadString() == "AccessControl ON";
                        while (c > 0)
                        {
                            reader.ReadString();
                            Users.Add(new User(reader.ReadString(), reader.ReadString(),
                                reader.ReadString(), reader.ReadString()));
                            c-=5;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Сохранение пользователей
        /// </summary>
        public static void SaveUsers()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("UsersWrite");
                        if (AccessControl)
                            writer.Write("AccessControl ON");
                        else
                            writer.Write("AccessControl OFF");
                        foreach (User u in Users)
                        {
                            writer.Write("--------------------");
                            writer.Write(u.Name);
                            writer.Write(u.Code);
                            writer.Write(u.Rule.ToString());
                            string a = "";
                            foreach (bool b in u.TPAAccess)
                                if (b) a += "1"; else a += "0";
                            writer.Write(a);
                        }
                        writer.Write("End");
                    }
                }
            }
            catch { }
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
                file.Close();
            }
            catch
            {
                Log.Error("Не удалось сохранить настройку принтера");
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
            Users.Add(new User(name, code, Rule, "00000000"));
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

        /// <summary>
        /// Открытие файла для редактирования в блокноте
        /// </summary>
        /// <param name="file"></param>
        public static void OpenInNotepad(string file)
        {
            file = Program.Patch + file;
            if (!File.Exists(file))
            {
                FileStream f = new FileStream(file, FileMode.Create);
                f.Close();
                //File.Create(file);
            }
            System.Diagnostics.Process.Start(file);
        }

        /// <summary>
        /// Задание вопроса
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool Ask(string question)
        {
            FormAsk form = new FormAsk(question);
            return (form.ShowDialog() == DialogResult.Yes);
        }
    }
}
