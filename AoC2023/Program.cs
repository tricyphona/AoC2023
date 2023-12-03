using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using AoC2023;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

LinkedList<String> puzzleInput = Input.getPuzzleInput("C:\\Users\\robbert.lapoutre\\source\\repos\\AoC2023\\AoC2023\\puzzleInput\\day_01.txt");

Console.WriteLine(AoC2023.Day1.getSum(puzzleInput, false));
Console.WriteLine(AoC2023.Day1.getSum(puzzleInput, true));
puzzleInput = Input.getPuzzleInput("C:\\Users\\robbert.lapoutre\\source\\repos\\AoC2023\\AoC2023\\puzzleInput\\day_02.txt");
AoC2023.Day2.solve(puzzleInput, false);
AoC2023.Day2.solve(puzzleInput, true);
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
