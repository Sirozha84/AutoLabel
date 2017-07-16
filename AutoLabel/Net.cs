using System;
using System.Net.Sockets;
using System.IO;

namespace AutoLabel
{
    class Net
    {
        public static string HostName = "localhost";
        public const int Port = 90;
        const string ParamFile = "Server.txt";

        /// <summary>
        /// Загрузка параметров сервера
        /// </summary>
        public static void Init()
        {
            try
            {
                using (StreamReader file = File.OpenText(ParamFile))
                    HostName = file.ReadLine();
            }
            catch
            {
                using (StreamWriter file = File.CreateText(ParamFile))
                    file.WriteLine(HostName);
            }
        }

        public static bool Test()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, Port);
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

        //Проверка на совместимость версий
        public static void TestСompatibility()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("Сompatibility");
                        string v = reader.ReadString();
                        if (v == Program.VersionForComp) return;
                        Log("Несовместимая версия программы");
                        System.Windows.Forms.MessageBox.Show("Версия программы: " +
                            Program.Version + "\nТребуемая версия: " + v +
                            " или выше\nРабота будет остановлена", "AutoLabel",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                }
            }
            catch { }
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
                    client.Connect(HostName, Port);
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
        /// Запись строчки в журнал
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(HostName, Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        str = "[" + Environment.MachineName + "] " + str;
                        writer.Write("Log");
                        writer.Write(str);
                    }
                }
            }
            catch { }
        }
    }
}
