namespace _2022;

public static class Day2
{
    public static string SolvePartOne(IList<string> input)
    {
        var score = 0;

        foreach (var item in input.Where(s => !string.IsNullOrWhiteSpace(s)))
        {
            var people = item.Split();
            score += CalculateScoreOne(people[0], people[1]);
        }

        return score.ToString();
    }

    public static string SolvePartTwo(IList<string> input)
    {
        var score = 0;

        foreach (var item in input.Where(s => !string.IsNullOrWhiteSpace(s)))
        {
            var people = item.Split();
            score += CalculateScoreTwo(people[0], people[1]);
        }

        return score.ToString();
    }

    private static int CalculateScoreOne(string elf, string me)
    {
        var score = me switch
        {
            "X" => 1,
            "Y" => 2,
            "Z" => 3,
            _ => throw new Exception("That bloody elf tricked me!")
        };

        score += (elf, me) switch
        {
            ("A", "X") => 3,
            ("A", "Y") => 6,
            ("A", "Z") => 0,
            ("B", "X") => 0,
            ("B", "Y") => 3,
            ("B", "Z") => 6,
            ("C", "X") => 6,
            ("C", "Y") => 0,
            ("C", "Z") => 3,
            _ => throw new Exception("That bloody elf tricked me!")
        };
        return score;
    }
    private static int CalculateScoreTwo(string elf, string me)
    {
        var score = (elf, me) switch
        {
            ("A", "X") => 3, //Lose
            ("A", "Y") => 4, //Draw
            ("A", "Z") => 8, //Win
            ("B", "X") => 1,
            ("B", "Y") => 5,
            ("B", "Z") => 9,
            ("C", "X") => 2,
            ("C", "Y") => 6,
            ("C", "Z") => 7,
            _ => throw new Exception("That bloody elf tricked me!")
        };
        return score;
    }
}
