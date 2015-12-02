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
        public string Count;    //Список количество
        public string Antistatic;   //Список количество антистатика
        public string Material;   //Список сырьё
        public string Limit;    //Список срок хранения

        public Label(int i) { TPA = (i + 1).ToString(); }

        public void Print(int num)
        {
            if (!Data.PrintSelected())
            {
                MessageBox.Show("Принтер не выбран, выберите принтер");
                Data.PrintSetup();
            }

            PrintDocument doc = new PrintDocument();

            doc.PrintPage += new PrintPageEventHandler(PD_PrintPage);
            doc.PrinterSettings = Data.printersettings;
            doc.Print();


            /*MessageBox.Show("ТПА:\t\t" + (TPA).ToString() +
                "\nУпаковщик:\tИванов И. И." +
                "\nНомер короба:\t" + num +
                "\nДата и время:\t" + DateTime.Now.ToString() +
                "\nТип:\t\t" + Type +
                "\nВес:\t\t" + Weight +
                "\nКоличество:\t" + Count +
                "\nНомер партии:\t" + PartNum);*/
            if (num == CurrentNum & CurrentNum > 0) CurrentNum++; //Увеличиваем номер, если печатался текущий

        }

        //Собственно печать этикетки
        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Times New Roman", 3, FontStyle.Regular, GraphicsUnit.Millimeter);
            e.Graphics.DrawString("Проверка", font, Brushes.Black, new Point(10, 10));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, 827, 1169));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(2, 2, 823, 1165));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(4, 4, 819, 1161));


        }
    }
}
