using System;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		public static void Main()
		{
			int n = int.Parse(Console.ReadLine());
			int sum = 0;
			for (int i = 0; i < n; i++)
			{
			    int x = int.Parse(Console.ReadLine());
			    sum += x;
			}
			Console.WriteLine(-sum);
		}
	}
}