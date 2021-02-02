//Жаль, такая красота теперь без дела лежит :-)
//Тут кстати ещё и ошибки есть, если пойёт в продакшн, надо чтоб всё проверили
namespace AutoLabel
{
    static class Barcode
    {
        public static string Code(Line lb)
        {
            if (lb.type == "BPF" &      lb.weight == "23")                                  return "2001000054176";

            if (lb.type == "BPF" &      lb.weight == "33,7")                                return "2001000054183";
            if (lb.type == "BPF" &      lb.weight == "34,5")                                return "2001000054183";

            if (lb.type == "BPF" &      lb.weight == "41")                                  return "2001000054213";
            if (lb.type == "BPF" &      lb.weight == "42")                                  return "2001000054213";

            if (lb.type == "PCO/BPF" &  lb.weight == "30" &     lb.color == "Бесцветный")  return "2001000054343";
            if (lb.type == "PCO/BPF" &  lb.weight == "30,7" &   lb.color == "Бесцветный")  return "2001000054343";
            if (lb.type == "PCO/BPF" &  lb.weight == "31,7" &   lb.color == "Бесцветный")  return "2001000054343";

            if (lb.type == "BPF" &      lb.weight == "40" &     lb.color == "Бесцветный")  return "2001000054350";

            if (lb.type == "PCO 1881" & lb.weight == "20,7" &   lb.color == "Бесцветный")  return "2001000054312";

            if (lb.type == "PCO" &      lb.weight == "28,5" &   lb.color == "Бесцветный")  return "2001000054336";

            if (lb.type == "PCO/BPF" &  lb.weight == "21" &     lb.color == "Бесцветный")  return "2001000054329";

            return "";
        }
    }
}
