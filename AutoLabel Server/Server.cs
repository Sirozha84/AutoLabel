using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AutoLabel_Server
{
    public class Server
    {
        const string ProgramLabel = "AutoLabel Server   Версия 1.0.3 (07.12.2016)   SG Software (Сергей Гордеев)";
        const int Port = 90;
        const string VersionForComp = "2.3.0";
        const string MessageFile = "Message.txt";
        const string TPAFile = "TPA.txt";
        const string ShiftFile = "Shift.txt";
        const string UsersFile = "Users.txt";
        const int ShiftStrings = 9;

        static string Message = "";
        static List<string[]> TPA = new List<string[]>();
        static List<Line> lines = new List<Line>();
        static string[] Shift;
        static List<string> Users = new List<string>();
        static List<DropList> DropLists = new List<DropList>();
        static List<Stat> Stats = new List<Stat>();
        static Timer timer = new Timer(SaveStat, null, 60000, 60000);
        
        public static void Main(string[] args)
        {
            
            Console.Title = "AutoLabel Server";
            //Загружаем данные
            LoadMessage();
            LoadTPA();
            LoadShift();
            LoadUsers();
            LoadStat();
            //Запускаем сервер
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, Port);
                server.Start();
                Console.WriteLine(ProgramLabel);
                Console.WriteLine("Требуемая версия клиента: " + VersionForComp + " и выше.\n");
                Log("Сервер запущен..................................................................");
                while (true)
                {
                    ThreadPool.QueueUserWorkItem(call, server.AcceptTcpClient());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Обработка запроса
        /// </summary>
        /// <param name="clientobject"></param>
        static void call(object clientobject)
        {
            try
            {
                TcpClient client = clientobject as TcpClient;
                using (NetworkStream stream = client.GetStream())
                {
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);
                    
                    //Принимаем запрос клиента
                    string query = reader.ReadString();
                    string[] q = query.Split('☺');
                    
                    if (query == "Ping")
                        writer.Write("Pong");
                    if (query == "Сompatibility")
                        writer.Write(VersionForComp);
                    if (query == "Log")
                        Log(reader.ReadString());
                    if (query == "MessageRead")
                        writer.Write(Message);
                    if (query == "MessageWrite")
                    {
                        Message = reader.ReadString();
                        try
                        {
                            File.WriteAllText(MessageFile, Message);
                        }
                        catch { };
                    }
                    if (q[0] == "LineRead")
                    {
                        Line line = lines.Find(o => o.name == q[1]);
                        if (line != null) writer.Write(line.ToSend()); else writer.Write("NotFound");
                    }
                    if (q[0] == "LineWrite")
                    {
                        Line line = lines.Find(o => o.name == q[1]);
                        line.Input(query.Substring(10, query.Length - 10));
                    }
                    if (query == "ShiftRead")
                    {
                        for (int i = 0; i < ShiftStrings; i++)
                        {
                            if (Shift[i] != null) writer.Write(Shift[i]);
                            else writer.Write("");
                        }
                    }
                    if (query == "ShiftWrite")
                    {
                        for (int i = 0; i < ShiftStrings; i++)
                            Shift[i] = reader.ReadString();
                        SaveShift();
                    }
                    if (query == "UsersRead")
                    {
                        writer.Write(Users.Count);
                        Users.ForEach(u => writer.Write(u));
                    }
                    if (query == "UsersWrite")
                    {
                        Users.Clear();
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End") Users.Add(s);
                        } while (s != "End");
                        SaveUsers();
                    }
                    if (query == "LogRecord")
                    {
                        Directory.CreateDirectory("Logs");
                        using (StreamWriter file = new StreamWriter("Logs\\" + reader.ReadString() + ".csv",
                            true, Encoding.Default))
                            file.WriteLine(reader.ReadString());
                    }
                    if (query == "LogRead")
                    {
                        using (StreamReader file = new StreamReader("Logs\\" + reader.ReadString() + ".csv",
                            Encoding.Default))

                        {
                            string s;
                            do
                            {
                                s = file.ReadLine();
                                if (s != null) writer.Write(s);
                            } while (s != null);
                            writer.Write("End");
                        }
                    }
                    if (query == "LogWrite")
                    {
                        using (StreamWriter file = new StreamWriter("Logs\\" + reader.ReadString() + ".csv",
                            false, Encoding.Default))
                        {
                            string s;
                            do
                            {
                                s = reader.ReadString();
                                if (s != "End") file.WriteLine(s);
                            } while (s != "End");
                        }
                    }
                    if (query == "ListRead")
                    {
                        string name = reader.ReadString();
                        //Ищем нужный список
                        DropList List = DropLists.Find(o => o.Name == name);
                        //Если его нет - создаём
                        if (List == null) List = new DropList(name);
                        DropLists.Add(List);
                        //А дальше уже отдаём старый или новый
                        List.List.ForEach(o => writer.Write(o));
                        writer.Write("End");
                    }
                    if (query == "ListWrite")
                    {
                        string name = reader.ReadString();
                        //Ищем нужный список
                        DropList List = DropLists.Find(o => o.Name == name);
                        //Если его нет - создаём
                        if (List == null) List = new DropList(name);
                        DropLists.Add(List);
                        //если такого списка нет - добавляем новый, а потом сохраняем его в файл
                        List.List.Clear();
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End") List.List.Add(s);
                        } while (s != "End");
                        List.Save();
                    }
                    if (query == "StatWrite")
                    {
                        AddStat(reader.ReadString());
                    }
                    if (query == "StatRead")
                    {
                        Stats.Sort(delegate (Stat s1, Stat s2) { return s2.Num.CompareTo(s1.Num); });
                        int sum = 0;
                        foreach (Stat s in Stats)
                        {
                            writer.Write(s.Name);
                            writer.Write(s.Num);
                            sum += s.Num;
                        }
                        writer.Write("Всего");
                        writer.Write(sum);
                        writer.Write("End");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        /// <summary>
        /// Запись лога
        /// </summary>
        /// <param name="str"></param>
        static void Log(string str)
        {
            str = DateTime.Now.ToString("[yyyy.MM.dd HH:mm] ") + str;
            Console.WriteLine(str);
            using (StreamWriter file = File.AppendText("Log.txt"))
                file.WriteLine(str);
        }

        /// <summary>
        /// Загрузка строки сообщения из файла
        /// </summary>
        static void LoadMessage()
        {
            try
            {
                Message = File.ReadAllText(MessageFile);
            }
            catch { File.CreateText(MessageFile); }
        }

        /// <summary>
        /// Сохранение строки сообщения в файл
        /// </summary>
        static void SaveMessage()
        {

        }

        /// <summary>
        /// Загрузка параметров ТПА из файла
        /// </summary>
        static void LoadTPA()
        {
            TPA.Clear();
            lines.Clear();
            try
            {
                using (TextReader file = File.OpenText(TPAFile))
                {
                    bool end = false;
                    while (!end)
                    {
                        string[] str = new string[20];
                        for (int i = 0; i < 20; i++) str[i] = file.ReadLine();
                        if (str[19] != null) TPA.Add(str);
                        if (str[19] != null) lines.Add(new Line(str));
                        else end = true;
                    }
                }
                
                /*var serializer = new XmlSerializer(typeof(Line));
                using (var reader = new StreamReader("Data.xml"))
                    lines = (List<Line>)serializer.Deserialize(reader);*/

            }
            catch { }
        }

        /// <summary>
        /// Сохранение параметров ТПА в файл
        /// </summary>
        static void SaveLines()
        {
            TPA.Sort((a, b) => a[0].CompareTo(b[0]));
            try
            {
                using (TextWriter file = File.CreateText(TPAFile))
                    foreach (string[] str in TPA)
                        for (int i = 0; i < 20; i++)
                            file.WriteLine(str[i]);

                var serializer = new XmlSerializer(typeof(List<Line>));
                using (var writer = new StreamWriter("Lines.xml"))
                    serializer.Serialize(writer, lines);

            }
            catch { }
        }

        /// <summary>
        /// Загрузка смен из файла
        /// </summary>
        static void LoadShift()
        {
            Shift = new string[ShiftStrings];
            try
            {
                using (TextReader file = File.OpenText(ShiftFile))
                    for (int i = 0; i < ShiftStrings; i++)
                        Shift[i] = file.ReadLine();
            }
            catch { }
        }

        /// <summary>
        /// Сохранение смен в файл
        /// </summary>
        static void SaveShift()
        {
            try
            {
                using (TextWriter file = File.CreateText(ShiftFile))
                    for (int i = 0; i < ShiftStrings; i++)
                        file.WriteLine(Shift[i]);
            }
            catch { }
        }

        /// <summary>
        /// Загрузка пользователей из файла
        /// </summary>
        static void LoadUsers()
        {
            try
            {
                using (TextReader file = File.OpenText(UsersFile))
                {
                    string s = "";
                    while (s!=null)
                    {
                        s = file.ReadLine();
                        if (s != null) Users.Add(s);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Запись пользователей в файл
        /// </summary>
        static void SaveUsers()
        {
            try
            {
                using (TextWriter file = File.CreateText(UsersFile))
                    Users.ForEach(u => file.WriteLine(u));
            }
            catch { }
        }

        /// <summary>
        /// Загрузка статистики
        /// </summary>
        static void LoadStat()
        {
            try
            {
                using (StreamReader file = File.OpenText("Statistics.txt"))
                {
                    int c = 0;
                    try { c = Convert.ToInt32(file.ReadLine()); } catch { }

                    for (int i = 0; i < c; i++)
                    {
                        try
                        {
                            Stat m = new Stat(file.ReadLine());
                            m.Num = Convert.ToInt32(file.ReadLine());
                            Stats.Add(m);
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        static void SaveStat(Object o)
        {
            Stats.Sort(delegate (Stat s1, Stat s2) { return s2.Num.CompareTo(s1.Num); });
            using (StreamWriter file = File.CreateText("Statistics.txt"))
            {
                file.WriteLine(Stats.Count);
                foreach (Stat s in Stats)
                {
                    file.WriteLine(s.Name);
                    file.WriteLine(s.Num);
                }
            }
        }

        /// <summary>
        /// Обновление статистики
        /// </summary>
        /// <param name="machine"></param>
        static void AddStat(string machine)
        {
            Stat m = Stats.Find(o => o.Name == machine);
            if (m == null)
            {
                m = new Stat(machine);
                Stats.Add(m);
            }
            m.Num++;
        }
    }
}
