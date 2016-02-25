using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

        public static string Current;
        public static string[] LogName = new string[ShiftMemory];


        /// <summary>
        /// Загрузка текущей смены
        /// </summary>
        public static void Load()
        {
            //Заполняем пустотой на случай если в файле нет информации
            for (int i = 0; i < ShiftMemory; i++)
                LogName[i] = "Пусто";
            try
            {
                StreamReader file = File.OpenText("Shift.txt");
                Current = file.ReadLine();
                for (int i = 0; i < ShiftMemory; i++)
                    LogName[i] = file.ReadLine();
                file.Dispose();
            }
            catch
            {
                Current = Names[0];
                Change(Current); //Если файла небыло, сохраним его
            }
        }

        /// <summary>
        /// Выбор новой смены
        /// </summary>
        /// <param name="shift"></param>
        public static void Change(string shift)
        {
            if (Current == shift) return;
            Current = shift;
            //Сдвигаем коллекцию последних смен
            for (int i = ShiftMemory - 1; i > 0; i--)
                LogName[i] = LogName[i - 1];
            //Изменяем файл журнала
            LogName[0] = (DateTime.Now.ToString("yyyy.MM.dd - ") + Current);
            //Сохраняем текущую смену в файл
            try
            {
                StreamWriter file = File.CreateText("Shift.txt");
                file.WriteLine(Current);
                for (int i = 0; i < ShiftMemory; i++)
                    file.WriteLine(LogName[i]);
                file.Dispose();
            }
            catch
            {
                Log.Error("Не удалось сохранить настройку смены.");
            }
            //Label.NewLog();
            Directory.CreateDirectory("Logs");
            //Обнуляем счётчики коробов
            foreach (Label l in Data.Labels)
            {
                l.CurrentNum = 1;
                l.Save();
            }
        }
    }
}
