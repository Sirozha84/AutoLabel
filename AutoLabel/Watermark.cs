namespace AutoLabel
{
    static class Watermark
    {
        public static string Image(string txt)
        {
            if (txt == "АЯН") return "АЯН";
            if (txt == "ИЗРМВ") return "BaikalSea";
            if (txt == "Coca-cola") return "CocaCola";
            if (txt == "Европласт - Обь") return "Обь";
            return "";
        }
    }
}

//Пропорции логотипа: 1,06 : 1
//Цвет логотипа 176*176*176