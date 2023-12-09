using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        List<Card> cards = new List<Card>();
        long somme = 0;
        while (line != null)
        {
            Console.WriteLine(line);
            cards.Add(new Card(line));
            line = sr.ReadLine();
        }
        cards.ForEach(c => Console.WriteLine($"{c.numberCard} : Points {c.numberWonCards} Copy :{c.numberCopies}"));
        CopiesWon(cards);
        cards.ForEach(c => Console.WriteLine($"{c.numberCard} : {c.numberCopies}"));
        cards.ForEach(e=>somme+=e.numberCopies);
        Console.WriteLine(somme);
        sr.Close();
    }

    private static void CopiesWon(List<Card> cards) 
    {
        foreach(Card c in cards) 
        {
            Console.WriteLine($"{c.numberCopies}, {c.numberWonCards}");
            foreach(int i in Enumerable.Range(c.numberCard, c.numberWonCards)) 
            {
                Console.WriteLine(i);
                try { cards.ElementAt(i).numberCopies=cards.ElementAt(i).numberCopies+1*c.numberCopies; } catch { }
            }

        }
    }
}

internal class Card 
{
    List<int> winningNumbers;
    List<int> gameNumbers;
    public int numberCard;
    public int numberWonCards;
    public int numberCopies;
    int pow; 
    public Card(string line)
    { 
        int.TryParse(line.Substring(5,line.IndexOf(':')-5),out numberCard);
        winningNumbers = line.Substring(line.IndexOf(':')+1, line.IndexOf('|') - line.IndexOf(':')-1).Trim().Replace("  ", " ").Split(' ').ToList().ConvertAll(e=>int.Parse(e));
        //winningNumbers.ForEach(w=>Console.Write(w+","));Console.Write("|");
        gameNumbers = line.Substring(line.IndexOf('|') + 1).Trim().Replace("  "," ").Split(' ').ToList().ConvertAll(e => int.Parse(e));
        //gameNumbers.ForEach(w => Console.Write(w + ","));
        pow = -1;
        gameNumbers.ForEach(e =>  pow += winningNumbers.Exists(w => w == e)?1:0 );

        numberWonCards = pow+1;
        numberCopies = 1;
        //Console.WriteLine(numberCard);
    }

    public long Points() { return pow<0?0:(long)Math.Pow(2, pow); }
}