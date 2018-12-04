using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D03
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            var len = 1000;
            var area = new int[len, len];

            while (!_stream.EndOfStream)
            {
                var line = _stream.ReadLine();
                var claim = GetClaim(line);

                for (var i = claim.Left; i < claim.Left + claim.Width; i++)
                    for (int j = claim.Top; j < claim.Top + claim.High; j++)
                        area[i, j] += 1;
            }

            var total = 0;
            for (var i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                    if (area[i, j] > 1)
                        total++;

            Console.WriteLine(total);
        }

        private (int Id, int Left, int Top, int Width, int High) GetClaim(string line)
        {
            var pattern = @"#(\d+)\s@\s(\d+),(\d+):\s(\d+)x(\d+)";
            var groups = Regex.Matches(line, pattern)[0].Groups;
            var id = Convert.ToInt32(groups[1].Value);
            var left = Convert.ToInt32(groups[2].Value);
            var top = Convert.ToInt32(groups[3].Value);
            var width = Convert.ToInt32(groups[4].Value);
            var high = Convert.ToInt32(groups[5].Value);

            return (id, left, top, width, high);
        }

        public void FindSolutionOld()
        {
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

        private (int Id, int Left, int Top, int Width, int High) GetClaim2(string line)
        {
            // Todo: try with regex
            var temp = line.Split(' ').ToArray();
            var position = temp[2];
            var temp2 = position.Split(',');
            var left = int.Parse(temp2[0]);
            var top = int.Parse(temp2[1].Substring(0, temp2[1].Length - 1));
            var size = temp[3].Split('x');
            var width = int.Parse(size[0]);
            var high = int.Parse(size[1]);
            var id = int.Parse(temp[0].Substring(1, temp[0].Length - 1));

            return (id, left, top, width, high);
        }
    }
}
