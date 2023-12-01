// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var data = File.ReadAllLines("input.txt");

var sum = 0;

/* One
 * Two
 * Three
 * Four
 * Five
 * Six
 * Seven
 * Eight
 * Nine
 * NOT zero
 * 
 * NOTE:
 * twone
 * eightwo
 */

for (int h = 0; h < data.Length; h++)
{
    var chars = ReplaceStringNumbers(data[h]);
    var first = 0;
    var second = 0;
    for (int i = 0; i < chars.Length; i++)
    {
        if (int.TryParse(chars[i].ToString(), out var result))
        {
            first = result;
            break;
        }
    }
    for (int i = chars.Length - 1; i >= 0; i--)
    {
        if (int.TryParse(chars[i].ToString(), out var result))
        {
            second = result;
            break;
        }
    }

    Console.WriteLine($"{first}{second}: {data[h]}");
    var number = int.Parse($"{first}{second}");
    sum += number;
}

Console.WriteLine($"The sum is: {sum}");
// The sum is: 55218

string ReplaceStringNumbers(string input) => input
    .Replace("twone",   "21")
    .Replace("eightwo", "82")
    .Replace("oneight", "18")
    .Replace("one",     "1")
    .Replace("two",     "2")
    .Replace("three",   "3")
    .Replace("four",    "4")
    .Replace("five",    "5")
    .Replace("six",     "6")
    .Replace("seven",   "7")
    .Replace("eight",   "8")
    .Replace("nine",    "9");
