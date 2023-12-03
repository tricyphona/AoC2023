using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class Day1
    {
            public static int GetCalibrationValue(String line)
            {
                int firstValue = 0;
                int secondValue = 0;
                bool foundFirstValue = false;
                foreach (Char c in line)
                {
                    if (Char.IsDigit(c))
                    {
                        if (foundFirstValue == false)
                        {
                            firstValue = (int)Char.GetNumericValue(c);
                            foundFirstValue = true;
                        }
                        secondValue = (int)Char.GetNumericValue(c); ;
                    }
                }
                return (int)(firstValue * 10 + secondValue);
            }

            public static int getSum(LinkedList<String> puzzleInput, bool partOne)
            {
                int totalValue = 0;
                if (partOne)
                {
                    foreach (String line in puzzleInput)
                    {
                        totalValue += GetCalibrationValue(line);
                    }
                }
                else
                {
                    foreach (String line in puzzleInput)
                    {
                        totalValue += GetCalibrationValuePartTwo(line);
                    }
                }
                return totalValue;
            }

            private static int GetCalibrationValuePartTwo(String line)
            {
                int firstValue;
                int lastValue;

                String regexString = "one|two|three|four|five|six|seven|eight|nine";
                String fullRegexString = regexString + @"|\d{1}";
                firstValue = regexMagic(fullRegexString, line);
                String regexReverseString = Reverse(regexString);
                fullRegexString = regexReverseString + @"|\d{1}";
                lastValue = regexMagic(fullRegexString, Reverse(line));
                return firstValue * 10 + lastValue;
            }
            private static int regexMagic(string regexString, string text)
            {
                String regexValue;
                StringDictionary numberPairs = new StringDictionary();
                numberPairs.Add("one", "1");
                numberPairs.Add("two", "2");
                numberPairs.Add("three", "3");
                numberPairs.Add("four", "4");
                numberPairs.Add("five", "5");
                numberPairs.Add("six", "6");
                numberPairs.Add("seven", "7");
                numberPairs.Add("eight", "8");
                numberPairs.Add("nine", "9");
                numberPairs.Add("eno", "1");
                numberPairs.Add("owt", "2");
                numberPairs.Add("eerht", "3");
                numberPairs.Add("ruof", "4");
                numberPairs.Add("evif", "5");
                numberPairs.Add("xis", "6");
                numberPairs.Add("neves", "7");
                numberPairs.Add("thgie", "8");
                numberPairs.Add("enin", "9");

                Regex rx = new Regex(regexString);
                MatchCollection match = rx.Matches(text);
                if (numberPairs.ContainsKey(match.First().Value))
                {
                    regexValue = numberPairs[match.First().Value];
                }
                else
                {
                    regexValue = match.First().Value;
                }
                return int.Parse(regexValue);
            }
            private static string Reverse(string s)
            {
                char[] charArray = s.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

        }
    }
