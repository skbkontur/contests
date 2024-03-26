using System;
using System.Linq;

namespace Summer2024
{
    public class Program
    {
        public static void Main()
        {
            var line = Console.ReadLine();
            var queriesCount = int.Parse(Console.ReadLine());
 
            var alphabet = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i).SelectMany(c => new[] { c, c.ToString().ToUpper()[0] }).ToArray();
            var alphabetCharToIndex = Enumerable.Range(0, alphabet.Length).ToDictionary(x => alphabet[x]);
 
            var offsets = new long[line.Length + 1];
            foreach (var _ in Enumerable.Range(0, queriesCount))
            {
                var (left, right, value) = ReadQuery();
                offsets[left - 1] += value;
                offsets[right] -= value;
            }
 
            foreach (var i in Enumerable.Range(1, line.Length))
                offsets[i] += offsets[i - 1];
 
            var result = Enumerable.Range(0, line.Length).Select(i => alphabet[(alphabetCharToIndex[line[i]] + offsets[i]) % alphabet.Length]);
            Console.WriteLine(string.Join("", result));
        }
 
        private static (int left, int right, int value) ReadQuery()
        {
            var inputValues = Console.ReadLine().Split().Select(int.Parse).ToArray();
            return (inputValues[0], inputValues[1], inputValues[2]);
        }
    }
}