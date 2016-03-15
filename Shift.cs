using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    class Shift
    {
        /// <summary>
        /// Количество последних запоминаемых смен
        /// </summary>
        const int ShiftMemory = 7;
        /// <summary>
        /// Названия смен
        /// </summary>
        public static string[] Names = { "Смена 1", "Смена 2", "Смена 3", "Смена 4" };
        /// <summary>
        /// Текущая смена
        /// </summary>
        public static string Current;
        /// <summary>
        /// Дата начала текущей смены
        /// </summary>
        public static string Date;
        /// <summary>
        /// Список последних смен
        /// </summary>
        public static string[] LogName = new string[ShiftMemory];


        /// <summary>
        /// Загрузка текущей смены
        /// </summary>
        public static void Load()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("ShiftRead");
                        Current = reader.ReadString();
                        Date = reader.ReadString();
                        for (int i = 0; i < ShiftMemory; i++)
                            LogName[i] = reader.ReadString();
                    }
                }
            }
            catch { }
            if (Current == "") Current = Names[0];
        }

        /// <summary>
        /// Выбор новой смены
        /// </summary>
        /// <param name="shift"></param>
        public static void Change(string shift)
        {
            if (Current == shift) return;
            Current = shift;
            //Устанавливаем дату текущей смены
            Date = DateToString();
            //Сдвигаем коллекцию последних смен
            for (int i = ShiftMemory - 1; i > 0; i--)
                LogName[i] = LogName[i - 1];
            //Изменяем файл журнала
            LogName[0] = (DateTime.Now.ToString("yyyy.MM.dd - ") + Current);
            //Сохраняем текущую смену в файл
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, 80);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("ShiftWrite");
                        writer.Write(Current);
                        writer.Write(Date);
                        for (int i = 0; i < ShiftMemory; i++)
                            writer.Write(LogName[i]);
                    }
                }
            }
            catch { }
                        /*try
                        {
                            StreamWriter file = File.CreateText("Shift.txt");
                            file.WriteLine(Current);
                            file.WriteLine(Date);
                            for (int i = 0; i < ShiftMemory; i++)
                                file.WriteLine(LogName[i]);
                            file.Close();
                        }
                        catch
                        {
                            Log.Error("Не удалось сохранить настройку смены.");
                        }
                        Directory.CreateDirectory("Logs");*/
                        //Обнуляем счётчики коробов
            foreach (Label l in Data.Labels)
            {
                l.CurrentNum = 1;
                l.Save();
            }
            Net.Log("Заступание новой смены");
        }

        /// <summary>
        /// Предоставление текущей даты в человечьем виде
        /// </summary>
        /// <returns></returns>
        public static string DateToString()
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("dd ");
            switch (now.Month)
            {
                case 1: date += "янв"; break;
                case 2: date += "фев"; break;
                case 3: date += "мар"; break;
                case 4: date += "апр"; break;
                case 5: date += "май"; break;
                case 6: date += "июн"; break;
                case 7: date += "июл"; break;
                case 8: date += "авг"; break;
                case 9: date += "сен"; break;
                case 10: date += "окт"; break;
                case 11: date += "ноя"; break;
                case 12: date += "дек"; break;
            }
            return date + now.ToString(" yy");
        }
    }
}
