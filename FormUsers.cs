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
            Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            buttonRules.Visible = true;
            buttonDelete.Visible = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //Удаление
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            DrawList();
        }

        void DrawList()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Гордеев - администратор");
            listBox1.Items.Add("Иванов - упаковщик");
            listBox1.Items.Add("Петров - упаковщик");
            listBox1.Items.Add("Сидоров - упаковщик (без ключа)");
        }

        private void buttonRules_Click(object sender, EventArgs e)
        {
            //Доступ
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            FormKeyboardLetter keyboard = new FormKeyboardLetter("Введите имя пользователя");
            if (keyboard.ShowDialog() == DialogResult.Cancel) return;
            MessageBox.Show(keyboard.Str);
        }
    }
}
