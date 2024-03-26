using System;
using System.Linq;

namespace Summer2024
{
    class Program
    {
        static void Main()
        {
            var solution = new Solution();
            solution.Solve();
        }
    }  
    public class Solution
    {
        public Solution()
        {
            (n, q) = Read2Int();
            rows = new int[n + 2];
            columns = new int[n + 2];
            rows[0] = rows[n + 1] = columns[0] = columns[n + 1] = 1;
            (verticalSplits, horizontalSplits) = (2, 2);
        }

        public void Solve()
        {
            for (var i = 0; i < q; i++)
            {
                var input = Console.ReadLine().Split(' ').ToArray();
                var (type, x, y) = (input[0], int.Parse(input[1]), int.Parse(input[2]));

                if (type == "+")
                {
                    UpdateState(x, y, type);
                    rows[x]++;
                    columns[y]++;
                }
                else
                {
                    rows[x]--;
                    columns[y]--;
                    UpdateState(x, y, type);
                }

                Console.WriteLine(((long)horizontalSplits - 1) * (verticalSplits - 1));
            }
        }

        private void UpdateState(int x, int y, string type)
        {
            if (rows[x] == 0)
            {
                if (rows[x - 1] != 0 && rows[x + 1] != 0)
                    horizontalSplits -= type == "+" ? 1 : -1;
                else if (rows[x - 1] == 0 && rows[x + 1] == 0)
                    horizontalSplits += type == "+" ? 1 : -1;
            }

            if (columns[y] == 0)
            {
                if (columns[y - 1] != 0 && columns[y + 1] != 0)
                    verticalSplits -= type == "+" ? 1 : -1;
                else if (columns[y - 1] == 0 && columns[y + 1] == 0)
                    verticalSplits += type == "+" ? 1 : -1;
            }
        }

        private (int, int) Read2Int()
        {
            var s = Console.ReadLine().Split().ToArray();
            return (int.Parse(s[0]), int.Parse(s[1]));
        }
        
        private int n, q;
        private int[] rows, columns;
        private int verticalSplits, horizontalSplits;
    }
}
