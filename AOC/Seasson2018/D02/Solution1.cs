using AOC.Common.Solution;
using System;
using System.Collections.Generic;

namespace AOC.Season2018.D02
{
    class Solution1 : BaseSolution
    {
        private int _contains2 = 0;
        private int _contains3 = 0;

        public override void FindSolution()
        {
            while (!_stream.EndOfStream)
            {
                var line = _stream.ReadLine();
                CheckLine(line);
            }

            var result = _contains2 * _contains3;
            Console.WriteLine(result);
        }

        private void CheckLine(string line)
        {
            var dictionary = new Dictionary<char, int>();
            for (int i = 0; i < line.Length; i++)
            {
                var currrent = line[i];
                if (!dictionary.ContainsKey(currrent))
                    dictionary[currrent] = 0;
                dictionary[currrent] += 1;
            }

            var flag2 = false;
            var flag3 = false;
            foreach (var value in dictionary.Values)
            {
                if (value == 2 && !flag2)
                {
                    flag2 = true;
                    _contains2++;
                }
                else if (value == 3 && !flag3)
                {
                    flag3 = true;
                    _contains3++;
                }

                if (flag2 && flag3)
                    return;
            }
        }
    }
}
