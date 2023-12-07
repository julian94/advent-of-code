// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Text;

Console.WriteLine("Hello, World!");

var data = File.ReadAllLines("input.txt");

var cardValues = new Dictionary<char, int>()
{
    { '2', 0 },
    { '3', 1 },
    { '4', 2 },
    { '5', 3 },
    { '6', 4 },
    { '7', 5 },
    { '8', 6 },
    { '9', 7 },
    { 'T', 8 },
    { 'J', 9 },
    { 'Q', 10 },
    { 'K', 11 },
    { 'A', 12 },
};


var hands = new List<Hand>();

foreach (var line in data)
{
    var parts = line.Split(' ');
    var cards = parts[0];
    var bid = int.Parse(parts[1]);

    var hand = new Hand
    {
        Bid = bid,
        Cards = new(),
    };

    foreach (var card in cards)
    {
        hand.Cards.Add(cardValues[card]);
    }
    hands.Add(hand);
}

var sortedHands = hands.OrderBy(h => h.GetValue()).ToList();

long total = 0;

for (int i = 0; i < sortedHands.Count(); i++)
{
    total += (i + 1) * sortedHands[i].Bid;
}

Console.WriteLine($"The total value is {total}");
// 254085819 >


enum HandType
{
    HighCard = 0,
    OnePair = 1,
    TwoPair = 2,
    Three = 3,
    FullHouse = 4,
    Four = 5,
    Five = 6,
}

struct Hand
{
    public List<int> Cards { get; set; }

    public int Bid;

    public HandType GetHandType()
    {
        var distinctCards = Cards.Distinct().ToList();
        if (distinctCards.Count() == 5) return HandType.HighCard;
        if (distinctCards.Count() == 4) return HandType.OnePair;
        if (distinctCards.Count() == 1) return HandType.Five;

        var sortedCards = Cards.Order().ToList();
        var pairs = 0;
        var triplets = 0;
        foreach (var card in distinctCards)
        {
            if (Cards.Where(c => c == card).Count() == 3) triplets++;
            else if (Cards.Where(c => c == card).Count() == 2) pairs++;
        }

        if (triplets == 1)
        {
            if (pairs == 1) return HandType.FullHouse;
            else return HandType.Three;
        }
        else
        {
            if (pairs == 1) return HandType.OnePair;
            else return HandType.TwoPair;
        }
    }

    public long GetValue()
    {
        var value = 0;
        var mult = 1;

        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            value += (Cards[i] * mult);
            mult *= 13;
        }
        value += ((int)GetHandType() * mult);

        return value;
    }

    public static bool operator <(Hand left, Hand right)
    {
        if (left.GetHandType() < right.GetHandType()) return true;
        if (left.GetHandType() == right.GetHandType())
        {
            for (var i = 0; i < 5; i++)
            {
                if (left.Cards[i] < right.Cards[i]) return true;
            }
        }
        return false;
    }
    public static bool operator >(Hand left, Hand right)
    {
        if (left.GetHandType() > right.GetHandType()) return true;
        if (left.GetHandType() == right.GetHandType())
        {
            for (var i = 0; i < 5; i++)
            {
                if (left.Cards[i] > right.Cards[i]) return true;
            }
        }
        return false;
    }
}
