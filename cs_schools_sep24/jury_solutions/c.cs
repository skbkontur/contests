using System;

public class Solution
{
    static void Main()
    {
        new Solution().Run();
    }

    void Run()
    {
        var s = Console.ReadLine();
        var t = Console.ReadLine();
        int n = s.Length;
        if (s == t)
        {
            for (int i = 1; i < n; i++)
                if (s[i] == s[i - 1])
                {
                    Console.WriteLine("YES");
                    return;
                }
            Console.WriteLine("NO");
            return;
        }

        int l = 0;
        while (s[l] == t[l])
            l++;

        int r = n - 1;
        while (s[r] == t[r])
            r--;

        if ((s.Substring(l, r - l) == t.Substring(l + 1, r - l) && s[r] == t[l])
            || (s.Substring(l + 1, r - l) == t.Substring(l, r - l) && s[l] == t[r])
            )
            Console.WriteLine("YES");
        else
            Console.WriteLine("NO");

    }
}