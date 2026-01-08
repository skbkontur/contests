using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main()
    {
        int answer = 0, answerLeftKey = -1, current = 0, currentLeftKey = -1;

        var n = Convert.ToInt32(Console.ReadLine());
        var firstPath = CreateCompressedPath();
        var m = Convert.ToInt32(Console.ReadLine());
        var secondPath = CreateCompressedPath();

        var firstIterator = firstPath.GetEnumerator();
        var secondIterator = secondPath.GetEnumerator();
        var firstMoveNext = firstIterator.MoveNext();
        var secondMoveNext = secondIterator.MoveNext();

        while (firstMoveNext && secondMoveNext)
        {
            var firstKey = firstIterator.Current.Key;
            var secondKey = secondIterator.Current.Key;
            var firstValue = firstIterator.Current.Value;
            var secondValue = secondIterator.Current.Value;
            if (currentLeftKey == -1)
                currentLeftKey = firstKey;

            if (firstKey == secondKey)
            {
                current += Math.Min(firstValue, secondValue);
                if (answer < current)
                {
                    answer = current;
                    answerLeftKey = currentLeftKey;
                }

                if (firstValue != secondValue)
                {
                    current = Math.Min(firstValue, secondValue);
                    currentLeftKey = firstKey;
                }

                firstMoveNext = firstIterator.MoveNext();
                secondMoveNext = secondIterator.MoveNext();
                continue;
            }

            if (answer < current)
            {
                answer = current;
                answerLeftKey = currentLeftKey;
            }
            current = 0;
            currentLeftKey = -1;

            if (firstKey > secondKey)
                secondMoveNext = secondIterator.MoveNext();
            else
                firstMoveNext = firstIterator.MoveNext();
        }

        Console.WriteLine(answer);
        foreach (var key in firstPath.Keys)
        {
            if (answer == 0)
                break;
            if (key < answerLeftKey)
                continue;

            var count = Math.Min(firstPath[key], secondPath[key]);
            answer -= count;
            for(var i = 0; i < count; i++)
                Console.Write($"{key} ");
        }

        firstIterator.Dispose();
        secondIterator.Dispose();
    }

    private static SortedDictionary<int, int> CreateCompressedPath()
    {
        var groupedPath = Console.ReadLine()?.Split()
            .Select(e => Convert.ToInt32(e))
            .GroupBy(e => e)
            .ToDictionary(e => e.Key, e=> e.Count());
        var compressedPath = new SortedDictionary<int, int>(groupedPath ?? new Dictionary<int, int>());

        return compressedPath;
    }
}