using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            MessageBox.Show("ТПА:\t\t" + (TPA).ToString() +
                "\nУпаковщик:\tИванов И. И." +
                "\nНомер короба:\t" + num +
                "\nДата и время:\t" + DateTime.Now.ToString() +
                "\nТип:\t\t" + Type +
                "\nВес:\t\t" + Weight +
                "\nКоличество:\t" + Count+
                "\nНомер партии:\t" + PartNum);
            if (num == CurrentNum & CurrentNum > 0) CurrentNum++; //Увеличиваем номер, если печатался текущий

        }
    }
}
