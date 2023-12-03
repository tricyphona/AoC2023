using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace AoC2023
{
    internal static class Day2
    {
        public static void solve(LinkedList<String> input, bool secondPart)
        {
            LinkedList<Bag> allGames = new LinkedList<Bag>();
            int score = 0;

            foreach (String line in input)
            {
                String[] game = line.Split(':');
                String[] rounds = game[1].Split(';');
                LinkedList<Round> saveRounds = new LinkedList<Round>();
                Bag bag = new Bag(int.Parse(game[0].Split(" ")[1]));
                allGames.AddLast(bag);
                foreach(String round in rounds)
                {
                    int red = 0;
                    int blue = 0;
                    int green = 0;
                    String[] items = round.Split(",");
                    foreach(String item in items)
                    {
                        String[] result = item.Split(" ");

                        if (result[2].Equals("red")){
                            red = int.Parse(result[1]);
                        }
                        if (result[2].Equals("blue")){
                            blue = int.Parse(result[1]);
                        }
                        if (result[2].Equals("green")){
                            green = int.Parse(result[1]);
                        }
                        
                        saveRounds.AddLast(new Round(red, green, blue));
                    }
                }
                foreach (Round round in saveRounds) {
                    bag.addRound(round);
                     }
                bag.compareRounds();
            }
        
            foreach(Bag game in allGames)
            {
                if (game.possibleContainedOrLess() && !secondPart)
                {
                    
                    score += game.id;
                }
                else
                {
                    score += game.getPower();
                }
            }
            Console.WriteLine(score);
        }

    }

    internal class Bag
    {
        public int id { get; set; }
        public int cubes_red { get; set; }
        public int cubes_green { get; set; }
        public int cubes_blue { get; set; }
        LinkedList<Round> rounds;
        public Bag(int id)
        {
            this.id = id;
            this.cubes_red = 0;
            this.cubes_green = 0;
            this.cubes_blue = 0;
            this.rounds = new LinkedList<Round>();
        }
        public void addRound(Round round)
        {
            rounds.AddLast(round);
        }
        public void compareRounds()
        {
            foreach(Round round in rounds)
            {
                if (round.cubes_red > this.cubes_red)
                {
                    this.cubes_red = round.cubes_red;
                }
                if (round.cubes_green > this.cubes_green)
                {
                    this.cubes_green = round.cubes_green;
                }
                if (round.cubes_blue > this.cubes_blue)
                {
                    this.cubes_blue = round.cubes_blue;
                }
            }
        }
        public int getPower()
        {
            return this.cubes_red * this.cubes_green * this.cubes_blue;
        }
        
        public bool possibleContainedOrLess()
        {
            if (this.cubes_red <= 12 && this.cubes_green <= 13 && this.cubes_blue <= 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Round
    {
        public int cubes_red { set; get; }
        public int cubes_green { set; get; }
        public int cubes_blue { set; get; }
        public Round(int cubes_red, int cubes_green, int cubes_blue)
        {
            this.cubes_red = cubes_red;
            this.cubes_green = cubes_green;
            this.cubes_blue = cubes_blue;
        }
        
        public String toString()
        {
            return "Red: " + cubes_red + "\n Green: " + cubes_green + "\n Blue: " + cubes_blue;
        }
    }
    
}
