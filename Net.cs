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

        public static bool Test()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("Ping");
                        return reader.ReadString() == "Pong";
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Загрузка сообщения бегущей строки
        /// </summary>
        /// <param name="message"></param>
        public static string LoadMessage()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("MessageRead");
                        return reader.ReadString();
                    }
                }
            }
            catch { return "Ошибка сервера"; }
        }

        /// <summary>
        /// Загрузка параметров ТПА
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void LoadTPA(Label lab)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("TPARead");
                        writer.Write(lab.TPAName);
                        try { lab.CurrentNum = Convert.ToInt32(reader.ReadString()); }
                        catch { lab.CurrentNum = 0; }
                        lab.PartNum = reader.ReadString();
                        lab.Type = reader.ReadString();
                        lab.Weight = reader.ReadString();
                        lab.Count = reader.ReadString();
                        lab.Material = reader.ReadString();
                        lab.PColor = reader.ReadString();
                        lab.Antistatic = reader.ReadString();
                        lab.Colorant = reader.ReadString();
                        lab.Limit = reader.ReadString();
                        lab.Other = reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                        reader.ReadString();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Запись на сервер данных о тпа
        /// </summary>
        /// <param name="str"></param>
        public static void SaveTPA(Label lab)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("TPAWrite");
                        writer.Write(lab.TPAName);
                        writer.Write(lab.CurrentNum.ToString());
                        writer.Write(lab.PartNum);
                        writer.Write(lab.Type);
                        writer.Write(lab.Weight);
                        writer.Write(lab.Count);
                        writer.Write(lab.Material);
                        writer.Write(lab.PColor);
                        writer.Write(lab.Antistatic);
                        writer.Write(lab.Colorant);
                        writer.Write(lab.Limit);
                        writer.Write(lab.Other);
                        writer.Write("Дата и время изменения: " + DateTime.Now.ToString("yyyy.MM.dd HH:mm"));
                        writer.Write("*");
                        writer.Write("*");
                        writer.Write("*");
                        writer.Write("*");
                        writer.Write("*");
                        writer.Write("*");
                        writer.Write("*");
                    }
                }
            }
            catch
            {
                AutoLabel.Log.Error("Не удалось подключиться к серверу. Параметры ТПА не сохранены.");
            }
        }

        /// <summary>
        /// Запись строчки в журнал
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        str = @"Клиент """ + Environment.MachineName + @""": " + str;
                        writer.Write("Log");
                        writer.Write(str);
                    }
                }
            }
            catch { }
        }
    }
}
