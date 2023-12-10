using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    public static class StepsTaken
    {
        static public int steps = 0;
    }
    internal class Day8
    {
        public static void solve(LinkedList<String> input, bool PartTwo)
        {
            String[] lines = input.ToArray<String>();
            String instructions = lines[0];
            LinkedList<NodeDesert?> start = new LinkedList<NodeDesert?>();
            LinkedList<NodeDesert?> end = new LinkedList<NodeDesert?>();
            NodeDesert[] nodeDeserts = new NodeDesert[lines.Length - 2];
            for (int i = 2; i < lines.Length; i++)
            {
                String[] rowInput = lines[i].Split("=");
                nodeDeserts[i - 2] = new NodeDesert(rowInput[0], rowInput[1]);
                if (rowInput[0][2] == 'A')
                {
                    start.AddLast(nodeDeserts[i - 2]);
                }
                if (rowInput[0][2] == 'Z')
                {
                    end.AddLast(nodeDeserts[i - 2]);
                }
            }
            foreach (NodeDesert nodeDesert in nodeDeserts)
            {
                nodeDesert.assignLeft(nodeDeserts);
                nodeDesert.assignRight(nodeDeserts);
            }

            LinkedList<NodeDesert> currentNodes = start;
            bool searchingEnd = true;
            int startingPointNumber = 0;
            int[] scores = new int[start.Count];
            Console.WriteLine(start.Count);
            Console.WriteLine(end.Count);
            foreach (NodeDesert startingPoint in start)
            {
                NodeDesert currentNode = startingPoint;
                while (!NodeDesert.findEnd(currentNode))
                {
                    Char instruction = instructions[StepsTaken.steps % instructions.Length];
                        currentNode = currentNode.takeStep(instruction);
                    
                    StepsTaken.steps++;
                    Console.WriteLine(StepsTaken.steps);
                }
                scores[startingPointNumber] = StepsTaken.steps;
                startingPointNumber++;
                StepsTaken.steps = 0;
            }
            Console.WriteLine("STEPS:!!!");
            foreach(int score in scores)
            {
                Console.WriteLine(score.ToString());
            }
            // scores in score LCM op loslaten. 
        }
    }

    internal class NodeDesert
    {
        public String current;
        public String options;
        NodeDesert left;
        NodeDesert right;

        public NodeDesert(String current, String options)
        {
            this.current = current;
            this.options = options;
        }

        public void assignLeft(NodeDesert[] nodeDeserts)
        {
            String left = this.options.Substring(2, 3);
            for (int i = 0; i < nodeDeserts.Length; i++)
            {
                if (nodeDeserts[i].current.Contains(left))
                {
                    this.left = nodeDeserts[i]; }
            }
        }
        public String ToString()
        {
            return current;
        }
        public void assignRight(NodeDesert[] nodeDeserts)
        {
            String right = this.options.Substring(7, 3);
            for (int i = 0; i < nodeDeserts.Length; i++)
            {
                if (nodeDeserts[i].current.Contains(right))
                {
                    this.right = nodeDeserts[i];
                }
            }
        }
        public NodeDesert takeStep(Char direction)
        {

            if (direction == 'L')
            {
                return this.left;
            }
            else
            {
                return this.right;
            }
        }
        public static bool findEnd(NodeDesert currentNode) {
                if (currentNode.current[2] == 'Z')
                {
                    return true;
                }
            return false;
        } }
}
