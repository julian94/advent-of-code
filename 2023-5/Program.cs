// See https://aka.ms/new-console-template for more information
using System.Data;

Console.WriteLine("Hello, World!");
var watch = System.Diagnostics.Stopwatch.StartNew();
var data = File.ReadAllLines("input.txt");

watch.Stop();
Console.WriteLine($"Reading file took {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();

var mapSets = new List<List<Map>>();

foreach (var line in data.Skip(1))
{
    if (line.Contains(':'))
    {
        mapSets.Add(new List<Map>());
    }
    else if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }
    else
    {
        mapSets.Last().Add(new Map(line));
    }
}

watch.Stop();
Console.WriteLine($"Preparing maps took {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();

var seeds = data[0].Split(':')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => long.Parse(s)).ToList();

var locations = new List<long>();

foreach (var seed in seeds)
{
    var currentNumber = seed;
    foreach (var mapSet in mapSets)
    {
        foreach (var map in mapSet)
        {
            if (map.TryMap(currentNumber, out var newNumber))
            {
                currentNumber = newNumber;
                break;
            }
        }
    }
    locations.Add(currentNumber);
}

var lowestLocation = locations.Min();


watch.Stop();
Console.WriteLine($"Calculating easy seeds took {watch.Elapsed.TotalMilliseconds}ms");

Console.WriteLine($"The lowest location number is {lowestLocation}");
watch.Restart();


var seedsTwo = new List<long>();

var ranges = new List<Range>();

for (int i = 0; i < seeds.Count; i += 2)
{
    var start = seeds[i];
    var range = seeds[i + 1];
    ranges.Add(new(start, range));
}

var nextRanges = new List<Range>();
nextRanges.AddRange(ranges);

foreach (var mapSet in mapSets)
{
    var ms = new MapSet(mapSet);
    var splitRanges = new List<Range>();
    foreach (var range in nextRanges)
    {
        var r = range;
        //foreach(var map in ms.Maps)
        for (int i = 0; i < ms.Maps.Count; i++)
        {
            var (left, middle, right) = ms.Maps[i].GetRanges(r);

            if (left is not null)
            {
                splitRanges.Add(left);
            }
            if (middle is not null)
            {
                splitRanges.Add(middle);
            }
            if (right is not null)
            {
                r = right;
                if (i == ms.Maps.Count - 1)
                {
                    splitRanges.Add(r);
                }
            }
            else
            {
                break;
            }
        }
    }

    nextRanges.Clear();
    foreach (var range in splitRanges)
    {
        nextRanges.Add(ms.CalculateRange(range));
    }
    Console.WriteLine($"The current lowest number is {nextRanges.OrderBy(r => r.Start).First().Start}");
}

/*
foreach (var mapSet in mapSets)
{
    var ms = new MapSet(mapSet);
    var splitRanges = new List<Range>();
    foreach (var range in nextRanges)
    {
        splitRanges.AddRange(ms.SplitRange(range));
    }

    nextRanges.Clear();
    foreach (var range in splitRanges)
    {
        nextRanges.Add(ms.CalculateRange(range));
    }
    Console.WriteLine($"The current lowest number is {nextRanges.OrderBy(r => r.Start).First().Start}");
}
*/
var lowestLocationTwo = nextRanges.OrderBy(r => r.Start).First().Start;

/*
for (int i = 0; i < seeds.Count; i +=2)
{
    var start = seeds[i];
    var range = seeds[i + 1];
    for (var j = start; start < start + range; j++)
    {
        var currentNumber = j;
        foreach (var mapSet in mapSets)
        {
            foreach (var map in mapSet)
            {
                if (map.TryMap(currentNumber, out var newNumber))
                {
                    currentNumber = newNumber;
                    break;
                }
            }
        }
        if (currentNumber < lowestLocationTwo)
        {
            lowestLocation = currentNumber;
        }
    }
}
*/

watch.Stop();
Console.WriteLine($"The truly lowest location number is {lowestLocationTwo}");
Console.WriteLine($"Calculating hard seeds took {watch.Elapsed.TotalMilliseconds}ms");

// 302918330 >
// 1181555926 >
// 100644325 >
// 6683080 !=
// 37806486 ====

class Range(long start, long range)
{
    public long Start { get; set; } = start;
    public long Length { get; set; } = range;

    public long End { get =>  Start + Length - 1; }
}

class Map
{
    public long Source { get; set; }
    public long Destination { get; set; }
    public long Length { get; set; }

    public long SourceEnd { get => Source + Length - 1; }
    public long DestinationEnd { get => Destination + Length - 1; }

    public Map(string data)
    {
        var parts = data.Split();
        Destination = long.Parse(parts[0]);
        Source = long.Parse(parts[1]);
        Length = long.Parse(parts[2]);
    }

    public bool TryMap(long number, out long result)
    {
        if (number >= Source && number <= Source + Length)
        {
            result = Destination + (number - Source);
            return true;
        }
        else
        {
            result = 0;
            return false;
        }
    }
    public bool TryRange(Range range, out Range? result)
    {
        if (range.Start >= Source && range.End <= SourceEnd)
        {
            var offset = range.Start - Source;
            result = new Range(Destination + offset, range.Length);
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    public (Range? left, Range? middle, Range? right) GetRanges(Range input)
    {
        if (input.Start < Source)
        {
            if (input.End < Source)
            {
                return (input, null, null);
            }
            else if (input.End <= SourceEnd)
            {
                return (
                    new Range(input.Start, (Source - input.Start)),
                    new Range(Source, (input.End - Source)) ,
                    null
                    );
            }
            else
            {
                return (
                    new Range(input.Start, (Source - input.Start)),
                    new Range(Source, Length),
                    new Range((input.Start + Length), (input.Length - Length - (Source - input.Start)))
                    );
            }
        }
        else if (input.Start <= SourceEnd)
        {
            if (input.End <= SourceEnd)
            {
                return (null, input, null);
            }
            else
            {
                return (
                    null,
                    new Range(input.Start, (SourceEnd - input.Start)),
                    new Range(SourceEnd, (input.Length - (SourceEnd - input.Start)))
                    );
            }
        }
        else
        {
            return (null, null, input);
        }
    }
}

class MapSet
{
    public List<Map> Maps { get; set; }

    public MapSet(List<Map> maps)
    {
        Maps = maps.OrderBy(m => m.Source).ToList();
    }

    public List<Range> SplitRange(Range range)
    {
        var result = new List<Range>();

        var start = range.Start;
        var end = range.End;
        for (int i = 0; i < Maps.Count; i++)
        {
            if (start < Maps[i].Source)
            {
                if (end < Maps[i].Source)
                {
                    result.Add(range);
                    return result;
                }
                else if (start <= Maps[i].SourceEnd)
                {
                    result.Add(new Range(start, Maps[i].Source - start));
                    result.Add(new Range(Maps[i].Source, end - Maps[i].Source));
                    if (end > Maps[i].SourceEnd)
                    {
                        start = Maps[i].SourceEnd;
                    }
                }
            }
            else if (start < Maps[i].SourceEnd)
            {
                if (end < Maps[i].SourceEnd)
                {
                    result.Add(new Range(start, Maps[i].SourceEnd - start));
                    start = Maps[i].SourceEnd;
                }
            }
        }
        return result;
    }

    public Range CalculateRange(Range range)
    {
        for (int i = 0; i < Maps.Count; i++)
        {
            if (Maps[i].TryRange(range, out var result))
            {
                return result;
            }
        }
        return range;
    }
}
