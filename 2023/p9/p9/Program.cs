using System.Net.Http.Headers;

internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        long somme = 0;
        while (line != null)
        {
            Console.WriteLine(line);
            List<Sequence> sequences = new List<Sequence>();
            sequences.Add(new Sequence(line.Split(' ').ToList().ConvertAll(long.Parse)));
            while (!sequences.Last().AllZero()) 
            {
                sequences.Add(sequences.Last().GenerateSubSeq());
            }
            //Faire cette somme pour la première solution
            //sequences.ForEach(e => somme += e.Last());

            sequences.Reverse();
            long tmp = 0;
            sequences.ForEach(e=> { tmp = e.First() - tmp; });
            somme += tmp;
            line = sr.ReadLine();
        }
        Console.WriteLine(somme);
        sr.Close();
    }
}

internal class Sequence 
{
    List<long> seq;

    public long Last() { return seq.Last(); }

    public long First() { return seq.First(); }

    public bool AllZero() 
    {
        return seq.Sum() == 0;
    }

    public Sequence() 
    {
        seq = new List<long>();
    }

    public Sequence(List<long> seq2)
    {
        seq = seq2;
    }

    public Sequence GenerateSubSeq() 
    {
        int i = 1;
        List<long> seq2 = new List<long>();
        while (i < seq.Count) 
        {
            seq2.Add(seq[i] - seq[i-1]);
            i++;
        }
        return new Sequence(seq2);
    }
}