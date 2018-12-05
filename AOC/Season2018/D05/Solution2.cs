using AOC.Common.Solution;
using System;

namespace AOC.Season2018.D05
{
    class Solution2 : BaseSolution
    {
        private bool[] _flags;
        private string _str;
        private int _len;
        private int _skip;

        public override void FindSolution()
        {
            var tempStr = _stream.ReadLine();
            var solution = int.MaxValue;
            for (int i = 65; i <= 90; i++)
            {
                _str = tempStr;
                _skip = i;
                FindSolution1();
                if (_len < solution)
                    solution = _len;
            } 

            Console.WriteLine(solution);
        }

        private void FindSolution1()
        {
            _flags = new bool[_str.Length];
            _len = _str.Length;

            var skipC1 = (char)_skip;
            var skipC2 = (char)(_skip + 32);
            for (int i = 0; i < _str.Length; i++)
            {
                if (_str[i] == skipC1 || _str[i] == skipC2)
                {
                    _flags[i] = true;
                    _len--;
                }
            }

            int index = 0;
            while (index + 1 < _str.Length)
                index = Match(index, index + 1);
        }

        private int Match(int index1, int index2)
        {
            while (index1 >= 0 && (_flags[index1]))
                index1--;
            if (index1 < 0)
                return index2;

            while (index2 < _flags.Length && _flags[index2])
                index2++;
            if (index2 >= _flags.Length)
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






        public void FindSolution2()
        {
            var str = _stream.ReadLine();

            var min = int.MaxValue;
            for (int i = 65; i <= 65 + 32; i++)
            {
                var temp = Compute(str, i);
                if (temp < min)
                    min = temp;
            }

            Console.WriteLine(min);
        }

        private int Compute(string str, int n)
        {
            var c1 = (char)n;
            var c2 = (char)(n + 32);
            //str = String.Join("", str.Where(t => t != c1 && t != c2));
            str = str.Replace(c1.ToString(), "");
            str = str.Replace(c2.ToString(), "");
            while (true)
            {
                var found = false;

                for (int i = 0; i < str.Length - 1; i++)
                {
                    int current = str[i];
                    int next = str[i + 1];
                    if (Math.Abs(current - next) == 32)
                    {
                        var temp1 = str[i];
                        var temp2 = str[i + 1];
                        str = str.Remove(i, 2);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return str.Length;
                }
            }
        }
    }
}
