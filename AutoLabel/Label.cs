using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    public class Label
    {
        public string TPAName;  //Имя ТПА ___
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
        bool isCustom;          //Кастомная этикетка

        //Карандаши и ручки :-)
        static Pen ClipLine = new Pen(Color.Black, 0.5f);
        static Pen Slim = new Pen(Color.Black, 1);
        static Pen Bold = new Pen(Color.Black, 3);
        static Font F09 = new Font("Arial", 9, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F11 = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F11Italic = new Font("Arial", 11, FontStyle.Italic, GraphicsUnit.Pixel);
        static Font F12 = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F14 = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F14Bold = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F22 = new Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font F22Bold = new Font("Arial", 22, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F26Bold = new Font("Arial", 26, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F30Bold = new Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F37Bold = new Font("Arial", 37, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font F70Bold = new Font("Arial", 70, FontStyle.Bold, GraphicsUnit.Pixel);
        static StringFormat InRect = new StringFormat();

        //Графика
        static Image logo = Image.FromFile("Graphics\\Logo.png");
        //static Image rst = Image.FromFile("Graphics\\RST.png");
        static Image HDPE = Image.FromFile("Graphics\\HDPE.png");
        static Image Eda = Image.FromFile("Graphics\\Eda.png");
        static Image EAC = Image.FromFile("Graphics\\EAC.png");
        static Image Pet = Image.FromFile("Graphics\\Pet.png");

        //Текущие данные для печати
        static int Num;
        static string Packer;
        static int LabelCount;
        static string Date;
        static string Time;
        static string Shift;
        static string kN, kP, dN, dP; //Добавочные поля для производственного задания
        
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
        
        #region Управление печатью этикеток (выбор типа, формирование листов)
        /// <summary>
        /// Печать этикетки
        /// </summary>
        /// <param name="num">Номер ящика</param>
        /// <param name="packer">Фамилия упаковщика</param>
        /// <param name="count">Количество этикеток (0 - если одна двойная)</param>
        public void Print(int num, string packer, int count)
        {
            Print(num, packer, count, AutoLabel.Shift.Date, DateTime.Now.ToString("HH:mm"), AutoLabel.Shift.Current, false);
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
        public void Print(int num, string packer, int count, string date, string time, string shift, bool isCustom)
        {
            Num = num;
            Packer = packer;
            LabelCount = count;
            Date = date;
            Time = time;
            Shift = shift;
            this.isCustom = isCustom;
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
                int y = 0;
                DrawLabelR(e.Graphics, 6, y, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, y, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 6, y + 189, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, y + 189, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 6, y + 378, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, y + 378, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 6, y + 567, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, y + 567, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 6, y + 756, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, y + 756, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 6, y + 945, true);
                if (LabelCount > 0) DrawLabelR(e.Graphics, 400, y + 945, true);
            }
            e.HasMorePages = LabelCount > 0;
        }
        /// <summary>
        /// Увеличение номера короба и запись в журнал (если надо)
        /// </summary>
        void IncAndLog()
        {
            if (!isCustom) Log();
            //Увеличиваем номер, если печатался текущий
            if (Num >= CurrentNum & CurrentNum > 0) CurrentNum = Num + 1;
            Num++;
            LabelCount--;
        }
        #endregion

        #region Этикетка Преформы
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
            g.DrawImage(logo, X + 190, Y + 3, 327, 45);
            //g.DrawImage(rst, X + 388, Y + 100, 45, 45);
            g.DrawImage(Pet, X + 433, Y + 100, 45, 45);
            g.DrawImage(Eda, X + 478, Y + 100, 45, 45);
            g.DrawString("КРАСНОЯРСКИЙ ЗАВОД", F14Bold, Brushes.Black, X + 330, Y + 50);
            g.DrawString("Общество с ограниченной ответственностью «Европласт - ЕнисейПром»",
                F11Italic, Brushes.Black, X + 10, Y + 70); //20*65
            g.DrawString("662500, Красноярский край, г. Сосновоборск, ул. Заводская, д. 1, стр. 41,",
                F11Italic, Brushes.Black, X + 10, Y + 90); //95
            g.DrawString("тел (3912) 180201, e-mail: eniseyprom@europlast.biz",
                F11Italic, Brushes.Black, X + 10, Y + 102); //107
            g.DrawString("ПРЕФОРМА ДЛЯ ИЗГОТОВЛЕНИЯ БУТЫЛОК",
                            F14Bold, Brushes.Black, X + 10, Y + 120);
            g.DrawString("ИЗ ПОЛИЭТИЛЕНТЕРЕФТАЛАТА",
                            F14Bold, Brushes.Black, X + 10, Y + 135);
            g.DrawString("Технические условия / Specification - ТУ - 22.22.14 - 001 - 19334399 - 2018",
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
            g.DrawString("Перед выдувом бутылок рекомендуется выдержать преформы не менее 24 часов при температуре (20 ± 5)°С",
                F09, Brushes.Black, X + 30, Y + Height - 15);
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
                if (g.MeasureString(s3, F37Bold).Width < 280)
                {
                    g.DrawString(s3, F37Bold, Brushes.Black, X + 226, Y + y - 5);
                    g.DrawString(s3, F37Bold, Brushes.Black, X + 228, Y + y - 5);
                }
                else if (g.MeasureString(s3, F30Bold).Width < 280)
                {
                    g.DrawString(s3, F30Bold, Brushes.Black, X + 230, Y + y - 5);
                    g.DrawString(s3, F30Bold, Brushes.Black, X + 232, Y + y - 5);
                }
                else if (g.MeasureString(s3, F22Bold).Width < 280)
                {
                    g.DrawString(s3, F22Bold, Brushes.Black, X + 231, Y + y - 5);
                    g.DrawString(s3, F22Bold, Brushes.Black, X + 232, Y + y - 5);
                }
                else
                {
                    g.DrawString(s3, F22, Brushes.Black, X + 231, Y + y - 5);
                    g.DrawString(s3, F22, Brushes.Black, X + 232, Y + y - 5);
                }
            }
            else
                g.DrawString(s3, F30Bold, Brushes.Black, X + 230, Y + y);
        }
        #endregion

        #region Этикетка Колпачка
        /// <summary>
        /// Формирование этикетки колпачка
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x">Положение на листе по X</param>
        /// <param name="y">Положение на листе по Y</param>
        /// <param name="IncNum">Увеличить ли номер после этой этикетки и записать в журнал</param>
        void DrawLabelC(Graphics g, int x, int y)
        {
            InRect.Alignment = StringAlignment.Center;
            int width = 550;
            int height = 380;

            //Рамки
            if (TPAName == "C2")
                g.FillRectangle(Brushes.LightGray, x + 10, y + 270, 402, 70);
            //g.FillRectangle(Brushes.LightGray, x, y, width, height); 
            g.DrawRectangle(Slim, x, y, width, height);
            g.DrawRectangle(Slim, x + 10, y + 160, width - 20, 180);
            g.DrawLine(Slim, x + 10, y + 215, x + width - 10, y + 215);
            g.DrawLine(Slim, x + 10, y + 270, x + width - 10, y + 270);
            g.DrawLine(Slim, x + 143, y + 270, x + 143, y + 160);
            g.DrawLine(Slim, x + 275, y + 270, x + 275, y + 160);
            g.DrawLine(Slim, x + 412, y + 160, x + 412, y + 340);

            //Шапка
            g.DrawImage(logo, x + 110, y + 3, 327, 45); //654*90
            g.DrawString(@"ООО «Европласт - ЕнисейПром»,",
                F12, Brushes.Black, new Rectangle(x, y + 50, width, 20), InRect);
            g.DrawString("РФ 662500, Красноярский край, г. Сосновоборск, ул. Заводская, д. 1, стр. 41",
                F12, Brushes.Black, new Rectangle(x, y + 62, width, 20), InRect);
            g.DrawString("тел./факс: (391)218-02-01",
                F12, Brushes.Black, new Rectangle(x, y + 74, width, 20), InRect);
            g.DrawString("КОЛПАЧОК ВИНТОВОЙ Ø 28",
                F22, Brushes.Black, new Rectangle(x, y + 90, width, 25), InRect);
            g.DrawString("ГОСТ 32626-2014",
                F22, Brushes.Black, new Rectangle(x, y + 120, width, 25), InRect);

            //Поля
            g.DrawString("Масса, гр.", F14, Brushes.Black, new Rectangle(x + 10, y + 160, 140, 20), InRect);
            g.DrawString(Weight, F26Bold, Brushes.Black, new Rectangle(x + 10, y + 180, 140, 30), InRect);
            g.DrawString("Количество, шт", F14, Brushes.Black, new Rectangle(x + 143, y + 160, 140, 20), InRect);
            g.DrawString(Count, F26Bold, Brushes.Black, new Rectangle(x + 143, y + 180, 140, 30), InRect);
            g.DrawString("Дата", F14, Brushes.Black, new Rectangle(x + 275, y + 160, 137, 20), InRect);
            g.DrawString(Date, F26Bold, Brushes.Black, new Rectangle(x + 265, y + 180, 157, 30), InRect);
            g.DrawString("Смена №", F14, Brushes.Black, new Rectangle(x + 412, y + 160, 135, 20), InRect);
            g.DrawString(Shift, F26Bold, Brushes.Black, new Rectangle(x + 412, y + 180, 135, 30), InRect);
            g.DrawString("Цвет", F14, Brushes.Black, new Rectangle(x + 10, y + 215, 140, 20), InRect);
            g.DrawString(PColor, F22Bold, Brushes.Black, new Rectangle(x + 0, y + 235, 155, 30), InRect);
            g.DrawString("Короб №", F14, Brushes.Black, new Rectangle(x + 143, y + 215, 140, 20), InRect);
            g.DrawString(Num.ToString(), F26Bold, Brushes.Black, new Rectangle(x + 143, y + 235, 140, 30), InRect);
            g.DrawString("№ Линии", F14, Brushes.Black, new Rectangle(x + 275, y + 215, 137, 20), InRect);
            g.DrawString(TPAName, F26Bold, Brushes.Black, new Rectangle(x + 275, y + 235, 137, 30), InRect);
            g.DrawString("Партия", F14, Brushes.Black, new Rectangle(x + 412, y + 215, 135, 20), InRect);
            g.DrawString(PartNum, F26Bold, Brushes.Black, new Rectangle(x + 412, y + 235, 135, 30), InRect);
            g.DrawString("Код", F14, Brushes.Black, new Rectangle(x + 10, y + 272, 400, 20), InRect);

            string code = Type + "." + Material + "." + Colorant + (Antistatic != "" ? "." + Antistatic : "");
            string codeSub = code.Substring(14, 1);
            code = code.Substring(0, 14) + "   " + code.Substring(15);
            if (g.MeasureString(code, F30Bold).Width < 410)
            {
                g.DrawString(code, F30Bold, Brushes.Black, new Rectangle(x + 5, y + 290, 410, 30), InRect);
                g.DrawString(codeSub, F37Bold, Brushes.Black, x + 210 - g.MeasureString(code, F30Bold).Width / 2 + 220, y + 284);
            }
            else
            {
                g.DrawString(code, F26Bold, Brushes.Black, new Rectangle(x + 5, y + 290, 410, 30), InRect);
                g.DrawString(codeSub, F37Bold, Brushes.Black, x + 210 - g.MeasureString(code, F26Bold).Width / 2 + 190, y + 280);
            }

            g.DrawString(Other, F14, Brushes.Black, new Rectangle(x + 10, y + 320, 400, 30), InRect);

            //Графика
            g.DrawImage(HDPE, x + 415, y + 285, 40, 40);
            g.DrawImage(Eda, x + 455, y + 285, 40, 40);
            g.DrawImage(EAC, x + 495, y + 285, 40, 40);

            //Нижний колонтитул
            g.DrawString("Гарантированный срок годности - 12 месяцев со дня изготовления, при температуре от 5 до 25°С",
                F11Italic, Brushes.Black, x + 7, y + height - 35);
            g.DrawString("и относительной влажности воздуха 40% - 80%.",
                F11Italic, Brushes.Black, x + 7, y + height - 20);

            //Инкрементим номер и пишем журнал
            IncAndLog();
        }
        #endregion

        #region Этикетка Ротопринта
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
            g.DrawString(Colorant, F22, Brushes.Black, new Point(X + 200, Y + 75));

            g.DrawString("Дата нанесения:", F22, Brushes.Black, new Point(X + 10, Y + 100));
            g.DrawString(Date, F22, Brushes.Black, new Point(X + 200, Y + 100));

            g.DrawString("Оператор:", F22, Brushes.Black, new Point(X + 10, Y + 125));
            g.DrawString(Packer, F22, Brushes.Black, new Point(X + 200, Y + 125));

            g.DrawString("Смена:", F22, Brushes.Black, new Point(X + 10, Y + 150));
            g.DrawString(Shift[Shift.Length - 1].ToString(), F22, Brushes.Black, new Point(X + 200, Y + 150));

            g.DrawString(Num.ToString(), F14, Brushes.Black, new Point(X + 350, Y + 160));

            //Инкрементим номер и пишем журнал
            IncAndLog();
        }
        #endregion

        #region Производственное задание
        /// <summary>
        /// Печать производственного задания
        /// </summary>
        public void PrintProductionTask(string kName, string kPer, string dName, string dPer)
        {
            kN = kName;
            kP = kPer;
            dN = dName;
            dP = dPer;
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += new PrintPageEventHandler(PD_PrintProductionTask);
                doc.PrinterSettings = Data.printersettings;
                doc.PrinterSettings.DefaultPageSettings.Landscape = false;
                doc.Print();
            }
            catch (ArgumentException e)
            {
                AutoLabel.Log.Error(e.Message);
            }
        }

        /// <summary>
        /// Формирование листа производственного задания
        /// </summary>
        private void PD_PrintProductionTask(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawProductionTask(g, 40, 40);
            DrawProductionTask(g, 40, 620);

            ClipLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(ClipLine, 0, 580, 900, 580);
        }
        /// <summary>
        /// Рисование производственного задания
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void DrawProductionTask(Graphics g, int Left, int Top)
        {
            int TopK = Top + 2; //Скорректированная высота для надписей
            int TopKb = Top + 10; //Скорректированная высота для надписей
            InRect.Alignment = StringAlignment.Center;
            g.DrawRectangle(Bold, Left, Top, 750, 500);
            g.DrawRectangle(Bold, Left, Top, 750, 125);
            g.DrawRectangle(Slim, Left, Top, 750, 60);
            g.DrawRectangle(Slim, Left, Top, 375, 60);

            g.DrawRectangle(Slim, Left, Top + 25, 375, 35);
            g.DrawString("Производственное задание", F22Bold, Brushes.Black, new Rectangle(Left, Top + 25 + 4, 375, 35), InRect);
            g.DrawString(TPAName, F37Bold, Brushes.Black, new Rectangle(Left + 375, Top + 10, 375, 60), InRect);

            g.DrawRectangle(Slim, Left + 150, Top + 85, 225, 20);
            g.DrawString("Начало", F14, Brushes.Black, Left + 5, Top + 85);
            g.DrawString("производства", F14, Brushes.Black, Left + 5, Top + 105);
            g.DrawString(DateTime.Now.ToString("dd.MM.yyyy"), F14, Brushes.Black, Left + 150, TopK + 85);

            g.DrawRectangle(Slim, Left + 525, Top + 85, 225, 20);
            g.DrawString("Окончание", F14, Brushes.Black, Left + 380, Top + 85);
            g.DrawString("производства", F14, Brushes.Black, Left + 380, Top + 105);

            g.DrawString(Weight + " " + Type, F70Bold, Brushes.Black, new Rectangle(Left, Top + 125 + 15, 750, 105 - 15), InRect);

            g.DrawRectangle(Slim, Left, Top + 230, 150, 20);
            g.DrawString("Сырьё", F14, Brushes.Black, new Rectangle(Left, TopK + 230, 150, 20), InRect);
            g.DrawRectangle(Slim, Left, Top + 250, 150, 50);
            g.DrawString(Material, F22, Brushes.Black, new Rectangle(Left, TopKb + 250, 150, 50), InRect);

            g.DrawRectangle(Slim, Left + 150, Top + 230, 150, 20);
            g.DrawString("Цвет", F14, Brushes.Black, new Rectangle(Left + 150, TopK + 230, 150, 20), InRect);
            g.DrawRectangle(Slim, Left + 150, Top + 250, 150, 50);
            g.DrawString(PColor, F22, Brushes.Black, new Rectangle(Left + 150, TopKb + 250, 150, 50), InRect);

            g.DrawRectangle(Slim, Left + 300, Top + 230, 150, 20);
            g.DrawString("Код цвета", F14, Brushes.Black, new Rectangle(Left + 300, TopK + 230, 150, 20), InRect);
            g.DrawRectangle(Slim, Left + 300, Top + 250, 150, 50);
            g.DrawString(Colorant, F22, Brushes.Black, new Rectangle(Left + 300, TopKb + 250, 150, 50), InRect);

            g.DrawRectangle(Slim, Left + 450, Top + 230, 150, 20);
            g.DrawString("Кол-во в коробе", F14, Brushes.Black, new Rectangle(Left + 450, TopK + 230, 150, 20), InRect);
            g.DrawRectangle(Slim, Left + 450, Top + 250, 150, 50);
            g.DrawString(Count, F22, Brushes.Black, new Rectangle(Left + 450, TopKb + 250, 150, 50), InRect);

            g.DrawRectangle(Slim, Left + 600, Top + 230, 150, 20);
            g.DrawString("Коробов", F14, Brushes.Black, new Rectangle(Left + 600, TopK + 230, 150, 20), InRect);
            g.DrawRectangle(Slim, Left + 600, Top + 250, 150, 50);


            g.DrawRectangle(Slim, Left, Top + 300, 375, 20);
            g.DrawString("КРАСИТЕЛЬ", F14, Brushes.Black, new Rectangle(Left, TopK + 300, 375, 20), InRect);

            g.DrawRectangle(Slim, Left + 375, Top + 300, 375, 20);
            g.DrawString("ДОБАВКА", F14, Brushes.Black, new Rectangle(Left + 375, TopK + 300, 375, 20), InRect);

            g.DrawRectangle(Slim, Left, Top + 320, 225, 20);
            g.DrawString("Название", F14, Brushes.Black, new Rectangle(Left, TopK + 320, 225, 20), InRect);
            g.DrawRectangle(Slim, Left, Top + 340, 225, 50);
            g.DrawString(kN, F22, Brushes.Black, new Rectangle(Left, TopKb + 340, 225, 50), InRect);

            g.DrawRectangle(Slim, Left + 225, Top + 320, 150, 20);
            g.DrawString("Процент ввода", F14, Brushes.Black, new Rectangle(Left + 225, TopK + 320, 150, 20), InRect);
            g.DrawRectangle(Slim, Left + 225, Top + 340, 150, 50);
            g.DrawString(kP + "%", F22, Brushes.Black, new Rectangle(Left + 225, TopKb + 340, 150, 50), InRect);

            g.DrawRectangle(Slim, Left + 375, Top + 320, 225, 20);
            g.DrawString("Название", F14, Brushes.Black, new Rectangle(Left + 375, TopK + 320, 225, 20), InRect);
            g.DrawRectangle(Slim, Left + 375, Top + 340, 225, 50);
            g.DrawString(dN, F22, Brushes.Black, new Rectangle(Left + 375, TopKb + 340, 225, 50), InRect);

            g.DrawRectangle(Slim, Left + 600, Top + 320, 150, 20);
            g.DrawString("Процент ввода", F14, Brushes.Black, new Rectangle(Left + 600, TopK + 320, 150, 20), InRect);
            g.DrawRectangle(Slim, Left + 600, Top + 340, 150, 50);
            g.DrawString(dP + "%", F22, Brushes.Black, new Rectangle(Left + 600, TopKb + 340, 155, 50), InRect);

            g.DrawRectangle(Slim, Left, Top + 390, 375, 40);
            g.DrawString("Партия: " + PartNum, F22, Brushes.Black, new Rectangle(Left, TopKb + 390, 375, 40), InRect);
            g.DrawRectangle(Slim, Left + 375, Top + 390, 375, 40);
            g.DrawString(Antistatic, F22, Brushes.Black, new Rectangle(Left + 375, TopKb + 390, 375, 40), InRect);


            g.DrawString("Запуск/переход произвёл_________________", F14, Brushes.Black, Left + 5, Top + 440);
            g.DrawString("_подпись_________________________Ф.И.О.            Смена____",
                F14, Brushes.Black, Left + 300, Top + 440);

            g.DrawString("Соответствие чек-листу__________________", F14, Brushes.Black, Left + 5, Top + 460);
            g.DrawString("_подпись_________________________Ф.И.О.            Смена____",
                F14, Brushes.Black, Left + 300, Top + 460);

            g.DrawString("Допущенно по качеству___________________", F14, Brushes.Black, Left + 5, Top + 480);
            g.DrawString("_подпись_________________________Ф.И.О.            Смена____",
                F14, Brushes.Black, Left + 300, Top + 480);
        }

        #endregion

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
