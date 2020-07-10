namespace AutoLabel
{
    static class Watermark
    {
        public static string Image(string txt)
        {
            if (txt == "АЯН") return "АЯН";
            if (txt == "Данон") return "DAN";
            if (txt == "Европласт - Обь") return "Обь";
            if (txt == "ИЗРМВ") return "BaikalSea";
            if (txt == "Coca-cola") return "CocaCola";
            return "";
        }
    }
}

//Пропорции логотипа: 1,06 : 1
//Цвет логотипа 176*176*176