Console.WriteLine("Hello, Advent Calendar!");

var data = File.ReadAllLines("input.txt");

var runningTotal = 0;

foreach (var line in data)
{
    var lineData = line.Split(':')[1];
    var winners = lineData.Split('|')[0].Split(' ');
    var guesses = lineData.Split('|')[1].Split(' ');

    var winNum = new List<int>();
    var guessNum  = new List<int>();

    foreach (var win in winners) if (!string.IsNullOrWhiteSpace(win)) winNum.Add(int.Parse(win));
    foreach (var guess in guesses) if (!string.IsNullOrWhiteSpace(guess)) guessNum.Add(int.Parse(guess));

    var correctGuesses = 0;

    foreach (var guess in guessNum) if (winNum.Contains(guess)) correctGuesses++;

    if (correctGuesses > 0)
    {
        var score = Math.Pow(2, correctGuesses - 1);
        runningTotal += Convert.ToInt32(score);
    }
}

Console.WriteLine($"The sum of scores is {runningTotal}");

var copies = new Dictionary<int, long>();

for (int i = 0; i < data.Length; i++)
{
    copies[i] = 1;
}

for (int i = 0; i < data.Length; i++)
{
    var line = data[i];
    var lineData = line.Split(':')[1];
    var winners = lineData.Split('|')[0].Split(' ');
    var guesses = lineData.Split('|')[1].Split(' ');

    var winNum = new List<int>();
    var guessNum = new List<int>();

    foreach (var win in winners) if (!string.IsNullOrWhiteSpace(win)) winNum.Add(int.Parse(win));
    foreach (var guess in guesses) if (!string.IsNullOrWhiteSpace(guess)) guessNum.Add(int.Parse(guess));

    var correctGuesses = 0;

    foreach (var guess in guessNum) if (winNum.Contains(guess)) correctGuesses++;
    
    for (int j = i + 1; j <= i + correctGuesses; j++)
    {
        copies[j] = copies[j] + copies[i];
    }

}

long scratchCardTotal = 0;

for (int i = 0; i < data.Length; i++)
{
    scratchCardTotal += copies[i];
}

Console.WriteLine($"The total number of scratchards is {scratchCardTotal}");

