using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    public class Label
    {
        public string TPAName;  //Имя ТПА
        public int TPAType;     //Тип ТПА (0 - преформа, 1 - колпак, 2 - ротопринт)
        public int CurrentNum;  //Автомат номер короба
        public string PartNum;  //Вручную номер партии
        public string Type;     //Список тип горловиры
        public string Weight;   //Список вес
        public string Count;    //Список количество
        public string Material; //Список материал
        public string PColor;   //Цвет
        public string Antistatic;   //Список тип антистатика
        public string Colorant; //Список количество антистатика
        public string Limit;    //Список срок хранения
        public string Other;    //Вручную Дополнительные параметры

        //Карандаши и ручки :-)
        static Pen ClipLine = new Pen(Color.Black, 0.5f);
        static Pen Slim = new Pen(Color.Black, 1);
        static Pen Bold = new Pen(Color.Black, 3);
        static Font F11 = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F11Italic = new Font("Arial", 11, FontStyle.Italic, GraphicsUnit.Pixel);
        static Font F12 = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F14 = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F14Bold = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F20Bold = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F22 = new Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F22Bold = new Font("Arial", 22, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F30Bold = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F37Bold = new Font("Arial", 37, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F70Bold = new Font("Arial", 70, FontStyle.Bold, GraphicsUnit.Pixel);
        static StringFormat InRect = new StringFormat();

        //Графика
        static Image logo = Image.FromFile("Graphics\\Logo.png");
        static Image rst = Image.FromFile("Graphics\\RST.png");
        static Image HDPE = Image.FromFile("Graphics\\HDPE.png");
        static Image Eda = Image.FromFile("Graphics\\Eda.png");
        static Image EAC = Image.FromFile("Graphics\\EAC.png");

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
        /// <param name="name">Название машины</param>
        /// <param name="type">Тип ТПА (0-преформа, 1-колпак)</param>
        public Label(string name, int type)
        {
            TPAName = name;
            TPAType = type;
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
            Print(num, packer, count, AutoLabel.Shift.Date, DateTime.Now.ToString("HH:mm"), AutoLabel.Shift.Current);
            Save();
            Net.Log("Печать этикетки \"" + Conformity.LabelName(TPAType) + "\"");
            //Запись данных для статистики
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("StatWrite");
                        writer.Write(Environment.MachineName);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Печать этикетки с заданными датой и временем (для кастомных)
        /// </summary>
        /// <param name="num">Номер ящика</param>
        /// <param name="packer">Фамилия упаковщика</param>
        /// <param name="count">Количество этикеток (0 - если одна двойная)</param>
        /// <param name="date">Дата</param>
        /// <param name="time">Время</param>
        /// <param name="shift">Смена</param>
        public void Print(int num, string packer, int count, string date, string time, string shift)
        {
            Num = num;
            Packer = packer;
            LabelCount = count;
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
                doc.PrinterSettings.DefaultPageSettings.Landscape = (TPAType != 2); //Горизонтальный, если не для ротопринта
                doc.Print();
            }
            catch (ArgumentException e)
            {
                AutoLabel.Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Формирование листа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (TPAType == 0)
            {
                //Этикетка для преформы
                if (LabelCount == 0)
                {
                    DrawLabel(e.Graphics, 20, 16, false);
                    DrawLabel(e.Graphics, 604, 16, true);
                }
                else
                {
                    DrawLabel(e.Graphics, 20, 16, true);
                    if (LabelCount > 0) DrawLabel(e.Graphics, 604, 16, true);
                }
                ClipLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawLine(ClipLine, 574, 0, 574, 850);
            }
            if (TPAType == 1)
            {
                //Этикетка для колпачка
                DrawLabelC(e.Graphics, 20, 16);
                if (LabelCount > 0) DrawLabelC(e.Graphics, 20, 396);
                if (LabelCount > 0) DrawLabelC(e.Graphics, 570, 16);
                if (LabelCount > 0) DrawLabelC(e.Graphics, 570, 396);
            }
            if (TPAType == 2)
            {
                //Этикетка для ротопринта
                DrawLabelR(e.Graphics, 20, 16, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, 16, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 20, 200, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, 200, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 20, 384, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, 384, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 20, 568, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, 568, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 20, 752, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, 752, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 20, 936, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, 936, true);
            }
            e.HasMorePages = LabelCount > 0;
        }

        /// <summary>
        /// Формирование этикетки преформы
        /// </summary>
        /// <param name="g"></param>
        /// <param name="X">Положение на листе по X</param>
        /// <param name="Y">Положение на листе по Y</param>
        /// <param name="IncNum">Увеличить ли номер после этой этикетки и записать в журнал</param>
        void DrawLabel(Graphics g, int X, int Y, bool IncNum)
        {
            int Width = 525;
            int Height = 767;
            //Ватермарка
            string file = Watermark.Image(Other);
            if (file != "")
            {
                try
                {
                    Image logo = Image.FromFile("Graphics\\Logos\\" + file + ".png");

                    g.DrawImage(logo, X, Y + 270, Width, 497);
                }
                catch { }
            }
            //Рамки
            g.DrawRectangle(Bold, new Rectangle(X, Y, Width, Height));
            g.DrawLine(Bold, X, Y + 170, X + Width, Y + 170);
            g.DrawLine(Bold, X, Y + 270, X + Width, Y + 270);
            g.DrawLine(Bold, X + 220, Y + 170, X + 220, Y + 270);
            g.DrawLine(Bold, X + 410, Y + 170, X + 410, Y + 270);
            //Шапка
            g.DrawImage(logo, X + 190, Y + 3, 327, 45); //654*90
            g.DrawImage(rst, X + 445, Y + 80, 70, 70); //83*83
            g.DrawString("КРАСНОЯРСКИЙ ЗАВОД", F14Bold, Brushes.Black, X + 330, Y + 50);
            g.DrawString("Общество с ограниченной ответственностью \"Краснояркий завод полимерной упаковки",
                F11Italic, Brushes.Black, X + 20, Y + 65);
            g.DrawString("\"ЕВРОПЛАСТ\",", F11Italic, Brushes.Black, X + 220, Y + 80);
            g.DrawString("662500, Красноярский край, г. Сосновоборск, ул. Заводская д. 1, а/я 104,",
                F11Italic, Brushes.Black, X + 10, Y + 95);
            g.DrawString("тел (3912) 180201, e-mail: krasnoyarsk@europlast.ru",
                F11Italic, Brushes.Black, X + 10, Y + 107);
            g.DrawString("ISO 9001:2008", F14, Brushes.Black, X + 10, Y + 119);
            g.DrawString("Преформа для изготовления бутылок из полиэтилентерефталата",
                F14, Brushes.Black, X + 10, Y + 135);
            g.DrawString("Технические условия / Specification - ТУ - 2297 - 001 - 30463750 - 2012 с изм. №1, изм. №2",
                F11, Brushes.Black, X + 10, Y + 155);
            //Главные поля
            g.DrawString(Weight, F70Bold, Brushes.Black, X, Y + 180);
            g.DrawString(Type, F37Bold, Brushes.Black, X + 220, Y + 200);
            g.DrawString(Colorant, F37Bold, Brushes.Black, X + 420, Y + 177);
            g.DrawString(Antistatic, F37Bold, Brushes.Black, X + 420, Y + 217);
            //Дополнительные поля
            g.DrawString("Прочие дополнения: " + Other, F14, Brushes.Black, X + 10, Y + 280);
            DrawStrings(g, X, Y, 300, "Машина", "Machine", TPAName, false);
            DrawStrings(g, X, Y, 340, "Марка материала", "Material", Material, false);
            DrawStrings(g, X, Y, 380, "Цвет преформы", "Preform colour", PColor, true);
            DrawStrings(g, X, Y, 420, "Количество преформ в коробе", "Preform quantity per box", Count, false);
            DrawStrings(g, X, Y, 460, "Дата изготовления", "Date of manufacturnig", Date, false);
            DrawStrings(g, X, Y, 500, "Время", "Time", Time, false);
            DrawStrings(g, X, Y, 540, "Номер партии", "Batch number", PartNum, false);
            DrawStrings(g, X, Y, 580, "Номер короба", "Box number", Num.ToString(), true);
            DrawStrings(g, X, Y, 620, "Смена", "Shift", Shift, false);
            DrawStrings(g, X, Y, 660, "Укладчик", "Packer", Packer, false);
            //Нижний колонтитул
            g.DrawString("Сделано в России / Made in Russia",
                F14Bold, Brushes.Black, X + 130, Y + Height - 55);
            g.DrawString("Гарантированный срок хранения - " + Limit + " со дня изготовления",
                F14Bold, Brushes.Black, X + 30, Y + Height - 35);
            g.DrawString("Перед выдувом бутылок рекомендуется выдержать преформы не менее 24 часов при t + 18°С",
                F11, Brushes.Black, X + 10, Y + Height - 15);
            //Штрихкод, платить отказались, убираем... :-( может потом вернём
            /*string code = Barcode.Code(this);
            if (code != "")
            {
                try
                {
                    Image barcode = Image.FromFile("Barcodes\\" + code + ".gif");
                    g.DrawImage(barcode, X + 10, Y + 10, 170, 50);
                }
                catch { }
            }*/

            //Если надо, инкрементим номер и пишем журнал
            if (IncNum) IncAndLog();
        }
        static void DrawStrings(Graphics g, int X, int Y, int y, string s1, string s2, string s3, bool BigLabel)
        {
            //Имя поля на русском и английском
            g.DrawString(s1, F14Bold, Brushes.Black, X + 10, Y + y);
            g.DrawString(s2, F14, Brushes.Black, X + 10, Y + y + 14);
            //Значение поля
            if (BigLabel)
            {
                bool Find = false;
                if (g.MeasureString(s3, F37Bold).Width < 280)
                {
                    g.DrawString(s3, F37Bold, Brushes.Black, X + 226, Y + y - 5);
                    g.DrawString(s3, F37Bold, Brushes.Black, X + 228, Y + y - 5);
                    Find = true;
                }
                if (g.MeasureString(s3, F30Bold).Width < 280 & !Find)
                {
                    g.DrawString(s3, F30Bold, Brushes.Black, X + 230, Y + y - 5);
                    g.DrawString(s3, F30Bold, Brushes.Black, X + 232, Y + y - 5);
                    Find = true;
                }
                if (g.MeasureString(s3, F22Bold).Width < 280 & !Find)
                {
                    g.DrawString(s3, F22Bold, Brushes.Black, X + 231, Y + y - 5);
                    g.DrawString(s3, F22Bold, Brushes.Black, X + 232, Y + y - 5);
                }
            }
            else
                g.DrawString(s3, F30Bold, Brushes.Black, X + 230, Y + y);
        }

        /// <summary>
        /// Формирование этикетки колпачка
        /// </summary>
        /// <param name="g"></param>
        /// <param name="X">Положение на листе по X</param>
        /// <param name="Y">Положение на листе по Y</param>
        /// <param name="IncNum">Увеличить ли номер после этой этикетки и записать в журнал</param>
        void DrawLabelC(Graphics g, int X, int Y)
        {
            InRect.Alignment = StringAlignment.Center;
            int Width = 550;
            int Height = 380;
            //Рамки
            g.DrawRectangle(Slim, X, Y, Width, Height);
            g.DrawRectangle(Slim, X + 10, Y + 160, Width - 20, 180);
            g.DrawLine(Slim, X + 10, Y + 215, X + Width - 10, Y + 215);
            g.DrawLine(Slim, X + 10, Y + 270, X + Width - 10, Y + 270);
            g.DrawLine(Slim, X + 143, Y + 160, X + 143, Y + 270);
            g.DrawLine(Slim, X + 275, Y + 160, X + 275, Y + 340);
            g.DrawLine(Slim, X + 412, Y + 160, X + 412, Y + 340);
            //Шапка
            g.DrawImage(logo, X + 110, Y + 3, 327, 45); //654*90
            g.DrawString(@"ООО «Красноярский завод полимерной упаковки «Европласт»,",
                F12, Brushes.Black, new Rectangle(X, Y + 50, Width, 20), InRect);
            g.DrawString("РФ 662500, Красноярский край, г. Сосновоборск, ул. Заводская, 1",
                F12, Brushes.Black, new Rectangle(X, Y + 62, Width, 20), InRect);
            g.DrawString("тел./факс: (391)218-02-01",
                F12, Brushes.Black, new Rectangle(X, Y + 74, Width, 20), InRect);
            g.DrawString("КОЛПАЧОК ВИНТОВОЙ Ø 28",
                F22, Brushes.Black, new Rectangle(X, Y + 90, Width, 25), InRect);
            g.DrawString("ГОСТ 32626-2014",
                F22, Brushes.Black, new Rectangle(X, Y + 120, Width, 25), InRect);
            //Поля
            g.DrawString("Масса, гр.", F14, Brushes.Black, new Rectangle(X + 10, Y + 160, 140, 20), InRect);
            g.DrawString(Weight, F22Bold, Brushes.Black, new Rectangle(X + 10, Y + 180, 140, 30), InRect);
            g.DrawString("Количество", F14, Brushes.Black, new Rectangle(X + 143, Y + 160, 140, 20), InRect);
            g.DrawString(Count, F22Bold, Brushes.Black, new Rectangle(X + 143, Y + 180, 140, 30), InRect);
            g.DrawString("Дата", F14, Brushes.Black, new Rectangle(X + 275, Y + 160, 137, 20), InRect);
            g.DrawString(Date, F22Bold, Brushes.Black, new Rectangle(X + 275, Y + 180, 137, 30), InRect);
            g.DrawString("Смена", F14, Brushes.Black, new Rectangle(X + 412, Y + 160, 135, 20), InRect);
            g.DrawString(Shift, F22Bold, Brushes.Black, new Rectangle(X + 412, Y + 180, 135, 30), InRect);
            g.DrawString("Цвет", F14, Brushes.Black, new Rectangle(X + 10, Y + 215, 140, 20), InRect);
            g.DrawString(PColor, F20Bold, Brushes.Black, new Rectangle(X + 0, Y + 235, 155, 30), InRect); //140
            g.DrawString("Короб №", F14, Brushes.Black, new Rectangle(X + 143, Y + 215, 140, 20), InRect);
            g.DrawString(Num.ToString(), F22Bold, Brushes.Black, new Rectangle(X + 143, Y + 235, 140, 30), InRect);
            g.DrawString("Логотип", F14, Brushes.Black, new Rectangle(X + 275, Y + 215, 137, 20), InRect);
            g.DrawString("№ Линии", F14, Brushes.Black, new Rectangle(X + 412, Y + 215, 135, 20), InRect);
            g.DrawString(TPAName, F22Bold, Brushes.Black, new Rectangle(X + 412, Y + 235, 135, 30), InRect);
            g.DrawString("Код", F14, Brushes.Black, new Rectangle(X + 10, Y + 270, 275, 20), InRect);
            string dobavka = ""; if (Antistatic != "") dobavka += "." + Antistatic;
            g.DrawString(Type + "." + Material + "." + Colorant + dobavka,
                F22Bold, Brushes.Black, new Rectangle(X + 10, Y + 290, 275, 30), InRect);
            g.DrawString(Other, F14, Brushes.Black, new Rectangle(X + 10, Y + 310, 275, 30), InRect);
            g.DrawString("Партия", F14, Brushes.Black, new Rectangle(X + 412, Y + 270, 135, 20), InRect);
            g.DrawString(PartNum, F22Bold, Brushes.Black, new Rectangle(X + 412, Y + 290, 135, 30), InRect);
            //Графика
            g.DrawImage(HDPE, X + 285, Y + 285, 40, 40);
            g.DrawImage(Eda, X + 325, Y + 285, 40, 40);
            g.DrawImage(EAC, X + 365, Y + 285, 40, 40);
            //Нижний колонтитул
            g.DrawString("Гарантированный срок годности - 12 месяцев со дня изготовления, при температуре от 5 до 25°С",
                F11Italic, Brushes.Black, X + 7, Y + Height - 35);
            g.DrawString("и относительной влажности воздуха 40% - 80%.",
                F11Italic, Brushes.Black, X + 7, Y + Height - 20);
            //Инкрементим номер и пишем журнал
            IncAndLog();
        }

        /// <summary>
        /// Формирование этикетки ротопринта
        /// </summary>
        /// <param name="g"></param>
        /// <param name="X">Положение на листе по X</param>
        /// <param name="Y">Положение на листе по Y</param>
        /// <param name="IncNum">Увеличить ли номер после этой этикетки и записать в журнал</param>
        void DrawLabelR(Graphics g, int X, int Y, bool IncNum)
        {
            int Width = 380;
            int Height = 184;
            //Рамки
            g.DrawRectangle(Slim, X, Y, Width, Height);

            g.DrawString("Логотип:", F22, Brushes.Black, new Point(X + 10, Y + 15));
            g.DrawString(Weight, F22Bold, Brushes.Black, new Rectangle(X + 170, Y + 15, 195, 60));
            g.DrawRectangle(Slim, X + 165, Y + 15, 200, 55);

            g.DrawString("Код цвета:", F22, Brushes.Black, new Point(X + 10, Y + 75));
            g.DrawString(PColor, F22, Brushes.Black, new Point(X + 200, Y + 75));

            g.DrawString("Дата нанесения:", F22, Brushes.Black, new Point(X + 10, Y + 100));
            g.DrawString(Date, F22, Brushes.Black, new Point(X + 200, Y + 100));

            g.DrawString("Оператор:", F22, Brushes.Black, new Point(X + 10, Y + 125));
            g.DrawString(Packer, F22, Brushes.Black, new Point(X + 200, Y + 125));

            g.DrawString("Смена:", F22, Brushes.Black, new Point(X + 10, Y + 150));
            g.DrawString(Shift[Shift.Length-1].ToString(), F22, Brushes.Black, new Point(X + 200, Y + 150));

            g.DrawString(Num.ToString(), F14, Brushes.Black, new Point(X + 350, Y + 160));
            
            //Инкрементим номер и пишем журнал
            IncAndLog();
        }

        /// <summary>
        /// Увеличение номера короба и запись в журнал (если надо)
        /// </summary>
        void IncAndLog()
        {
            Log(); //Теперь пишем журнал в любом случае
            //Увеличиваем номер, если печатался текущий
            if (Num >= CurrentNum & CurrentNum > 0) CurrentNum = Num + 1; 
            Num++;
            LabelCount--;
        }

        /// <summary>
        /// Сохранение на сервер
        /// </summary>
        public void Save()
        {
            //Что бы кнопка появилась и печать работала
            if (TPAType == 2)
            {
                PartNum = "Ротопринт";
                Count = "Ротопринт";
            }

            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("TPAWrite");
                        writer.Write(TPAName);
                        writer.Write(CurrentNum.ToString());
                        writer.Write(PartNum);
                        writer.Write(Type);
                        writer.Write(Weight);
                        writer.Write(Count);
                        writer.Write(Material);
                        writer.Write(PColor);
                        writer.Write(Antistatic);
                        writer.Write(Colorant);
                        writer.Write(Limit);
                        writer.Write(Other);
                        for (int i = 0; i < 8; i++)
                            writer.Write("----------------------------------------");
                    }
                }
            }
            catch
            {
                AutoLabel.Log.Error("Не удалось подключиться к серверу. Параметры ТПА не сохранены.");
            }
        }

        /// <summary>
        /// Загрузка с сервера
        /// </summary>
        public void Load()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("TPARead");
                        writer.Write(TPAName);
                        try { CurrentNum = Convert.ToInt32(reader.ReadString()); }
                        catch { CurrentNum = 0; }
                        PartNum = reader.ReadString();
                        Type = reader.ReadString();
                        Weight = reader.ReadString();
                        Count = reader.ReadString();
                        Material = reader.ReadString();
                        PColor = reader.ReadString();
                        Antistatic = reader.ReadString();
                        Colorant = reader.ReadString();
                        Limit = reader.ReadString();
                        Other = reader.ReadString();
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
        /// Запись в журнал отчёта о напечатанной этикетке
        /// </summary>
        void Log()
        {
            string comment = "";
            //if (Custom) comment = " - этикетка с произвольными полями";
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write("LogRecord");
                        writer.Write(AutoLabel.Shift.LogName[0]);
                        writer.Write(AutoLabel.Shift.Date + DateTime.Now.ToString("; HH:mm") +
                        "; " + TPAName + "; " + PartNum + "; " + Type + "; " + Weight + "; " +
                        PColor + "; " + Num + "; " + Packer + comment);
                    }
                }
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

        /// <summary>
        /// Разрешить ли печатать несколько этикеток?
        /// </summary>
        /// <returns></returns>
        public bool AllowSelectCount()
        {
            //Разшешаем если тип "колпак" или "ротопринт"
            if (TPAType != 0) return true;
            //И разрешаем если количество меньше чем заданное
            try { return (Convert.ToInt32(Count) <= Data.PreformsInLittleBox); }
            catch { return false; }
        }
    }
}
