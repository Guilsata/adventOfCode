namespace p5
{
    internal class Verticale
    {
        private static void solutionVerticale()
        {//Algo valide mais démentiellement long, un bon brut force
         //peut etre fonctionnel avec une bonne dose de parallélisation
            StreamReader sr = new StreamReader(".\\ressources.txt");
            string? line = sr.ReadLine();
            Dictionary<uint, uint> seedsTo = new Dictionary<uint, uint>();
            line.Substring(line.IndexOf(':') + 1).Trim().Split(' ').ToList().ForEach(e => seedsTo.Add(uint.Parse(e), uint.Parse(e)));

            line = sr.ReadLine();
            while (line != null)
            {
                if (line.Contains("map")) { line = sr.ReadLine(); continue; }
                Dictionary<uint, uint> tmp = new Dictionary<uint, uint>();
                seedsTo.Values.ToList().ForEach(e => tmp.Add(e, e));

                while (line != "")
                {
                    List<uint> work = line.Split(' ').ToList().ConvertAll(e => uint.Parse(e));
                    foreach (uint seed in tmp.Keys)
                    {
                        if (seed >= work[1] & seed < work[1] + work[2])
                        {
                            tmp[seed] = work[0] + seed - work[1];
                        }
                    }
                    line = sr.ReadLine();
                    if (line == null) { break; }
                }

                seedsTo.Keys.ToList().ForEach(e => seedsTo[e] = tmp[seedsTo[e]]);

                Console.WriteLine();

                line = sr.ReadLine();
            }
            uint mini = uint.MaxValue;
            seedsTo.Keys.ToList().ForEach(e => mini = seedsTo[e] < mini ? seedsTo[e] : mini);
            Console.WriteLine(mini);
            sr.Close();
        }
    }
}
