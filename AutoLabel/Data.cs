using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    class Data
    {
        public const int lineCount = 12;            //Количество линий
        public const int prefInLittleBox = 1920;    //Максимальное количество преформ в малом коробе
        public static bool isTerminal = false;      //true - если терминал, false - если десктоп
        public static bool accessControl = true;    //Проверка привязки к ТПА
        const string printerFile = "Printer.txt";   //Файл с именем принтера
        public static bool loading = false;         //Идёт ли сейчас процесс загрузки
        
        //Типы линий по номерам
        public static int firstType0 = 0;
        public static int lastType0 = 8;
        public static int firstType1 = 9;
        public static int lastType1 = 10;
        public static int firstType2 = 11;
        public static int lastType2 = 11;
        
        //Списки пользователей и лейблов
        public static List<User> users = new List<User>();
        public static List<Line> lines = new List<Line>();
        //Списки выпадающих меню
        public static List<string> Types0 = new List<string>();
        public static List<string> Types1 = new List<string>();
        public static List<string> Weights0 = new List<string>();
        public static List<string> Weights1 = new List<string>();
        public static List<string> Weights2 = new List<string>();
        public static List<string> Quantitys0 = new List<string>();
        public static List<string> Quantitys1 = new List<string>();
        public static List<string> Colors0 = new List<string>();
        public static List<string> Colors1 = new List<string>();
        public static List<string> Colors2 = new List<string>();
        public static List<string> Materials0 = new List<string>();
        public static List<string> Materials1 = new List<string>();
        public static List<string> Limits = new List<string>();
        public static List<string> Antistatics0 = new List<string>();
        public static List<string> Antistatics1 = new List<string>();
        public static List<string> Colorants0 = new List<string>();
        public static List<string> Colorants1 = new List<string>();
        public static List<string> Colorants2 = new List<string>();
        public static List<string> Others = new List<string>();

        public static PrinterSettings printersettings;// = new PrinterSettings();

        //Инициализация первоначальных данных
        public static void Init()
        {
            try
            {
                StreamReader file = File.OpenText(printerFile);
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
        /// Ограничение максимального количество этикеток
        /// </summary>
        public static int MaxLabels(int line)
        {
            if (line >= firstType1 & line <= lastType1) return 24;  //Ограничение по Колпаку
            if (line >= firstType2 & line <= lastType2) return 24;  //Ограничение по Ротопринту
            return 9;
        }

        /// <summary>
        /// Загрузка выпадающих списков
        /// </summary>
        public static void ListsLoad()
        {
            loading = true;
            ListLoad(Types0, "Types0");
            ListLoad(Types1, "Types1");
            ListLoad(Weights0, "Weights0");
            ListLoad(Weights1, "Weights1");
            ListLoad(Weights2, "Weights2");
            ListLoad(Quantitys0, "Quantitys0");
            ListLoad(Quantitys1, "Quantitys1");
            ListLoad(Colors0, "Colors0");
            ListLoad(Colors1, "Colors1");
            ListLoad(Colors2, "Colors2");
            ListLoad(Materials0, "Materials0");
            ListLoad(Materials1, "Materials1");
            ListLoad(Limits, "Limits");
            ListLoad(Antistatics0, "Antistatics0");
            ListLoad(Antistatics1, "Antistatics1");
            ListLoad(Colorants0, "Colorants0");
            ListLoad(Colorants1, "Colorants1");
            ListLoad(Colorants2, "Colorants2");
            ListLoad(Others, "Others");
            loading = false;
        }

        static void ListLoad(List<string> list, string filename)
        {
            list.Clear();
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("ListRead");
                        writer.Write(filename);
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End") list.Add(s);
                        } while (s != "End");
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузка последних данных в случае сбоя программы, или инициализация данных по умолчанию
        /// </summary>
        public static void Load()
        {
            if (loading) return;
            loading = true;
            //Смена
            Shift.Load();
            //Линии
            lines.Clear();
            lines.Add(new Line("Husky №1", 0));
            lines.Add(new Line("Netstal №2", 0));
            lines.Add(new Line("Netstal №3", 0));
            lines.Add(new Line("Netstal №4", 0));
            lines.Add(new Line("Netstal №5", 0));
            lines.Add(new Line("Netstal №6", 0));
            lines.Add(new Line("Netstal №7", 0));
            lines.Add(new Line("Netstal №8", 0));
            lines.Add(new Line("Netstal №9", 0));
            lines.Add(new Line("C1", 1));
            lines.Add(new Line("C2", 1));
            lines.Add(new Line("Ротопринт", 2));
            loading = false;
        }

        /// <summary>
        /// Загрузка пользователей
        /// </summary>
        public static void UsersLoad()
        {
            users.Clear();
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
                        accessControl = reader.ReadString() == "AccessControl ON";
                        while (c > 0)
                        {
                            reader.ReadString();
                            users.Add(new User(reader.ReadString(), reader.ReadString(),
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
                        if (accessControl)
                            writer.Write("AccessControl ON");
                        else
                            writer.Write("AccessControl OFF");
                        foreach (User u in users)
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
                StreamWriter file = File.CreateText(printerFile);
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
            if (isTerminal)
            {
                //Сначала проверим, есть ли в списке хоть один админ
                if (users.Find(u => (u.Rule == 255 & u.Code != "")) == null) return 255;
                //Админы есть, значит просим прислонить ключ
                FormKey key = new FormKey();
                key.ShowDialog();
                //Ищем ключ в базе
                User user = users.Find(u => u.Code == key.Code);
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
            if (isTerminal)
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
            users.Add(new User(name, code, Rule));
            users.Sort((a,b) => a.Name.CompareTo(b.Name));
        }

        /// <summary>
        /// Проверка доступности ТПА данному пользователю
        /// </summary>
        /// <param name="name">Имя проверяемого</param>
        /// <param name="tpa">Номер ТПА</param>
        /// <returns></returns>
        public static bool AccessTest(string name, int tpa)
        {
            if (!accessControl) return true;
            User u = users.Find(us => us.Name == name);
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
            list.BeginUpdate();
            list.Items.Clear();
            foreach (User u in users)
            {
                ListViewItem it = new ListViewItem(u.Name);
                it.SubItems.Add(UserIsAdmin(u));
                it.SubItems.Add(UserWidthKey(u));
                it.SubItems.Add(u.StringWidthTPA());
                list.Items.Add(it);
            }
            list.EndUpdate();
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

        /// <summary>
        /// Возвращает название поля вес/логотип в зависимости от типа этикетки
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string WeightOrLogo(Line l)
        {
            if (l.lineType == 2) return "Логотип:";
            return "Вес:";
        }
    }
}
