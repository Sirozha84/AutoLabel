using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace AutoLabel
{
    static class Conformity
    {
        /// <summary>
        /// Задание цвета для кнопки
        /// </summary>
        /// <param name="but">Кнопку</param>
        /// <param name="lab">Лейбл</param>
        public static void SetColor(Button but, int tpa)
        {
            but.Visible = Data.lines[tpa].partNum != "" & Data.lines[tpa].enable;
            string FirstWord = Data.lines[tpa].color.Split(' ')[0].ToLower();
            switch (FirstWord)
            {
                case "бесцветный":
                    but.BackColor = Color.FromArgb(64, 64, 64);
                    but.ForeColor = Color.FromArgb(192, 192, 192);
                    break;
                case "белый":
                    but.BackColor = Color.FromArgb(255, 255, 255);
                    but.ForeColor = Color.FromArgb(192, 192, 192);
                    break;
                case "бирюзовый":
                    but.BackColor = Color.FromArgb(0, 128, 128);
                    but.ForeColor = Color.FromArgb(0, 255, 255);
                    break;
                case "бордовый":
                    but.BackColor = Color.FromArgb(128, 32, 32);
                    but.ForeColor = Color.FromArgb(255, 64, 64);
                    break;
                case "голубой":
                    but.BackColor = Color.FromArgb(80, 158, 255);
                    but.ForeColor = Color.FromArgb(150, 200, 255);
                    break;
                case "оранжевый":
                    but.BackColor = Color.FromArgb(255, 128, 0);
                    but.ForeColor = Color.FromArgb(255, 178, 0);
                    break;
                case "жёлтый":
                case "желтый":
                case "золотой":
                    but.BackColor = Color.FromArgb(255, 255, 0);
                    but.ForeColor = Color.FromArgb(128, 128, 0);
                    break;
                case "зелёный":
                case "зеленый":
                    but.BackColor = Color.FromArgb(0, 128, 0);
                    but.ForeColor = Color.FromArgb(0, 255, 0);
                    break;
                case "синий":
                    but.BackColor = Color.FromArgb(0, 0, 128);
                    but.ForeColor = Color.FromArgb(0, 128, 255);
                    break;
                case "рубиновый":
                case "красный":
                    but.BackColor = Color.FromArgb(128, 0, 0);
                    but.ForeColor = Color.FromArgb(255, 64, 64);
                    break;
                case "коричневый":
                    but.BackColor = Color.FromArgb(64, 32, 0);
                    but.ForeColor = Color.FromArgb(128, 64, 0);
                    break;
                case "фиолетовый":
                    but.BackColor = Color.FromArgb(128, 0, 192);
                    but.ForeColor = Color.FromArgb(192, 0, 255);
                    break;
                case "чёрный":
                case "черный":
                    but.BackColor = Color.FromArgb(0, 0, 0);
                    but.ForeColor = Color.FromArgb(64, 64, 64);
                    break;
                default:
                    but.BackColor = Color.FromArgb(0, 0, 0);
                    but.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
            }
        }


        /// <summary>
        /// Список типов горловин из веса и линии (колпак)
        /// </summary>
        /// <param name="Weight">Вес</param>
        /// <param name="Line">Линия</param>
        /// <returns></returns>
        public static List<string> WeightToType(string Weight, string Line)
        {
            List<string> list = new List<string>();
            /*if (Line == "C1")
            {
                if (Weight == "1,95±0,1") { list.Add("КВП-1-28/1881/1.(1.95)"); list.Add("КВП-1-28/1881/2.(1.95)"); }
                if (Weight == "2,05±0,1") { list.Add("КВП-1-28/1881/1.(2.05)"); list.Add("КВП-1-28/1881/2.(2.05)"); }
                if (Weight == "2,15±0,1") { list.Add("КВП-1-28/1881/1.(2.15)"); list.Add("КВП-1-28/1881/2.(2.15)"); }
                if (Weight == "3,15±0,1") list.Add("КВП-1-28");
            }
            if (Line == "C2")
            {
                if (Weight == "2,35±0,1") list.Add("КВП-1-28/1881/2");
                if (Weight == "2,5±0,1") list.Add("КВП-1-28/1881/1");
            }*/

            //В принципе так и так этот список не зависит от линии, по этому лишние условия можно убртаь
            if (Weight == "1,95±0,1") { list.Add("КВП-1-28.1881/1.(1.95)"); list.Add("КВП-1-28.1881/2.(1.95)"); }
            if (Weight == "2,05±0,1") { list.Add("КВП-1-28.1881/1.(2.05)"); list.Add("КВП-1-28.1881/2.(2.05)"); }
            if (Weight == "2,15±0,1") { list.Add("КВП-1-28.1881/1.(2.15)"); list.Add("КВП-1-28.1881/2.(2.15)"); }
            if (Weight == "3,15±0,1") list.Add("КВП-1-28");
            if (Weight == "2,35±0,1") list.Add("КВП-1-28.1881/2");
            if (Weight == "2,5±0,1") list.Add("КВП-1-28.1881/1");

            return list;

        }

        /// <summary>
        /// Вес из веса и линии
        /// </summary>
        /// <param name="Weight"></param>
        /// <param name="Line"></param>
        /// <returns></returns>
        public static string WeightToCount(string Weight, string Line)
        {
            if (Line == "C1")
            {
                if (Weight == "3,15±0,1") return "3800";
                return "4750";
            }
            if (Line == "C2")
            {
                return "4750";
            }
            return "";
        }

        /// <summary>
        /// Понятное имя из типа этикетки
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string LabelName(int type)
        {
            switch (type)
            {
                case 0: return "Преформа";
                case 1: return "Колпак";
                default: return "Ротопринт";
            }
        }

        public static string NameAndColorToCode(string Name, string Color)
        {
            if (Name == "Aviva" &                       Color == "Красный 48")      return "4";
            if (Name == "Aviva" &                       Color == "Оранжевый 61")    return "4";
            if (Name == "Aviva" &                       Color == "Жёлтый 62")       return "4";
            if (Name == "BAIKAL" &                      Color == "Белый 53")        return "5/8";
            if (Name == "burn" &                        Color == "Чёрный 74")       return "4";
            if (Name == "FLASH" &                       Color == "Чёрный 74")       return "4";
            if (Name == "FLASH" &                       Color == "Жёлтый 69")       return "8";
            if (Name == "SPRING" &                      Color == "Красный 45")      return "4";
            if (Name == "SPRING" &                      Color == "Зелёный 37")      return "4";
            if (Name == "SPRING OF THE BAIKAL" &        Color == "Зелёный 37")      return "4";
            if (Name == "Ангара-Реактив" &              Color == "Белый 53")        return "12";
            if (Name == "Байкал AQUA" &                 Color == "Синий 19")        return "4";
            if (Name == "БАЛТИКА" &                     Color == "Синий 19")        return "4";
            if (Name == "БАЛТИКА-9" &                   Color == "Красный 45")      return "4/6/9";
            if (Name == "Белый медведь" &               Color == "Бордовый 47")     return "3";
            if (Name == "Белый медведь" &               Color == "Золотой 83")      return "3";
            if (Name == "Благая весть" &                Color == "Синий 17")        return "4";
            if (Name == "Бочкари" &                     Color == "Синий 15")        return "4";
            if (Name == "Бочкари" &                     Color == "Бордовый 43")     return "6";
            if (Name == "Бочкари" &                     Color == "Зелёный 37")      return "6";
            if (Name == "Бочкари" &                     Color == "Серебро 59")      return "8";
            if (Name == "Бочкари" &                     Color == "Золотой 83")      return "9";
            if (Name == "ДВ" &                          Color == "Белый 55/53")     return "2/3";
            if (Name == "ДРАКОН" &                      Color == "Белый 53")        return "6/5";
            if (Name == "Жатецкий Гусь" &               Color == "Зелёный 39")      return "6";
            if (Name == "Завод напитков АЛЬПИНА" &      Color == "Красный 48")      return "4/8";
            if (Name == "Завод напитков АЛЬПИНА" &      Color == "Синий 14")        return "4/8";
            if (Name == "Завод напитков АЛЬПИНА" &      Color == "Зелёный 34")      return "4/8";
            if (Name == "Завод напитков АЛЬПИНА" &      Color == "Жёлтый 64")       return "4/8";
            if (Name == "Завод напитков АЛЬПИНА" &      Color == "Персик 63")       return "4/8";
            if (Name == "Завод напитков АЛЬПИНА" &      Color == "Голубой 17")      return "4/8";
            if (Name == "Завьяловская" &                Color == "Зелёный 37")      return "4";
            if (Name == "Заповедное" &                  Color == "Золотой 88")      return "10";
            if (Name == "ИП «Мамонтов»" &               Color == "Голубой 16")      return "4";
            if (Name == "Клинское" &                    Color == "Зелёный 39")      return "4/10";
            if (Name == "Корона" &                      Color == "Золотой 83")      return "21";
            if (Name == "КУКА 7" &                      Color == "Синий 15")        return "4/5";
            if (Name == "КУКА Курортная" &              Color == "Синий 15")        return "4";
            if (Name == "Legend of Baikal" &            Color == "Синий 19")        return "4";
            if (Name == "Legend of Baikal export" &     Color == "Синий 19")        return "4";
            if (Name == "Лимоша" &                      Color == "Жёлтый 62")       return "3";
            if (Name == "Лимоша" &                      Color == "Зелёный 35")      return "4";
            if (Name == "НЕРПА" &                       Color == "Голубой 20")      return "4/8";
            if (Name == "НЕРПА" &                       Color == "Красный 48")      return "4/8/7";
            if (Name == "ОХОТА" &                       Color == "Золотой 84")      return "41/5";
            if (Name == "ПЗ АЛЬПИНА" &                  Color == "Синий 14")        return "4/8";
            if (Name == "ПЗ АЛЬПИНА" &                  Color == "Зелёный 34")      return "4/8";
            if (Name == "ПЗ АЛЬПИНА" &                  Color == "Красный 48 14")   return "4/8";
            if (Name == "Пивоварня Кожевниково" &       Color == "Красный 45")      return "4";
            if (Name == "Пивоварня Кожевниково" &       Color == "Зелёный 34")      return "5";
            if (Name == "Пивоварня Кожевниково" &       Color == "Золотой 83")      return "9";
            if (Name == "СБ" &                          Color == "Серебро 58")      return "4/5";
            if (Name == "ТРИ МЕДВЕДЯ" &                 Color == "Золотой 84")      return "4/5/3";
            if (Name == "Фейлонг" &                     Color == "Белый 53")        return "3/5";
            if (Name == "Фруктайм" &                    Color == "Жёлтый 64")       return "52";
            if (Name == "Чашка кофе" &                  Color == "Белый 53")        return "3/11";
            if (Name == "Coca-Cola Zero" &              Color == "Чёрный 74")       return "4";
            if (Name == "Coca-Cola без консервантов" &  Color == "Красный 46,48")   return "4";
            if (Name == "Bonaqua" &                     Color == "Голубой 16")      return "4";
            if (Name == "Bonaqua" &                     Color == "Зелёный 32")      return "4";
            if (Name == "Bonaqua" &                     Color == "Синий 13")        return "4";
            if (Name == "Fanta" &                       Color == "Жёлтый 64")       return "4";
            if (Name == "Fanta" &                       Color == "Зелёный 33")      return "5";
            if (Name == "Fanta без консервантов" &      Color == "Оранжевый 61")    return "4";
            if (Name == "Fanta без консервантов" &      Color == "Голубой 16")      return "4";
            if (Name == "Fanta новая" &                 Color == "Жёлтый 64")       return "1";
            if (Name == "Fanta без консервантов новая"& Color == "Жёлтый 64")       return "1";
            if (Name == "Fanta без консервантов новая"& Color == "Оранжевый 61")    return "8";
            if (Name == "Fanta шоката")                                             return "1"; //Цвет не указан?
            if (Name == "Sprite" &                      Color == "Синий 14")        return "4";
            if (Name == "Sprite новый" &                Color == "Зелёный 31,32")   return "4";
            if (Name == "Sсhweppes" &                   Color == "Бирюзовый 10")    return "4";
            if (Name == "Sсhweppes" &                   Color == "Зелёный 31,32")   return "4";
            if (Name == "Sсhweppes" &                   Color == "Красный 49")      return "4";
            if (Name == "Sсhweppes" &                   Color == "Жёлтый 64")       return "3";
            if (Name == "«FANTA» промо 2 балла" &       Color == "Жёлтый 64")       return "4";
            if (Name == "«FANTA» промо 3 балла" &       Color == "Жёлтый 64")       return "4";
            if (Name == "«FANTA» промо 4 балла" &       Color == "Жёлтый 64")       return "4";
            if (Name == "«FANTA» промо 2 балла" &       Color == "Оранжевый 61")    return "8";
            if (Name == "«FANTA» промо 3 балла" &       Color == "Оранжевый 61")    return "8";
            if (Name == "«FANTA» промо 4 балла" &       Color == "Оранжевый 61")    return "8";
            if (Name == "«FANTA» промо 2 балла" &       Color == "Жёлтый 64")       return "4";
            if (Name == "«FANTA» промо 3 балла" &       Color == "Жёлтый 64")       return "4";
            if (Name == "«FANTA» промо 4 балла" &       Color == "Жёлтый 64")       return "4";
            if (Name == "«Сoca-Cola Zero» промо 2 балла" & Color == "Чёрный 74")    return "4";
            if (Name == "«Сoca-Cola Zero» промо 3 балла" & Color == "Чёрный 74")    return "4";
            if (Name == "«Сoca-Cola Zero» промо 4 балла" & Color == "Чёрный 74")    return "4";
            if (Name == "«Сoca-Cola» промо 2 балла" &   Color == "Красный 46,48")   return "4";
            if (Name == "«Сoca-Cola» промо 3 балла" &   Color == "Красный 46,48")   return "4";
            if (Name == "«Сoca-Cola» промо 4 балла" &   Color == "Красный 46,48")   return "4";
            return "";
        }
    }
}
