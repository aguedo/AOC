using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D12
{
    class Solution2 : BaseSolution
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

            long prevTotal = 0;

            for (long k = 0; k < 50000000000; k++)
            {
                var str = _str.ToString();                

                var count = 0;                 
                _nextStr = new StringBuilder(str);
                for (int i = 0; i < _str.Length; i++)
                {
                    var currentPattern = GetPattern(i);
                    if (_patterns.Contains(currentPattern))
                    {
                        _nextStr[i] = '#';
                        count++;                        
                    }
                    else
                        _nextStr[i] = '.';
                }

                //if (count == 57)
                //{
                //    Console.WriteLine(k);
                //    Console.WriteLine(_str.ToString());
                //}

                //if (k > 200)
                //    break;
                //Console.WriteLine(count);

                CheckInitial();
                CheckFinal();

                _str = _nextStr;

                var total = GetSum();
                Console.WriteLine($"{k} -- {total}----{prevTotal}-----{total - prevTotal}"); // -> 58
                prevTotal = total;

                //var tempStr = _nextStr.ToString();
                //var temp = Regex.Replace(tempStr, @"^(\.+)", Evaluator);
                //_str = new StringBuilder(temp);
            }
        }

        private long GetSum()
        {
            var final = _str.ToString();
            var initialIndex = _negativeCount;
            long total = 0;
            for (int i = 0; i < final.Length; i++)
            {
                var number = i - initialIndex;
                if (final[i] == '#')
                    total += number;
            }
            return total;
        }

        private string Evaluator(Match match)
        {
            return "";
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