using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D23
{
    class Solution1 : BaseSolution
    {
        private List<Nanobot> _nanobots = new List<Nanobot>();
        private Nanobot _selected = new Nanobot { Radius = int.MinValue };
        public override void FindSolution()
        {
            ReadPositions();

            var count = 0;
            foreach (var nanobot in _nanobots)
            {
                if (_selected.InRange(nanobot))
                    count++;
            }

            Console.WriteLine(count);
        }

        private void ReadPositions()
        {
            var lines = _stream.ReadStringDocument();
            const string pattern = @"pos=<([\d-]+),([\d-]+),([\d-]+)>, r=(\d+)";
            foreach (var line in lines)
            {
                var groups = Regex.Match(line, pattern).Groups;
                var nanobot = new Nanobot
                {
                    Position = new int[3] 
                    {
                        int.Parse(groups[1].Value),
                        int.Parse(groups[2].Value),
                        int.Parse(groups[3].Value),
                    },
                    Radius = int.Parse(groups[4].Value)
                };
                _nanobots.Add(nanobot);
                if (_selected.Radius < nanobot.Radius)
                    _selected = nanobot;
            }
        }

        class Nanobot
        {
            public int[] Position { get; set; } 
            public int Radius { get; set; }

            public bool InRange(Nanobot other)
            {
                var distance = 0;
                for (int i = 0; i < Position.Length; i++)
                {
                    var value1 = Position[i];
                    var value2 = other.Position[i];
                    if ((value1 > 0 && value2 > 0) || (value1 < 0 && value2 < 0))
                        distance += Math.Abs(Math.Abs(value1) - Math.Abs(value2));
                    else
                        distance += Math.Abs(value1) + Math.Abs(value2);
                }

                return distance <= Radius;
            }
        }
    }
}
