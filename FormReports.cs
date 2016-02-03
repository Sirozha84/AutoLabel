using System;
using System.Collections.Generic;
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
        Font Big = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Pixel);

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
                foreach (string str in File.ReadLines("Logs\\" +
                    comboBoxShift.SelectedItem.ToString() + ".csv", Encoding.Default))
                {
                    string[] Str = str.Split(';');
                    for (int i = 0; i < Str.Count(); i++)
                        if (Str[i][0] == ' ') Str[i] = Str[i].Trim(' ');
                    log.Add(Str);
                }
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
            e.Graphics.DrawString("Журнал     " + comboBoxShift.SelectedItem, Big, Brushes.Black, new Point(40, 40));
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
            e.Graphics.DrawString("Короб", Bold, Brushes.Black, new Point(570, 80));
            e.Graphics.DrawString("Упаковщик", Bold, Brushes.Black, new Point(620, 80));

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
                    e.Graphics.DrawString(log[line][7], Normal, Brushes.Black, new Point(570, y + i * height));
                    e.Graphics.DrawString(log[line][8], Normal, Brushes.Black, new Point(620, y + i * height));
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
            doc.PrintPage += Doc_PrintPage1;
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
            e.Graphics.DrawString("Общий отчёт     " + comboBoxShift.SelectedItem, Big, Brushes.Black, new Point(40, 40));
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

        /// <summary>
        /// Отчёт по партиям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReportByPart_Click(object sender, EventArgs e)
        {
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = false;
            doc.PrintPage += Doc_PrintPage2;
            line = 0;
            doc.Print();
            Close();
        }
        private void Doc_PrintPage2(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Отчёт по партиям     " + comboBoxShift.SelectedItem, Big, Brushes.Black, new Point(40, 40));
            //e.Graphics.DrawString("Страница " + page + " из " + pages, Normal, Brushes.Black, new Point(650, 40));
            //Шапка таблицы
            e.Graphics.DrawString("Номер", Bold, Brushes.Black, new Point(40, 80));
            e.Graphics.DrawString("Партия", Bold, Brushes.Black, new Point(100, 80));
            e.Graphics.DrawString("Количество коробов", Bold, Brushes.Black, new Point(200, 80));
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
                e.Graphics.DrawString((line + 1).ToString(), Normal, Brushes.Black, new Point(40, y + line * height));
                e.Graphics.DrawString(part[i], Normal, Brushes.Black, new Point(100, y + line * height));
                e.Graphics.DrawString(counter[i].ToString(), Normal, Brushes.Black, new Point(200, y + line * height));
                line++;
            }
        }

        /// <summary>
        /// Отчёт по ТПА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonByTPA_Click(object sender, EventArgs e)
        {
            LoadLog();
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings = Data.printersettings;
            doc.PrinterSettings.DefaultPageSettings.Landscape = true;
            doc.PrintPage += Doc_PrintPage3;
            line = 0;
            doc.Print();
            Close();
        }
        private void Doc_PrintPage3(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            int height = 20; //Расстояние между строками
            int strings = 1000 / height; //Количество строк на странице
            int page = line / strings + 1;
            int pages = log.Count / strings + 1;
            //Заголовок
            e.Graphics.DrawString("Отчёт по ТПА     " + comboBoxShift.SelectedItem, Big, Brushes.Black, new Point(40, 40));
            //e.Graphics.DrawString("Страница " + page + " из " + pages, Normal, Brushes.Black, new Point(650, 40));
            //Шапка таблицы
            for (int tpa = 0; tpa < 6; tpa++)
            {
                e.Graphics.DrawString("ТПА" + (tpa + 1).ToString(), Bold, Brushes.Black, new Point(tpa * 180 + 40, 80));
                e.Graphics.DrawString("Номер", Bold, Brushes.Black, new Point(tpa * 180 + 40, 100));
                e.Graphics.DrawString("Время", Bold, Brushes.Black, new Point(tpa * 180 + 90, 100));
                e.Graphics.DrawString("Короб", Bold, Brushes.Black, new Point(tpa * 180 + 140, 100));
                line = 0;
                int y = 120;
                foreach (string[] rec in log)
                {
                    if (rec[2] == "ТПА" + (tpa + 1).ToString())
                    {
                        e.Graphics.DrawString((line+1).ToString(), Normal, Brushes.Black, new Point(tpa * 180 + 40, y + line * height));
                        e.Graphics.DrawString(rec[1], Normal, Brushes.Black, new Point(tpa * 180 + 90, y + line * height));
                        e.Graphics.DrawString(rec[7], Normal, Brushes.Black, new Point(tpa * 180 + 140, y + line * height));
                        line++;
                    }
                }
            }
        }
    }
}
