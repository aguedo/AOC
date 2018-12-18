using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D17
{
    class Solution1 : BaseSolution
    {
        private Cell[,] _board;
        private List<XInterval> _xIntervals = new List<XInterval>();
        private List<YInterval> _yIntervals = new List<YInterval>();
        private int _minX = int.MaxValue;
        private int _maxX = int.MinValue;
        private int _minY = int.MaxValue;
        private int _maxY = int.MinValue;
        private int _boardMaxY, _boardMaxX;
        private int _total;

        public override void FindSolution()
        {
            BuildBoard();
            
            //Print();
            MoveDown(0, GetX(500));
            //Print();
            Console.WriteLine(_total);
            FindLeftWater();
        }

        private void FindLeftWater()
        {
            var result = 0;
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    var current = _board[i, j];
                    if (current != null && current.Type == Type.RestWater)
                        result++;
                }
            }
            Console.WriteLine(result);
        }

        private bool MoveDown(int y, int x)
        {
            if (y > _boardMaxY)
                return true;
            var cell = _board[y, x];
            if (cell != null && (cell.Type == Type.Sand || cell.Type == Type.RestWater))
                return false;
            if (cell != null && cell.Type == Type.FluidWater)
                return true;

            _total++;
            var newCell = new Cell();
            _board[y, x] = newCell;
            if (MoveDown(y + 1, x))
            {
                newCell.Type = Type.FluidWater;
                return true;
            }

            var moveLeft = MoveLeft(y, x - 1, newCell);
            var moveRight = MoveRight(y, x + 1, newCell);
            if (moveLeft || moveRight)
            {
                newCell.Type = Type.FluidWater;
                return true;
            }
            newCell.Type = Type.RestWater;
            return false;
        }

        private bool MoveLeft(int y, int x, Cell parent)
        {
            if (x < 0)
                return false;
            var cell = _board[y, x];
            if (cell != null && (cell.Type == Type.Sand || cell.Type == Type.RestWater))
                return false;
            if (cell != null && cell.Type == Type.FluidWater)
                return true;

            _total++;
            _board[y, x] = parent;
            if (MoveDown(y + 1, x))
                return true;

            return MoveLeft(y, x - 1, parent);
        }

        private bool MoveRight(int y, int x, Cell parent)
        {
            if (x >= _board.GetLength(1))
                return false;
            var cell = _board[y, x];
            if (cell != null && (cell.Type == Type.Sand || cell.Type == Type.RestWater))
                return false;
            if (cell != null && cell.Type == Type.FluidWater)
                return true;

            _total++;
            _board[y, x] = parent;
            if (MoveDown(y + 1, x))
                return true;

            return MoveRight(y, x + 1, parent);
        }

        private void BuildBoard()
        {
            var lines = _stream.ReadStringDocument();
            const string pattern1 = @"^x=(\d+), y=(\d+)\.\.(\d+)";
            const string pattern2 = @"^y=(\d+), x=(\d+)\.\.(\d+)";

            foreach (var line in lines)
            {
                var match = Regex.Match(line, pattern2);
                if (match.Success)
                {
                    var groups = match.Groups;
                    var xInterval = new XInterval
                    {
                        Y = int.Parse(groups[1].Value),
                        MinX = int.Parse(groups[2].Value),
                        MaxX = int.Parse(groups[3].Value)
                    };
                    _xIntervals.Add(xInterval);
                    CheckInterval(xInterval);
                }
                else
                {
                    match = Regex.Match(line, pattern1);
                    if (match.Success)
                    {
                        var groups = match.Groups;
                        var yInterval = new YInterval
                        {
                            X = int.Parse(groups[1].Value),
                            MinY = int.Parse(groups[2].Value),
                            MaxY = int.Parse(groups[3].Value)
                        };
                        _yIntervals.Add(yInterval);
                        CheckInterval(yInterval);
                    }
                    else
                        throw new Exception("Invalid");
                }
            }

            var xLen = _maxX - _minX + 1;
            var yLen = _maxY - _minY + 1;
            _board = new Cell[yLen, xLen + 2];
            _boardMaxX = GetX(_maxX);
            _boardMaxY = GetY(_maxY);

            foreach (var item in _xIntervals)
            {
                var y = GetY(item.Y);
                for (int i = item.MinX; i <= item.MaxX; i++)
                {
                    _board[y, GetX(i)] = new Cell { Type = Type.Sand };
                }
            }

            foreach (var item in _yIntervals)
            {
                var x = GetX(item.X);
                for (int i = item.MinY; i <= item.MaxY; i++)
                {
                    _board[GetY(i), x] = new Cell { Type = Type.Sand };
                }
            }
        }

        private int GetX(int x)
        {
            return x - _minX + 1;
        }

        private int GetY(int y)
        {
            return y - _minY;
        }

        private void CheckInterval(YInterval yInterval)
        {
            if (yInterval.MinY < _minY)
                _minY = yInterval.MinY;
            if (yInterval.MaxY > _maxY)
                _maxY = yInterval.MaxY;
            if (yInterval.X < _minX)
                _minX = yInterval.X;
            if (yInterval.X > _maxX)
                _maxX = yInterval.X;
        }

        private void CheckInterval(XInterval xInterval)
        {
            if (xInterval.MinX < _minX)
                _minX = xInterval.MinX;
            if (xInterval.MaxX > _maxX)
                _maxX = xInterval.MaxX;
            if (xInterval.Y < _minY)
                _minY = xInterval.Y;
            if (xInterval.Y > _maxY)
                _maxY = xInterval.Y;
        }

        private void Print()
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    var current = _board[i, j];
                    if (i == 0 && j == GetX(500))
                        Console.Write("+");
                    else if (current == null)
                        Console.Write(".");
                    else if (current.Type == Type.Sand)
                        Console.Write("#");
                    else if (current.Type == Type.RestWater)
                        Console.Write("~");
                    else if (current.Type == Type.FluidWater)
                        Console.Write("|");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }
    }

    class XInterval
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int Y { get; set; }
    }

    class YInterval
    {
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public int X { get; set; }
    }

    class Cell
    {
        public Type Type { get; set; }
    }

    enum Type
    {
        Sand = 0,
        Clay = 1,
        RestWater = 2,
        FluidWater = 3
    }

}
