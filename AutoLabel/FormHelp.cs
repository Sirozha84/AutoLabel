using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();

            textHistory.Text =
                "Версия 3.3 (23.06.2020)\n\n"+
                "• Убран логотип \"РСТ\".\n\n" +
                "Версия 3.2 (21.06.2020)\n\n" +
                "• Новый водяной знак для Европласт - Обь.\n\n" +
                textHistory.Text;
        }
    }
}
