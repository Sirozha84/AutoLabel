using System;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
            Data.UsersLoad();
            DrawList();
            buttonsave.Visible = false;
        }

        //Заполняем список пользователей
        void DrawList()
        {
            checkBoxOn.Checked = Data.AccessControl;
            Data.UserListDraw(listView1);
            listView1_SelectedIndexChanged(null, null);
            buttonsave.Visible = true;
        }

        //Кнопка назад
        private void buttonquit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Кнопка удаления
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listView1.SelectedIndices[0]);
            DrawList();
        }

        //Кнопка нового пользователя
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("1");
            DrawList();
        }

        //Кнопка нового админа
        private void buttonRules_Click(object sender, EventArgs e)
        {
            Data.AddNewUser("255");
            DrawList();
        }

        //Кнопка сохранения
        private void buttonsave_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            buttonsave.Visible = false;
            Net.Log("Изменение списка пользователей на терминале");
        }

        //Кнопка изменения ключа
        private void buttonKey_Click(object sender, EventArgs e)
        {
            FormKey key = new FormKey();
            key.ShowDialog();
            if (key.Code != "")
            {
                Data.Users[listView1.SelectedIndices[0]].Code = key.Code;
                DrawList();
            }
        }

        //Изменение выделения списка
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool sel = listView1.SelectedIndices.Count > 0;
            if (sel)
            {
                User u = Data.Users[listView1.SelectedIndices[0]];
                checkBox1.Checked = u.TPAAccess[0];
                checkBox2.Checked = u.TPAAccess[1];
                checkBox3.Checked = u.TPAAccess[2];
                checkBox4.Checked = u.TPAAccess[3];
                checkBox5.Checked = u.TPAAccess[4];
                checkBox6.Checked = u.TPAAccess[5];
                checkBox7.Checked = u.TPAAccess[6];
                checkBox8.Checked = u.TPAAccess[7];
                checkBox9.Checked = u.TPAAccess[8];
            }
            else
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
            }
            checkBox1.Visible = sel;
            checkBox2.Visible = sel;
            checkBox3.Visible = sel;
            checkBox4.Visible = sel;
            checkBox5.Visible = sel;
            checkBox6.Visible = sel;
            checkBox7.Visible = sel;
            checkBox8.Visible = sel;
            checkBox9.Visible = sel;
            buttonKey.Visible = sel;
            buttonKeyDel.Visible = sel;
            buttonDelete.Visible = sel;
        }

        //Изменение привязки определённого пользователя к определённому ТПА
        void ChangeTPA(byte num, bool Access)
        {
            if (listView1.SelectedIndices.Count == 0) return;
            User u = Data.Users[listView1.SelectedIndices[0]];
            u.TPAAccess[num] = Access;
            listView1.Items[listView1.SelectedIndices[0]].SubItems[3].Text = u.StringWidthTPA();
            buttonsave.Visible = true;
        }

        //Изменение привязки
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { ChangeTPA(0, checkBox1.Checked); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { ChangeTPA(1, checkBox2.Checked); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { ChangeTPA(2, checkBox3.Checked); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { ChangeTPA(3, checkBox4.Checked); }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { ChangeTPA(4, checkBox5.Checked); }
        private void checkBox6_CheckedChanged(object sender, EventArgs e) { ChangeTPA(5, checkBox6.Checked); }
        private void checkBox7_CheckedChanged(object sender, EventArgs e) { ChangeTPA(6, checkBox7.Checked); }
        private void checkBox8_CheckedChanged(object sender, EventArgs e) { ChangeTPA(7, checkBox8.Checked); }
        private void checkBox9_CheckedChanged(object sender, EventArgs e) { ChangeTPA(8, checkBox9.Checked); }

        //Кнопка удаление ключа
        private void buttonKeyDel_Click(object sender, EventArgs e)
        {
            Data.Users[listView1.SelectedIndices[0]].Code = "";
            DrawList();
        }

        //Включение/выключение проверки привязок
        private void checkBoxOn_CheckedChanged(object sender, EventArgs e)
        {
            Data.AccessControl = checkBoxOn.Checked;
            buttonsave.Visible = true;
        }
    }
}
