using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace AutoLabel_Server
{
    class S
    {
        const string ProgramLabel = "AutoLabel Server   Версия 0.0.1 (11.03.2016)   SG Software(Сергей Гордеев)";
        const string MessageFile = "Message.txt";

        static void Main(string[] args)
        {
            Console.Title = "AutoLabel Server";
            //Запускаем сервер
            TcpListener server = new TcpListener(IPAddress.Any, 80);
            server.Start();
            Console.WriteLine(ProgramLabel);
            Log("Сервер запущен...");
            while (true)
            {
                ThreadPool.QueueUserWorkItem(call, server.AcceptTcpClient());
            }
        }

        //Обработка запроса
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
                    if (query == "MessageRead")
                    {
                        string s = "";
                        //Отправляем ответ
                        try { s = File.ReadAllText(MessageFile); }
                        catch { File.CreateText(MessageFile); }
                        writer.Write(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
        }

        static void Log(string str)
        {
            str = DateTime.Now.ToString("[yyyy.MM.dd HH:mm] ") + str;
            Console.WriteLine(str);
            using (StreamWriter file = File.AppendText("Log.txt"))
                file.WriteLine(str);
        }
    }
}
