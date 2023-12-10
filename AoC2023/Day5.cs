using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class Day5
    {
        public static void solve(LinkedList<String> input, bool PartTwo)
        {
            String[] inputAlmanac = input.ToArray();
            int a = 0;
            LinkedList<AlamanacRow>[] conversionTables = new LinkedList<AlamanacRow>[8];
            HashSet<Seed2> seeds = new HashSet<Seed2>();
            for (int i = 0; i < inputAlmanac.Length; i++)
            {
                if (i == 0)
                {
                    LinkedList<String> tempSeedsSeed2 = new LinkedList<String>();
                    String[] tempSeedsList = inputAlmanac[i].ToString().Split(':')[1].Split(" ");
                    foreach(String tempSeedList in tempSeedsList)
                    {
                        if (!String.IsNullOrEmpty(tempSeedList)){
                            tempSeedsSeed2.AddLast(tempSeedList);
                        }
                    }
                    String[] newTempSeeds = tempSeedsSeed2.ToArray();

                    for (int j = 0; j < newTempSeeds.Length; j += 2)
                    {
                        seeds.Add(new Seed2(BigInteger.Parse(newTempSeeds[j]), BigInteger.Parse(newTempSeeds[j]) + BigInteger.Parse(newTempSeeds[j+1]) - 1));
                    }

                }



                if (Regex.IsMatch(inputAlmanac[i], @"[a-zA-Z]"))
                {

                    conversionTables[a] = (new LinkedList<AlamanacRow>());
                    a++;


                    //Console.WriteLine(conversionTables[0]);
                    //Console.WriteLine(conversionTables[0].First());
                }

                else if (Regex.IsMatch(inputAlmanac[i], @"\d"))
                {
                    String[] partsAlamanac = inputAlmanac[i].Split(" ");
                    BigInteger start = BigInteger.Parse(partsAlamanac[1]);
                    BigInteger difference = BigInteger.Parse(partsAlamanac[0]) - BigInteger.Parse(partsAlamanac[1]);
                    BigInteger end = BigInteger.Parse(partsAlamanac[1]) + BigInteger.Parse(partsAlamanac[2]);
                    conversionTables[a - 1].AddLast(new AlamanacRow(start, end, difference));
                }
            }
            HashSet<Seed2> rondePlusEen = new HashSet<Seed2>();
            for (a = 1; a < conversionTables.Length; a++) {
                Console.WriteLine("RONDE: " + a);
                while (seeds.Count() > 0)
                {
                    LinkedList<Seed2> currentSeed = new LinkedList<Seed2>() ;
                    Seed2 seedInUse = seeds.ElementAt(0);
                    currentSeed.AddFirst(seedInUse);
                    bool matchFound = false;
                    foreach (AlamanacRow almanacRow in conversionTables[a])
                    {
                        LinkedList<Seed2> newSeeds = new LinkedList<Seed2>() ;  
                        foreach (Seed2 newSeed in currentSeed) {
                            newSeeds = newSeed.findOverlap(almanacRow);
                            if (newSeeds.First() != null)
                            {
                                matchFound = true;
                                Seed2 translationSeed = new Seed2(newSeeds.First().start + almanacRow.difference, newSeeds.First().end + almanacRow.difference);
                                if (!rondePlusEen.Contains(translationSeed))
                                {
                                    rondePlusEen.Add(translationSeed);
                                }
                            }
                            newSeeds.RemoveFirst(); }
                        
                        foreach(Seed2 newSeed in newSeeds)
                        {
                            currentSeed.AddLast(newSeed);
                            //Console.WriteLine(newSeed.start + " " + newSeed.end);
                        }
                    }
                    // Console.WriteLine(seeds.First().start + " " + seeds.First().end);
                    if (!matchFound) { rondePlusEen.Add(seedInUse);
                        Console.WriteLine("FALSE");
                    };
                    seeds.Remove(seedInUse);
                    Console.WriteLine(seeds.Count());
                    //Console.WriteLine(rondePlusEen.Count());
                }
                seeds = new HashSet<Seed2>(rondePlusEen, rondePlusEen.Comparer);
                rondePlusEen.Clear();
            }
            BigInteger minValue = seeds.ElementAt(1).start;
            foreach(Seed2 seed in seeds)
            {
                if(minValue >  seed.start) minValue = seed.start;
                Console.WriteLine(minValue);
            }
            Console.WriteLine(minValue);
        }
    }

    internal class Seed2
{
    public BigInteger start { get;set; }
    public BigInteger end { get;set; }
    internal Seed2(BigInteger start, BigInteger end)
        {
            this.start = start; 
            this.end = end; 
            
        }
        public bool Equals(Seed2? obj)
        {
            return this.start.Equals(obj.start) && this.end.Equals(obj.end);
        }
        public override int GetHashCode()
        {
            return this.start.GetHashCode() ^ this.end.GetHashCode();
        }

        internal LinkedList<Seed2?> findOverlap(AlamanacRow almanacRow)
        {
            LinkedList<Seed2> temp = new LinkedList<Seed2>();
            if (this.start > almanacRow.end - 1 || this.end < almanacRow.start)
            {
                // return null+seed
                temp.AddFirst((Seed2?) null);
                
            }
            else if (this.start < almanacRow.start && this.end > almanacRow.end - 1)
            {
                temp.AddLast(new Seed2(almanacRow.start, almanacRow.end - 1));
                temp.AddLast(new Seed2(this.start, almanacRow.start - 1));
                temp.AddLast(new Seed2(almanacRow.end, this.end));
                // Seed complete overlap Alamanac, give back Alamanac
                // alamanac.start -- almanac.end & seed.start -- almanac.start-1 & almanac.end+1 -- seed.end
            }
            else if (this.start >= almanacRow.start && this.end <= almanacRow.end -1)
            {
                temp.AddLast(this);
                // Seed compleet in alamanac.
                // seed start -- seed end
            }
            else if (this.start < almanacRow.start && this.end <= almanacRow.end - 1)
            {
                temp.AddLast(new Seed2(almanacRow.start, this.end));
                temp.AddLast(new Seed2(this.start, almanacRow.start - 1));
                // Seed begint voor Almanac, eindigt in.
                // Seed: alamanac.start -- seed.end & seed start -- almanac.start-1
            }
            else if (this.start >= almanacRow.start && this.end > almanacRow.end - 1)
            {
                temp.AddLast(new Seed2(this.start, almanacRow.end - 1));
                temp.AddLast(new Seed2(almanacRow.end, this.end));
                
                // Seed begint in almanac, eindigt er buiten.
                // Seed: Seed start --> end almanac & seed.almanac end -- seed.end
            }
            return temp;
        }
}
    
    internal class Almanac
    {
        LinkedList<AlamanacRow> conversionTable = new LinkedList<AlamanacRow>();

        public void add(AlamanacRow row)
        {
            conversionTable.AddLast(row);
        }
    }

    internal class AlamanacRow
    {
        public BigInteger start;
        public BigInteger end;
        public BigInteger difference;

        public AlamanacRow(BigInteger start, BigInteger end, BigInteger difference) {
            this.start = start;
            this.end = end;
            this.difference = difference;
        }
        public static BigInteger newValue(BigInteger value, LinkedList<AlamanacRow> conversionTable)
        {
            foreach(AlamanacRow conversion in conversionTable)
            {
                if (value >= conversion.start && value <= conversion.end)
                {
                    return value + conversion.difference;
                }
            }
            return value;
        }
    }
    
}
