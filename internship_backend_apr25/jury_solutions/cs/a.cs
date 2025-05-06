using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    internal class Program
    {
        private const int AllCardsCount = 52;

        private static readonly HashSet<char> AllSuits = new HashSet<char>()
        {
            'C', 'D', 'H', 'S'
        };

        private static readonly HashSet<char> AllRanks = new HashSet<char>()
        {
            '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'
        };
        
        public static void Main(string[] args)
        {
            var groupsCount = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            
            var probability1 = CalculateProbability(groupsCount[0], groupsCount[1]);
            var probability2 = CalculateProbability(groupsCount[2], groupsCount[3]);
            
            if (probability1 > probability2)
                Console.WriteLine(probability1);
            else
                Console.WriteLine(probability2);
        }

        private static double CalculateProbability(int removedGroupsCount, int soughtGroupsCount)
        {
            var removedCards = new HashSet<string>();
            for (int i = 0; i < removedGroupsCount; i++)
            {
                var group = new HashSet<char>(Console.ReadLine());
                removedCards.UnionWith(GetCards(group));
            }
            
            var soughtCards = new HashSet<string>();
            for (int i = 0; i < soughtGroupsCount; i++)
            {
                var group = new HashSet<char>(Console.ReadLine());
                soughtCards.UnionWith(GetCards(group));
            }
            
            if (removedCards.Count == AllCardsCount)
                return 0.0;
            var removedCardsCount = removedCards.Count;
            
            removedCards.IntersectWith(soughtCards);
            var cardsIntersection = removedCards.Count;
        
            return (double)(soughtCards.Count - cardsIntersection) / (AllCardsCount - removedCardsCount);
        }
        
        private static HashSet<string> GetCards(HashSet<char> group)
        {
            var currentSuits = new HashSet<char>(AllSuits);
            currentSuits.IntersectWith(group);
            if (currentSuits.Count == 0)
                currentSuits = AllSuits;
        
            group.ExceptWith(currentSuits);
            var currentRanks = group;
            if (currentRanks.Count == 0)
                currentRanks = AllRanks;

            var cards = new HashSet<string>();
            foreach (var rank in currentRanks)
            {
                foreach (var suit in currentSuits)
                {
                    cards.Add(rank.ToString() + suit);   
                }
            }

            return cards;
        }
    }
}