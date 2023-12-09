internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines(".\\ressources.txt");
        int somme = 0;
        foreach (string line in lines)
        {
            Foo foo = new Foo();
            translateFirst(line, foo);
            translateLast(line, foo);

            Console.WriteLine(line);
            line.Reverse().ToList().ForEach(e => Console.Write(e));
            Console.WriteLine();


            Console.WriteLine(foo.number());
            somme += foo.number();
        }
        Console.WriteLine(somme);
    }

    private static void translateFirst(string line, Foo f)
    {
        int i = 0;
        while (i < line.Count())
        {
            if (int.TryParse(line[i].ToString(), out int x))
            {
                f.add(x);
                return;
            }
            try
            {
                if (line[i] == 'o' & line[i + 1] == 'n' & line[i + 2] == 'e') { f.add(1); return; }
            }
            catch { }
            try
            {
                if (line[i] == 't' & line[i + 1] == 'w' & line[i + 2] == 'o') { f.add(2); return; }
            }
            catch { }
            try
            {
                if (line[i] == 't' & line[i + 1] == 'h' & line[i + 2] == 'r' & line[i + 3] == 'e' & line[i + 4] == 'e') { f.add(3); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'f' & line[i + 1] == 'o' & line[i + 2] == 'u' & line[i + 3] == 'r') { f.add(4); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'f' & line[i + 1] == 'i' & line[i + 2] == 'v' & line[i + 3] == 'e') { f.add(5); return; }
            }
            catch { }
            try
            {
                if (line[i] == 's' & line[i + 1] == 'i' & line[i + 2] == 'x') { f.add(6); return; }
            }
            catch { }
            try
            {
                if (line[i] == 's' & line[i + 1] == 'e' & line[i + 2] == 'v' & line[i + 3] == 'e' & line[i + 4] == 'n') { f.add(7); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'e' & line[i + 1] == 'i' & line[i + 2] == 'g' & line[i + 3] == 'h' & line[i + 4] == 't') { f.add(8); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'n' & line[i + 1] == 'i' & line[i + 2] == 'n' & line[i + 3] == 'e') { f.add(9); return; }
            }
            catch { }
            i++;
        }
    }

    private static void translateLast(string lineR, Foo f)
    {
        int i = 0;
        List<char> line = lineR.Reverse().ToList();
        while (i < line.Count())
        {
            if (int.TryParse(line[i].ToString(), out int x))
            {
                f.add(x);
                return;
            }
            try
            {
                if (line[i] == 'e' & line[i + 1] == 'n' & line[i + 2] == 'o') { f.add(1); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'o' & line[i + 1] == 'w' & line[i + 2] == 't') { f.add(2); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'e' & line[i + 1] == 'e' & line[i + 2] == 'r' & line[i + 3] == 'h' & line[i + 4] == 't') { f.add(3); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'r' & line[i + 1] == 'u' & line[i + 2] == 'o' & line[i + 3] == 'f') { f.add(4); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'e' & line[i + 1] == 'v' & line[i + 2] == 'i' & line[i + 3] == 'f') { f.add(5); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'x' & line[i + 1] == 'i' & line[i + 2] == 's') { f.add(6); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'n' & line[i + 1] == 'e' & line[i + 2] == 'v' & line[i + 3] == 'e' & line[i + 4] == 's') { f.add(7); return; }
            }
            catch { }
            try
            {
                if (line[i] == 't' & line[i + 1] == 'h' & line[i + 2] == 'g' & line[i + 3] == 'i' & line[i + 4] == 'e') { f.add(8); return; }
            }
            catch { }
            try
            {
                if (line[i] == 'e' & line[i + 1] == 'n' & line[i + 2] == 'i' & line[i + 3] == 'n') { f.add(9); return; }
            }
            catch { }
            i++;
        }

    }
}

internal class Foo
{
    public int debut;
    public int current;

    public Foo() { debut = -1; current = -1; }

    public void add(int i) { if (debut == -1) { debut = i; current = i; } else { current = i; } }
    public int number() { return debut * 10 + current; }
}
