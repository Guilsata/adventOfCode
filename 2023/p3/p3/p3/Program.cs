using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? lineDown = sr.ReadLine();
        char[]? lineUp = null;
        char[] line;
        List<Tableau> Tabs = new List<Tableau>();
        line = lineDown.ToArray();
        lineDown = sr.ReadLine();
        while (lineDown != null) 
        {
            Tabs.Add(new Tableau(lineUp, lineDown.ToArray(), line));
            lineUp = line;
            line = lineDown.ToArray();
            lineDown = sr.ReadLine();
        }
        Tabs.Add(new Tableau(lineUp, null, line));
        sr.Close();
        uint rtr = 0;
        int i = 2;
        Tabs.ForEach(t => { rtr += t.searchStar();Console.WriteLine("Line : " + i++); ; });
        Console.WriteLine(rtr);


    }
}

public class Tableau
{
    public char[]? lineUp;
    public char[]? lineDown;
    public char[] line;
    public int lenght;

    public Tableau(char[]? lineUp, char[]? lineDown, char[] line)
    {
        this.lineUp = lineUp;
        this.lineDown = lineDown;
        this.line = line;
        lenght = line.Length;
    }

    public int readLine() 
    {
        int number = 0, tmp = 0;

        List<int> ints = new List<int>();
        int id = 0;
        foreach(char c in line) 
        {
            if (int.TryParse(c.ToString(), out int i))
            {
                number = number == 0 ? number = i : number = number * 10 + i;
                ints.Add(id);
            }
            else if(number != 0)
            {
                tmp = isPartNumber(ints) ? tmp + number : tmp;
                number = 0;
                ints = new List<int>();
            }
            id ++;
        }
        if (number != 0)
        {
            tmp = isPartNumber(ints) ? tmp + number : tmp;
            number = 0;
            ints = new List<int>();
        }
        return tmp;
    }

    public uint searchStar()
    {
        int id = 0;
        uint somme = 0;
        foreach (char c in line)
        {
            if (c == '*')
            {
                uint tmp = looksForNumber(id);
                Console.WriteLine(tmp);
                somme += tmp;
            }
            id++;
        }
        
        return somme;
    }

    public uint looksForNumber(int pos) 
    {
        uint j = 1;
        List<uint> ints = new List<uint>();
        looksRight(pos, ints, line);
        looksLeft(pos, ints, line);
        looksUp(pos, ints);
        looksDown(pos, ints);
        if (ints.Count > 1) 
        {
            ints.ForEach(i => { j = j * i;Console.Write(i + ","); });
            return j;
        }
        return 0;
        
       
    }

    public void looksUp(int pos, List<uint> ints) 
    {
        if (lineUp != null) { looksUpAndDown(pos, ints, lineUp); }
    }
    public void looksDown(int pos, List<uint> ints)
    {
        if (lineDown != null) { looksUpAndDown(pos, ints, lineDown); }
    }

    public void looksUpAndDown(int pos, List<uint> ints, char[] currentLine) 
    {
        if (int.TryParse(currentLine[pos].ToString(), out int j))
        {
            if (pos != 0)
            {
                while (int.TryParse(currentLine[pos - 1].ToString(), out int k))
                {
                    pos--;
                    if (pos == 0) { break; }
                }
            }
            looksRight(pos-1, ints, currentLine);
            return;
        }
        looksRight(pos, ints, currentLine);
        looksLeft(pos, ints, currentLine);
    }

    public void looksRight(int pos, List<uint> ints, char[] currentLine)
    {

        int number = 0;
        if (pos < lenght - 1) {
            for (int i = pos + 1; int.TryParse(currentLine[i].ToString(), out int j); i++ )
            {
                number = number == 0 ? j : number * 10 + j;
                if (i == lenght - 1) { break; }
            } 
        }
        if (number != 0) { ints.Add((uint)number); }
    }

    public void looksLeft(int pos, List<uint> ints, char[] currentLine)
    {
        string sn = "";
        if (pos > 0)
        {
            for (int i = pos - 1; int.TryParse(currentLine[i].ToString(), out int j); i--)
            {
                sn = currentLine[i].ToString()+sn;
                if (i == 0) { break; }
            }
        }
        
        if (sn.Length != 0) { ints.Add(uint.Parse(sn)); }
    }

    public bool isPartNumber(List<int> ints) 
    {
        int start = 0;
        int end = ints.Last();
        if (ints.First() > 0) 
        {
            if (line[ints.First() - 1] != '.') { return true; }; 
            start = ints.First() - 1;
        }
        if (end < lenght-1)
        { 
            if (line[end + 1] != '.') { return true; }; 
            end++;
        }
        for (int i = start; i != end+1; i++) 
        {
            if (lineUp != null) { if (lineUp[i] != '.') { return true; } }
            if (lineDown != null) { if (lineDown[i] != '.') { return true; } }
        }
        Console.WriteLine(start + "," + end);
        return false;
    }
}