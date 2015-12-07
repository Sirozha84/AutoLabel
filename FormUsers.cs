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
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }

        private void buttonquit_Click(object sender, EventArgs e)
        {
            //Отменяем изменения путём перезагрузки старых данных
            Data.LoadUsers();
            Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Data.Users.RemoveAt(listBox1.SelectedIndex);
            DrawList();
            listBox1_SelectedIndexChanged(null, null);
            buttonsave.Visible = true;
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            DrawList();
        }

        /// <summary>
        /// Вывод списка пользователей
        /// </summary>
        void DrawList()
        {
            listBox1.Items.Clear();
            foreach (User u in Data.Users)
            {
                string str = u.Name;
                if (u.Rule == 255) str += "     администратор";
                if (u.Code == "") str += "     (без ключа)";
                listBox1.Items.Add(str);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            AddUser("0");
        }

        private void buttonRules_Click(object sender, EventArgs e)
        {
            AddUser("255");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonKey.Visible = listBox1.SelectedIndex >= 0;
            buttonDelete.Visible = listBox1.SelectedIndex >= 0;
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            Data.SaveUsers();
            buttonsave.Visible = false;
            //Close();
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="Rule">Права</param>
        void AddUser(string Rule)
        {
            FormKeyboardLetter keyboard = new FormKeyboardLetter("Введите имя пользователя");
            if (keyboard.ShowDialog() == DialogResult.Cancel) return;
            //Добавление нового пользователя
            string name = keyboard.Str;
            FormKey key = new FormKey();
            key.ShowDialog();
            string code = key.Code;
            Data.Users.Add(new User(keyboard.Str, code, Rule));
            buttonsave.Visible = true;
            DrawList();
        }

        private void buttonKey_Click(object sender, EventArgs e)
        {
            FormKey key = new FormKey();
            key.ShowDialog();
            if (key.Code != "")
            {
                Data.Users[listBox1.SelectedIndex].Code = key.Code;
                buttonsave.Visible = true;
                DrawList();
            }
        }
    }
}
