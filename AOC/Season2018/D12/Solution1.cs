using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D12
{
    class Solution1 : BaseSolution
    {
        private StringBuilder _str;
        private int _negativeCount;
        private HashSet<string> _patterns = new HashSet<string>();
        private StringBuilder _nextStr;

        public override void FindSolution()
        {
            var lines = _stream.ReadStringDocument();
            var initial = ".##..##..####..#.#.#.###....#...#..#.#.#..#...#....##.#.#.#.#.#..######.##....##.###....##..#.####.#";
            _str = new StringBuilder(initial);
            var initialLen = initial.Length;

            for (int i = 2; i < lines.Count; i++)
            {
                var line = lines[i].Split(' ');
                if (line[2] == "#")
                    _patterns.Add(line[0]);
            }

            for (long k = 0; k < 20; k++)
            {
                _nextStr = new StringBuilder(_str.ToString());
                for (int i = 0; i < _str.Length; i++)
                {
                    var currentPattern = GetPattern(i);
                    if (_patterns.Contains(currentPattern))
                        _nextStr[i] = '#';
                    else
                        _nextStr[i] = '.';
                }

                CheckInitial();
                CheckFinal();

                _str = _nextStr;
            }            

            var final = _str.ToString();
            var initialIndex = _negativeCount;
            long total = 0;
            for (int i = 0; i < final.Length; i++)
            {
                var number = i - initialIndex;
                if (final[i] == '#')
                    total += number;
            }

            Console.WriteLine(total);
        }

        private string GetPattern(int index)
        {
            if (index == 0)
                return $"..{CharStr(index)}{CharStr(index + 1)}{CharStr(index + 2)}";

            if (index == 1)
                return $".{CharStr(index - 1)}{CharStr(index)}{CharStr(index + 1)}{CharStr(index + 2)}";

            if (index == _str.Length - 1)
                return $"{CharStr(index - 2)}{CharStr(index - 1)}{CharStr(index)}..";

            if (index == _str.Length - 2)
                return $"{CharStr(index - 2)}{CharStr(index - 1)}{CharStr(index)}{CharStr(index + 1)}.";

            return $"{CharStr(index - 2)}{CharStr(index - 1)}{CharStr(index)}{CharStr(index + 1)}{CharStr(index + 2)}";
        }

        private string CharStr(int index)
        {
            return _str[index].ToString();
        }

        private void CheckInitial()
        {
            var pattern1 = $"...{CharStr(0)}{CharStr(1)}";
            if (_patterns.Contains(pattern1))
            {
                _negativeCount++;
                _nextStr.Insert(0, '#');
            }
            var pattern2 = $"....{CharStr(0)}";
            if (_patterns.Contains(pattern2))
            {
                _nextStr.Insert(0, "#.");
                _negativeCount += 2;
            }
        }

        private void CheckFinal()
        {
            var pattern1 = $"{CharStr(_str.Length - 2)}{CharStr(_str.Length - 1)}...";
            if (_patterns.Contains(pattern1))
            {
                _nextStr.Append("#");
            }
            var pattern2 = $"{CharStr(_str.Length - 1)}....";
            if (_patterns.Contains(pattern2))
            {
                _nextStr.Append(".#");
            }
        }
    }
}