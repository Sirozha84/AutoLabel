using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormAccess : Form
    {
        public FormAccess()
        {
            InitializeComponent();
        }

        private void FormAccess_Load(object sender, EventArgs e)
        {
            DrawList();
        }
        
        //Заполняем список пользователей
        void DrawList()
        {
            checkBox7.Checked = Data.AccessControl;
            listView1.Items.Clear();
            foreach (User u in Data.Users)
            {
                ListViewItem it = new ListViewItem(u.Name);
                //Рисуем красивую строчку с перечнем привязанных ТПА
                string tpas = "";
                int tc = 0;
                foreach (byte b in u.TPAAccess) if (b == 1) tc++; //Узнаём количество тпа
                int tca = 0;
                for (int i = 0; i < u.TPAAccess.Length; i++)
                {
                    if (u.TPAAccess[i] == 1)
                    {
                        tpas += (i + 1).ToString();
                        tca++;
                        if (tca < tc) tpas += ", ";
                    }
                }
                it.SubItems.Add(tpas);
                listView1.Items.Add(it);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            labelName.Text = listView1.SelectedItems[0].SubItems[0].Text ;//.ToString();
        }
    }
}
