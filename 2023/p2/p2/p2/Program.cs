using System;

internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        int s = 0;
        int i = 1;
        while (line != null) 
        {
            Game t = new Game(line);
            s += t.PowerGame();
            line = sr.ReadLine();
            i++;
        }
        Console.WriteLine(s);
        sr.Close();
    }
}

internal class Game 
{
    //Game 1: 8 green, 4 red, 4 blue; 1 green, 6 red, 4 blue; 7 red, 4 green, 1 blue; 2 blue, 8 red, 8 green
    List<Fist> fists = new List<Fist>();
    int mGreen = 0;
    int mRed = 0;
    int mBlue = 0;

    public Game(string line) 
    {
        string working = line.Substring(line.IndexOf(':')+2);
        List<string> rawFist = working.Split("; ").ToList();
        
        foreach (string f in rawFist) { fists.Add(new Fist(f)); }
    }

    public bool GamePossible() 
    {
        foreach (Fist f in fists) 
        {
            if (f.red > 12) { return false; }
            if (f.green > 13) { return false; }
            if (f.blue > 14) {  return false; }
        }
        return true;
    }

    private void MinimalGame() 
    {
        foreach(Fist f in fists)
        {
            if (f.red > mRed) { mRed = f.red; }
            if (f.green > mGreen) { mGreen = f.green; }
            if (f.blue > mBlue) { mBlue = f.blue; }
        }
    }
    public int PowerGame() 
    {
        MinimalGame();
        return mRed*mGreen*mBlue;
    }
}

internal class Fist
{
    public int green;
    public int blue;
    public int red;

    public Fist(string fist) 
    {
        List<string> tmp = fist.Split(", ").ToList();
        foreach (string f in tmp) 
        {
            string[] tmp1 = f.Split(" ");
            if (tmp1[1][0] == 'g') { green = int.Parse(tmp1[0]); }
            if (tmp1[1][0] == 'b') { blue = int.Parse(tmp1[0]); }
            if (tmp1[1][0] == 'r') { red = int.Parse(tmp1[0]); }
        }
    }

}