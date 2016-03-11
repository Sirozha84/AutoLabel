using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace AutoLabel
{
    class Net
    {
        static string HostName = "localhost";
        const string ParamFile = "Server.txt";

        /// <summary>
        /// Загрузка параметров сервера
        /// </summary>
        public static void Init()
        {
            try
            {
                StreamReader file = File.OpenText(ParamFile);
                file.Close();
            }
            catch
            {
                StreamWriter file = File.CreateText(ParamFile);
                file.Write(HostName);
                file.Close();
            }
        }

        /// <summary>
        /// Загрузка сообщения бегущей строки
        /// </summary>
        /// <param name="message"></param>
        public static string LoadMessage()
        {
            Console.WriteLine("Соединение...");
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        Console.WriteLine("Соединение установлено");
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("MessageRead");
                        Console.WriteLine("Сответ получен");
                        return reader.ReadString();
                    }
                }
            }
            catch { return "Ошибка сервера"; }
        }
    }
}
