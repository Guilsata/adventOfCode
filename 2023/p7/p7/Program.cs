internal class Program
{
    private static void Main(string[] args)
    {
        {
            string[] lines = File.ReadAllLines(".\\ressources.txt");
            List<Hand> hands = new List<Hand>();
            foreach (string line in lines)
            {
                hands.Add(new Hand(long.Parse(line.Split(' ')[1]), line.Split(' ')[0]));
                hands.Last().CalculateValueWithJoker();
            }
            hands = hands.OrderBy(e => e.value).ThenBy(e => e.valuerank).ToList();
            hands.ForEach(Console.WriteLine);
            long somme = 0;
            int i = 1;
            hands.ForEach(e => somme += e.bid * i++);
            Console.WriteLine(somme);
        }
    }//254837312

    internal class Hand
    {
        public long bid;
        List<long> cards;
        public long value;
        public long valuerank;

        public Hand(long bid, string cards)
        {
            this.bid = bid;
            this.cards = cards.ToArray().ToList().ConvertAll(e => CardsParse(e));
            CalculateValue();
        }

        public Hand(List<long> cards)
        {
            bid = 0;
            this.cards = cards;
            CalculateValue();
        }

        private long CardsParse(char c)
        {
            switch (c)
            {
                case 'T':
                    return 10;
                case 'J':
                    return 1;
                case 'Q':
                    return 11;
                case 'K':
                    return 12;
                case 'A':
                    return 13;
                default:
                    return long.Parse("" + c);
            }
        }

        public override string ToString()
        {
            string r = "";
            cards.ForEach(e => r = r + e + ",");
            r = r.Substring(0, r.Length - 1);
            return $"{r} {bid} :: {value}";
        }


        private void CalculateValue()
        {
            if (IsHightCard()) { value = 0; }
            else if (IsOnePair()) { value = 1; }
            else if (IsTwoPairOrThreeKind())
            {
                value = 2;
                cards.ForEach(e => { if (cards.FindAll(f => e == f).Count() == 3) { value = 3; return; } });
            }
            else if (IsFullHouseOrFourKind())
            {
                value = 4;
                cards.ForEach(e => { if (cards.FindAll(f => e == f).Count() == 4) { value = 5; return; } });
            }
            else { value = 6; }

            valuerank = 0;
            cards.ForEach(e => valuerank = valuerank * 15 + e);

        }

        public void CalculateValueWithJoker()
        {
            if (cards.Contains(1))
            {
                foreach (int i in Enumerable.Range(2, 12))
                {
                    List<long> tmp = new List<long>();
                    cards.ForEach(e => { if (e == 1) { tmp.Add(i); } else { tmp.Add(e); } });
                    Hand htmp = new Hand(tmp);
                    if (htmp.value > value) { value = htmp.value; }
                }
            }

        }
        private bool IsHightCard()
        {
            List<long> tmp = cards.ToHashSet().ToList();
            return tmp.Count == 5;
        }

        private bool IsOnePair()
        {
            List<long> tmp = cards.ToHashSet().ToList();
            return tmp.Count == 4;
        }

        private bool IsTwoPairOrThreeKind()
        {
            List<long> tmp = cards.ToHashSet().ToList();
            return tmp.Count == 3;
        }

        private bool IsFullHouseOrFourKind()
        {
            List<long> tmp = cards.ToHashSet().ToList();
            return tmp.Count == 2;
        }
    }
}