using System.Collections.Generic;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        
        List<string> pipes = new List<string>();
        while (line != null)
        {
            pipes.Add(line);
            line = sr.ReadLine();
        }
        sr.Close();

        Node starter;
        List<Node> nodes = BuildNodes(pipes, out starter);
        BuildNeighborsNodes(nodes, pipes.First().Length);
        FindNeighborsStarter(nodes, starter, pipes.First().Length);
        int count = CountStepsLoop(starter);
        Console.WriteLine(count);
        
        //ShowPipes(nodes, pipes.First().Length);
        nodes = SeparatePipe(nodes, pipes.First().Length, out int newMaxCol, out int newMaxRow);
        //ShowPipes(nodes, newMaxCol);
        MarkExternalNode(nodes, newMaxCol, newMaxRow);
        //ShowPipes(nodes, newMaxCol);
        Console.WriteLine(nodes.Where(e=>!(e.IsOut | e.IsLoop) & !e.Fictional).Count());
        ShowPipesClean(nodes, newMaxCol);
    }
    
    private static List<Node> SeparatePipe(List<Node> nodes,int maxCol, out int newMaxCol, out int newMaxRow) 
    {
        List<List<Node>> listNodes = ConvertNodes(nodes, maxCol);
        int row = 0;
        while(row<listNodes.Count)
        {
            if (!listNodes[row][0].Fictional)
            {
                int col = 0;
                while (col < listNodes[row].Count)
                {
                    if (listNodes[row][col].IsLoop & !listNodes[row][col].Fictional)
                    {
                        switch (listNodes[row][col].Name)
                        {
                            case "|":
                            case "7":
                                if (col < listNodes[row].Count - 1)
                                {
                                    if (listNodes[row][col + 1].IsLoop) { AddColumn(listNodes, col); }
                                    col++;
                                }
                                break;
                            case "-":
                            case "L":
                                if (row < listNodes.Count - 1)
                                {
                                    if (listNodes[row + 1][col].IsLoop) { AddRow(listNodes, row); }
                                }
                                break;
                            case "J":
                                if (row < listNodes.Count - 1)
                                {
                                    if (listNodes[row + 1][col].IsLoop) { AddRow(listNodes, row); }
                                }
                                if (col < listNodes[row].Count - 1)
                                {
                                    if (listNodes[row][col + 1].IsLoop) { AddColumn(listNodes, col); }
                                    col++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    col++;
                } 
            }
            row++;

        }
        newMaxCol = listNodes.First().Count;
        newMaxRow = listNodes.Count;
        return ReverseConvertNodes(listNodes);
    }

    private static List<List<Node>> ConvertNodes(List<Node> nodes, int maxCol) 
    {
        List<List<Node>> listNodes = new List<List<Node>>();

        for (int i = 0; i < nodes.Count; i += maxCol) 
        {
            listNodes.Add(nodes.GetRange(i, maxCol));
        }

        return listNodes;
    }

    private static List<Node> ReverseConvertNodes(List<List<Node>> listNodes) 
    {
        List<Node> nodes = new List<Node>();
        listNodes.ForEach(e => nodes.AddRange(e));
        return nodes;
    }

    private static void AddColumn(List<List<Node>> listNodes, int col) 
    {
        foreach (List<Node> nodes in listNodes) 
        {
            bool falseLoop = nodes[col].IsLoop & nodes[col].Neighbors.Exists(e => e == nodes[col+1]);
            nodes.Insert(col + 1, new Node(true,falseLoop));
        }
    }
   
    private static void AddRow(List<List<Node>> listNodes, int row) 
    {
        if (!listNodes[row + 1][0].Fictional)
        {
            listNodes.Insert(row + 1, new List<Node>());
            foreach (int i in Enumerable.Range(0, listNodes.First().Count))
            {
                bool falseLoop = listNodes[row][i].IsLoop & listNodes[row][i].Neighbors.Exists(e => e == listNodes[row + 2][i]);
                listNodes[row + 1].Add(new Node(true, falseLoop));
            }
        }
    }

    private static void MarkExternalNode(List<Node> nodes, int maxCol, int maxRow) 
    {
        foreach (Node node in nodes) 
        {
            if (node.IsLoop) { continue; }
            PositionOf(nodes.IndexOf(node), maxCol, out int col, out int row);
            if (col == 0 || col == maxCol - 1) { node.IsOut = true; }
            if (row == 0 || row == maxRow - 1) { node.IsOut = true; }
        }
        List<Node> watchList = new List<Node>();
        nodes.ForEach(node => { watchList.Add(node);node.Seen = false; });
        while (watchList.Count != 0)
        {
            if (watchList.Last().IsOut) 
            {
                PositionOf(nodes.IndexOf(watchList.Last()), maxCol, out int col, out int row);
                List<Node> AbsNeighbors = AbsoluteNeighbors(nodes, maxCol, maxRow, col, row);
                watchList.Remove(watchList.Last());
                AbsNeighbors.Where(e=> !e.IsLoop & !e.Seen).ToList().ForEach(e => { e.IsOut = true; e.Seen=true; watchList.Add(e); });
            }
            else
            {
                watchList.Last().Seen = true;
                watchList.Remove(watchList.Last()); 
            }
        }
    }

    private static List<Node> AbsoluteNeighbors(List<Node> nodes, int maxCol, int maxRow, int col, int row) 
    {
        List<Node> neighbors = new List<Node>();
        if (col == 0)
        {
            if (row == 0) 
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol,col+1,row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row+1)));
            }
            else if (row == maxRow - 1) 
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col + 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row - 1)));
            }
            else
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row - 1)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col + 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row + 1)));
            }
        }
        else if (col == maxCol - 1)
        {
            if (row == 0) 
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col - 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row + 1)));
            }
            else if (row == maxRow - 1) 
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col - 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row - 1)));
            }
            else
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row - 1)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col - 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row + 1)));
            }
        }
        else 
        {
            if (row == 0) 
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col - 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col + 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row + 1)));
            }
            else if (row == maxRow - 1) 
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row - 1)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col - 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col + 1, row)));
            }
            else
            {
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row - 1)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col - 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col + 1, row)));
                neighbors.Add(nodes.ElementAt(IndexOf(maxCol, col, row + 1)));
            }
        }
        return neighbors;
    }

    private static int IndexOf(int maxCol, int col, int row) 
    {
        return maxCol * row + col;
    }

    private static void PositionOf(int index, int maxCol, out int col, out int row) 
    {
        col = index%maxCol;
        row = (index-index%maxCol)/maxCol;
    }

    private static void ShowPipesClean(List<Node> nodes, int length) 
    {
        foreach (Node node in nodes)
        {
            bool line = false;
            if (node.IsLoop & !node.Fictional)
            {
                line = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(node);
            }
            else if (node.IsOut & !node.Fictional)
            {
                line = true;
                Console.Write(" ");
            }
            else if (!node.Fictional)
            {
                line = true;
                Console.Write(node);
            }
            
            Console.ResetColor();
            if (nodes.IndexOf(node) % length == length - 1 & line) { Console.WriteLine(); }
        }
    }

    private static void ShowPipes(List<Node> nodes, int length) 
    {
        foreach (Node node in nodes) 
        {
            
            if (node.IsLoop & !node.Fictional) 
            {
                Console.ForegroundColor = ConsoleColor.Red; 
            }
            else if (node.IsLoop & node.Fictional)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (node.IsOut & !node.Fictional)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (node.IsOut & node.Fictional)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if (node.Fictional) 
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            Console.Write(node);
            Console.ResetColor();                           
            if (nodes.IndexOf(node)%length == length - 1) { Console.WriteLine(); }
        }
    }

    private static int CountStepsLoop(Node starter) 
    {
        int count = 1;
        Node current = starter;
        current.stepIn = true;
        while (true)
        {
            
            current = current.Neighbors.Where(e => e.stepIn == false).First();
            current.stepIn = true;
            if (current.Neighbors.Where(e => e.stepIn == true).Count() == 2 & current.Neighbors.Exists(e => e.Name == starter.Name)) { break; }
            count++;
        }
        count++;
        return count/2;
    }

    private static void FindNeighborsStarter(List<Node> nodes, Node starter, int lengthline) 
    {
        if (nodes.IndexOf(starter) - lengthline >= 0)
        {
            if (nodes.ElementAt(nodes.IndexOf(starter) - lengthline).Neighbors.Exists(e => e.Name == starter.Name)) { starter.Neighbors.Add(nodes.ElementAt(nodes.IndexOf(starter) - lengthline)); }
        }
        if (nodes.IndexOf(starter) - 1 >= 0)
        {
            if (nodes.ElementAt(nodes.IndexOf(starter) - 1).Neighbors.Exists(e => e.Name == starter.Name)) { starter.Neighbors.Add(nodes.ElementAt(nodes.IndexOf(starter) - 1)); }
        }
        if (nodes.IndexOf(starter) + 1 < nodes.Count)
        {
            if (nodes.ElementAt(nodes.IndexOf(starter) + 1).Neighbors.Exists(e => e.Name == starter.Name)) { starter.Neighbors.Add(nodes.ElementAt(nodes.IndexOf(starter) + 1)); }
        }
        if (nodes.IndexOf(starter) + lengthline < nodes.Count)
        {
            if (nodes.ElementAt(nodes.IndexOf(starter) + lengthline).Neighbors.Exists(e => e.Name == starter.Name)) { starter.Neighbors.Add(nodes.ElementAt(nodes.IndexOf(starter) + lengthline)); }
        }
    }

    private static void BuildNeighborsNodes(List<Node> nodes, int lengthLine) 
    {
        foreach (Node node in nodes) 
        {
            switch (node.Name) 
            {
                case "|":
                    AddNeighborsNodes(nodes, node, nodes.IndexOf(node) - lengthLine, nodes.IndexOf(node) + lengthLine);
                    break;
                case "-":
                    AddNeighborsNodes(nodes, node, nodes.IndexOf(node) - 1, nodes.IndexOf(node) + 1);
                    break;
                case "L":
                    AddNeighborsNodes(nodes, node, nodes.IndexOf(node) - lengthLine, nodes.IndexOf(node) + 1);
                    break;
                case "J":
                    AddNeighborsNodes(nodes, node, nodes.IndexOf(node) - lengthLine, nodes.IndexOf(node) -1);
                    break;
                case "7":
                    AddNeighborsNodes(nodes, node, nodes.IndexOf(node) - 1, nodes.IndexOf(node) + lengthLine);
                    break;
                case "F":
                    AddNeighborsNodes(nodes, node, nodes.IndexOf(node) + lengthLine, nodes.IndexOf(node) + 1);
                    break;
                default:
                    break;
            }
        }
    }

    private static void AddNeighborsNodes(List<Node> nodes, Node node, int one, int two) 
    {
        if (one >= 0 & one < nodes.Count) { node.Neighbors.Add(nodes.ElementAt(one)); }
        if (two >= 0 & two < nodes.Count) { node.Neighbors.Add(nodes.ElementAt(two)); }
    }

    private static List<Node> BuildNodes(List<string> pipes, out Node starter) 
    {
        List<Node> nodes = new List<Node>();
        starter = null;
        foreach (string linePipes in pipes) 
        {
            foreach (char pipe in linePipes) 
            {
                nodes.Add(new Node(pipe));
                if (nodes.Last().Name == "S") { starter = nodes.Last(); }
            }
        }
        return nodes;
    }
}
         
internal class Node 
{
    public bool Seen { get; set; }

    public bool Fictional { get; set; }

    public bool IsLoop { get { return stepIn; } }

    private bool isOut;
    public bool IsOut { get { return isOut; } set { isOut = value; } }

    public bool stepIn;

    public string Name { get; private set; }
    public List<Node> Neighbors { get; set; }

    public Node(char c)
    {
        Name = c.ToString(); 
        stepIn = false;
        Neighbors = new List<Node>();
        Fictional = false;
        Seen = false;
    }

    public Node(bool Fictional,bool FalseLoop)
    {
        Seen = false;
        stepIn = false;
        Neighbors = new List<Node>();
        stepIn = FalseLoop;
        this.Fictional = Fictional;
    }

    public override string ToString()
    {
        if (Fictional) { return IsOut?"O":"X"; }
        if (IsOut) { return "O"; }
        return Name;
    }
}