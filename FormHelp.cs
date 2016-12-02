﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string txt = "";
            string n = Environment.NewLine;
            if (listBoxVolume.SelectedIndex == 0)
            {
                txt += "Настройка клиента и сервера" + n + n;
                txt += "Параметры сервера хранятся в файле Server.txt" + n;
                txt += "Соединение происходит по порту 90, который следует разрешить в фаерволе." + n + n;
                txt += "Запуск в режиме терминала" + n + n;
                txt += "Для запуска программы в режиме терминала (полноэкранный с поддержкой сенсорного экрана и";
                txt += "считывателя карт) в ярлыке необходимо прописать любой ключ, например: \"autolabel.exe 1\".";

            }
            if (listBoxVolume.SelectedIndex == 1)
            {
                txt += "Список изменнений" + n + n;
                txt += "2.3.0 (02.12.2016)" + n + n;
                txt += "• Статистика по этикеткам" + n + n;
                txt += "2.2.2 (07.07.2016)" + n + n;
                txt += "• Правка этикетки для преформы" + n + n;
                txt += "2.2.1 (27.06.2016)" + n + n;
                txt += "• Выпадающий список прочих дополнений для преформы (пока только в режиме ПК и без возможности изменения)" + n + n;
                txt += "2.2.0 (22.06.2016)" + n + n;
                txt += "• Возможность изменять начальный номер короба" + n;
                txt += "• Справка" + n + n;
                txt += "2.1.0 (02.06.2016)" + n + n;
                txt += "• Учитывание названия цветов с длинным названием, а также упрощение их отображения на главном экране" + n;
                txt += "• Разрешение менять номер короба только администраторам" + n + n;
                txt += "2.0.1 (16.05.2016)" + n + n;
                txt += "• Исправление этикетки для пробки, не влезали длинные слова" + n;
                txt += "• Исправлена печать этикетки для пробки с компьютера, не брался актуальный номер" + n + n;
                txt += "2.0.0 Бета 5 (10.05.2016)" + n + n;
                txt += "• Соответствие поля типа к полю веса для параметров пробки" + n;
                txt += "• Исправлен отчёт \"Общий\"" + n + n;
                txt += "2.0.0 Бета 4 (29.04.2016)" + n + n;
                txt += "• Разный лимит этикеток на разных ТПА" + n;
                txt += "• Исправлено создание нового пользователя" + n + n;
                txt += "2.0.0 Бета 3 (28.04.2016)" + n + n;
                txt += "• Запрет запуска второго экземпляра программы" + n + n;
                txt += "2.0.0 Бета 2 (24.03.2016)" + n + n;
                txt += "• Исправлена масса ошибок, связанных с переходом на клиент-серверную версию" + n + n;
                txt += "2.0.0 Бета 1 (23.03.2016)" + n + n;
                txt += "• Переход программы на клиент-серверную версию, теперь возможна работа программы на нескольких машинах одновременно" + n + n;
                txt += "1.2.0 (17.03.2016)" + n + n;
                txt += "• Да кто его уже помнит что там было, главное что это был мой день рождения :-)" + n + n;
                txt += "1.0.0 (03.02.2016)" + n + n;
                txt += "• После 12-и бет вышла такая, какая вышла.";
            }
            textBoxHelp.Text = txt;
        }

        private void FormHelp_Load(object sender, EventArgs e)
        {
            listBoxVolume.SelectedIndex = 0;
        }
    }
}
