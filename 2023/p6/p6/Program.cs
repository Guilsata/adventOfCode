using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        Regex rgx = new Regex(@"\s+");
        long times = long.Parse(rgx.Replace(line.Substring(line.IndexOf(':') + 1), ""));
        
        line = sr.ReadLine();
        long dists = long.Parse(rgx.Replace(line.Substring(line.IndexOf(':') + 1), ""));
        Console.Write("Times ! ");
        Console.Write(times);
        Console.WriteLine();
        Console.Write("Dist ! ");
        Console.Write(dists);
        Console.WriteLine();
        long lli = 0;
        long j = 1;
        while (j < times) 
        {
            if (j * (times - j) > dists) 
            {
                lli++;
            }
            j++;

        }
        
        Console.WriteLine(lli);
        sr.Close();
    }
}