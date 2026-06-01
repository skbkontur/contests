using System;
using System.Collections.Generic;
using System.Linq;
 
class Program
{
    private const int nn = 1 << 11;

    private static int[,] _ans;
    private static List<(int, int)>[] _colorCoordinates;
    private static int[] _segmentTree;

    private static int _ourN;
    private static int _ourC;
 
    public static void Main()
    {
        Read();
        Solve();
        Output();
    }

    private static void Read()
    {
        _ourN = int.Parse(Console.ReadLine());
        _ourC = _ourN * _ourN;
        _colorCoordinates = new List<(int, int)>[_ourC];
        _ans = new int[_ourN, _ourN];
        _segmentTree = new int[nn + _ourN + 5];
 
        for (var i = 0; i < _ourC; i++)
            _colorCoordinates[i] = new List<(int, int)>();
 
        for (var i = 0; i < _ourN; i++)
        {
            var parts = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for (var j = 0; j < _ourN; j++)
            {
                _colorCoordinates[parts[j] - 1].Add((i, j));
            }
        }
    }
  
    private static void Solve()
    {
        Array.Clear(_ans, 0, _ans.Length);
 
        SolveForTopLeft();
 
        for (var i = 0; i < _ourC; i++)
        {
            _colorCoordinates[i].Reverse();
            for (var k = 0; k < _colorCoordinates[i].Count; k++)
            {
                var cell = _colorCoordinates[i][k];
                _colorCoordinates[i][k] = (_ourN - 1 - cell.Item1, _ourN - 1 - cell.Item2);
            }
        }
 
        Flip(_ans);
        SolveForTopLeft();
        Flip(_ans);
    }
 
    private static void SolveForTopLeft()
    {
        for (var c = 0; c < _ourC; c++)
        {
            if (_colorCoordinates[c].Count > 0)
            {
                foreach (var coordinate in _colorCoordinates[c])
                {
                    var mx = SegmentTreeMaximum(coordinate.Item2) + 1;
                    SegmentTreeUpdate(coordinate.Item2, mx);
                    _ans[coordinate.Item1, coordinate.Item2] += mx;
                }
                if (_colorCoordinates[c].Count > 20)
                    Array.Clear(_segmentTree, 0, _segmentTree.Length);
                else
                    foreach (var coordinate in _colorCoordinates[c])
                        SegmentTreeClear(coordinate.Item2);
            }
        }
    }
 
    private static void Flip(int[,] a)
    {
        for (int i = 0; i < _ourN / 2; i++)
        {
            for (int j = 0; j < _ourN; j++)
            {
                int tmp = a[i, j];
                a[i, j] = a[_ourN - 1 - i, j];
                a[_ourN - 1 - i, j] = tmp;
            }
        }
 
        for (int i = 0; i < _ourN; i++)
        {
            for (int j = 0; j < _ourN / 2; j++)
            {
                int tmp = a[i, j];
                a[i, j] = a[i, _ourN - 1 - j];
                a[i, _ourN - 1 - j] = tmp;
            }
        }
    }

    private static void SegmentTreeUpdate(int j, int v)
    {
        for (var i = j + nn; i > 0; i /= 2)
        {
            _segmentTree[i] = Math.Max(_segmentTree[i], v);
        }
    } 
    
    private static void SegmentTreeClear(int j)
    {
        for (var i = j + nn; i > 0; i /= 2)
        {
            _segmentTree[i] = 0;
        }
    }
 
    private static int SegmentTreeMaximum(int j)
    {
        var mx = _segmentTree[j + nn];
        for (var i = j + nn; i > 0; i /= 2)
        {
            if (i % 2 == 1)
            {
                mx = Math.Max(mx, _segmentTree[i - 1]);
            }
        }
        return mx;
    }
 
    private static void Output()
    {
        var counts = new int[_ourN + _ourN];
 
        for (var i = 0; i < _ourN; i++)
            for (var j = 0; j < _ourN; j++)
                counts[_ans[i, j] - 1]++;
 
        for (var i = 1; i < 2 * _ourN; i++)
            Console.WriteLine(counts[i]);
    } 
}
