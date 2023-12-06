// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

var input = """
    Time:        56     97     78     75
    Distance:   546   1927   1131   1139
    
    """;

var races = new List<(int time, int distance)>()
{
    (56, 546),
    (97, 1927),
    (78, 1131),
    (75, 1139),
};

long product = 1;


foreach (var race in races)
{
    var winningCombos = 0;
    for (var i = 1; i < race.time; i++)
    {
        if (i * (race.time - i) > race.distance)
        {
            winningCombos++;
        }
    }
    Console.WriteLine($"The winningCombos are {winningCombos}");
    product *= winningCombos;
}

Console.WriteLine($"The product is {product}");
// 1286376 < 
// 2979970200 >
// 2852131680 >
// 1624896 ==


{
    (long time, long distance) = (56977875, 546192711311139);
    var winningCombos = 0;
    for (long i = 1; i < time; i++)
    {
        if (i * (time - i) > distance)
        {
            winningCombos++;
        }
    }
    Console.WriteLine($"The winningCombos are {winningCombos}");
    // 32583852 ==
}

