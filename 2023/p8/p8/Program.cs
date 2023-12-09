internal class Program
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
        eachStep.ForEach(Console.WriteLine);

        //43089384579255253
        long r = BrutForceSolution(eachStep);
        Console.WriteLine(r);
    }

    public static List<long> 

    public static List<long> Decomposition() { return new List<long>(); }

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