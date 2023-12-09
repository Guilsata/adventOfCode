internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(".\\ressources.txt");
        string? line = sr.ReadLine();
        List<Interval> seedsTo = new List<Interval>();
        List<string> longs = new List<string>();
        longs = line.Substring(line.IndexOf(':') + 1).Trim().Split(' ').ToList();
        int i= 0;
        while (i < longs.Count) {seedsTo.Add(new Interval(long.Parse(longs[i]), long.Parse(longs[i+1]))) ;i+=2; }
        line = sr.ReadLine();
        line = sr.ReadLine();
        Carte carte = new Carte();
        List<CoupleInterval> tmp = new List<CoupleInterval>();
        while (line != null)
        {
            if (line.Contains("map")) { line = sr.ReadLine(); continue; }
            if (line == "")
            {
                
                    carte.coupleIntervals = carte.GenerateCollisions(tmp);
                
                tmp = new List<CoupleInterval>();
                line = sr.ReadLine(); 
                continue;
            }
            List<long> foo = line.Split(' ').ToList().ConvertAll(long.Parse);
            tmp.Add(new CoupleInterval(foo[0], foo[1], foo[2]));
            line = sr.ReadLine();
        }
        carte.coupleIntervals = carte.GenerateCollisions(tmp);
        Console.WriteLine(carte);
        sr.Close();
        long mini = long.MaxValue;
        foreach (Interval se in seedsTo)
        {
            foreach (CoupleInterval e in carte.coupleIntervals)
            {
                if (se.Chevauchement(e.source))
                {
                    long rangColli = ChevauchementSeed(se.debut,e);
                    mini = rangColli < mini ? rangColli : mini;
                }
            }
        }
        Console.WriteLine(mini);
    }

    public static long ChevauchementSeed(long l, CoupleInterval ci)
    {
        if (l < ci.source.debut)
        {
            return ci.destination.debut;
        }
        else
        {
            return l - ci.source.debut + ci.destination.debut;
        }
    }
}

internal class Carte
{
    public List<CoupleInterval> coupleIntervals;

    public Carte(){coupleIntervals = new List<CoupleInterval>();}

    public override string ToString()
    {
        return string.Concat(coupleIntervals.ConvertAll(e=>$"{e.destination.debut} {e.source}\n"));
    }

    public List<CoupleInterval> GenerateCollisions(List<CoupleInterval> cible) 
    {
        List<CoupleIntervalFlag> intervalsFlag = new List<CoupleIntervalFlag>();
        List<CoupleInterval> intervals = new List<CoupleInterval>();

        intervalsFlag.AddRange(coupleIntervals.ConvertAll(e => new CoupleIntervalFlag(e, e.destination, true)));
        intervalsFlag.AddRange(cible.ConvertAll(e => new CoupleIntervalFlag(e, e.source, false)));
        intervalsFlag = intervalsFlag.OrderBy(e => e.sort.debut).ThenByDescending(e => e.sort.taille).ToList();
        
        int i = 0;
        while (i < intervalsFlag.Count) 
        {
            if (i == intervalsFlag.Count - 1) 
            {
                intervals.Add(intervalsFlag[i]);
                break;
            }
            if (intervalsFlag[i].sort.Chevauchement(intervalsFlag[i + 1].sort))
            {//Collision impossible si i et i+1 du meme grp1
                if (intervalsFlag[i].grp1)
                {
                    if (intervalsFlag[i].sort.fin < intervalsFlag[i + 1].sort.fin)
                    {
                        if (intervalsFlag[i].sort.debut < intervalsFlag[i + 1].sort.debut)
                        {
                            long t1 = intervalsFlag[i + 1].sort.debut - intervalsFlag[i].sort.debut;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             t1));
                            long t2 = intervalsFlag[i].taille-t1;
                            intervals.Add(new CoupleInterval(intervalsFlag[i + 1].destination.debut,
                                                             intervalsFlag[i].source.debut + t1 + 1,
                                                             t2));
                            intervalsFlag[i + 1].Maj(t2);
                            i++;
                        }
                        else
                        {
                            intervals.Add(new CoupleInterval(intervalsFlag[i + 1].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             intervalsFlag[i].taille));
                            intervalsFlag[i + 1].Maj(intervalsFlag[i].taille);
                            i++;
                        }
                    }
                    else if (intervalsFlag[i].sort.fin > intervalsFlag[i + 1].sort.fin)
                    {
                        if (intervalsFlag[i].sort.debut < intervalsFlag[i + 1].sort.debut)
                        {
                            long t1 = intervalsFlag[i + 1].sort.debut - intervalsFlag[i].sort.debut;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             t1));
                            long t2 = intervalsFlag[i+1].taille;
                            intervals.Add(new CoupleInterval(intervalsFlag[i + 1].destination.debut,
                                                             intervalsFlag[i].source.debut + t1 + 1,
                                                             t2));
                            intervalsFlag[i].Maj(t1+t2);
                            intervalsFlag.Remove(intervalsFlag[i + 1]);
                        }
                        else
                        {
                            intervals.Add(new CoupleInterval(intervalsFlag[i + 1].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             intervalsFlag[i + 1].taille));
                            intervalsFlag[i].Maj(intervalsFlag[i + 1].taille);
                            intervalsFlag.Remove(intervalsFlag[i + 1]);
                        }
                    }
                    else
                    {
                        if (intervalsFlag[i].sort.debut < intervalsFlag[i + 1].sort.debut)
                        {
                            long t1 = intervalsFlag[i + 1].source.debut - intervalsFlag[i].destination.debut;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             t1));
                            long t2 = intervalsFlag[i + 1].taille;
                            intervals.Add(new CoupleInterval(intervalsFlag[i + 1].destination.debut,
                                                             intervalsFlag[i].source.debut + t1 + 1,
                                                             t2));
                        }
                        else
                        {
                            intervals.Add(new CoupleInterval(intervalsFlag[i+1].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             intervalsFlag[i].taille));
                        }
                        i += 2;
                    }
                }
                else 
                {
                    if (intervalsFlag[i].sort.fin < intervalsFlag[i + 1].sort.fin)
                    {
                        if (intervalsFlag[i].sort.debut < intervalsFlag[i + 1].sort.debut)
                        {
                            long t1 = intervalsFlag[i + 1].sort.debut - intervalsFlag[i].sort.debut;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             t1));
                            long t2 = intervalsFlag[i].taille - t1;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut + t1,
                                                             intervalsFlag[i+1].source.debut,
                                                             t2));
                            intervalsFlag[i + 1].Maj(t2);
                            i++;
                        }
                        else
                        {
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i+1].source.debut,
                                                             intervalsFlag[i].taille));
                            intervalsFlag[i + 1].Maj(intervalsFlag[i].taille);
                            i++;
                        }
                    }
                    else if (intervalsFlag[i].sort.fin > intervalsFlag[i + 1].sort.fin)
                    {
                        if (intervalsFlag[i].sort.debut < intervalsFlag[i + 1].sort.debut)
                        {
                            long t1 = intervalsFlag[i + 1].sort.debut - intervalsFlag[i].sort.debut;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             t1));
                            long t2 = intervalsFlag[i + 1].taille;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut + t1,
                                                             intervalsFlag[i+1].source.debut,
                                                             t2));
                            intervalsFlag[i].Maj(t1 + t2);
                            intervalsFlag.Remove(intervalsFlag[i + 1]);
                        }
                        else
                        {
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i + 1].source.debut,
                                                             intervalsFlag[i + 1].taille));
                            intervalsFlag[i].Maj(intervalsFlag[i + 1].taille);
                            intervalsFlag.Remove(intervalsFlag[i + 1]);
                        }
                    }
                    else
                    {
                        if (intervalsFlag[i].sort.debut < intervalsFlag[i + 1].sort.debut)
                        {
                            long t1 = intervalsFlag[i + 1].sort.debut - intervalsFlag[i].sort.debut;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             t1));
                            long t2 = intervalsFlag[i + 1].taille;
                            intervals.Add(new CoupleInterval(intervalsFlag[i].destination.debut + t1,
                                                             intervalsFlag[i+1].source.debut,
                                                             t2));
                        }
                        else
                        {
                            intervals.Add(new CoupleInterval(intervalsFlag[i+1].destination.debut,
                                                             intervalsFlag[i].source.debut,
                                                             intervalsFlag[i].taille));
                        }
                        i += 2;
                    }
                }
            }
            else 
            {
                intervals.Add(intervalsFlag[i]);
                i++;
            }
        }
        return intervals;
    }
}

internal class CoupleIntervalFlag : CoupleInterval 
{
    public bool grp1;
    public Interval sort;

    public CoupleIntervalFlag(CoupleInterval coupleInterval, Interval sort,bool grp1)
        :base(coupleInterval.destination.debut, coupleInterval.source.debut, coupleInterval.source.taille)
    {
        this.grp1 = grp1;
        this.sort = new Interval(sort);
    }

    public void Maj(long taille) 
    {
        sort = new Interval(sort.debut+taille, sort.taille - taille);
        source = new Interval(source.debut+taille, source.taille-taille);
        destination = new Interval(destination.debut+taille, destination.taille-taille);
    }
}

internal class CoupleInterval
{
    public Interval source;
    public Interval destination;
    public long taille { get { return source.taille; } }

    public static CoupleInterval Copy(CoupleInterval ci)
    {
        return new CoupleInterval(ci.destination.debut, ci.source.debut, ci.source.taille);
    }

    public CoupleInterval(CoupleIntervalFlag cf) 
    {
        source = new Interval(cf.source.debut, cf.taille);
        destination = new Interval(cf.destination.debut, cf.taille);
    }

    public CoupleInterval(long destination, long source, long taille)
    {
        this.source = new Interval(source, taille);
        this.destination = new Interval(destination, taille);
    }

    public override string ToString()
    {
        return $"{destination.debut} {source}";
    }
}

internal class Interval
{
    public long debut { get; protected set; }
    public long taille { get; protected set; }
    public long fin { get { return debut + taille - 1; } }

    public Interval(long debut, long taille)
    {
        this.debut = debut;
        this.taille = taille;
    }

    public Interval(Interval i)
    {
        debut = i.debut;
        taille = i.taille;
    }

    public bool Chevauchement(Interval i) 
    {
        if (debut < i.debut) { return Chevauchement(i.debut) != -1; }
        else { return i.Chevauchement(this.debut) != -1; }
    }

    private long Chevauchement(long debut) 
    {
        return fin >= debut?debut:-1;
    }

    public override int GetHashCode()
    {
        return (int)debut * 2 +(int)taille;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null) { return false; }
        return obj is Interval & Equals((Interval)obj);
    }

    public bool Equals(Interval e)
    {
        return e.debut == debut & e.taille == taille;
    }

    public override string ToString()
    {
        return $"{debut} {taille}";
    }
}