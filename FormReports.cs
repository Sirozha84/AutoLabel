using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutoLabel
{
    public partial class FormReports : Form
    {
        int line; //Счётчик строк, чтоб если что перенестись на следующую страницу

        List<string[]> log = new List<string[]>();

        //Шрифты
        Font Normal = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        Font Bold = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);

        public FormReports()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            //Заполнение списка последних смен в комбо-бокс
            foreach (string sh in Data.LogName)
                comboBoxShift.Items.Add(sh);
            comboBoxShift.SelectedIndex = 0;
        }

        /// <summary>
        /// Загрузка и парсинг журнала
        /// </summary>
        void LoadLog()
        {
            try
            {
                log.Clear();
                foreach (string str in File.ReadLines(comboBoxShift.SelectedItem.ToString() + ".csv", Encoding.Default))
                {
                    string[] Str = str.Split(';');
                    for (int i = 0; i < Str.Count(); i++)
                        if (Str[i][0] == ' ') Str[i] = Str[i].Trim(' ');
                    log.Add(Str);
                }
                log.RemoveAt(0);
            }
            catch { }
        }

        /// <summary>
        /// Журнал выпущенных этикетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLog_Click(object sender, EventArgs e)
        {
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = false;
            doc.PrintPage += Doc_PrintPage;
            line = 0;
            doc.Print();
            Close();
        }



        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Журнал     " + comboBoxShift.SelectedItem, Bold, Brushes.Black, new Point(40, 40));
            e.Graphics.DrawString("Страница " + page + " из " + pages, Normal, Brushes.Black, new Point(650, 40));
            //Шапка таблицы
            e.Graphics.DrawString("Номер", Bold, Brushes.Black, new Point(40, 80));
            e.Graphics.DrawString("Дата", Bold, Brushes.Black, new Point(100, 80));
            e.Graphics.DrawString("Время", Bold, Brushes.Black, new Point(160, 80));
            e.Graphics.DrawString("Машина", Bold, Brushes.Black, new Point(220, 80));
            e.Graphics.DrawString("Партия", Bold, Brushes.Black, new Point(280, 80));
            e.Graphics.DrawString("Тип", Bold, Brushes.Black, new Point(340, 80));
            e.Graphics.DrawString("Вес", Bold, Brushes.Black, new Point(420, 80));
            e.Graphics.DrawString("Цвет", Bold, Brushes.Black, new Point(460, 80));
            e.Graphics.DrawString("Короб", Bold, Brushes.Black, new Point(550, 80));
            e.Graphics.DrawString("Упаковщик", Bold, Brushes.Black, new Point(600, 80));

            int y = 110;
            for (int i = 0; i < strings; i++ )
            {
                if (line < log.Count)
                {
                    e.Graphics.DrawString((line + 1).ToString(), Normal, Brushes.Black, new Point(40, y + i * height));
                    e.Graphics.DrawString(log[line][0], Normal, Brushes.Black, new Point(100, y + i * height));
                    e.Graphics.DrawString(log[line][1], Normal, Brushes.Black, new Point(160, y + i * height));
                    e.Graphics.DrawString(log[line][2], Normal, Brushes.Black, new Point(220, y + i * height));
                    e.Graphics.DrawString(log[line][3], Normal, Brushes.Black, new Point(280, y + i * height));
                    e.Graphics.DrawString(log[line][4], Normal, Brushes.Black, new Point(340, y + i * height));
                    e.Graphics.DrawString(log[line][5], Normal, Brushes.Black, new Point(420, y + i * height));
                    e.Graphics.DrawString(log[line][6], Normal, Brushes.Black, new Point(460, y + i * height));
                    e.Graphics.DrawString(log[line][7], Normal, Brushes.Black, new Point(550, y + i * height));
                    e.Graphics.DrawString(log[line][8], Normal, Brushes.Black, new Point(600, y + i * height));
                }
                line++;
            }

            e.HasMorePages = line < log.Count();
        }

        /// <summary>
        /// Общий отчёт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMianRepotr_Click(object sender, EventArgs e)
        {
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = false;
            doc.PrintPage += Doc_PrintPage1; ;
            line = 0;
            doc.Print();
            Close();
        }

        private void Doc_PrintPage1(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Отчёт     " + comboBoxShift.SelectedItem, Bold, Brushes.Black, new Point(40, 40));
            //e.Graphics.DrawString("Страница " + page + " из " + pages, Normal, Brushes.Black, new Point(650, 40));
            //Шапка таблицы
            e.Graphics.DrawString("Номер", Bold, Brushes.Black, new Point(40, 80));
            e.Graphics.DrawString("Машина", Bold, Brushes.Black, new Point(100, 80));
            e.Graphics.DrawString("Партия", Bold, Brushes.Black, new Point(160, 80));
            e.Graphics.DrawString("Тип", Bold, Brushes.Black, new Point(220, 80));
            e.Graphics.DrawString("Вес", Bold, Brushes.Black, new Point(300, 80));
            e.Graphics.DrawString("Цвет", Bold, Brushes.Black, new Point(360, 80));
            e.Graphics.DrawString("Количество коробов", Bold, Brushes.Black, new Point(500, 80));
            //Допустим, это всё влезет на одну страницу..... тогда делаем тупо внешний цикл по ТПАшкам
            for (int tpa = 1; tpa <= 6; tpa++)
            {
                //Подготавливаем коллекцию сигнатур и коллекцию счётчиков
                List<string[]> sigs = new List<string[]>();
                List<int> counter = new List<int>();
                //Проходимся по журналу
                foreach (string[] rec in log)
                {
                    //Ищем только записи с нужным номером ТПА
                    if (rec[2] == "ТПА" + tpa.ToString())
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
                    e.Graphics.DrawString((line + 1).ToString(), Normal, Brushes.Black, new Point(40, y + line * height));
                    e.Graphics.DrawString(sigs[i][2], Normal, Brushes.Black, new Point(100, y + line * height));
                    e.Graphics.DrawString(sigs[i][3], Normal, Brushes.Black, new Point(160, y + line * height));
                    e.Graphics.DrawString(sigs[i][4], Normal, Brushes.Black, new Point(220, y + line * height));
                    e.Graphics.DrawString(sigs[i][5], Normal, Brushes.Black, new Point(300, y + line * height));
                    e.Graphics.DrawString(sigs[i][6], Normal, Brushes.Black, new Point(360, y + line * height));
                    e.Graphics.DrawString(counter[i].ToString(), Normal, Brushes.Black, new Point(500, y + line * height));
                    line++;
                }
            }
        }
    }
}
