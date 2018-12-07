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
        private List<Coord> _coords = new List<Coord>();
        private int _length = int.MinValue;
        private Cell[,] _board;

        public override void FindSolution()
        {
            ReadCoords();
            LoadBoard();
            CountRegions();           
        }

        private void CountRegions()
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    var cell = _board[i, j];
                    if (cell.Active)
                    {
                        if (!dict.ContainsKey(cell.Id))
                            dict[cell.Id] = 0;

                        if (i == 0 || i == _length - 1 || j == 0 || j == _length - 1)
                        {
                            dict[cell.Id] = -1;
                        }

                        if (dict[cell.Id] >= 0)
                            dict[cell.Id] += 1;
                    }
                }
            }

            var solution = dict.Max(t => t.Value);
            Console.WriteLine(solution);
        }

        private void LoadBoard()
        {
            _board = new Cell[_length, _length];
            foreach (var item in _coords)
            {
                for (int i = 0; i < _length; i++)
                {
                    for (int j = 0; j < _length; j++)
                    {
                        if (_board[i, j] == null)
                            _board[i, j] = new Cell();

                        var cell = _board[i, j];
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
        }

        private void ReadCoords()
        {
            var lines = _stream.ReadStringDocument();            
            for (int i = 0; i < lines.Count; i++)
            {
                var temp = lines[i].Split(',');
                var coord = new Coord
                {
                    X = int.Parse(temp[0]),
                    Y = int.Parse(temp[1]),
                    Id = i
                };
                if (coord.X > _length)
                    _length = coord.X;
                if (coord.Y > _length)
                    _length = coord.Y;
                _coords.Add(coord);
            }
        }

        class Cell
        {
            public int Dist { get; set; } = int.MaxValue;
            public int Id { get; set; }
            public bool Active { get; set; }
        }
    }

    
}
