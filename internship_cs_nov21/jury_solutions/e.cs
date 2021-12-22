using System;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		static void Main()
		{
			var longs = ReadLongArray();
			var ySize = longs[0];
			var xSize = longs[1];
			var yLines = ReadLongArray();
			var xLines = ReadLongArray();

			var grayRows = CountGrayLines(yLines);
			var grayColumns = CountGrayLines(xLines);

			var black =
				ySize * xLines.Length
				+ xSize * yLines.Length
				- xLines.Length * yLines.Length;
			var gray =
				(ySize - yLines.Length) * grayColumns
				+ (xSize - xLines.Length) * grayRows
				- grayColumns * grayRows;
			var white = ySize * xSize - black - gray;
			Console.WriteLine($"{gray} {white} {black}");
		}

		private static long CountGrayLines(long[] coords)
		{
			long grayLines = 2 * coords.Length - 2;
			for (int i = 0; i < coords.Length - 1; i++)
				if (coords[i + 1] - coords[i] == 2)
					grayLines--;
			return grayLines;
		}

		private static long[] ReadLongArray()
		{
			return Console.ReadLine()
				.Split()
				.Select(long.Parse)
				.ToArray();
		}
	}
}