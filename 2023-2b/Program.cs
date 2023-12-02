// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var data = File.ReadAllLines("input.txt");


var games = new List<Game>();

foreach (var line in data)
{
    games.Add(ParseLine(line));
}

var gamePowerSum = 0;

foreach (var game in games)
{
    var (red, green, blue) = (0, 0, 0);
    foreach (var round in game.Rounds)
    {
        if (round.Red > red) red = round.Red;
        if (round.Green > green) green = round.Green;
        if (round.Blue > blue) blue = round.Blue;
    }
    gamePowerSum += red * green * blue;
}

Console.WriteLine($"The sum of cube powers is {gamePowerSum}");


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
