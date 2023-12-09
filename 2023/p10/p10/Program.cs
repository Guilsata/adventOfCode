internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        while (line != null)
        {
            Console.WriteLine(line);
            line = sr.ReadLine();
        }
        sr.Close();
    }
}