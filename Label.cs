using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace AutoLabel
{
    class Label
    {
        string TPA;      //Номер ТПА
        public string PartNum;  //Вручную номер партии
        public int CurrentNum;  //Автомат номер короба
        public string Type;     //Список тип горловиры
        public string Weight;   //Список вес
        public string Quantity;    //Список количество
        public string Antistatic;   //Список количество антистатика
        public string Material;   //Список сырьё
        public string Limit;    //Список срок хранения

        //Координаты и размеры этикетки
        static int X;
        static int Y;
        static int Width;
        static int Height;

        //Карандаши и ручки :-)
        static Pen Bold = new Pen(Color.Black, 3);
        static Font Smalllll = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font Small = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font SmallBold = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Normal = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Big = new Font("Arial", 40, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font Biggg = new Font("Arial", 90, FontStyle.Bold, GraphicsUnit.Pixel);

        public Label(int i) { TPA = (i + 1).ToString(); }

        public void Print(int num)
        {
            if (!Data.PrintSelected())
            {
                MessageBox.Show("Принтер не выбран, выберите принтер");
                Data.PrintSetup();
            }
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += new PrintPageEventHandler(PD_PrintPage);
                doc.PrinterSettings = Data.printersettings;
                doc.Print();
                if (num == CurrentNum & CurrentNum > 0) CurrentNum++; //Увеличиваем номер, если печатался текущий
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при печати.\n" +
                    "Проверьте включен ли принтер, есть ли в нём бумага и тонер.");
            }
        }

        //Формирование листа
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
            //DrawLabel(e.Graphics, Space + Shift, Space + Shift, 585 - Space * 2, 827 - Space * 2); //581*827
            //DrawLabel(e.Graphics, Space + Shift + 584, Space + Shift, 585 - Space * 2, 827 - Space * 2);
        }

        //Формирование этикетки
        void DrawLabel(Graphics g)
        {
            //Рамки
            g.DrawRectangle(Bold, new Rectangle(X, Y, Width, Height));
            g.DrawLine(Bold, X, Y + 170, X + Width, Y + 170);
            g.DrawLine(Bold, X, Y + 270, X + Width, Y + 270);
            g.DrawLine(Bold, X + 220, Y + 170, X + 220, Y + 270);
            g.DrawLine(Bold, X + 410, Y + 170, X + 410, Y + 270);
            //Шапка
            g.DrawString("Demo", Biggg, Brushes.Black, new Point(X + 110, Y + 40));
            //Главне поля
            g.DrawString(Weight, Biggg, Brushes.Black, new Point(X, Y + 170));
            g.DrawString(Type, Big, Brushes.Black, new Point(X + 220, Y + 200));
            //Дополнительные поля
            g.DrawString("Прочие дополнения:", Small, Brushes.Black, new Point(X + 10, Y + 280));
            DrawStrings(g, 270, 300, "Количество преформ в коробе", "Preform quantity per box", Quantity);
            DrawStrings(g, 220, 340, "Номер короба", "Box number", CurrentNum.ToString());
            DrawStrings(g, 220, 380, "Дата изготовления", "Date of manufacturnig", DateTime.Now.ToString("dd mmmm yy"));
            DrawStrings(g, 220, 420, "Цвет преформы", "Preform colour", "Белый");
            DrawStrings(g, 220, 460, "Машина", "Machine", "NETSTAL №"+TPA);
            DrawStrings(g, 220, 500, "Смена", "Shift", "");
            DrawStrings(g, 220, 540, "Марка материала", "Material", Material);
            DrawStrings(g, 220, 580, "Время", "Time", DateTime.Now.ToString("HH:MM"));
            DrawStrings(g, 220, 620, "Номер партии", "Batch number", "");
            DrawStrings(g, 220, 660, "Укладчик", "Packer", "");
            //Нижний колонтитул
            g.DrawString("Сделано в России / Made in Russia",
                SmallBold, Brushes.Black, new Point(X + 130, Y + Height - 55));
            g.DrawString("Гарантированный срок хранения - 24 месяца со дня изготовления",
                SmallBold, Brushes.Black, new Point(X + 30, Y + Height - 35));
            g.DrawString("Перед выдувом бутылок рекомендуется выдержать преформы не менее 24 часов при t + 18°С",
                Smalllll, Brushes.Black, new Point(X + 10, Y + Height - 15));
        }

        //Вывод поля
        static void DrawStrings(Graphics g, int x, int y, string s1, string s2, string s3)
        {
            g.DrawString(s1, SmallBold, Brushes.Black, new Point(X + 10, Y + y));
            g.DrawString(s2, Small, Brushes.Black, new Point(X + 10, Y + y + 14));
            g.DrawString(s3, Normal, Brushes.Black, new Point(X + x + 10, Y + y));
        }

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
    }
}
