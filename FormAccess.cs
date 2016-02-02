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
            Data.LoadUsers();
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
                it.SubItems.Add(StringWidthTPA(u));
                listView1.Items.Add(it);
            }
        }

        //Рисуем красивую строчку с перечнем привязанных ТПА
        string StringWidthTPA(User u)
        {
            string tpas = "";
            int tc = 0;
            foreach (bool b in u.TPAAccess) if (b) tc++; //Узнаём количество тпа
            int tca = 0;
            for (int i = 0; i < u.TPAAccess.Length; i++)
            {
                if (u.TPAAccess[i])
                {
                    tpas += (i + 1).ToString();
                    tca++;
                    if (tca < tc) tpas += ", ";
                }
            }
            return tpas;
        }

        //Заполнение флажков
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            User u = Data.Users[listView1.SelectedIndices[0]];
            labelName.Text = u.Name;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox1.Checked = u.TPAAccess[0];
            checkBox2.Checked = u.TPAAccess[1];
            checkBox3.Checked = u.TPAAccess[2];
            checkBox4.Checked = u.TPAAccess[3];
            checkBox5.Checked = u.TPAAccess[4];
            checkBox6.Checked = u.TPAAccess[5];
        }

        //Изменение привязки определённого пользователя к определённому ТПА
        void ChangeTPA(byte num, bool Access)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            User u = Data.Users[listView1.SelectedIndices[0]];
            u.TPAAccess[num] = Access;
            listView1.Items[listView1.SelectedIndices[0]].SubItems[1].Text = StringWidthTPA(u);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTPA(0, checkBox1.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTPA(1, checkBox2.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTPA(2, checkBox3.Checked);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTPA(3, checkBox4.Checked);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTPA(4, checkBox5.Checked);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTPA(5, checkBox6.Checked);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            Close();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Data.AccessControl = checkBox7.Checked;
        }
    }
}
