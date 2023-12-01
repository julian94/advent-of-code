namespace _2022;

public static class Day3
{
    public static string SolvePartOne(IList<string> input)
    {
        var priorities = new Dictionary<string, int>()
        {
            { "a", 1 },
            { "b", 2 },
            { "c", 3 },
            { "d", 4 },
            { "e", 5 },
            { "f", 6 },
            { "g", 7 },
            { "h", 8 },
            { "i", 9 },
            { "j", 10 },
            { "k", 11 },
            { "l", 12 },
            { "m", 13 },
            { "n", 14 },
            { "o", 15 },
            { "p", 16 },
            { "q", 17 },
            { "r", 18 },
            { "s", 19 },
            { "t", 20 },
            { "u", 21 },
            { "v", 22 },
            { "w", 23 },
            { "x", 24 },
            { "y", 25 },
            { "z", 26 },
            { "A", 27 },
            { "B", 28 },
            { "C", 29 },
            { "D", 30 },
            { "E", 31 },
            { "F", 32 },
            { "G", 33 },
            { "H", 34 },
            { "I", 35 },
            { "J", 36 },
            { "K", 37 },
            { "L", 38 },
            { "M", 39 },
            { "N", 40 },
            { "O", 41 },
            { "P", 42 },
            { "Q", 43 },
            { "R", 44 },
            { "S", 45 },
            { "T", 46 },
            { "U", 47 },
            { "V", 48 },
            { "W", 49 },
            { "X", 50 },
            { "Y", 51 },
            { "Z", 52 },
        };

        var sum = 0;

        foreach (var rucksack in input)
        {
            sum += priorities[FindOverlap(rucksack)];
        }

        return sum.ToString();
    }
    public static string SolvePartTwo(IList<string> input)
    {
        var priorities = new Dictionary<string, int>()
        {
            { "a", 1 },
            { "b", 2 },
            { "c", 3 },
            { "d", 4 },
            { "e", 5 },
            { "f", 6 },
            { "g", 7 },
            { "h", 8 },
            { "i", 9 },
            { "j", 10 },
            { "k", 11 },
            { "l", 12 },
            { "m", 13 },
            { "n", 14 },
            { "o", 15 },
            { "p", 16 },
            { "q", 17 },
            { "r", 18 },
            { "s", 19 },
            { "t", 20 },
            { "u", 21 },
            { "v", 22 },
            { "w", 23 },
            { "x", 24 },
            { "y", 25 },
            { "z", 26 },
            { "A", 27 },
            { "B", 28 },
            { "C", 29 },
            { "D", 30 },
            { "E", 31 },
            { "F", 32 },
            { "G", 33 },
            { "H", 34 },
            { "I", 35 },
            { "J", 36 },
            { "K", 37 },
            { "L", 38 },
            { "M", 39 },
            { "N", 40 },
            { "O", 41 },
            { "P", 42 },
            { "Q", 43 },
            { "R", 44 },
            { "S", 45 },
            { "T", 46 },
            { "U", 47 },
            { "V", 48 },
            { "W", 49 },
            { "X", 50 },
            { "Y", 51 },
            { "Z", 52 },
        };

        var sum = 0;

        for (int i = 0; i < input.Count; i += 3) {
            var badge = FindBadge(input[i], input[i + 1], input[i + 2]);
            sum += priorities[badge];
        }

        return sum.ToString();
    }

    public static string FindOverlap(string rucksack)
    {
        var (left, right) = SplitRucksack(rucksack);

        var distinctLeft = left.Distinct();
        var distinctRight = right.Distinct();


        foreach (var item in distinctLeft)
        {
            if (distinctRight.Contains(item))
            {
                return item.ToString();
            }
        }
        throw new Exception("Problem");
    }

    public static (string left, string right) SplitRucksack(string rucksack)
    {
        var half = rucksack.Length / 2;

        return (rucksack[..half], rucksack[half..]);
    }

    public static string FindBadge(string a, string b, string c)
    {
        var da = a.Distinct();
        var db = b.Distinct();
        var dc = c.Distinct();


        foreach (var item in da)
        {
            if (db.Contains(item))
            {
                if (dc.Contains(item))
                {
                    return item.ToString();
                }
            }
        }
        throw new Exception("Problem");
    }
}
