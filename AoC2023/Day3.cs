using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class Day3
    {
        public static void solve(LinkedList<String> puzzleInput, bool secondPart)
        {

            LinkedList<Number> numbers = new LinkedList<Number>();
            Finder.find(puzzleInput, secondPart);

        }

        internal static class Finder
        {

            public static void find(LinkedList<String> puzzleInput, bool partTwo)
            {
                int puzzleInputSize = puzzleInput.Count;
                int puzzleInputWidth = puzzleInput.First().Length;
                LinkedList<Number> numbers = new LinkedList<Number>();
                char[,] schema = new char[puzzleInputSize, puzzleInputWidth];
                int i = 0;
                foreach (String line in puzzleInput)
                {
                    
                    for (int j = 0; j < line.Length; j++)
                    {
                        schema[i, j] = line[j];
                    }
                    i++;
                }
                int value = 0;
                int length = 0;
                bool found;
                int sum = 0;
                LinkedList<Gear> gears = new LinkedList<Gear>();
                for(int y=0; y < puzzleInputSize; y++)
                {
                    for (int j = 0; j < puzzleInputWidth; j++)
                    {
                        if (char.IsDigit(schema[y, j]))
                        {
                            value *= 10;
                            value += int.Parse(schema[y, j].ToString());
                            length++;
                            if (j == puzzleInputWidth - 1)
                            {
                                if (FindNumber.check_for_symbol(schema, j - (length + 1), j, y - 1, y + 1, puzzleInputSize, puzzleInputWidth, partTwo))
                                {
                                    if (partTwo)
                                    {

                                        foreach (Gear gear in FindNumber.getGears(schema, j - (length + 1), j, y - 1, y + 1, puzzleInputSize, puzzleInputWidth, value))
                                        {
                                            Console.WriteLine("HOI");
                                            gears.AddFirst(gear);
                                        }
                                    }
                                    sum += value;
                                    value = 0;
                                    length = 0;
                                }
                                else
                                {
                                    value = 0;
                                    length = 0;
                                }
                            }
                        }
                        else
                        {
                            if (value > 0)
                            {
                                if (FindNumber.check_for_symbol(schema, j - (length + 1), j, y - 1, y + 1, puzzleInputSize, puzzleInputWidth, partTwo))
                                {
                                 
                                    if (partTwo)
                                    {
                                        
                                        foreach(Gear gear in FindNumber.getGears(schema, j - (length + 1), j, y - 1, y + 1, puzzleInputSize, puzzleInputWidth, value))
                                        {
                                            Console.WriteLine("HOI");
                                            gears.AddFirst(gear);
                                        }
                                    }
                                    sum += value;
                                    value = 0;
                                    length = 0;
                                }
                                else
                                {
                                    value = 0;
                                    length = 0;
                                }
                            }
                        }
                    }
                }
                
                Gear gearIter;
                int product = 0;
                
                while (gears.Count > 0)
                {
                    LinkedListNode<Gear> remove = null;
                    gearIter = gears.First();
                    gears.RemoveFirst();
                    
                    foreach(Gear gear in gears)
                    {
                        
                        if (gear.position[0] == gearIter.position[0] && gear.position[1] == gearIter.position[1])
                        {
                            product += gearIter.adjacentValue * gear.adjacentValue;
                            remove = gears.Find(gear);
                        }
                    }

                    {
                        if (remove != null)
                        {
                            gears.Remove(gears.Find(remove.Value));
                        }
                        remove = null;
                    }
                    
                }

                Console.WriteLine(product);
            }
        }
        internal class Number
        {
            int value { get; set; }

            int length { get; set; }
            bool adjacent { get; set; }
        }
    }

    internal static class FindNumber
    {
        public static bool check_for_symbol(char[,] schema, int x_min, int x_max, int y_min, int y_max, int inputSize, int inputWidth, bool partTwo)
        {
            if (x_min < 0)
            {
                x_min = 0;
            }
            if (x_max >= inputWidth)
            {
                x_max = inputWidth -1;
            } 
            if (y_min < 0)
            {
                y_min = 0;
            }
            if (y_max >= inputSize )
            {
                y_max = inputSize - 1;
            }

            for(int i = y_min; i <= y_max; i++)
            {
                for(int j = x_min; j <= x_max; j++)
                {
                    if (schema[i,j] != Convert.ToChar(".") && !char.IsDigit(schema[i,j]) && !partTwo)
                    {
                        return true;
                    }
                    else if(schema[i,j] == Convert.ToChar("*") && partTwo){
                        return true;
                    }
                }
            }
            return false;
        }

        public static LinkedList<Gear> getGears(char[,] schema, int x_min, int x_max, int y_min, int y_max, int inputSize, int inputWidth, int value)
        {
            LinkedList<Gear> gears = new LinkedList<Gear>();
            if (x_min < 0)
            {
                x_min = 0;
            }
            if (x_max >= inputWidth)
            {
                x_max = inputWidth - 1;
            }
            if (y_min < 0)
            {
                y_min = 0;
            }
            if (y_max >= inputSize)
            {
                y_max = inputSize - 1;
            }
            for (int i = y_min; i <= y_max; i++)
            {
                for (int j = x_min; j <= x_max; j++)
                {
                    if (schema[i,j].Equals(Convert.ToChar("*")))
                    {
                        Console.WriteLine(schema[i, j]);
                        

                        gears.AddFirst(new Gear(value, i, j));
                    }
                }
            }
            return gears;
        }
    }

    internal class Gear
    {
        public int adjacentValue;
        public int[] position;

        public Gear(int adjacentValue, int position_y, int position_x)
        {
            this.adjacentValue = adjacentValue;
            this.position = new[] { position_y, position_x } ;
        } 
        public override String ToString()
        {
            return "GEAR: [" + position[0] + ", " + position[1] + "]: " + adjacentValue.ToString();
        }
        public bool Equals(Gear obj)
        {
            return obj.position == this.position;
        }
    }
    
}
