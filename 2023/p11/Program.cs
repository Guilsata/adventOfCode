using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace PERSONAL
{
    internal class p11
    {
        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(".\\ressources.txt");
            string? line = sr.ReadLine();
            List<List<char>> space = new List<List<char>>();
            while (line != null)
            {
                //Console.WriteLine(line);
                space.Add(line.ToArray().ToList());
                line = sr.ReadLine();
            }
            sr.Close();
            Sol1(space);
            //Sol2(space);
        }

        public static void Sol1(List<List<char>> space) 
        {
            int col = 1;
            while (col < space.First().Count)
            {

                if (space.Count(e => e[col - 1] == '#') == 0)
                {
                    //A noté qu'on ignore la dernière column
                    AddColumn(space, col - 1);
                    col++;
                }
                col++;
            }
            int row = 1;

            while (row < space.Count)
            {
                if (space[row - 1].Count(e => e == '#') == 0)
                {
                    //A noté qu'on ignore la dernière ligne
                    AddRow(space, row - 1);
                    row++;
                }
                row++;
            }
            Affiche(space);

            List<Point> points = new List<Point>();
            row = 0;
            while (row < space.Count)
            {
                col = 0;
                while (col < space.First().Count)
                {
                    if (space[row][col] == '#')
                    {
                        points.Add(new Point(row, col));
                    }
                    col++;
                }
                row++;
            }
            List<Segment> Segments = new List<Segment>();

            List<Point> tmp1 = [.. points];
            List<Point> tmp2 = [.. points.GetRange(0, points.Count - 1)];

            while (0 < tmp1.Count)
            {
                while (0 < tmp2.Count)
                {
                    Segments.Add(new Segment(tmp1.Last(), tmp2.Last()));
                    tmp2.RemoveAt(tmp2.Count - 1);
                }
                tmp1.RemoveAt(tmp1.Count - 1);
                if (tmp1.Count == 1) { break; }
                tmp2.AddRange(tmp1.GetRange(0, tmp1.Count - 1));
            }

            Console.WriteLine($"Nombre paires : {Segments.Count}");

            long somme = 0;
            int i = 1;
            Segments.ForEach(v => { Console.WriteLine($"{i++}/{Segments.Count} :: {v} :: {somme} "); somme += v.Length(); });
            Console.WriteLine($"{somme}");
        }

        public static void Sol2(List<List<char>> space)
        { 
            int col = 1;
            while (col < space.First().Count)
            {

                if (space.Count(e => e[col - 1] == '#') == 0)
                {
                    //A noté qu'on ignore la dernière column
                    //AddColumn(space, col - 1);
                    ReplaceColumn(space, col - 1);
                    col++;
                }
                col++;
            }
            int row = 1;

            while (row < space.Count)
            {
                if (space[row - 1].Count(e => e == '#') == 0)
                {
                    //A noté qu'on ignore la dernière ligne
                    //AddRow(space, row - 1);
                    ReplaceRow(space, row - 1);
                    row++;
                }
                row++;
            }
            Affiche(space);

            List<Point> points = new List<Point>();
            row = 0;
            while (row < space.Count)
            {
                col = 0;
                while (col < space.First().Count)
                {
                    if (space[row][col] == '#')
                    {
                        points.Add(new Point(row, col));
                    }
                    col++;
                }
                row++;
            }
            List<Segment> Segments = new List<Segment>();
            
            List<Point> tmp1 = [.. points];
            List<Point> tmp2 = [.. points.GetRange(0,points.Count-1)];
            
            while (0 < tmp1.Count) 
            {
                while (0 < tmp2.Count) 
                {
                    Segments.Add(new Segment(tmp1.Last(), tmp2.Last()));
                    tmp2.RemoveAt(tmp2.Count - 1);
                }
                tmp1.RemoveAt(tmp1.Count - 1);
                if (tmp1.Count == 1) { break; }
                tmp2.AddRange(tmp1.GetRange(0, tmp1.Count - 1));
            }

            Console.WriteLine($"Nombre paires : {Segments.Count}");
            List<Segment> gravityHoles = SegmentGravityHole(space);
            Segments.ForEach(s => s.nbrIntersections = gravityHoles.Count(g => g.Intersection(s)));
            long somme = 0;
            int i = 1;
            Segments.ForEach(v => {Console.WriteLine($"{i++}/{Segments.Count} :: {v} :: {somme} ") ; somme += v.Length(); });
            Console.WriteLine($"{somme}");
        }

        private static List<Segment> SegmentGravityHole(List<List<char>> space) 
        {
            List<Segment> Segments = new List<Segment>();
            int col = 0, row = 0;
            while(row<space.Count) 
            {
                if (space[row].Count(e => e == '*') == space[row].Count) 
                {
                    Segments.Add(new Segment(new Point(row, 0), new Point(row, space[row].Count)));
                }
                row++;
            }
            while (col < space.First().Count)
            {
                if (space.Count(e => e[col] == '*') == space.Count)
                {
                    Segments.Add(new Segment(new Point(0, col), new Point(space.Count, col)));
                }
                col++;
            }
            return Segments;
        }

        private static void AddColumn(List<List<char>> space, int col)
        {
            foreach (List<char> s in space)
            {
                s.Insert(col, '.');
            }
        }

        private static void ReplaceColumn(List<List<char>> space, int col)
        {
            foreach (List<char> s in space)
            {
                s[col] =  '*';
            }
        }

        private static void AddRow(List<List<char>> space, int row)
        {

            space.Insert(row, new List<char>());
            foreach (int i in Enumerable.Range(0, space.First().Count))
            {
                space[row].Add('.');
            }
        }
        private static void ReplaceRow(List<List<char>> space, int row)
        {

            foreach (int i in Enumerable.Range(0, space.First().Count))
            {
                space[row][i]='*';
            }
        }

        private static void Affiche(List<List<char>> space)
        {
            foreach (List<char> lc in space)
            {
                foreach (char c in lc)
                {
                    if (c == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(c);
                    }
                    else if (c == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(c);
                    }
                    else if (c == '*') 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(c);
                    }
                }
                Console.WriteLine();
            }
        }
    }

    internal class Point
    {
        static int absId = 1;
        public readonly int id;
        public readonly int x, y;
        public Point(int x, int y)
        {
            id = absId++;
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{id}";
        }
    }

    internal class Vector 
    {
        public readonly int x;
        public readonly int y;

        public Vector(Point p1, Point p2)
        {
            this.x = p1.x-p2.x;
            this.y = p1.y-p2.y;
        }
    }

    internal class Segment
    {
        const int ValueGravityHole = 1000000;

        public readonly Point p1, p2;
        public int nbrIntersections;


        public bool Intersection(Segment seg) 
        {
            //Expression des deux segments sous forme de vecteurs
            //Merci : https://openclassrooms.com/forum/sujet/calcul-du-point-d-intersection-de-deux-segments-21661 : Fvirtman
            Vector v1 = new Vector(p2, p1);//I
            Vector v2 = new Vector(seg.p2, seg.p1);//J
            //double o = (double)p1.x + (-2.0 / 3.0) * v1.x + (-2.0 / 3.0) * v1.y;
            //double oo = (double)seg.p2.y 
            decimal denominateur = (v1.x * v2.y - v1.y * v2.x);
            if (denominateur == 0) { return false; }

           //                           -(- Ix  * Ay   + Ix * Cy + Iy * Ax - Iy * Cx) / (Ix * Jy - Iy * Jx)
            decimal firstParamInter = (-v1.x * p2.y + v1.x * seg.p2.y + v1.y * p2.x - v1.y * seg.p2.x) / denominateur;
            decimal secondParamInter = (p2.x * v2.y - seg.p2.x * v2.y - v2.x * p2.y + v2.x * seg.p2.y) / denominateur;

            return firstParamInter >= 0 & firstParamInter <= 1 & secondParamInter >= 0 & secondParamInter <= 1;
        }

        public Segment(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            nbrIntersections = 0;
    }

        public long Length()
        {
            return (long)ValueGravityHole * (long)nbrIntersections +Math.Abs((long)p1.x - (long)p2.x) + Math.Abs((long)p1.y - (long)p2.y)- (long)nbrIntersections;
        }

        public override int GetHashCode()
        {
            if (p1.id < p2.id) { return p1.id*19 + p2.id*13; }
            return p2.id*19 + p1.id*13;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            else
            {
                Segment p = obj as Segment;
                return Equals(p);
            }
            
        }

        public bool Equals(Segment other) 
        {
            if(other == null) { return false; }
            if(ReferenceEquals(this, other)) {  return true; }
            return GetHashCode()==other.GetHashCode();
        }

        public static bool operator ==(Segment a, Segment b) 
        {
            return a.Equals((object?)b);
        }

        public static bool operator !=(Segment a, Segment b)
        {
            return !a.Equals((object?)b);
        }

        public override string ToString()
        {
            return $"[ {p1.x} , {p1.y} ]:[ {p2.x} , {p2.y} ], {nbrIntersections}";
        }
    }
}