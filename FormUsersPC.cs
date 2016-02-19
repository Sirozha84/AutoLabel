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
    public partial class FormUsersPC : Form
    {
        public FormUsersPC()
        {
            InitializeComponent();
            Data.UsersLoad();
            DrawList();
        }
       
        //Заполняем список пользователей
        void DrawList()
        {
            checkBoxOn.Checked = Data.AccessControl;
            Data.UserListDraw(listView1);
            listView1_SelectedIndexChanged(null, null);
        }

        //Заполнение флажков
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool sel = listView1.SelectedIndices.Count > 0;
            //if (listView1.SelectedIndices.Count == 0) return;
            if (sel)
            {
                User u = Data.Users[listView1.SelectedIndices[0]];
                labelName.Text = u.Name;
                checkBox1.Checked = u.TPAAccess[0];
                checkBox2.Checked = u.TPAAccess[1];
                checkBox3.Checked = u.TPAAccess[2];
                checkBox4.Checked = u.TPAAccess[3];
                checkBox5.Checked = u.TPAAccess[4];
                checkBox6.Checked = u.TPAAccess[5];
                checkBox7.Checked = u.TPAAccess[6];
                checkBox8.Checked = u.TPAAccess[7];
            }
            else
            {
                labelName.Text = "Не выбран";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
            }
            labelName.Enabled = sel;
            checkBox1.Enabled = sel;
            checkBox2.Enabled = sel;
            checkBox3.Enabled = sel;
            checkBox4.Enabled = sel;
            checkBox5.Enabled = sel;
            checkBox6.Enabled = sel;
            checkBox7.Enabled = sel;
            checkBox8.Enabled = sel;
            buttonSetKey.Enabled = sel;
            buttonKeyDel.Enabled = sel;
            buttonDel.Enabled = sel;
        }

        //Изменение привязки определённого пользователя к определённому ТПА
        void ChangeTPA(byte num, bool Access)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            User u = Data.Users[listView1.SelectedIndices[0]];
            u.TPAAccess[num] = Access;
            listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text = u.StringWidthTPA();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ChangeTPA(0, checkBox1.Checked); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { ChangeTPA(1, checkBox2.Checked); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { ChangeTPA(2, checkBox3.Checked); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { ChangeTPA(3, checkBox4.Checked); }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { ChangeTPA(4, checkBox5.Checked); }
        private void checkBox6_CheckedChanged(object sender, EventArgs e) { ChangeTPA(5, checkBox6.Checked); }
        private void checkBox7_CheckedChanged(object sender, EventArgs e) { ChangeTPA(6, checkBox7.Checked); }
        private void checkBox8_CheckedChanged(object sender, EventArgs e) { ChangeTPA(7, checkBox8.Checked); }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            Close();
        }

        private void checkBoxOn_CheckedChanged(object sender, EventArgs e)
        {
            Data.AccessControl = checkBoxOn.Checked;
        }

        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("1");
            DrawList();
        }

        private void buttonNewAdmin_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("255");
            DrawList();
        }

        private void buttonSetKey_Click(object sender, EventArgs e)
        {
            FormKey key = new FormKey();
            key.ShowDialog();
            if (key.Code != "")
            {
                Data.Users[listView1.SelectedIndices[0]].Code = key.Code;
                DrawList();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listView1.SelectedIndices[0]);
            DrawList();
        }

        private void buttonKeyDel_Click(object sender, EventArgs e)
        {
            Data.Users[listView1.SelectedIndices[0]].Code = "";
            DrawList();
        }
    }
}
