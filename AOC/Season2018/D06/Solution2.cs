using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D06
{
    class Solution2 : BaseSolution
    {
        private List<Coord> _coords = new List<Coord>();
        private int _length = int.MinValue;
        private int[,] _board;

        /// <summary>
        /// This solution doesn't cover all inputs. Input (1, 1), (1, 2) breaks it.
        /// </summary>
        public override void FindSolution()
        {
            ReadCoords();
            LoadBoard();
            CountRegions();
        }

        private void CountRegions()
        {
            var solution = 0;
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    if (_board[i, j] < 10000)
                        solution++;
                }
            }

            Console.WriteLine(solution);
        }

        private void LoadBoard()
        {
            _board = new int[_length, _length];
            foreach (var item in _coords)
            {
                for (int i = 0; i < _length; i++)
                {
                    for (int j = 0; j < _length; j++)
                    {
                        var cell = _board[i, j];
                        var dist = Math.Abs(item.X - i) + Math.Abs(item.Y - j);
                        _board[i, j] += dist;
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

        public void FindSolutionOld()
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
                        cell.Dist += dist;
                    }
                }
            }

            var solution = 0;

            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    var cell = board[i, j];
                    if (cell.Dist < 10000)
                        solution++;
                }
            }

            Console.WriteLine(solution);
        }

        class Cell
        {
            public int Dist { get; set; }
        }
    }
}
