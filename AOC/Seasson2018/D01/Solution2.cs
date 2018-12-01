using AOC.Common.Input;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;

namespace AOC.Season2018.D01
{
    class Solution2 : BaseSolution
    {
        private HashSet<long> _set = new HashSet<long>();
        private long _total = 0;

        public override void FindSolution()
        {
            _set.Add(_total);

            while (!Iterate())
            {
                _stream.Dispose();
                _stream = new MyFileStream();
            }
        }

        private bool Iterate()
        {
            while (!_stream.EndOfStream)
            {
                var value = _stream.ReadLong();
                _total += value;
                if (!_set.Add(_total))
                {
                    Console.WriteLine(_total);
                    return true;
                }
            }

            return false;
        }
    }
}
