using AOC.Common.Solution;
using System;

namespace AOC.Season2018.D08
{
    class Solution1 : BaseSolution
    {
        private int[] _lines;
        private int _total = 0;

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
                next = Read(next);

            for (int i = 0; i < meta; i++)
                _total += _lines[next + i];

            return next + meta;
        }
    }
}
