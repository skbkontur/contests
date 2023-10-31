using System;
using System.Linq;

class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var notes = new (int Left, int Right)[n];
        for (var i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            notes[i] = (input[0], input[1]);
        }

        var sortedNotes = notes.OrderBy(x => x.Left).ToList();
        var currentRight = -1;
        var dryTrees = 0L;
        for (var i = 0; i < n; i++)
        {
            if (sortedNotes[i].Left > currentRight)
            {
                dryTrees += sortedNotes[i].Left - currentRight - 1;
            }

            currentRight = Math.Max(currentRight, sortedNotes[i].Right);
        }

        var answer = sortedNotes.Select(x => x.Right).Max() + 1 - dryTrees;
        Console.WriteLine(answer);
    }
}