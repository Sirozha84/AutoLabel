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

            textSetup.SelectionFont = fontB;
            textSetup.AppendText("Запуск клиента и сервера\n\n");
            textSetup.SelectionFont = fontR;
            textSetup.AppendText("Параметры сервера хранятся в файле Server.txt, в котором указывается только строка с именем сервера или " +
                "его IP - адресом. Если клиент и сервер запускаются на одной машине, в качестве сервера указывается localhost или 127.0.0.1.\n" +
                "Соединение происходит по порту 90, который следует разрешить в фаерволе.\n\n");

            textSetup.SelectionFont = fontB;
            textSetup.AppendText("Запуск в режиме терминала\n\n");
            textSetup.SelectionFont = fontR;
            textSetup.AppendText("Для запуска программы в режиме терминала (полноэкранный с поддержкой сенсорного экрана и считывателя карт) " +
                "в ярлыке необходимо прописать любой ключ, например: \"autolabel.exe 1\"\n\n");

            textSetup.SelectionFont = fontB;
            textSetup.AppendText("Бесконтактные ключи RFID\n\n");
            textSetup.SelectionFont = fontR;
            textSetup.AppendText("В программе используется считыватель бесконтактных карт RFID эмулирующий клавиатуру. " +
                "По этому, для тестов можно использовать ввод с клавиатуры 8 - и символов.");



            textTips.AppendText("В режиме терминала при нажатии на поле \"Прочие дополнения\" предлагается ввести текст вручную. " +
                "Для того чтобы выбрать одно из значений из списка необходимо нажать на кнопку выбора (в правой части с треугольником вниз) " +
                "и отвести палец вверх или вниз.\n\n");
            textTips.AppendText("Для корректного отображения цвета в списке констант могут быть цвета: бесцветный, белый, бирюзовый, " +
                "бордовый, голубой, оранжевый, жёлтый, желтый, золотой, зелёный, зеленый, синий, рубиновый, красный, коричневый, фиолетовый, " +
                "чёрный и черный.\n\n");
            textTips.AppendText("Логотипы компаний поддерживаются следующие(строка указывается в прочих дополнениях): " +
                "\"АЯН\", \"Данон\", \"Европласт - Обь\", \"ИЗРМВ\" и \"Coca-cola\".\n\n");
            textTips.AppendText("В соответствии с весом колпака и линией его производства выбирается его тип, если он один, " +
                "или выбор, если больше одного, а так же выбирается вес по умолчанию.\n\n");



            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 3.7 (12.10.2020)\n\n");
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
                "• Возможность повтора печати производственного задания на случай сбоя принтера\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.8.1 (15.03.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Доработан отчёт по ТПА, теперь в нём работает перенос строк на следующий столбец\n" +
                "• Увеличилось количество столбцов в отчёте по ТПА до 9-и\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.8.0 (10.02.2020)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новая линия преформы \"7\"\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.7.2 (29.12.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменена заливка для линии C2. Теперь она не на всю страницу, а только на поле с кодом\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.7.1 (05.11.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Исправлен выпадающий список веса для этикеток с произвольными полями (сделано так же как и в " +
                "параметрах для линий)\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.7.0 (29.08.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Разблокирована привязка весов и типов к линиям С1 и С2, теперь это общий список\n" +
                "• Отключено попадание в журнал записей об этикетках с произвольными полями\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.6 (09.07.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Правки в этикетке колпака\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.5 (04.06.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Небольшие правки в этикетке преформы\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.4 (31.05.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Небольшие правки в этикетке колпака\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.3 (30.05.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Небольшие правки в этикетке преформы\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.2 (16.05.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Добавление логотипов сертификатов на этикетку преформы\n" +
                "• Изменен шрифт \"Преформа из...\"\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.1 (08.04.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Исправлено положение надписи примечания в этикетке для колпачка\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.6.0 (25.03.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменение этикетки колпачка(увеличенный шрифт и изменение порядка полей)\n" +
                "• Исправление ошибки неправильного заполнения поля \"Тип горловины\" при открытии параметров для колпачка\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.7 (20.02.2019)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменение этикетки колпачка(увеличенный шрифт и изменение порядка полей)\n" +
                "• Netstal 3 был переименован в Netstal 7\n• Netstal 7 был переименован обратно в Netstal 3\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.6 (24.10.2018)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Затемнённый задний фон этикетки на линии C2\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.5 (29.06.2018)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменение температуры в этикетке преформы\n• Изменение адреса в этикетке преформы\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.4 (28.05.2018)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменился номер ТУ в этикетке преформы\n• Убран ISO в этикетке преформы\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.3 (26.12.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Добавлен новый веса колпака 2,05\n• Добавлены новые типы горловин колпака для веса 2,05\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.2 (11.11.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Добавлен водяной знак для \"АЯН\"\n" +
                "• Исправлено: не появлялась кнопка изменить при изменении текста с прочими дополнениями\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.1 (23.10.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Изменено название компании на этикетках\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.5.0 (22.09.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новые соответствия веса колпака и линии к его типу\n" +
                "• Выбор вложимости колпака от веса\n" +
                "• \"Константы\" теперь называются \"Справочники\", по аналогии с 1С\n" +
                "• Из - за большой связи типа и веса колпака, эти справочники теперь нельзя редактировать.\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.4.3 (27.04.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Подгон этикеток ротопринта под шаблонно - нарезанную самоклеящуюся бумагу.\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.4.2 (18.04.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Вставлен 4 - ый вариант строки для цвета(не влазила сильно длинная трока).\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.4.1 (14.04.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Привязка кода цвета логотипа к логотипу и цвету колпачка(ротопринт)/n" +
                "• Улучшено поведение печати в режиме ПК(если привязанный упаковщик только один, " +
                "то он сразу выбирается и доступна печать, и если убрать и снова поставить галочку \"только привязанные\" " +
                "он снова становится выбранным). Так же убрана ошибка которая оставляет кнопку печати если убрать галочку " +
                "\"только привязанные\" и не выбрать упаковщика\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.4.0 (07.04.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Возможность выбора прочих дополнениях при печати этикеток с произвольными полями\n" +
                "• Константы прочих  дополнений теперь можно редактировать пользователю\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.3.1 (05.04.2017)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Водяной знак с логотипами компаний(Кока - кола и Байкал)\n" +
                "• Исправлено не реагирование на изменение прочих дополнений в режиме ПК\n" +
                "• Убраны штрихкоды\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.3.0 (07.12.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Штрихкод на некоторые заданные этикетки\n" +
                "• Отбор только привязанных упаковщиков при печати этикеток в режиме ПК\n" +
                "• Изменение ограничений максимального количества этикеток на линию колпака\n" +
                "• Статистика по этикеткам\n" +
                "• Исправлена бегущая строка, теперь она снова работает!\n" +
                "• Обновлённая справка :-)\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.2.2 (07.07.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Правка этикетки для преформы\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.2.1 (27.06.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Выпадающий список прочих дополнений для преформы(пока только в режиме ПК и без возможности изменения)\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.2.0 (22.06.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Возможность изменять начальный номер короба\n• Справка\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.1.0 (02.06.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Учитывание названия цветов с длинным названием, а также упрощение их отображения на главном экране\n" +
                "• Разрешение менять номер короба только администраторам\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.0.1 (16.05.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Исправление этикетки для пробки, не влезали длинные слова\n" +
                "• Исправлена печать этикетки для пробки с компьютера, не брался актуальный номер\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.0.0 Бета 5 (10.05.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Соответствие поля типа к полю веса для параметров пробки\n• Исправлен отчёт \"Общий\"\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.0.0 Бета 4 (29.04.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Разный лимит этикеток на разных ТПА\n• Исправлено создание нового пользователя\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.0.0 Бета 3 (28.04.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n• Запрет запуска второго экземпляра программы\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.0.0 Бета 2 (24.03.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Исправлена масса ошибок, связанных с переходом на клиент - серверную версию\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 2.0.0 Бета 1 (23.03.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Переход программы на клиент-серверную версию, теперь возможна работа программы на нескольких машинах одновременно\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 1.2.0 (17.03.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n" +
                "• Да кто его уже помнит что там было, главное что это был мой день рождения :-)\n\n");

            textHistory.SelectionFont = fontB;
            textHistory.AppendText("Версия 1.0.0 (03.02.2016)\n\n");
            textHistory.SelectionFont = fontR;
            textHistory.AppendText("• Новый тип этиеток \"Ротопринт\"\n• После 12 - и бет вышла такая, какая вышла");
        }
    }
}
