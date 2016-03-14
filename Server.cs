using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace AutoLabel_Server
{
    class S
    {
        const string ProgramLabel = "AutoLabel Server   Версия 0.0.2 (11.03.2016)   SG Software (Сергей Гордеев)";
        const string MessageFile = "Message.txt";
        const string TPAFile = "TPA.txt";

        static string Message = "";
        static List<string[]> TPA = new List<string[]>();

        static void Main(string[] args)
        {
            Console.Title = "AutoLabel Server";
            //Загружаем данные
            LoadMessage();
            LoadTPA();
            //Запускаем сервер
            TcpListener server = new TcpListener(IPAddress.Any, 80);
            server.Start();
            Console.WriteLine(ProgramLabel);
            Log("------------------------------------------------------------");
            Log("Сервер запущен...");
            while (true)
            {
                ThreadPool.QueueUserWorkItem(call, server.AcceptTcpClient());
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
                        Log("Параметры ТПА сохранёны");
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

        static void LoadMessage()
        {
            try
            {
                Message = File.ReadAllText(MessageFile);
            }
            catch { File.CreateText(MessageFile); }
        }

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
    }
}
