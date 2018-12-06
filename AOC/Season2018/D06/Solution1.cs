using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D06
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            // TODO: refactor. (try with graph algorithms)

            var lines = _stream.ReadStringDocument();
            var coords = new List<Coord>();
            var max = int.MinValue;
            for (int i = 0; i < lines.Count; i++)
            {
                var temp = lines[i].Split(',');
                var coord = new Coord
                {
                    X = int.Parse(temp[0]),
                    Y = int.Parse(temp[1]),
                    Id = i
                };
                if (coord.X > max)
                    max = coord.X;
                if (coord.Y > max)
                    max = coord.Y;
                coords.Add(coord);
            }

            var board = new Cell[max, max];

            foreach (var item in coords)
            {
                for (int i = 0; i < max; i++)
                {
                    for (int j = 0; j < max; j++)
                    {
                        if (board[i, j] == null)
                            board[i, j] = new Cell();

                        var cell = board[i, j];
                        var dist = Math.Abs(item.X - i) + Math.Abs(item.Y - j);
                        if (cell.Dist > dist)
                        {
                            cell.Id = item.Id;
                            cell.Dist = dist;
                            cell.Active = true;
                        }
                        else if (cell.Dist == dist)
                        {
                            cell.Active = false;
                        }
                    }
                }
            }

            var dict = new Dictionary<int, int>();
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    var cell = board[i, j];
                    if (cell.Active)
                    {
                        if (!dict.ContainsKey(cell.Id))
                            dict[cell.Id] = 0;

                        if (i == 0 || i == max - 1 || j == 0 || j == max - 1)
                        {
                            dict[cell.Id] = -1;
                        }

                        if (dict[cell.Id] >= 0)
                            dict[cell.Id] += 1;
                    }
                }
            }

            var solution = dict.Where(t => t.Value >= 0).Max(t => t.Value);
            Console.WriteLine(solution);
        }

        class Cell
        {
            public int Dist { get; set; } = int.MaxValue;
            public int Id { get; set; }
            public bool Active { get; set; }
        }
    }

    
}
