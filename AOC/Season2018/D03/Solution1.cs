using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D03
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            // Todo: refactor and try better solution

            var len = 1000;
            var area = new int[len, len];

            while (!_stream.EndOfStream)
            {
                var item = _stream.ReadLine();
                var temp = item.Split(' ').ToArray();
                var pos = temp[2];
                var temp2 = pos.Split(',');
                var left = int.Parse(temp2[0]);
                var top = int.Parse(temp2[1].Substring(0, temp2[1].Length - 1));

                var size = temp[3].Split('x');
                var w = int.Parse(size[0]);
                var h = int.Parse(size[1]);

                for (var i = left; i < left + w; i++)
                {
                    for (int j = top; j < top + h; j++)
                    {
                        area[i, j] += 1;
                    }
                }
            }

            var total = 0;
            for (var i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (area[i, j] > 1)
                        total++;
                }
            }

            Console.WriteLine(total);

        }
    }
}
