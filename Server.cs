using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace AutoLabel_Server
{
    class Server
    {
        const string ProgramLabel = "AutoLabel Server   Версия 0.0.4 (21.03.2016)   SG Software (Сергей Гордеев)";
        const int Port = 90;
        const string MessageFile = "Message.txt";
        const string TPAFile = "TPA.txt";
        const string ShiftFile = "Shift.txt";
        const string UsersFile = "Users.txt";
        const int ShiftStrings = 9;

        static string Message = "";
        static List<string[]> TPA = new List<string[]>();
        static string[] Shift;
        static List<string> Users = new List<string>();
        static List<DropList> DropLists = new List<DropList>();

        static void Main(string[] args)
        {
            Console.Title = "AutoLabel Server";
            //Загружаем данные
            LoadMessage();
            LoadTPA();
            LoadShift();
            LoadUsers();
            //Запускаем сервер
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, Port);
                server.Start();
                Console.WriteLine(ProgramLabel);
                Log("------------------------------------------------------------");
                Log("Сервер запущен...");
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
                    if (query == "Ping")
                        writer.Write("Pong");
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
                    if (query == "TPARead")
                    {
                        string name = reader.ReadString();
                        int ind = TPA.FindIndex(tpa => tpa[0] == name);
                        if (ind >= 0) for (int i = 1; i < 20; i++) writer.Write(TPA[ind][i]);
                        else for (int i = 1; i < 20; i++) writer.Write("");
                    }
                    if (query == "TPAWrite")
                    {
                        string name = reader.ReadString();
                        int ind = TPA.FindIndex(tpa => tpa[0] == name);
                        if (ind == -1)
                        {
                            TPA.Add(new string[20]);
                            ind = TPA.Count - 1;
                        }
                        TPA[ind][0] = name;
                        for (int i = 1; i < 20; i++) TPA[ind][i] = reader.ReadString();
                        SaveTPA();
                        //Log("Параметры ТПА сохранёны");
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
                        using (TextWriter file = File.AppendText("Logs\\" + reader.ReadString() + ".csv"))
                            file.WriteLine(reader.ReadString());
                    }
                    if (query == "LogRead")
                    {
                        using (TextReader file = File.OpenText("Logs\\" + reader.ReadString()+".csv"))
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
                        using (TextWriter file = File.CreateText("Logs\\" + reader.ReadString() + ".csv"))
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
                        else end = true;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Сохранение параметров ТПА в файл
        /// </summary>
        static void SaveTPA()
        {
            TPA.Sort((a, b) => a[0].CompareTo(b[0]));
            try
            {
                using (TextWriter file = File.CreateText(TPAFile))
                    foreach (string[] str in TPA)
                        for (int i = 0; i < 20; i++)
                            file.WriteLine(str[i]);
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
    }
}
