using System.Windows.Forms;
using System.Drawing;

namespace AutoLabel
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();

            Font fontR = new Font(textHistory.Font.FontFamily, textHistory.Font.Size, FontStyle.Regular);
            Font fontB = new Font(textHistory.Font.FontFamily, textHistory.Font.Size, FontStyle.Bold);

            string temp = textHistory.Text;
            textHistory.Text = "";

            textHistory.SelectionFont = fontB;
            textHistory.AppendText ("Версия 3.7 (12.10.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменена надпись с техническими условиями на этикетке\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.6 (31.07.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Название красителя автоматически берётся из кода при его смене\n\n");


            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.5 (10.07.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый водяной знак для Данон \"РСТ\"\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.4 (27.06.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Доработка производственного задания: два экземпляра на лист и дополнительные поля\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.3 (23.06.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Убран логотип \"РСТ\".\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.2 (21.06.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый водяной знак для Европласт - Обь\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.1 (31.05.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Небольшие правки производственного задания (Граммаж и тип горловины)\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.0 (09.05.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Печать карточки производственного задания автоматически при изменении параметров на линии\n" +
                "• Возможность повтора печати производственного задания на случай сбоя принтера\n\n" + temp);
                
        }
    }
}
