using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string Quantity;    //Список количество
        public string Antistatic;   //Список количество антистатика
        public string Material;   //Список сырьё
        public string Limit;    //Список срок хранения
        public string PColor;    //Цвет

        //Координаты и размеры этикетки
        static int X;
        static int Y;
        static int Width;
        static int Height;

        //Карандаши и ручки :-)
        static Pen Bold = new Pen(Color.Black, 3);
        static Font Smalllll = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font SmallItalic = new Font("Arial", 11, FontStyle.Italic, GraphicsUnit.Pixel);
        static Font Small = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font SmallBold = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Normal = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Big = new Font("Arial", 40, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Biggg = new Font("Arial", 90, FontStyle.Bold, GraphicsUnit.Pixel);

        //Графика
        Image logo = Image.FromFile("Graphics\\Europlast logo.jpeg");
        Image rst = Image.FromFile("Graphics\\RST.png");

        //Текущие данные для печати
        static int Num;
        static string Packer;

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
        /// Печать
        /// </summary>
        /// <param name="num">Номер ящика</param>
        /// /// <param name="Packer">Фамилия упаковщика</param>
        public void Print(int num, string packer)
        {
            Num = num;
            Packer = packer;
            if (!Data.PrintSelected()) Data.PrintSetup();
            if (!Data.PrintSelected()) return;
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += new PrintPageEventHandler(PD_PrintPage);
                doc.PrinterSettings = Data.printersettings;
                doc.Print();
                if (num == CurrentNum & CurrentNum > 0)
                {
                    Log();  //Лог пишу только когда печатается новая этикетка... может понадобится ещё что-то на счёт брака
                    CurrentNum++; //Увеличиваем номер, если печатался текущий
                    Save(); //Сохраняем, вдруг программа вылетет...
                }
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
            int Space = 30;
            int Shift = -10;
            Width = 585 - Space * 2;
            Height = 827 - Space * 2;
            X = Space - 10;
            Y = Space - 14;
            DrawLabel(e.Graphics);
            X = Space - 10 + 584;
            Y = Space - 14;
            DrawLabel(e.Graphics);
        }

        /// <summary>
        /// Формирование этикетки
        /// </summary>
        /// <param name="g"></param>
        void DrawLabel(Graphics g)
        {
            //Рамки
            g.DrawRectangle(Bold, new Rectangle(X, Y, Width, Height));
            g.DrawLine(Bold, X, Y + 170, X + Width, Y + 170);
            g.DrawLine(Bold, X, Y + 270, X + Width, Y + 270);
            g.DrawLine(Bold, X + 220, Y + 170, X + 220, Y + 270);
            g.DrawLine(Bold, X + 410, Y + 170, X + 410, Y + 270);
            //Шапка
            g.DrawImage(logo, X + 190, Y + 3, 327, 45); //654*90
            g.DrawImage(rst, X + 440, Y + 90, 70, 70); //83*83
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
            g.DrawString("Преформа бутылки из полиэтилентерефталата", SmallBold,
                Brushes.Black, new Point(X + 10, Y + 135));
            g.DrawString("Технические условия / Specification - ТУ - 2297 - 001 - 30463750 - 2012 с изм. №1", Smalllll,
                Brushes.Black, new Point(X + 10, Y + 155));
            //Главне поля
            g.DrawString(Weight, Biggg, Brushes.Black, new Point(X, Y + 170));
            g.DrawString(Type, Big, Brushes.Black, new Point(X + 220, Y + 200));
            //Дополнительные поля
            g.DrawString("Прочие дополнения:", Small, Brushes.Black, new Point(X + 10, Y + 280));
            DrawStrings(g, 220, 300, "Машина", "Machine", "NETSTAL №" + TPA);
            DrawStrings(g, 220, 340, "Марка материала", "Material", Material);
            DrawStrings(g, 220, 380, "Цвет преформы", "Preform colour", PColor);
            DrawStrings(g, 270, 420, "Количество преформ в коробе", "Preform quantity per box", Quantity);
            DrawStrings(g, 220, 460, "Дата изготовления", "Date of manufacturnig", Date());
            DrawStrings(g, 220, 500, "Время", "Time", DateTime.Now.ToString("HH:mm"));
            DrawStrings(g, 220, 540, "Номер партии", "Batch number", PartNum);
            DrawStrings(g, 220, 580, "Номер короба", "Box number", Num.ToString());
            DrawStrings(g, 220, 620, "Смена", "Shift", Data.Shift);
            DrawStrings(g, 220, 660, "Укладчик", "Packer", Packer);
            //Нижний колонтитул
            g.DrawString("Сделано в России / Made in Russia",
                SmallBold, Brushes.Black, new Point(X + 130, Y + Height - 55));
            g.DrawString("Гарантированный срок хранения - 24 месяца со дня изготовления",
                SmallBold, Brushes.Black, new Point(X + 30, Y + Height - 35));
            g.DrawString("Перед выдувом бутылок рекомендуется выдержать преформы не менее 24 часов при t + 18°С",
                Smalllll, Brushes.Black, new Point(X + 10, Y + Height - 15));
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
        static void DrawStrings(Graphics g, int x, int y, string s1, string s2, string s3)
        {
            g.DrawString(s1, SmallBold, Brushes.Black, new Point(X + 10, Y + y));
            g.DrawString(s2, Small, Brushes.Black, new Point(X + 10, Y + y + 14));
            g.DrawString(s3, Normal, Brushes.Black, new Point(X + x + 10, Y + y));
        }

        /// <summary>
        /// Предоставление текущей даты в человечьем виде
        /// </summary>
        /// <returns></returns>
        static string Date()
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
                file.WriteLine(Quantity);
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
                Quantity = file.ReadLine();
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
                StreamWriter file = new StreamWriter(Data.LogName, true, Encoding.Default);
                file.WriteLine(DateTime.Now.ToString("dd.MM; HH:mm") +
                    "; ТПА" + TPA + "; " + PartNum + "; " + CurrentNum + "; " + Data.Shift + "; " + Packer);
                file.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Запись шапки для нового лога (не знаю надо ли, но на всякий случай сделаю)
        /// </summary>
        public static void NewLog()
        {
            try
            {
                StreamWriter file = new StreamWriter(Data.LogName, true, Encoding.Default);
                file.WriteLine("Дата; Время; Машина; Партия; Короб; Смена; Упаковщик");
                file.Dispose();
            }
            catch { }
        }
    }
}
