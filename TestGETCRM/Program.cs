using System.Text;

internal class Attraction
{
    public string Name { get; private set; }
    public double TimeRequired { get; private set; }
    public int Importance { get; private set; }

    public Attraction(string name, double timeRequired, int importance)
    {
        Name = name;
        TimeRequired = timeRequired;
        Importance = importance;
    }
}

class Program
{
    static void Main()
    {
        //Console.OutputEncoding = Encoding.UTF8;
        List<Attraction> attractions =
        [
            new("Исаакиевский собор", 5, 10),
            new("Эрмитаж", 8, 11),
            new("Кунсткамера", 3.5, 4),
            new("Петропавловская крепость", 10, 7),
            new("Ленинградский зоопарк", 9, 15),
            new("Медный всадник", 1, 17),
            new("Казанский собор", 4, 3),
            new("Спас на Крови", 2, 9),
            new("Зимний дворец Петра I", 7, 12),
            new("Зоологический музей", 5.5, 6),
            new("Музей обороны и блокады Ленинграда", 2, 19),
            new("Русский музей", 5, 8),
            new("Навестить друзей", 12, 20),
            new("Музей восковых фигур", 2, 13),
            new("Литературно-мемориальный музей Ф.М. Достоевского", 4, 2),
            new("Екатерининский дворец", 1.5, 5),
            new("Петербургский музей кукол", 1, 14),
            new("Музей микроминиатюры «Русский Левша»", 3, 18),
            new("Всероссийский музей А.С. Пушкина и филиалы", 6, 1),
            new ("Музей современного искусства Эрарта", 7, 16)
        ];

        double availableTime = 32; //48 - 16 часов сна

        var selectedAttractions = FindBestRoute(attractions, availableTime, out double sumTime);
        Console.WriteLine("Оптимальный маршрут:");
        foreach (var attraction in selectedAttractions)
        {
            Console.WriteLine($"{attraction.Name} - {attraction.TimeRequired}ч, важность: {attraction.Importance}");
        }
        Console.WriteLine($"Суммарное потраченное время: {sumTime}");
    }

    static List<Attraction> FindBestRoute(List<Attraction> attractions, double maxTime, out double sumTime)
    {
        int n = attractions.Count;
        int W = (int)(maxTime * 10); // Умножение для перевода в int, упрощения работы с индексами (время посещения)

        int[,] dp = new int[n + 1, W + 1]; //Таблица важности посещения достопримечательностей

        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= W; w++)
            {
                if (i == 0 || w == 0)
                    dp[i, w] = 0;
                else
                {
                    int timeAsInt = (int)(attractions[i - 1].TimeRequired * 10);
                    if (timeAsInt <= w)
                        dp[i, w] = Math.Max(attractions[i - 1].Importance + dp[i - 1, w - timeAsInt], dp[i - 1, w]);
                    else
                        dp[i, w] = dp[i - 1, w];
                }
            }
        }

        List<Attraction> selected = [];
        int remainingTime = W;
        for (int i = n; i > 0; i--)
        {
            if (dp[i, remainingTime] != dp[i - 1, remainingTime])
            {
                selected.Add(attractions[i - 1]);
                remainingTime -= (int)(attractions[i - 1].TimeRequired * 10);
            }
        }
        sumTime = 36.0 - remainingTime;

        selected.Reverse();
        return selected;
    }
}