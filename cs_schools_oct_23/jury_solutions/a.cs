using System;
using System.Linq;

class Program
    {
        static void Main()
        {
            var sizes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var result = 0;
            
            // Сгибаем лист строго по клеточкам и пополам по первому направлению
            // Это можно делать до тех пор, пока количество клеток делится на 2
            while (sizes[0] % 2 == 0)
            {
                result++;
                sizes[0] /= 2;
            }

            // Сгибаем лист строго по клеточкам и пополам по второму направлению
            // Это можно делать до тех пор, пока количество клеток делится на 2
            while (sizes[1] % 2 == 0)
            {
                result++;
                sizes[1] /= 2;
            }

            Console.WriteLine(result);
        }
    }