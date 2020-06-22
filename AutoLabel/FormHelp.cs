using System.Windows.Forms;

namespace AutoLabel
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();

            textHistory.Text =
                "Версия 3.2 (21.06.2020)\n\n" +
                "• Новый водяной знак для Европласт - Обь.\n\n" +
                textHistory.Text;
        }
    }
}
