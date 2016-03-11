using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace AutoLabel_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 80);
            server.Start();
            Console.Title = "AutoLabel Server";
            Console.WriteLine("AutoLabel Server   Версия 0.0.1 (11.03.2016) SG Software");
            Console.WriteLine("Сервер запущен...");
            while (true)
            {
                ThreadPool.QueueUserWorkItem(call, server.AcceptTcpClient());
            }
        }

        //Обработка запроса
        static void call(object clientobject)
        {
            TcpClient client = clientobject as TcpClient;
            using (NetworkStream stream = client.GetStream())
            {
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);
                //Принимаем запрос клиента
                Console.WriteLine(reader.ReadString());
                //Отправляем ответ
                writer.Write("OK");
            }
        }
    }
}
