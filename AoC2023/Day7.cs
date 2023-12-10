using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2023
{
    public enum CardValue
    {
        T=10, J=1, Q=11, K, A


    }
    internal class Day7
    {public static void solve(LinkedList<String> input, bool PartTwo)
        {
            Hand[] allCardsStart = new Hand[input.Count];
            String[] lines = input.ToArray<String>();
            int iteratorLines = 0;
            foreach (String line in lines)
            {
                String cardValue = line.Split(" ")[0];
                String bid = line.Split(" ")[1];
                allCardsStart[iteratorLines] = new Hand(cardValue.ToString(), int.Parse(bid));
                iteratorLines++;
            }
            LinkedList<Hand>[] handPiles = new LinkedList<Hand>[7];
            for (int i = 0; i < handPiles.Count(); i++)
            {
                handPiles[i] = new LinkedList<Hand>();
            }
            foreach (Hand hand in allCardsStart)
            {
                handPiles[hand.getTypeHand()].AddLast(hand);
            }

            LinkedList<Hand>[] sortedHands = new LinkedList<Hand>[7];
            for(int i = 0; i < handPiles.Count(); i++)
            {
                LinkedList<Hand> sortedList = new LinkedList<Hand>();
                
                
                foreach(Hand hand in handPiles[i])
                {
                    if(sortedList.Count == 0)
                    {
                        sortedList.AddLast(hand);
                        
                    }
                    else
                    {
                        LinkedListNode<Hand> currentHand = sortedList.First;
                        bool foundPlace = false;
                        int sortedListCount = sortedList.Count;
                        for(int j = 0; j < sortedListCount; j++)
                        {
                            //Console.WriteLine(hand.cards);
                            //Console.WriteLine(currentHand.Value.cards);
                            //Console.WriteLine(currentHand.Value.compareHands(hand));
                            if (currentHand.Value.compareHands(hand) < 0)
                            {
                                //Console.WriteLine(hand.cards);
                                //Console.WriteLine(currentHand.Value.cards);
                                foundPlace = true;
                                sortedList.AddBefore(currentHand, hand);
                                break;
                            }
                            currentHand = currentHand.Next;
                        }
                        if (!foundPlace)
                        {
                            sortedList.AddLast(hand);
                        }
                    }
                }
                sortedHands[i] = sortedList;

            }
            int counter = 0;
            int max = 0;
            foreach (LinkedList<Hand> hands in sortedHands)
            {
                max += hands.Count();
            }
            Console.WriteLine(max);
            int totalWinnings = 0;
            foreach(LinkedList<Hand> hands in sortedHands)
            {
                foreach(Hand hand in hands)
                {
                    totalWinnings += hand.bid * max;

                    //if (hand.cards[0] == 'J' || hand.cards[1] == 'J' || hand.cards[2] == 'J' || hand.cards[2] == 'J' || hand.cards[3] == 'J' || hand.cards[4] == 'J')
                    //{
                        Console.WriteLine(hand.cards[0].ToString() + hand.cards[1].ToString() + hand.cards[2].ToString() + hand.cards[3].ToString() + hand.cards[4].ToString() + ":  " + hand.bid + " " + max + " " + hand.kind);
                    //}
                    max--;
                }
            }
            Console.WriteLine(totalWinnings);
        }
    }
    internal class Hand
    {
        int pointsWorth;
        public char[] cards = new char[5];
        public int bid;
        public int kind;
        public Hand(String cards, int bid)
        {
            for(int i = 0; i < cards.Length; i++)
            {
                this.cards[i] = cards[i];
            }
            this.bid = bid;
            this.kind = this.getTypeHand();
        }
        public int getTypeHand()
        {
            if (this.isFiveOfAKind()) { return 0; }
            else if (this.isFourOfAKind()) { return 1; }
            else if (this.isFullHouse()) { return 2; }
            else if (this.isThreeOfAKind()) { return 3; }
            else if (this.isPair()) {  return 5; }
            else if (this.isHighCard()) { return 6; }
            else { return 4; }
        
        }

        private bool isFiveOfAKind()
        {
                int foundMatch = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (cards[i] == cards[j] || cards[j] == 'J')
                    {
                        foundMatch++;
                    }
                }
                if (foundMatch == 5)
                {
                    return true;
                }
                foundMatch = 0;
            }
            return false;
        }
        private bool isFourOfAKind()
        {
            for (int i = 0; i < 5; i++)
            {
                int foundMatch = 0;
                for (int j = 0; j < cards.Length; j++)
                {

                    
                    if (cards[i] == cards[j] || cards[j] == 'J')
                    {
                        foundMatch++;
                    }

                }
                if (foundMatch == 4)
                {
                    return true;
                }
            }
            return false;
        }
        private bool isFullHouse()
        {
            char? secondCard = null;
            char? firstCard = null;
            for(int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != 'J' && firstCard == null)
                {
                    firstCard = cards[i];
                }
            }
            for (int i = 0; i < cards.Length; i++)
            {
                if (firstCard != cards[i] && secondCard == null && cards[i] != 'J')
                {
                    secondCard = cards[i];
                }
                else if (cards[i].Equals(firstCard) || cards[i] == 'J' || cards[i].Equals(secondCard))
                {
                    ;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool isThreeOfAKind()
        {
            for (int i = 0; i < cards.Length;i++)
            {
                int foundDuplicates = 0;
                for (int j = 0; j < cards.Length; j++)
                {
                    if ((cards[i] == cards[j] || cards[j] == 'J'))
                    {
                        foundDuplicates++;
                    }
                }
                if (foundDuplicates == 3)
                {
                    return true;
                }

            }
            return false;
        }

        private bool isPair()
        {
            int foundPair = 0;
            for(int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (cards[i] == cards[j] && i != j)
                    {
                        foundPair++;

                    }
                }
            }
            if(foundPair == 2)
            {
                return true;
            }

            if (cards.Contains('J'))
            {
                return true;
            }
            return false;
        }

        private bool isHighCard()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (cards[i] == cards[j] && i != j || cards[j] == 'J')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int compareHands(Hand otherHand)
        {
            for(int i = 0; i < this.cards.Length;i++)
            {
                int card1;
                if (!int.TryParse(this.cards[i].ToString(), out card1))
                {
                    card1 = (int)Enum.Parse(typeof(CardValue), this.cards[i].ToString());
                }
                int card2;
                if (!int.TryParse(otherHand.cards[i].ToString(), out card2))
                {
                    card2 = (int)Enum.Parse(typeof(CardValue), otherHand.cards[i].ToString());
                }
                if(card1 < card2)
                {
                    return -1;
                }
                if(card1 > card2)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
