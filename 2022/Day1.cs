namespace _2022;

public static class Day1
{
    public static string SolvePartOne(IList<string> input)
    {
        var max = 0;
        var current = 0;

        foreach (var line in input)
        {
            if (int.TryParse(line, out var item))
            {
                current += item;
                if (current > max)
                {
                    max = current;
                }
            }
            else
            {
                current = 0;
            }
        }

        return max.ToString();
    }
    public static string SolvePartTwo(IList<string> input)
    {
        var elfs = new List<int>();
        var current = 0;

        foreach (var line in input)
        {
            if (int.TryParse(line, out var item))
            {
                current += item;
            }
            else
            {
                elfs.Add(current);
                current = 0;
            }
        }

        return elfs.OrderDescending().Take(3).Sum().ToString();
    }
}
