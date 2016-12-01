using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace AutoLabel
{
    public partial class FormLog : Form
    {
        List<string[]> log = new List<string[]>();
        bool save = false;
        string FileName;

        public FormLog()
        {
            InitializeComponent();
        }

        private void FormLogEditor_Load(object sender, EventArgs e)
        {
            //Заполнение списка последних смен в комбо-бокс
            foreach (string sh in Shift.LogName)
                comboBox1.Items.Add(sh);
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileName = comboBox1.SelectedItem.ToString();
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
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(Net.HostName, Net.Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        BinaryReader reader = new BinaryReader(stream);
                        writer.Write("LogRead");
                        writer.Write(FileName);
                        string s;
                        do
                        {
                            s = reader.ReadString();
                            if (s != "End")
                            {
                                string[] Str = s.Split(';');
                                for (int i = 0; i < Str.Length; i++)
                                    if (Str[i][0] == ' ') Str[i] = Str[i].Trim(' ');
                                log.Add(Str);
                            }
                        } while (s != "End");
                    }
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
                    using (TcpClient client = new TcpClient())
                    {
                        client.Connect(Net.HostName, Net.Port);
                        using (NetworkStream stream = client.GetStream())
                        {
                            BinaryWriter writer = new BinaryWriter(stream);
                            BinaryReader reader = new BinaryReader(stream);
                            writer.Write("LogWrite");
                            writer.Write(FileName);
                            foreach (string[] rec in log)
                                writer.Write(rec[0] + "; " + rec[1] + "; " + rec[2] + "; " +
                                rec[3] + "; " + rec[4] + "; " + rec[5] + "; " +
                                rec[6] + "; " + rec[7] + "; " + rec[8]);
                            writer.Write("End");
                        }
                    }
                    Net.Log("Журнал изменён");
                }
                catch { }
            }
            Close();
        }
    }
}
