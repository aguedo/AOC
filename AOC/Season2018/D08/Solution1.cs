using AOC.Common.Graphs;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D08
{
    class Solution1 : BaseSolution
    {
        int[] _lines;
        int _total = 0;

        public override void FindSolution()
        {
            _lines = _stream.ReadInt32ArrayLine();
            Read(0);
            Console.WriteLine(_total);
        }

        private int Read(int start)
        {
            var child = _lines[start];
            var meta = _lines[start + 1];

            var next = start + 2;
            for (int i = 0; i < child; i++)
            {
                next = Read(next);
            }

            for (int i = 0; i < meta; i++)
            {
                _total += _lines[next + i];
            }

            return next + meta;
        }
    }
}
