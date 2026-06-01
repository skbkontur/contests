using System;
using System.Linq;
using System.Text;

class Program
{
    private static int n = 10;
    private static StringBuilder sb = new StringBuilder();

    public static void Main()
    {
        var numbers = Console.ReadLine();
        var res = Solve(numbers);
        Console.WriteLine(res);
    }

    private static string Solve(string input)
    {
        sb.Clear();
        var numbers = input.Split().Select(int.Parse).ToArray();
        var answer = (0, 0, 0);
        for (var i = 0; i < n; ++i)
            if (numbers[i] > 0)
                answer = Max(answer, (1, i, i));

        for (var i = 1; i < n; ++i)
        {
            if (numbers[i] > 0)
            {
                numbers[i] -= 1;
                for (var j = 0; j < n; ++j)
                {
                    if (numbers[j] > 0)
                    {
                        numbers[j] -= 1;
                        answer = Max(answer, MakeLargestNumber(i, j, numbers.Select(x => x).ToArray(), false));
                        numbers[j] += 1;
                    }
                }

                numbers[i] += 1;
            }
        }

        var (len, firstNumber, secondNumber) = answer;
        if (len == 1)
        {
            sb.Append(firstNumber);
        }
        else
        {
            numbers[firstNumber] -= 1;
            numbers[secondNumber] -= 1;
            MakeLargestNumber(firstNumber, secondNumber, numbers, true);
        }
        
        return sb.ToString();
    }

    private static (int, int, int) Max((int, int, int) t1, (int, int, int) t2)
    {
        return t1.CompareTo(t2) > 0 ? t1 : t2;
    }

    private static (int, int, int) MakeLargestNumber(int number1, int number2, int[] numbers, bool print)
    {
        var firstNumber = number1;
        var secondNumber = number2;
        if (print)
        {
            sb.Append(number1);
            sb.Append(number2);
        }

        var len = 2;
        while (true)
        {
            var next = -1;
            for (var d = (99 - number1 - number2) % 3; d < n; d += 3)
                if (numbers[d] > 0)
                    next = d;

            if (next >= 0)
            {
                numbers[next] -= 1;
                len += 1;
                number1 = number2;
                number2 = next;
                if (print) 
                    sb.Append(next);
            }
            else
            {
                break;
            }
        }

        return (len, firstNumber, secondNumber);
    }
}