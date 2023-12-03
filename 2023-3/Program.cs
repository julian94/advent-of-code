// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var data = File.ReadAllLines("input.txt");

var partNumberSum = 0;
var gearRatioSum = 0;
var gears = new Dictionary<string, int>();

for (int y = 0; y < data.Length; y++)
{
    for (int x = 0; x < data[y].Length; x++)
    {
        if (int.TryParse(data[y][x].ToString(), out _))
        {
            // determine length of number
            var length = 1;
            if (x+1 < data[y].Length && int.TryParse(data[y][x+1].ToString(), out _))
            {
                length = 2;

                if (x + 2 < data[y].Length && int.TryParse(data[y][x + 2].ToString(), out _))
                {
                    length = 3;
                }
            }
            var actualNum = int.Parse(data[y].Substring(x, length));

            // bounding box = x-1,y-1, x+length,y+1

            var partFound = false;
            for (int xx = x-1; xx <= x+length; xx++)
            {
                for (int yy = y-1; yy <= y+1; yy++)
                {
                    // Don't check stuff that is out of range
                    if (xx >= 0 && yy >= 0 && xx < data[y].Length && yy < data.Length)
                    {
                        var c = data[yy][xx];
                        if (!".0123456789".Contains(c))
                        {
                            if (c == '*')
                            {
                                var label = $"{xx},{yy}";
                                if (gears.ContainsKey(label))
                                {
                                    gearRatioSum += (actualNum * gears[label]);
                                }
                                else
                                {
                                    gears[label] = actualNum;
                                }
                            }

                            partFound = true;
                            goto Found;
                        }
                    }
                }
            }
            Found:
                if (partFound)
            {
                partNumberSum += actualNum;
            }

            x += length - 1;
        }
    }
}

Console.WriteLine($"The sum of engine parts is: {partNumberSum}");
// Wrongs:
// 353269 <
// 588209 >
// Correct: 549908

Console.WriteLine($"The sum of gear ratios is: {gearRatioSum}");
