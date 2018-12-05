using AOC.Common.Solution;
using System;

namespace AOC.Season2018.D05
{
    class Solution1 : BaseSolution
    {
        private bool[] _flags;
        private string _str;
        private int _len;        

        public override void FindSolution()
        {
            _str = _stream.ReadLine();
            _flags = new bool[_str.Length];
            _len = _str.Length;

            int index = 0;
            while (index + 1 < _str.Length)
                index = Match(index, index + 1);

            Console.WriteLine(_len);
        }

        private int Match(int index1, int index2)
        {
            while (index1 >= 0 && _flags[index1])
                index1--;
            if (index1 < 0)
                return index2;

            var c1 = (int)_str[index1];
            var c2 = (int)_str[index2];
            if (Math.Abs(c1 - c2) == 32)
            {
                _flags[index1] = true;
                _flags[index2] = true;
                _len -= 2;
                return Match(index1 - 1, index2 + 1);
            }

            return index2;
        }
    }
}
