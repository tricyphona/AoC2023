using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace AoC2023
{
    internal class Day6
    {
        public static void solve(LinkedList<String> input, bool PartTwo)
        {
            String[] lines = input.ToArray<String>();
            String[] times = lines[0].Split(":")[1].Split(" ");
            String[] distances = lines[1].Split(":")[1].Split(" ");
            LinkedList<int> resultTime = new LinkedList<int>();
            LinkedList<int> resultDistance = new LinkedList<int>();   
            foreach (String time in times)
            {
                String foundMatch = Regex.Match(time, @"\d+").Value;
                if (!String.IsNullOrEmpty(foundMatch))
                {
                    resultTime.AddLast(int.Parse(Regex.Match(time, @"\d+").Value));
                }
                
            }
            foreach (String distance in distances)
            {
                String foundMatch = Regex.Match(distance, @"\d+").Value;
                if (!String.IsNullOrEmpty(foundMatch))
                {
                    resultDistance.AddLast(int.Parse(Regex.Match(distance, @"\d+").Value));
                }

            }
            int[] timeResults = resultTime.ToArray();
            int[] distanceResults = resultDistance.ToArray();

            for (int i = 0; i < timeResults.Length; i++) {

                //    - 1 + timeResults[i] - resultDistance[i] = 0;
                Console.WriteLine(timeResults[i] * timeResults[i]); 
                double determinant = (timeResults[i] * timeResults[i]) - (4 * distanceResults[i]);
                Console.WriteLine(determinant);
                double result1 = (double)((-timeResults[i] + Math.Sqrt(determinant)) / (double)-2);                
                double result2 = (double)((-timeResults[i] - Math.Sqrt(determinant)) / (double)-2);
                Console.WriteLine(distanceResults[i]);
                Console.WriteLine(timeResults[i]);
                Console.WriteLine(result1 + " " + result2);
                Console.WriteLine(Math.Ceiling(result1));
                Console.WriteLine(Math.Floor(result2));
                Console.WriteLine("OPTIONS: " + (Math.Floor(result2-0.00000005) - Math.Ceiling(result1+0.00000005) + 1));
            }
            String longTime = "";
            String longDistance = "";
            for(int i = 0; i< timeResults.Length; i++)
            {
                longTime += timeResults[i];
                longDistance += distanceResults[i];
            }
            
            Console.WriteLine(longTime);
            BigInteger longTimeDiscreet = BigInteger.Parse(longTime);
            BigInteger longDistanceDiscreet = BigInteger.Parse(longDistance);

            double determinantBig = (double)((longTimeDiscreet * longTimeDiscreet) - (4 * longDistanceDiscreet));
            Console.WriteLine(determinantBig);
            double result1Big = (double)((-(double)longTimeDiscreet + Math.Sqrt(determinantBig)) / (double)-2);
            double result2Big = (double)((-(double)longTimeDiscreet - Math.Sqrt(determinantBig)) / (double)-2);
            Console.WriteLine(result1Big + " " + result2Big);
            Console.WriteLine(Math.Ceiling(result1Big));
            Console.WriteLine(Math.Floor(result2Big));
            Console.WriteLine("OPTIONS: " + (Math.Floor(result2Big - 0.00000005) - Math.Ceiling(result1Big + 0.00000005) + 1));
        }
    }
}
