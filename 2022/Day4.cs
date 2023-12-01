using System.Diagnostics.CodeAnalysis;

namespace _2022;

public static class Day4
{
    public static string SolvePartOne(IList<string> input)
    {
        var sum = 0;

        foreach (var pair in input)
        {
            var elves = pair.Split(',');
            var left = new Line(elves[0]);
            var right = new Line(elves[1]);
            if (left.WhollyOverlaps(right))
            {
                sum++;
            }
        }

        return sum.ToString();
    }
    public static string SolvePartTwo(IList<string> input)
    {
        var sum = 0;

        foreach (var pair in input)
        {
            var elves = pair.Split(',');
            var left = new Line(elves[0]);
            var right = new Line(elves[1]);
            if (left.PartlyOverlaps(right))
            {
                sum++;
            }
        }

        return sum.ToString();
    }


    public struct Line
    {
        public int Left;
        public int Right;

        public Line(string data)
        {
            var parts = data.Split("-");
            Left = int.Parse(parts[0]);
            Right = int.Parse(parts[1]);
        }

        public bool Contains(Line other)
        {
            return (Left <= other.Left && Right >= other.Right);
        }

        public bool IsContainedBy(Line other) => other.Contains(this);

        public bool WhollyOverlaps(Line other)
        {
            return Contains(other) || IsContainedBy(other);
        }
        public bool PartlyOverlaps(Line other)
        {
            return (Left <= other.Right && Right >= other.Right) || (other.Left <= Right && other.Right >= Right);
        }
    }
}
