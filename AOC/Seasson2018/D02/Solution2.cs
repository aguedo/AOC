using AOC.Common.Solution;
using System;
using System.Collections.Generic;

namespace AOC.Season2018.D02
{
    class Solution2 : BaseSolution
    {
        List<string> _lines = new List<string>();

        public override void FindSolution()
        {
            LoadLines();
            FindIds();
        }

        private void FindIds()
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                var line1 = _lines[i];
                for (int j = 0; j < _lines.Count; j++)
                {
                    var line2 = _lines[j];
                    if (i != j && line1.Length == line2.Length)
                    {
                        var index = FindIndex(line1, line2);
                        if (index >= 0)
                        {
                            var solution = line1.Remove(index, 1);
                            Console.WriteLine(solution);
                            return;
                        }
                    }
                }
            }
        }

        private void LoadLines()
        {
            while (!_stream.EndOfStream)
            {
                var line = _stream.ReadLine();
                _lines.Add(line);
            }
        }

        private int FindIndex(string line1, string line2)
        {
            var index = -1;
            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i] != line2[i])
                {
                    if (index >= 0)
                        return -1;
                    index = i;
                }
            }
            return index;
        }
    }
}
