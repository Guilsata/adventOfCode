internal class p8
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines(".\\ressources.txt");
        List<string> instructions = new List<string>();
        lines[0].ToList().ForEach(e => instructions.Add(e.ToString()));
        List<string> listLines = lines.ToList();
        listLines.RemoveRange(0, 2);
        List<Node> nodes = new List<Node>();
        List<Node> start = new List<Node>();
        Node current = null;
        foreach (string line in listLines)
        {
            nodes.Add(new Node(line.Substring(0, 3), line.Substring(7, 3), line.Substring(12, 3)));
            if (line[2] == 'A') { start.Add(nodes.Last()); }
        }

        nodes.ForEach(e => { e.left = nodes.Find(f => f.name == e.leftName); e.right = nodes.Find(f => f.name == e.rightName); });
        bool ZZZnotFound = true;
        long step = 0;
        List<long> eachStep = new List<long>();
        foreach (Node n in start)
        {
            current = n;
            ZZZnotFound = true;
            while (ZZZnotFound)
            {
                foreach (string instruction in instructions)
                {
                    if (current == null)
                    {
                        throw new Exception("Oups");
                    }
                    else
                    {
                        current = current.GoTo(instruction);
                        step++;
                        if (current.name[2] == 'Z')
                        {
                            ZZZnotFound = false;
                            eachStep.Add(step);
                            step = 0;
                            break;
                        }
                    }
                }
            }
        }

        eachStep = eachStep.OrderByDescending(e => e).ToList();

        //long r = BrutForceSolution(eachStep);

        List<long> primeNumbers = PrimeNumberUnder((int)Math.Sqrt(eachStep.First()));
        List<List<long>> listls = new List<List<long>>();
        
        eachStep.ForEach(e => listls.Add(Decomposition(e, primeNumbers)));

        List<long> rtr = new List<long>();
        foreach (List<long> ls in listls) 
        {
            foreach (long l in ls) 
            {
                if (rtr.Exists(e => e == l))
                {
                    while(rtr.Count(e => e == l)- ls.Count(e => e == l) != 0)
                    {
                        rtr.Add(l);
                    }
                }
                else 
                {
                    rtr.Add(l);
                }
            }
        }
        long r = 1;
        rtr.ForEach(e=>r*=e);
        Console.WriteLine(r);
    }
    
    public static List<long> PrimeNumberUnder(int number) 
    {//algorithme naif
        List<long> longs = new List<long> { 2 };
        foreach (long cdt in Enumerable.Range(2, number - 1)) 
        { 
            bool isPrime = true;
            longs.ForEach(l => { if (isPrime & cdt % l == 0) { isPrime = false; } });
            if (isPrime) { longs.Add(cdt); }; 
        }
        return longs;
    }

    public static List<long> Decomposition(long number, List<long> primeNumbers)
    {
        List<long> rtr = new List<long>();
        return Decomposition(number, primeNumbers, rtr);
    }

    private static List<long> Decomposition(long number, List<long> primeNumbers, List<long> rtr) 
    {
        bool IsPrimeNumber(long number, List<long> listPrimeNumber)
        {
            foreach (long cdt in listPrimeNumber)
            {
                if (number % cdt == 0) { return false; };
            }
            return true;
        }
        void IsMultiple(long number, long cdt)
        {
            if (number % cdt == 0)
            {
                rtr.Add(cdt);
                if (IsPrimeNumber(number / cdt, primeNumbers)) { rtr.Add(number / cdt); }
                Decomposition(number / cdt, primeNumbers, rtr);
            }
            return;
            
        }
        foreach (long cdt in primeNumbers) 
        {
            IsMultiple(number, cdt);
        }
        return rtr; 
    }

    public static long BrutForceSolution(List<long> eachStep) 
    {
        long r = 1;
        long h = 0;
        bool b = true;
        while (b)
        {
            h++;
            if (1000000000 % h == 0)
            { Console.WriteLine("Trying : " + h); }
            r = h * eachStep.Last();
            int j = 0;
            bool gh = true;
            while (j < eachStep.Count - 1)
            {
                if (r % eachStep[j] != 0) { gh = false; }
                j++;
            }
            if (gh) { b = false; }
        }
        return r;
    }
}



public class Node
{
    public readonly string name;

    public readonly string leftName;
    public Node? left { get; set; }

    public readonly string rightName;
    public Node? right { get; set; }

    public Node(string name, string left, string right)
    {
        this.name = name;
        leftName = left;
        rightName = right;
    }

    public Node? GoTo(string instruction)
    {
        switch (instruction) { case "R": return right; default: return left; }
    }

    public override string ToString()
    {
        return $"{name} = ({leftName}, {rightName})";
    }
}