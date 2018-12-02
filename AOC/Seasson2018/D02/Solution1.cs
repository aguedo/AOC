using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Season2018.D02
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            var c3 = 0;
            var c2 = 0;

            while (!_stream.EndOfStream)
            {
                var line = _stream.ReadLine();
                if (Contains3(line))
                    c3++;
                if (Contains2(line))
                    c2++;
            }

            var res = c3 * c2;
            Console.WriteLine(res);
        }

        private bool Contains2(string line)
        {
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < line.Length; i++)
            {
                var currrent = line[i];
                if (!dict.ContainsKey(currrent))
                    dict[currrent] = 0;


                dict[currrent] += 1;
            }

            return dict.Any(t => t.Value == 2);
        }

        private bool Contains3(string line)
        {
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < line.Length; i++)
            {
                var currrent = line[i];
                if (!dict.ContainsKey(currrent))
                    dict[currrent] = 0;

                dict[currrent] += 1;
            }


            return dict.Any(t => t.Value == 3);

        }
    }
}
