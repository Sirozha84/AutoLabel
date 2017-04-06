namespace AutoLabel
{
    static class Watermark
    {
        public static string Image(string txt)
        {
            if (txt == "Coca-cola") return "CocaCola";
            if (txt == "BaikalSea Company") return "BaikalSea";
            return "";
        }
    }
}

//Пропорции логотипа: 1,06 : 1