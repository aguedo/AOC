using AOC.Common.Input;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;

namespace AOC.Season2018.D02
{
    class Solution2 : BaseSolution
    {
        List<string> _list = new List<string>();

        public override void FindSolution()
        {
            var c3 = 0;
            var c2 = 0;

            while (!_stream.EndOfStream)
            {
                var line = _stream.ReadLine();
                _list.Add(line);
            }

            for (int i = 0; i < _list.Count; i++)
            {
                var line1 = _list[i];
                for (int j = 0; j < _list.Count; j++)
                {
                    var line2 = _list[j];
                    if (i != j && line1.Length == line2.Length)
                    {
                        var diff = 0;
                        var index = -1;
                        for (int k = 0; k < line1.Length; k++)
                        {
                            if (line1[k] != line2[k])
                            {
                                diff++;
                                index = k;
                            }
                            if (diff > 1)
                                break;
                        }

                        if (diff == 1)
                        {
                            var result = "";

                            for (int t = 0; t < line2.Length; t++)
                            {
                                if (t != index)
                                    result += line2[t];
                            }

                            Console.WriteLine(result);
                        }
                    }
                }
            }

        }
    }
}
