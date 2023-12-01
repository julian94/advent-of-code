namespace _2022;

public static class Day5
{
    public static string SolvePartOne(IList<string> input)
    {
        var stacks = BuildStacks();

        var data = input.Skip(10).ToList();

        for (int i = 0; i < data.Count(); i++)
        {
            // move 2 from 4 to 2
            var parts = data[i].Split();

            var amount = int.Parse(parts[1]);
            var start = int.Parse(parts[3]) -1;
            var end = int.Parse(parts[5]) -1;

            for (int j = 0; j < amount; j++)
            {
                stacks[end].Push(stacks[start].Pop());
            }
        }

        var result = "";
        for (int i = 0; i < stacks.Count; i++)
        {
            result += stacks[i].Peek();
        }
        return result;
    }
    public static string SolvePartTwo(IList<string> input)
    {
        var stacks = BuildStacks();

        var data = input.Skip(10).ToList();

        for (int i = 0; i < data.Count(); i++)
        {
            // move 2 from 4 to 2
            var parts = data[i].Split();

            var amount = int.Parse(parts[1]);
            var start = int.Parse(parts[3]) - 1;
            var end = int.Parse(parts[5]) - 1;

            var tempstack = new Stack("");

            for (int j = 0; j < amount; j++)
            {
                tempstack.Push(stacks[start].Pop());
            }

            for (int j = 0; j < amount; j++)
            {
                stacks[end].Push(tempstack.Pop());
            }
        }


        var result = "";
        for (int i = 0; i < stacks.Count; i++)
        {
            result += stacks[i].Peek();
        }
        return result;
    }

    public static List<Stack> BuildStacks()
    {

        /*  [G]         [P]         [M]    
            [V]     [M] [W] [S]     [Q]    
            [N]     [N] [G] [H]     [T] [F]
            [J]     [W] [V] [Q] [W] [F] [P]
        [C] [H]     [T] [T] [G] [B] [Z] [B]
        [S] [W] [S] [L] [F] [B] [P] [C] [H]
        [G] [M] [Q] [S] [Z] [T] [J] [D] [S]
        [B] [T] [M] [B] [J] [C] [T] [G] [N]
         1   2   3   4   5   6   7   8   9 
        */
        var list = new List<Stack>()
        {
            new("BGSC"),
            new("TMWHJNVG"),
            new("MQS"),
            new("BSLTWNM"),
            new("JZFTVGWP"),
            new("CTBGQHS"),
            new("TJPBW"),
            new("GDCZFTQM"),
            new("NSHBPF"),
        };

        return list;
    }
}

public class Stack
{
    private List<string> Pile { get; init; }

    public Stack(string pile)
    {
        Pile = pile.AsEnumerable().Select(c => c.ToString()).ToList();
    }

    public void Push(string crate)
    {
        Pile.Add(crate);
    }

    public string Pop()
    {
        var crate = Pile.Last();
        Pile.RemoveAt(Pile.Count - 1);
        return crate;
    }

    public string Peek()
    {
        return Pile.Last();
    }
}
