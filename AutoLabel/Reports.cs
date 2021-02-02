using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    class Reports
    {
        static string LogFile;
        static int Shop;    //Цех (0-преформа, 1-колпак)
        static int First;   //Первая ТПА в цехе
        static int Last;    //Последняя ТПА в цехе
        static string ShopName;
        static int line;    //Счётчик строк, чтоб если что перенестись на следующую страницу
        static List<string[]> log = new List<string[]>();
        //Шрифты и линии
        static Font Normal = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font Bold = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Big = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Pixel);
        static Pen Slim = new Pen(Color.Black, 1);

        /// <summary>
        /// Загрузка и парсинг журнала
        /// </summary>
        static void LoadLog()
        {
            //Подготваливаем константы
            if (Shop == 0)
            {
                First = 0;
                Last = 6;
                ShopName = "Преформа";
            }
            else
            {
                First = 7;
                Last = 8;
                ShopName = "Колпак";
            }
            //Собственно загружаем лог
            try
            {
                log.Clear();
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("LogRead");
                        writer.Write(LogFile);
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End")
                            {
                                string[] Str = s.Split(';');
                                for (int i = 0; i < Str.Length; i++)
                                    if (Str[i][0] == ' ') Str[i] = Str[i].Trim(' ');
                                log.Add(Str);
                            }
                        } while (s != "End");
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Отчёт: журнал
        /// </summary>
        /// <param name="logfile">Файл журнала</param>
        /// <param name="shop">Цех (0 - преформа, 1 - колпак)</param>
        public static void Log(string logfile, int shop)
        {
            LogFile = logfile;
            Shop = shop;
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = false;
            doc.PrintPage += Doc_PrintPage;
            line = 0;
            doc.Print();
            Net.Log("Печать отчёта Журнал " + logfile + " (" + ShopName + ")");
        }
        static void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Журнал     " + LogFile + " (" + ShopName + ")", Big, Brushes.Black, 40, 40);
            e.Graphics.DrawString("Страница " + page + " из " + pages, Normal, Brushes.Black, 650, 40);
            //Шапка таблицы
            e.Graphics.DrawString("Номер", Bold, Brushes.Black, 40, 80);
            e.Graphics.DrawString("Дата", Bold, Brushes.Black, 100, 80);
            e.Graphics.DrawString("Время", Bold, Brushes.Black, 170, 80);
            e.Graphics.DrawString("Машина", Bold, Brushes.Black, 220, 80);
            e.Graphics.DrawString("Партия", Bold, Brushes.Black, 290, 80);
            e.Graphics.DrawString("Тип", Bold, Brushes.Black, 340, 80);
            e.Graphics.DrawString("Вес", Bold, Brushes.Black, 420, 80);
            e.Graphics.DrawString("Цвет", Bold, Brushes.Black, 470, 80);
            e.Graphics.DrawString("Короб", Bold, Brushes.Black, 580, 80);
            e.Graphics.DrawString("Упаковщик", Bold, Brushes.Black, 630, 80);
            int y = 110;
            int lines = 0;
            for (int i = 0; i < strings; i++)
            {
                if (line < log.Count)
                {
                    e.Graphics.DrawString((line + 1).ToString(), Normal, Brushes.Black, 40, y + i * height);
                    e.Graphics.DrawString(log[line][0], Normal, Brushes.Black, 100, y + i * height);
                    e.Graphics.DrawString(log[line][1], Normal, Brushes.Black, 170, y + i * height);
                    e.Graphics.DrawString(log[line][2], Normal, Brushes.Black, 220, y + i * height);
                    e.Graphics.DrawString(log[line][3], Normal, Brushes.Black, 290, y + i * height);
                    e.Graphics.DrawString(log[line][4], Normal, Brushes.Black, 340, y + i * height);
                    e.Graphics.DrawString(log[line][5], Normal, Brushes.Black, 420, y + i * height);
                    e.Graphics.DrawString(log[line][6], Normal, Brushes.Black, 470, y + i * height);
                    e.Graphics.DrawString(log[line][7], Normal, Brushes.Black, 580, y + i * height);
                    e.Graphics.DrawString(log[line][8], Normal, Brushes.Black, 630, y + i * height);
                    lines++;
                }
                line++;
            }
            //Рисование рамок таблицы
            int bot = 110 + lines * height;
            e.Graphics.DrawLine(Slim, 40, 70, 780, 70);
            e.Graphics.DrawLine(Slim, 40, 105, 780, 105);
            e.Graphics.DrawLine(Slim, 40, bot, 780, bot);
            e.Graphics.DrawLine(Slim, 40, 70, 40, bot);
            e.Graphics.DrawLine(Slim, 100, 70, 100, bot);
            e.Graphics.DrawLine(Slim, 170, 70, 170, bot);
            e.Graphics.DrawLine(Slim, 220, 70, 220, bot);
            e.Graphics.DrawLine(Slim, 290, 70, 290, bot);
            e.Graphics.DrawLine(Slim, 340, 70, 340, bot);
            e.Graphics.DrawLine(Slim, 420, 70, 420, bot);
            e.Graphics.DrawLine(Slim, 470, 70, 470, bot);
            e.Graphics.DrawLine(Slim, 580, 70, 580, bot);
            e.Graphics.DrawLine(Slim, 630, 70, 630, bot);
            e.Graphics.DrawLine(Slim, 780, 70, 780, bot);
            e.HasMorePages = line < log.Count();
        }

        /// <summary>
        /// Отчёт: общий
        /// </summary>
        /// <param name="logfile">Файл журнала</param>
        /// <param name="shop">Цех (0 - преформа, 1 - колпак)</param>
        public static void General(string logfile, int shop)
        {
            LogFile = logfile;
            Shop = shop;
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = false;
            doc.PrintPage += Doc_PrintPage1;
            line = 0;
            doc.Print();
            Net.Log("Печать отчёта Общий " + LogFile + " (" + ShopName + ")");
        }
        static void Doc_PrintPage1(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Общий отчёт     " + LogFile + " (" + ShopName + ")", Big, Brushes.Black, 40, 40);
            //Шапка таблицы
            e.Graphics.DrawString("Номер", Bold, Brushes.Black,40, 80);
            e.Graphics.DrawString("Машина", Bold, Brushes.Black, 100, 80);
            e.Graphics.DrawString("Партия", Bold, Brushes.Black, 180, 80);
            e.Graphics.DrawString("Тип", Bold, Brushes.Black, 240, 80);
            e.Graphics.DrawString("Вес", Bold, Brushes.Black, 320, 80);
            e.Graphics.DrawString("Цвет", Bold, Brushes.Black, 380, 80);
            e.Graphics.DrawString("Количество коробов", Bold, Brushes.Black, 520, 80);
            //Допустим, это всё влезет на одну страницу..... тогда делаем тупо внешний цикл по ТПАшкам
            for (int tpa = First; tpa <= Last; tpa++)
            {
                //Подготавливаем коллекцию сигнатур и коллекцию счётчиков
                List<string[]> sigs = new List<string[]>();
                List<int> counter = new List<int>();
                //Проходимся по журналу
                foreach (string[] rec in log)
                {
                    //Ищем только записи с нужным номером ТПА
                    if (rec[2] == Data.lines[tpa].name)
                    {
                        //Ищем текущую запись в коллекции сигнатур
                        bool found = false;
                        for (int i = 0; i < sigs.Count; i++)
                        {
                            //Проверяем, совпадает ли текущая запись с сигнатурой
                            if (rec[3] == sigs[i][3] & rec[4] == sigs[i][4] & rec[5] == sigs[i][5] & rec[6] == sigs[i][6])
                            {
                                counter[i]++; //Запись есть, увеличиваем значение соответствующего счётчика
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            sigs.Add(rec);  //Если такой записи не нашлось, создаём новую сигнатуру
                            counter.Add(1); //И создаём ещё один счётчик
                        }
                    }
                }
                //Рисуем строчки в отчёт
                int y = 110;
                for (int i = 0; i < sigs.Count; i++)
                {
                    e.Graphics.DrawString((line + 1).ToString(), Normal, Brushes.Black, 40, y + line * height);
                    e.Graphics.DrawString(sigs[i][2], Normal, Brushes.Black, 100, y + line * height);
                    e.Graphics.DrawString(sigs[i][3], Normal, Brushes.Black, 180, y + line * height);
                    e.Graphics.DrawString(sigs[i][4], Normal, Brushes.Black, 240, y + line * height);
                    e.Graphics.DrawString(sigs[i][5], Normal, Brushes.Black, 320, y + line * height);
                    e.Graphics.DrawString(sigs[i][6], Normal, Brushes.Black, 380, y + line * height);
                    e.Graphics.DrawString(counter[i].ToString(), Normal, Brushes.Black, 520, y + line * height);
                    line++;
                }
            }
            //Рисование рамок таблицы
            int bot = 110 + line * height;
            e.Graphics.DrawLine(Slim, 40, 70, 650, 70);
            e.Graphics.DrawLine(Slim, 40, 105, 650, 105);
            e.Graphics.DrawLine(Slim, 40, bot, 650, bot);
            e.Graphics.DrawLine(Slim, 40, 70, 40, bot);
            e.Graphics.DrawLine(Slim, 100, 70, 100, bot);
            e.Graphics.DrawLine(Slim, 180, 70, 180, bot);
            e.Graphics.DrawLine(Slim, 240, 70, 240, bot);
            e.Graphics.DrawLine(Slim, 320, 70, 320, bot);
            e.Graphics.DrawLine(Slim, 380, 70, 380, bot);
            e.Graphics.DrawLine(Slim, 520, 70, 520, bot);
            e.Graphics.DrawLine(Slim, 650, 70, 650, bot);
            //e.HasMorePages = line < log.Count();
        }

        /// <summary>
        /// Отчёт: По партиям
        /// </summary>
        /// <param name="logfile">Файл журнала</param>
        /// <param name="shop">Цех (0 - преформа, 1 - колпак)</param>
        public static void ByPart(string logfile, int shop)
        {
            LogFile = logfile;
            Shop = shop;
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = false;
            doc.PrintPage += Doc_PrintPage2;
            line = 0;
            doc.Print();
            Net.Log("Печать отчёта По партиям " + LogFile + " (" + ShopName + ")");
        }
        static void Doc_PrintPage2(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Отчёт по партиям     " + LogFile + " (" + ShopName + ")", Big, Brushes.Black, 40, 40);
            //Шапка таблицы
            e.Graphics.DrawString("Номер", Bold, Brushes.Black, 40, 80);
            e.Graphics.DrawString("Партия", Bold, Brushes.Black, 100, 80);
            e.Graphics.DrawString("Количество коробов", Bold, Brushes.Black, 200, 80);
            //Подготавливаем коллекцию партий и коллекцию счётчиков
            List<string> part = new List<string>();
            List<int> counter = new List<int>();
            //Проходимся по журналу
            foreach (string[] rec in log)
            {
                //Ищем текущую запись в коллекции сигнатур
                bool found = false;
                for (int i = 0; i < part.Count; i++)
                {
                    //Проверяем, совпадает ли текущая запись с сигнатурой
                    if (rec[3] == part[i])
                    {
                        counter[i]++; //Запись есть, увеличиваем значение соответствующего счётчика
                        found = true;
                    }
                }
                if (!found)
                {
                    part.Add(rec[3]);   //Если такой партии не нашлось - добавляем новую
                    counter.Add(1);     //И создаём ещё один счётчик
                }
            }
            //Рисуем строчки в отчёт
            int y = 110;
            for (int i = 0; i < part.Count; i++)
            {
                e.Graphics.DrawString((line + 1).ToString(), Normal, Brushes.Black, 40, y + line * height);
                e.Graphics.DrawString(part[i], Normal, Brushes.Black, 100, y + line * height);
                e.Graphics.DrawString(counter[i].ToString(), Normal, Brushes.Black, 200, y + line * height);
                line++;
            }
            //Рисование рамок таблицы
            int bot = 110 + line * height;
            e.Graphics.DrawLine(Slim, 40, 70, 330, 70);
            e.Graphics.DrawLine(Slim, 40, 105, 330, 105);
            e.Graphics.DrawLine(Slim, 40, bot, 330, bot);
            e.Graphics.DrawLine(Slim, 40, 70, 40, bot);
            e.Graphics.DrawLine(Slim, 100, 70, 100, bot);
            e.Graphics.DrawLine(Slim, 200, 70, 200, bot);
            e.Graphics.DrawLine(Slim, 330, 70, 330, bot);
            e.HasMorePages = line < log.Count();
        }

        /// <summary>
        /// Отчёт: По ТПА
        /// </summary>
        /// <param name="logfile">Файл журнала</param>
        /// <param name="shop">Цех (0 - преформа, 1 - колпак)</param>
        public static void ByTPA(string logfile, int shop)
        {
            LogFile = logfile;
            Shop = shop;
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = true;
            doc.PrintPage += Doc_PrintPage3;
            line = 0;
            doc.Print();
            Net.Log("Печать отчёта По ТПА " + LogFile + " (" + ShopName + ")");
        }
        static void Doc_PrintPage3(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            int step = 115; //Расстояние между колонками
            int maxlines = 32; //Максимальное количество строк
            //Заголовок
            e.Graphics.DrawString("Отчёт по ТПА     " + LogFile + " (" + ShopName + ")", Big, Brushes.Black, 40, 40);
            //Рамка
            int width = 9 * step;// (Last - First + 1) * step; 
            e.Graphics.DrawRectangle(Slim, 40, 75, width, 700);
            e.Graphics.DrawLine(Slim, 40, 115, width + 40, 115);
            for (int i = 40; i < width; i += step)
                e.Graphics.DrawLine(Slim, i, 75, i, 775);
            //Шапка таблицы
            int column = 0;
            for (int tpa = First; tpa <= Last; tpa++)
            {
                int x = column * step;
                e.Graphics.DrawString(Data.lines[tpa].name, Bold, Brushes.Black, x + 40, 80);
                e.Graphics.DrawString("№", Bold, Brushes.Black, x + 40, 100);
                e.Graphics.DrawString("Время", Bold, Brushes.Black, x + 58, 100);
                e.Graphics.DrawString("Короб", Bold, Brushes.Black, x + 100, 100);
                int y = 120;
                line = -1; //Начинаем с отрицательного, потому что по логике сначала строка инкрементируется.. так вышло..
                int n = 1;
                string lasttime = "";
                string lastbox = "";
                List<string> Packers = new List<string>();
                for (int i = 0; i < log.Count; i++) 
                {
                    //Подходит ли запись под эту ТПА
                    if (log[i][2] == Data.lines[tpa].name)
                    {
                        //Если время не совпадает с предыдущим, рисуем строку
                        if (log[i][1] == lasttime)
                        {
                            lastbox = log[i][7];
                            //Если следующая запись другая или её нет, то пишем последний
                            if (i < log.Count || log[i][1] != log[i + 1][1])
                                e.Graphics.DrawString("- " + lastbox, Normal, Brushes.Black, x + 115, y + line * height);
                        }
                        else
                        {
                            if (++line > maxlines) { line = 0; x = ++column * step; }
                            e.Graphics.DrawString(n.ToString(), Normal, Brushes.Black, x + 40, y + line * height);
                            e.Graphics.DrawString(log[i][1], Normal, Brushes.Black, x + 60, y + line * height);
                            e.Graphics.DrawString(log[i][7], Normal, Brushes.Black, x + 100, y + line * height);
                        }

                        n++;
                        lasttime = log[i][1];
                        //А теперь смотрим кто упаковищик и добавляем его в список если его там нет
                        if (Packers.Find(s => s == log[i][8]) == null)
                            Packers.Add(log[i][8]);
                    }
                    
                }

                //Под списком рисуем список упаковщиков
                line++;
                foreach (string s in Packers)
                {
                    if (++line > maxlines) { line = 0; x = ++column * step; }
                    e.Graphics.DrawString(s.ToString(), Normal, Brushes.Black, x + 40, y + line * height);
                }
                column++;
            }
        }
    }
}
