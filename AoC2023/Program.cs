using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using AoC2023;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
LinkedList<String> puzzleInput;
//puzzleInput = Input.getPuzzleInput("C:\\Users\\robbert.lapoutre\\source\\repos\\AoC2023\\AoC2023\\puzzleInput\\day_01.txt");

//Console.WriteLine(AoC2023.Day1.getSum(puzzleInput, false));
//Console.WriteLine(AoC2023.Day1.getSum(puzzleInput, true));
//puzzleInput = Input.getPuzzleInput("C:\\Users\\robbert.lapoutre\\source\\repos\\AoC2023\\AoC2023\\puzzleInput\\day_02.txt");
//AoC2023.Day2.solve(puzzleInput, false);
//AoC2023.Day2.solve(puzzleInput, true);
//puzzleInput = Input.getPuzzleInput("C:\\Users\\robbert.lapoutre\\source\\repos\\AoC2023\\AoC2023\\puzzleInput\\day_03.txt");
//AoC2023.Day3.solve(puzzleInput, false);
//AoC2023.Day3.solve(puzzleInput, true);
//puzzleInput = Input.getPuzzleInput("C:\\users\\robbert.lapoutre\\source\\repos\\AoC2023\\Aoc2023\\puzzleinput\\day_04.txt");
//AoC2023.Day4.solve(puzzleInput, false);
//AoC2023.Day4.solve(puzzleInput, true);
//puzzleInput = Input.getPuzzleInput("C:\\Users\\robbe\\source\\repos\\tricyphona\\AoC2023\\puzzleInput\\day_05.txt");
//AoC2023.Day5.solve(puzzleInput, false);
//puzzleInput = Input.getPuzzleInput("C:\\Users\\robbe\\source\\repos\\tricyphona\\AoC2023\\puzzleInput\\day_06.txt");
//AoC2023.Day6.solve(puzzleInput, false);
//puzzleInput = Input.getPuzzleInput("C:\\Users\\robbe\\source\\repos\\tricyphona\\AoC2023\\puzzleInput\\day_07.txt");
//AoC2023.Day7.solve(puzzleInput, false);
puzzleInput = Input.getPuzzleInput("C:\\Users\\robbe\\source\\repos\\tricyphona\\AoC2023\\puzzleInput\\day_08.txt");
AoC2023.Day8.solve(puzzleInput, false);
class Input
{
    public static LinkedList<String> getPuzzleInput(String directPath) {
        String line;
        LinkedList<String> puzzleInput = new LinkedList<String>();
        StreamReader sr = new StreamReader(directPath);
        line = sr.ReadLine();
        while (line != null){ 
            puzzleInput.AddLast(line);
            line = sr.ReadLine();
        }
        
        return puzzleInput;
    } 
}
