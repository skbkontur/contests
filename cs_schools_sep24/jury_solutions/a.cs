using System;

public class Solution
{
    static void Main()
    {
        new Solution().Run();
    }

    int GetDistance(int start, int l, int r)
    {
        if (l == -1)
            return 0;
        if (r < start)
            return start - l;
        if (l > start)
            return r - start;
        return Math.Min((start - l) * 2 + (r - start), (start - l) + (r - start) * 2);
    }

    void Run()
    {
        var s = Console.ReadLine();
        int start = s.IndexOf('O');
        int left = s.IndexOf('X');
        int right = s.LastIndexOf('X');
        Console.WriteLine(GetDistance(start, left, right));
    }
}