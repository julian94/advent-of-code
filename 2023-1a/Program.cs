// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var data = File.ReadAllLines("input.txt");

var sum = 0;

for (int h = 0; h < data.Length; h++)
{
    var chars = data[h];
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
    for (int i = chars.Length -1; i >= 0; i--)
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
// The sum is: 54951
