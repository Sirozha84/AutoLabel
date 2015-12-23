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

        List<string> log = new List<string>();

        //Шрифты
        Font Normal = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);


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
            foreach (string sh in Data.LogName)
            {
                    comboBoxShift.Items.Add(sh);
            }
            comboBoxShift.SelectedIndex = 0;
        }

        /// <summary>
        /// Загрузка журнала
        /// </summary>
        void LoadLog()
        {
            try
            {
                log.Clear();
                foreach (string str in File.ReadLines(comboBoxShift.SelectedItem.ToString() + ".csv", Encoding.Default))
                    log.Add(str);
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

        /// <summary>
        /// Правильный сплиттер, убирающий пробелы в начале
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string[] Separator(string str)
        {
            string[] Str = str.Split(';');
            for (int i = 0;i<Str.Count();i++)
                if (Str[i][0] == ' ') Str[i] = Str[i].Trim(' ');
            return Str;
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //780*1100 - примерный предел
            //Заголовок
            e.Graphics.DrawString("Журнал "+comboBoxShift.SelectedItem, Normal, Brushes.Black, new Point(40, 40));
            //Шапка таблицы
            e.Graphics.DrawString("Дата", Normal, Brushes.Black, new Point(40, 80));
            e.Graphics.DrawString("Время", Normal, Brushes.Black, new Point(100, 80));
            e.Graphics.DrawString("Машина", Normal, Brushes.Black, new Point(160, 80));
            e.Graphics.DrawString("Партия", Normal, Brushes.Black, new Point(220, 80));
            e.Graphics.DrawString("Короб", Normal, Brushes.Black, new Point(280, 80));
            e.Graphics.DrawString("Упаковщик", Normal, Brushes.Black, new Point(340, 80));

            for (int i = 0; i < 30; i++ )
            {
                if (line < log.Count)
                {
                    string[] str = Separator(log[line]);
                    e.Graphics.DrawString(str[0], Normal, Brushes.Black, new Point(40, 100 + i * 20));
                    e.Graphics.DrawString(str[1], Normal, Brushes.Black, new Point(100, 100 + i * 20));
                    e.Graphics.DrawString(str[2], Normal, Brushes.Black, new Point(160, 100 + i * 20));
                    e.Graphics.DrawString(str[3], Normal, Brushes.Black, new Point(220, 100 + i * 20));
                    e.Graphics.DrawString(str[4], Normal, Brushes.Black, new Point(280, 100 + i * 20));
                    e.Graphics.DrawString(str[6], Normal, Brushes.Black, new Point(340, 100 + i * 20));
                }
                line++;
            }

            e.HasMorePages = line < log.Count();
            //Дата Время   Машина Партия  Короб Смена   Упаковщик

        }

        /// <summary>
        /// Сводный отчёт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMianRepotr_Click(object sender, EventArgs e)
        {

        }
    }
}
