using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC2023
{
    internal class Day4
    {
        public static void solve(LinkedList<String> input, bool partTwo)
        {
            Card[] cards = new Card[input.Count];
            int i = 0;
            foreach (String line in input)
            {
                String[] cardIdNumbers = line.Split(':');
                String[] cardIdList = cardIdNumbers[0].Split(" ");
                int cardId = int.Parse(cardIdList[cardIdList.Length - 1]);
                String[] allNumbers = cardIdNumbers[1].Split("|");
                String[] yourNumbers = allNumbers[0].Split(" ");

                int[] yourNumbersNumbers = Card.convertArray(yourNumbers);
                String[] winningNumbers = allNumbers[1].Split(" ");
                int[] winningNumbersNumbers = Card.convertArray(winningNumbers);
                cards[i] = new Card(cardId, yourNumbersNumbers, winningNumbersNumbers);
                i++;
            }
            int totalPoints = 0;
            foreach (Card card in cards)
            {

                card.updateNumbersWon(partTwo);

                if (!partTwo) { totalPoints += card.points; }
                if (partTwo)
                {
                    for (int j = 0; j < card.points; j++)
                    {
                        if ((card.id + j) < cards.Length)
                        {
                            cards[card.id + j].amountOfCards += card.amountOfCards;
                        }
                    }
                }
            }
            if (partTwo) { 
            int totalAmountOfCards = 0;
            foreach (Card card in cards)
            {
                totalAmountOfCards += card.amountOfCards;

            }
                Console.WriteLine(totalAmountOfCards);
            }
            if (!partTwo) { Console.WriteLine(totalPoints.ToString()); }
            
        }
    }

    internal class Card
    {
        public int id { get; }
        int[] yourNumbers = new int[10];
        int[] winningNumbers = new int[25];
        public int points { get; set; }
        public int amountOfCards = 1;
        public Card(int id, int[] yourNumbers, int[] winningNumbers)
        {
            this.id = id;
            this.yourNumbers = yourNumbers;
            this.winningNumbers = winningNumbers;

        }

    public static int[] convertArray(String[] numbers)
        {

            int[] intNumbers = new int[numbers.Length];
            int i = 0;
            
            foreach(String stringNumber in numbers)
            {
               
                if (Regex.Match(stringNumber, @"\d").Success)
                {
                    
                    intNumbers[i] = int.Parse(stringNumber);
                    i++;
                }
            }
            int[] intNumberslist = new int[i];
            Array.Copy(intNumbers, intNumberslist, i);
            return intNumberslist;
        }
        private bool isNumberInWinning(int number)
        {
            foreach(int winningNumber in this.winningNumbers)
            {
                if(number == winningNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public void updateNumbersWon(bool partTwo)
        {
            foreach(int yourNumber in yourNumbers)
            {
                if (this.isNumberInWinning(yourNumber))
                {
                    if (!partTwo) { 
                        if (this.points == 0)
                        {
                            this.points++;
                        }
                        else
                        {
                            this.points *= 2;
                        }
                    }
                    else
                    {
                        this.points++;
                    }
                }
            }
        }
    }

    

}
