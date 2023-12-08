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
            LinkedList<Seed2> seeds = new LinkedList<Seed2>();
            for (int i = 0; i < inputAlmanac.Length; i++)
            {
                if (i == 0)
                {
                    String[] tempSeedsList = inputAlmanac[i].ToString().Split(':')[1].Split(" ");
                    foreach (String tempseedList in tempSeedsList) { tempSeedsList[i] = Regex.Replace(tempseedList, @"\s+", string.Empty); }

                    for (int j = 0; j < tempSeedsList.Length; j += 2)
                    {
                        seeds.AddLast(new Seed2(BigInteger.Parse(tempSeedsList[j]), BigInteger.Parse(tempSeedsList[j])));
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

                Console.WriteLine("LOWEST: ");
                Console.WriteLine(Seed.getResultSeeds(seeds, conversionTables));


            
        }
    }

    internal class Seed2
{
    BigInteger start { get;set; }
    BigInteger end { get;set; }
    internal Seed2(BigInteger start, BigInteger end)
        {
            this.start = start; 
            this.end = end; 
        }
}
    internal class Seed
    {
        public static BigInteger getResultSeeds(LinkedList<Seed2> seeds, LinkedList<AlamanacRow>[] conversionTables)
        {
            BigInteger[,] result = new BigInteger[seeds.LongCount(), 2];
            int i = 0;
            foreach (BigInteger seed in seeds)
            {
                result[i, 0] = seed;
                
                BigInteger value = seed;

                for (int a = 0; a < conversionTables.Length; a++)
                {
                    value = AlamanacRow.newValue(value, conversionTables[a]);

                }
                
                result[i, 1] = value;
                i++;
            }
            BigInteger lowest = result[0, 1];
            for(i = 0; i < seeds.Count(); i++)
            {
                if (result[i,1] < lowest)
                {
                    lowest = result[i,1];
                }
            }
            return lowest;
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
        BigInteger start;
        BigInteger end;
        BigInteger difference;

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
