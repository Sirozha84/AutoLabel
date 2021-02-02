//Жаль, такая красота теперь без дела лежит :-)
//Тут кстати ещё и ошибки есть, если пойёт в продакшн, надо чтоб всё проверили
namespace AutoLabel
{
    static class Barcode
    {
        public static string Code(Line lb)
        {
            if (lb.Type == "BPF" &      lb.Weight == "23")                                  return "2001000054176";

            if (lb.Type == "BPF" &      lb.Weight == "33,7")                                return "2001000054183";
            if (lb.Type == "BPF" &      lb.Weight == "34,5")                                return "2001000054183";

            if (lb.Type == "BPF" &      lb.Weight == "41")                                  return "2001000054213";
            if (lb.Type == "BPF" &      lb.Weight == "42")                                  return "2001000054213";

            if (lb.Type == "PCO/BPF" &  lb.Weight == "30" &     lb.PColor == "Бесцветный")  return "2001000054343";
            if (lb.Type == "PCO/BPF" &  lb.Weight == "30,7" &   lb.PColor == "Бесцветный")  return "2001000054343";
            if (lb.Type == "PCO/BPF" &  lb.Weight == "31,7" &   lb.PColor == "Бесцветный")  return "2001000054343";

            if (lb.Type == "BPF" &      lb.Weight == "40" &     lb.PColor == "Бесцветный")  return "2001000054350";

            if (lb.Type == "PCO 1881" & lb.Weight == "20,7" &   lb.PColor == "Бесцветный")  return "2001000054312";

            if (lb.Type == "PCO" &      lb.Weight == "28,5" &   lb.PColor == "Бесцветный")  return "2001000054336";

            if (lb.Type == "PCO/BPF" &  lb.Weight == "21" &     lb.PColor == "Бесцветный")  return "2001000054329";

            return "";
        }
    }
}
