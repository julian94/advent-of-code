// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var data = File.ReadAllLines("input.txt");


var games = new List<Game>();

foreach (var line in data)
{
    games.Add(ParseLine(line));
}

var gamenumberSum = 0;
var biggestPossibleRound = new Round
{
    Red = 12,
    Green = 13,
    Blue = 14,
};

foreach (var game in games)
{
    var possible = true;
    foreach (var round in game.Rounds)
    {
        if (!(round <= biggestPossibleRound))
        {
            possible = false; 
            break;
        }
    }
    if (possible)
    {
        gamenumberSum += game.Number;
    }
}

Console.WriteLine($"The sum of possible games is {gamenumberSum}");


Game ParseLine(string line)
{
    var bigParts = line.Split(':');
    var number = int.Parse(bigParts[0].Split()[1]);

    var game = new Game
    {
        Number = number,
        Rounds = [],
    };

    var roundStrings = bigParts[1].Split(";");

    foreach (var roundString in roundStrings)
    {
        var cubes = roundString.Split(',');

        var round = new Round
        {
            Red = 0,
            Green = 0,
            Blue = 0,
        };
        
        foreach (var c in cubes)
        {
            var countString = c.Trim().Split()[0].Trim();
            var count = int.Parse(countString);
            if (c.Contains("red"))
            {
                round.Red = count;
            }
            else if (c.Contains("green"))
            {
                round.Green = count;
            }
            else if (c.Contains("blue"))
            {
                round.Blue = count;
            }
        }
        game.Rounds.Add(round);
    }
    return game;
}

struct Round
{
    public int Red;
    public int Green;
    public int Blue;

    public static bool operator <=(Round left, Round right)
    {
        return (left.Red <= right.Red && left.Green <= right.Green && left.Blue <= right.Blue);
    }
    public static bool operator >=(Round left, Round right)
    {
        return (left.Red >= right.Red && left.Green >= right.Green && left.Blue >= right.Blue);
    }
}
struct Game
{
    public int Number;
    public List<Round> Rounds;
}
