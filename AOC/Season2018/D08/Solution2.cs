using AOC.Common.Solution;
using System;
using System.Collections.Generic;

namespace AOC.Season2018.D08
{
    class Solution2 : BaseSolution
    {
        private int[] _lines;

        public override void FindSolution()
        {
            _lines = _stream.ReadInt32ArrayLine();
            var temp = Read(0);
            Console.WriteLine(temp.value);
        }

        private (int next, int value) Read(int start)
        {
            var child = _lines[start];
            var meta = _lines[start + 1];

            var next = start + 2;
            var result = 0;
            if (child == 0)
            {
                for (int i = 0; i < meta; i++)
                    result += _lines[next + i];
            }
            else
            {
                var dict = new Dictionary<int, int>();   
                for (int i = 0; i < child; i++)
                {
                    var temp = Read(next);
                    next = temp.next;
                    dict[i + 1] = temp.value;
                }

                for (int i = 0; i < meta; i++)
                {
                    var temp = _lines[next + i];
                    if (dict.ContainsKey(temp))
                        result += dict[temp];
                }
            }

            return (next + meta, result);
        }
    }
}
