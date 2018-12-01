using AOC.Common.Solution;
using System;

namespace AOC.Season2018.D01
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            long total = 0;
            while (!_stream.EndOfStream)
            {
                var value = _stream.ReadLong();
                total += value;
            }

            Console.WriteLine(total);
        }
    }
}
