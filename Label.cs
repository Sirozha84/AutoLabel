using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace AutoLabel
{
    class Label
    {
        string TPA;      //Номер ТПА
        public int CurrentNum;  //Автомат номер короба
        public string PartNum;  //Вручную номер партии
        public string Type;     //Список тип горловиры
        public string Weight;   //Список вес
        public string Count;    //Список количество
        public string Material; //Список материал
        public string PColor;   //Цвет
        public string AntistaticType;   //Список тип антистатика
        public string AntistaticCount;  //Список количество антистатика
        public string Limit;    //Список срок хранения
        public string Other;    //Вручную Дополнительные параметры

        //Карандаши и ручки :-)
        static Pen Bold = new Pen(Color.Black, 3);
        static Font Smalllll = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font SmallItalic = new Font("Arial", 11, FontStyle.Italic, GraphicsUnit.Pixel);
        static Font Small = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font SmallBold = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Normal = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Big = new Font("Arial", 37, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Biggg = new Font("Arial", 90, FontStyle.Bold, GraphicsUnit.Pixel);

        //Графика
        static Image logo = Image.FromFile("Graphics\\Europlast logo.jpeg");
        static Image rst = Image.FromFile("Graphics\\RST.png");

        //Текущие данные для печати
        static int Num;
        static string Packer;
        static int LabelCount;
        static string Date;
        static string Time;
        static string Shift;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="i">Номер ТПА</param>
        public Label(int i)
        {
            TPA = (i + 1).ToString();
            Load();
        }

        /// <summary>
        /// Печать этикетки
        /// </summary>
        /// <param name="num">Номер ящика</param>
        /// <param name="packer">Фамилия упаковщика</param>
        /// <param name="count">Количество этикеток (0 - если одна двойная)</param>
        public void Print(int num, string packer, int count)
        {
            Num = num;
            Packer = packer;
            LabelCount = count;
            Print(num, packer, count, DateToString(), DateTime.Now.ToString("HH:mm"), Data.Shift);
        }

        /// <summary>
        /// Печать этикетки с заданными датой и временем
        /// </summary>
        /// <param name="num">Номер ящика</param>
        /// <param name="packer">Фамилия упаковщика</param>
        /// <param name="count">Количество этикеток (0 - если одна двойная)</param>
        /// <param name="date">Дата</param>
        /// <param name="time">Время</param>
        /// <param name="shift">Смена</param>
        public void Print(int num, string packer, int count, string date, string time, string shift)
        {
            Date = date;
            Time = time;
            Shift = shift;

            if (!Data.PrintSelected()) Data.PrintSetup();
            if (!Data.PrintSelected()) return;
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += new PrintPageEventHandler(PD_PrintPage);
                doc.PrinterSettings = Data.printersettings;
                doc.Print();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при печати.\n" +
                    "Проверьте включен ли принтер, есть ли в нём бумага и тонер.");
            }
        }

        /// <summary>
        /// Формирование листа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (LabelCount == 0)
            {
                DrawLabel(e.Graphics, 20, 16,false);
                DrawLabel(e.Graphics, 604, 16,true);
            }
            else
            {
                DrawLabel(e.Graphics, 20, 16, true);
                if (LabelCount > 0) DrawLabel(e.Graphics, 604, 16, true);
            }
            e.HasMorePages = LabelCount > 0;
        }

        /// <summary>
        /// Формирование этикетки
        /// </summary>
        /// <param name="g"></param>
        /// <param name="IncNum">Увеличить ли номер после этой этикетки и записать в журнал</param>
        void DrawLabel(Graphics g, int X, int Y, bool IncNum)
        {
            int Width = 525;
            int Height = 767;
            //Рамки
            g.DrawRectangle(Bold, new Rectangle(X, Y, Width, Height));
            g.DrawLine(Bold, X, Y + 170, X + Width, Y + 170);
            g.DrawLine(Bold, X, Y + 270, X + Width, Y + 270);
            g.DrawLine(Bold, X + 220, Y + 170, X + 220, Y + 270);
            g.DrawLine(Bold, X + 410, Y + 170, X + 410, Y + 270);
            //Шапка
            g.DrawImage(logo, X + 190, Y + 3, 327, 45); //654*90
            g.DrawImage(rst, X + 445, Y + 90, 70, 70); //83*83
            g.DrawString("КРАСНОЯРСКИЙ ЗАВОД", SmallBold, Brushes.Black, new Point(X + 330, Y + 50));
            g.DrawString("Общество с ограниченной ответственностью \"Краснояркий завод полимерной упаковки", SmallItalic,
                Brushes.Black, new Point(X + 20, Y + 65));
            g.DrawString("\"ЕВРОПЛАСТ\",", SmallItalic, Brushes.Black, new Point(X + 220, Y + 80));
            g.DrawString("662500, Красноярский край, г. Сосновоборск, ул. Заводская д. 1, а/я 104,", SmallItalic,
                Brushes.Black, new Point(X + 10, Y + 95));
            g.DrawString("тел (3912) 180201, e-mail: krasnoyarsk@europlast.ru", SmallItalic,
                Brushes.Black, new Point(X + 10, Y + 107));
            g.DrawString("ISO 9001:2008", Small,
                Brushes.Black, new Point(X + 10, Y + 119));
            g.DrawString("Преформа для изготовления бутылок из полиэтилентерефталата", Small,
                Brushes.Black, new Point(X + 10, Y + 135));
            g.DrawString("Технические условия / Specification - ТУ - 2297 - 001 - 30463750 - 2012 с изм. №1", Smalllll,
                Brushes.Black, new Point(X + 10, Y + 155));
            //Главные поля
            g.DrawString(Weight, Biggg, Brushes.Black, new Point(X, Y + 170));
            g.DrawString(Type, Big, Brushes.Black, new Point(X + 220, Y + 200));
            g.DrawString(AntistaticCount, Big, Brushes.Black, new Point(X + 420, Y + 177));
            g.DrawString(AntistaticType, Big, Brushes.Black, new Point(X + 420, Y + 217));
            //Дополнительные поля
            g.DrawString("Прочие дополнения: " + Other, Small, Brushes.Black, new Point(X + 10, Y + 280));
            DrawStrings(g, X, Y, 220, 300, "Машина", "Machine", Data.TPAtoString(TPA));
            DrawStrings(g, X, Y, 220, 340, "Марка материала", "Material", Material);
            DrawStrings(g, X, Y, 220, 380, "Цвет преформы", "Preform colour", PColor);
            DrawStrings(g, X, Y, 220, 420, "Количество преформ в коробе", "Preform quantity per box", Count);
            DrawStrings(g, X, Y, 220, 460, "Дата изготовления", "Date of manufacturnig", Date);
            DrawStrings(g, X, Y, 220, 500, "Время", "Time", Time);
            DrawStrings(g, X, Y, 220, 540, "Номер партии", "Batch number", PartNum);
            DrawStrings(g, X, Y, 220, 580, "Номер короба", "Box number", Num.ToString());
            DrawStrings(g, X, Y, 220, 620, "Смена", "Shift", Shift);
            DrawStrings(g, X, Y, 220, 660, "Укладчик", "Packer", Packer);
            //Нижний колонтитул
            g.DrawString("Сделано в России / Made in Russia",
                SmallBold, Brushes.Black, new Point(X + 130, Y + Height - 55));
            g.DrawString("Гарантированный срок хранения - " + Limit + " со дня изготовления",
                SmallBold, Brushes.Black, new Point(X + 30, Y + Height - 35));
            g.DrawString("Перед выдувом бутылок рекомендуется выдержать преформы не менее 24 часов при t + 18°С",
                Smalllll, Brushes.Black, new Point(X + 10, Y + Height - 15));
            //Историю с логом переносим сюда же.. так как на одном листе могут
            if (IncNum)
            {
                if (Num == CurrentNum & CurrentNum > 0)
                {
                    Log();  //Лог пишу только когда печатается новая этикетка... может понадобится ещё что-то на счёт брака
                    CurrentNum++; //Увеличиваем номер, если печатался текущий
                    Save(); //Сохраняем, вдруг программа вылетет...
                }
                Num++;
                LabelCount--;
            }
        }

        /// <summary>
        /// Вывод поля
        /// </summary>
        /// <param name="g">Куда рисовать</param>
        /// <param name="x">Координата</param>
        /// <param name="y">Координата</param>
        /// <param name="s1">Строка по-русски</param>
        /// <param name="s2">Строка по-английски</param>
        /// <param name="s3">Значение</param>
        static void DrawStrings(Graphics g, int X, int Y, int x, int y, string s1, string s2, string s3)
        {
            g.DrawString(s1, SmallBold, Brushes.Black, new Point(X + 10, Y + y));
            g.DrawString(s2, Small, Brushes.Black, new Point(X + 10, Y + y + 14));
            g.DrawString(s3, Normal, Brushes.Black, new Point(X + x + 10, Y + y));
        }

        /// <summary>
        /// Предоставление текущей даты в человечьем виде
        /// </summary>
        /// <returns></returns>
        static string DateToString()
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

        /// <summary>
        /// Сохранение на диск
        /// </summary>
        public void Save()
        {
            try
            {
                StreamWriter file = File.CreateText("TPA"+TPA+".txt");
                file.WriteLine(CurrentNum);
                file.WriteLine(PartNum);
                file.WriteLine(Type);
                file.WriteLine(Weight);
                file.WriteLine(Count);
                file.WriteLine(Material);
                file.WriteLine(PColor);
                file.WriteLine(AntistaticType);
                file.WriteLine(AntistaticCount);
                file.WriteLine(Limit);
                file.WriteLine(Other);
                file.Dispose();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить параметр ТПА. Позовите админа! Пусть он в этом разберётся.",
                    "Случилось что-то страшное");
                //Ох, надеюсь мне не придётся увидеть этой надписи...
            }
        }

        /// <summary>
        /// Загрузка с диска
        /// </summary>
        public void Load()
        {
            try
            {
                StreamReader file = File.OpenText("TPA" + TPA + ".txt");
                CurrentNum = Convert.ToInt32(file.ReadLine());
                PartNum = file.ReadLine();
                Type = file.ReadLine();
                Weight = file.ReadLine();
                Count = file.ReadLine();
                Material = file.ReadLine();
                PColor = file.ReadLine();
                AntistaticType = file.ReadLine();
                AntistaticCount = file.ReadLine();
                Limit = file.ReadLine();
                Other = file.ReadLine();
                file.Dispose();
            }
            catch { } //нишмагла...
        }

        /// <summary>
        /// Запись в журнал отчёта о напечатанной этикетке
        /// </summary>
        void Log()
        {
            try
            {
                StreamWriter file = new StreamWriter("Logs\\" + Data.LogName[0]+".csv", true, Encoding.Default);
                file.WriteLine(DateTime.Now.ToString("dd.MM; HH:mm") +
                    "; ТПА" + TPA + "; " + PartNum + "; " + Type +"; " + Weight + "; " +
                    PColor +"; " + CurrentNum + "; " + Packer);
                file.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Подпись над кнопкой на главном экране
        /// </summary>
        /// <returns></returns>
        public string LabelUnderButton()
        {
            string str = "";
            if (PartNum != null & PartNum != "")
            {
                str = "Партия: " + PartNum;
                if (CurrentNum > 0) str += "   Выпущено коробов: " + (CurrentNum - 1).ToString();
            }
            return str;
        }
    }
}
