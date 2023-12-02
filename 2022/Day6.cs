namespace _2022;

public static class Day6
{
    public static string SolvePartOne(IList<string> input)
    {
        var markerSize = 4;
        var stack = input.First().ToList().Take(markerSize).ToList();

        var current = markerSize;
        var list = input.First().ToList().Skip(markerSize).ToList();

        while (stack.Distinct().Count() < markerSize)
        {
            stack.RemoveAt(0);
            stack.Add(list[current]);
            current++;
        }

        return (current + markerSize).ToString();
    }

    public static string SolvePartTwo(IList<string> input)
    {
        var markerSize = 14;
        var stack = input.First().ToList().Take(markerSize).ToList();

        var current = markerSize;
        var list = input.First().ToList().Skip(markerSize).ToList();

        while (stack.Distinct().Count() < markerSize)
        {
            stack.RemoveAt(0);
            stack.Add(list[current]);
            current++;
        }

        return (current + markerSize).ToString();
    }
}
