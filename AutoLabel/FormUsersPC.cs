using System;
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
            checkBoxOn.Checked = Data.accessControl;
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
                User u = Data.users[listView1.SelectedIndices[0]];
                labelName.Text = u.Name;
                checkP1.Checked = u.TPAAccess[0];
                checkP2.Checked = u.TPAAccess[1];
                checkP3.Checked = u.TPAAccess[2];
                checkP4.Checked = u.TPAAccess[3];
                checkP5.Checked = u.TPAAccess[4];
                checkP6.Checked = u.TPAAccess[5];
                checkP7.Checked = u.TPAAccess[6];
                checkP8.Checked = u.TPAAccess[7];
                checkP9.Checked = u.TPAAccess[8];
                checkK1.Checked = u.TPAAccess[9];
                checkK2.Checked = u.TPAAccess[10];
                checkR1.Checked = u.TPAAccess[11];
            }
            else
            {
                labelName.Text = "Не выбран";
                checkP1.Checked = false;
                checkP2.Checked = false;
                checkP3.Checked = false;
                checkP4.Checked = false;
                checkP5.Checked = false;
                checkP6.Checked = false;
                checkP7.Checked = false;
                checkP8.Checked = false;
                checkP9.Checked = false;
                checkK1.Checked = false;
                checkK2.Checked = false;
                checkR1.Checked = false;
            }
            labelName.Enabled = sel;
            checkP1.Enabled = sel;
            checkP2.Enabled = sel;
            checkP3.Enabled = sel;
            checkP4.Enabled = sel;
            checkP5.Enabled = sel;
            checkP6.Enabled = sel;
            checkP7.Enabled = sel;
            checkP8.Enabled = sel;
            checkP9.Enabled = sel;
            checkK1.Enabled = sel;
            checkK2.Enabled = sel;
            checkR1.Enabled = sel;
            buttonSetKey.Enabled = sel;
            buttonKeyDel.Enabled = sel;
            buttonDel.Enabled = sel;
        }

        //Изменение привязки определённого пользователя к определённому ТПА
        void ChangeTPA(byte num, bool Access)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            User u = Data.users[listView1.SelectedIndices[0]];
            u.TPAAccess[num] = Access;
            listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text = u.StringWidthTPA();
        }

        private void checkP1_change(object sender, EventArgs e) { ChangeTPA(0, checkP1.Checked); }
        private void checkP2_change(object sender, EventArgs e) { ChangeTPA(1, checkP2.Checked); }
        private void checkP3_change(object sender, EventArgs e) { ChangeTPA(2, checkP3.Checked); }
        private void checkP4_change(object sender, EventArgs e) { ChangeTPA(3, checkP4.Checked); }
        private void checkP5_change(object sender, EventArgs e) { ChangeTPA(4, checkP5.Checked); }
        private void checkP6_change(object sender, EventArgs e) { ChangeTPA(5, checkP6.Checked); }
        private void checkP7_change(object sender, EventArgs e) { ChangeTPA(6, checkP7.Checked); }
        private void checkP8_change(object sender, EventArgs e) { ChangeTPA(7, checkP8.Checked); }
        private void checkP9_change(object sender, EventArgs e) { ChangeTPA(8, checkP9.Checked); }
        private void checkC1_change(object sender, EventArgs e) { ChangeTPA(9, checkK1.Checked); }
        private void checkC2_change(object sender, EventArgs e) { ChangeTPA(10, checkK2.Checked); }
        private void checkR1_change(object sender, EventArgs e) { ChangeTPA(11, checkR1.Checked); }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            Net.Log("Изменение списка пользователей на ПК");
            Close();
        }

        private void checkBoxOn_CheckedChanged(object sender, EventArgs e)
        {
            Data.accessControl = checkBoxOn.Checked;
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
                Data.users[listView1.SelectedIndices[0]].Code = key.Code;
                DrawList();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            Data.users.RemoveAt(listView1.SelectedIndices[0]);
            DrawList();
        }

        private void buttonKeyDel_Click(object sender, EventArgs e)
        {
            Data.users[listView1.SelectedIndices[0]].Code = "";
            DrawList();
        }

    }
}
