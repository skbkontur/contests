using System;
using System.Collections.Generic;

class Program
{
    public static void Main()
    {
        var ma = new Dictionary<string, string>();
        var ma2 = new Dictionary<string, string>[100000 + 10];
        for (var i = 0; i < ma2.Length; i++)
            ma2[i] = new Dictionary<string, string>();

        var nestingLevel = 0;

        var linesCount = int.Parse(Console.ReadLine());
        for (var lc = 0; lc < linesCount; lc++)
        {
            var line = Console.ReadLine();
            if (line[0] == '{')
            {
                nestingLevel++;
            }
            else if (line[0] == '}')
            {
                foreach (var kvp in ma2[nestingLevel])
                    ma[kvp.Key] = kvp.Value;

                ma2[nestingLevel].Clear();
                nestingLevel--;
            }
            else
            {
                var currentLine = line.Split('=');
                var varName = currentLine[0];
                var varValue = currentLine[1];

                if (!ma.ContainsKey(varName))
                    ma[varName] = "0";

                var newValue = varValue;
                if (varValue[0] >= 'a' && varValue[0] <= 'z')
                {
                    if (!ma.TryGetValue(varValue, out newValue))
                        newValue = "0";
                    Console.WriteLine(newValue);
                }

                if (!ma2[nestingLevel].ContainsKey(varName))
                    ma2[nestingLevel][varName] = ma[varName];

                ma[varName] = newValue;
            }
        }
    }
}