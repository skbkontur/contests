using System;
using System.Linq;


class Program
{
    static void Main()
    {
        Solve();
    }

    public static void Solve()
    {
        var bounds = Console.ReadLine().Split().Select(long.Parse).ToArray();
        var maxX = bounds[0];
        var maxY = bounds[1];
        
        var pointsCount = Console.ReadLine().Split().Select(long.Parse).ToArray();
        var xCount = pointsCount[0];
        var yCount = pointsCount[1];
        
        var xLines = Console.ReadLine().Split().Select(long.Parse).ToList();
        var yLines = Console.ReadLine().Split().Select(long.Parse).ToList();
        
        var queriesCount = int.Parse(Console.ReadLine());
        
        xLines.Add(0);
        xLines.Add(maxX);
        xLines.Sort();
        
        yLines.Add(0);
        yLines.Add(maxY);
        yLines.Sort();

        while (queriesCount-- > 0)
        {
            var query = Console.ReadLine().Split().Select(long.Parse).ToArray();
            var x1 = query[0];
            var y1 = query[1];
            
            var x2 = query[2];
            var y2 = query[3];

            var xRangDiff = xLines.BinarySearch(x1) - xLines.BinarySearch(x2) == 0;
            var yRangDiff = yLines.BinarySearch(y1) - yLines.BinarySearch(y2) == 0;
            
            if (xRangDiff && yRangDiff)
                Console.WriteLine("YES");
            else
                Console.WriteLine("NO");
        }
        
    }
}