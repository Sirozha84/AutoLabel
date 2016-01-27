using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutoLabel
{
    public partial class FormLogEditor : Form
    {
        List<string[]> log = new List<string[]>();
        bool save = false;
        string FileName;

        public FormLogEditor()
        {
            InitializeComponent();
        }

        private void FormLogEditor_Load(object sender, EventArgs e)
        {
            //Заполнение списка последних смен в комбо-бокс
            foreach (string sh in Data.LogName)
                comboBox1.Items.Add(sh);
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileName = "Logs\\" + comboBox1.SelectedItem.ToString() + ".csv";
            LoadLog();
            DrawLog();
            save = false;
        }

        // Загрузка и парсинг журнала
        void LoadLog()
        {
            try
            {
                log.Clear();
                foreach (string str in File.ReadLines(FileName, Encoding.Default))
                {
                    string[] Str = str.Split(';');
                    for (int i = 0; i < Str.Count(); i++)
                        if (Str[i][0] == ' ') Str[i] = Str[i].Trim(' ');
                    log.Add(Str);
                }
            }
            catch { }
        }

        //Рисование журнала
        void DrawLog()
        {
            listView1.Items.Clear();
            foreach (string[] rec in log)
                listView1.Items.Add(new ListViewItem(rec));
            buttonDel.Enabled = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDel.Enabled = listView1.SelectedIndices.Count > 0;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            log.RemoveAt(listView1.SelectedIndices[0]);
            DrawLog();
            save = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Сохранить лог надо здесь
            if (save)
            {
                try
                {
                    File.Delete(FileName);
                    StreamWriter file = new StreamWriter(FileName, true, Encoding.Default);
                    foreach (string[] rec in log)
                        file.WriteLine(rec[0] + "; " + rec[1] + "; " + rec[2] + "; " +
                                       rec[3] + "; " + rec[4] + "; " + rec[5] + "; " +
                                       rec[6] + "; " + rec[7] + "; " + rec[8]);
                    file.Dispose();
                }
                catch { }
            }
            Close();
        }
    }
}
