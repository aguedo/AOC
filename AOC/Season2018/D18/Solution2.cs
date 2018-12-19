using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D18
{
    class Solution2 : BaseSolution
    {
        private int _len;
        private Cell[,] _board;
        private int _minute = 0;
        private int _getIndex = 0;
        private int _setIndex = 0;
        private int[] _directions = new int[] { -1, 0, 1 };
        private HashSet<string> _hash = new HashSet<string>();

        public override void FindSolution()
        {
            // Todo: refactor.
            ReadBoard();

            while (!NextMinute())
            { }

            var cycle = _minute - _duplicatedMinute;
            var remaining = 1000000000 - _minute;
            var left = remaining % cycle;
            _duplicatedMinute = 0;
            while (left > 0)
            {
                NextMinute(false);
                left--;
            }

            var trees = 0;
            var lumberyards = 0;
            _getIndex = _setIndex;

            for (int i = 0; i < _len; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    var cell = _board[i, j];
                    var type = GetType(cell);
                    if (type == Type.Lumberyard)
                        lumberyards++;
                    else if (type == Type.Tree)
                        trees++;
                }
            }

            var result = lumberyards * trees;
            Console.WriteLine(result);

        }

        private int _duplicatedMinute = 0;
        private string _duplicatedStr = "";

        private bool NextMinute(bool useHash = true)
        {
            _minute++;
            _getIndex = (_minute + 1) % 2;
            _setIndex = _minute % 2;
            
            //Print();
            var strBuilder = new StringBuilder();
            for (int i = 0; i < _len; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    if (i == 0 && j == 8)
                    {
                    }
                    var cell = _board[i, j];
                    var type = GetType(cell);
                    var adjacents = GetAdjacents(i, j);
                    if (type == Type.Open)
                    {
                        if (adjacents.tree >= 3)
                        {
                            SetType(cell, Type.Tree);
                            strBuilder.Append(GetChar(Type.Tree));
                        }
                        else
                        {
                            SetType(cell, type);
                            strBuilder.Append(GetChar(type));
                        }
                    }
                    else if (type == Type.Tree)
                    {
                        if (adjacents.lumberyard >= 3)
                        {
                            SetType(cell, Type.Lumberyard);
                            strBuilder.Append(GetChar(Type.Lumberyard));
                        }
                        else
                        {
                            SetType(cell, type);
                            strBuilder.Append(GetChar(type));
                        }
                    }
                    else
                    {
                        if (adjacents.lumberyard == 0 || adjacents.tree == 0)
                        {
                            SetType(cell, Type.Open);
                            strBuilder.Append(GetChar(Type.Open));
                        }
                        else
                        {
                            SetType(cell, type);
                            strBuilder.Append(GetChar(type));
                        }
                    }
                }
            }

            if (useHash && _duplicatedMinute == 0)
            {
                var str = strBuilder.ToString();
                if (!_hash.Add(str))
                {
                    Console.WriteLine(_minute);
                    _duplicatedStr = str;
                    _duplicatedMinute = _minute;
                }
            }
            else if (_duplicatedMinute > 0)
            {
                var str = strBuilder.ToString();
                if (str == _duplicatedStr)
                {
                    Console.WriteLine(_minute);
                    return true;
                }
            }

            return false;
        }

        private char GetChar(Type type)
        {
            return type == Type.Lumberyard ? '#'
                : type == Type.Open ? '.' : '|';
        }

        private (int open, int tree, int lumberyard) GetAdjacents(int i1, int j1)
        {
            var open = 0;
            var tree = 0;
            var lumberyard = 0;

            for (int i = 0; i < _directions.Length; i++)
            {
                var y = i1 + _directions[i];
                if (y >= 0 && y < _len)
                {
                    for (int j = 0; j < _directions.Length; j++)
                    {
                        var x = j1 + _directions[j];
                        if ((y != i1 || x != j1) && x >= 0 && x < _len)
                        {
                            var type = GetType(_board[y, x]);
                            if (type == Type.Open)
                                open++;
                            else if (type == Type.Tree)
                                tree++;
                            else
                                lumberyard++;
                        }
                    }
                }
            }

            return (open, tree, lumberyard);
        }

        private void ReadBoard()
        {
            var lines = _stream.ReadStringDocument();
            _len = lines.Count;
            _board = new Cell[_len, _len];

            for (int i = 0; i < _len; i++)
            {
                var line = lines[i];
                for (int j = 0; j < _len; j++)
                {
                    var cell = new Cell();
                    _board[i, j] = cell;
                    var current = line[j];
                    if (current == '.')
                    {
                        SetType(cell, Type.Open);
                    }
                    else if (current == '#')
                    {
                        SetType(cell, Type.Lumberyard);
                    }
                    else
                    {
                        SetType(cell, Type.Tree);
                    }
                }
            }
        }

        private Type GetType(Cell cell)
        {
            return cell.Types[_getIndex];
        }

        private void SetType(Cell cell, Type type)
        {
            cell.Types[_setIndex] = type;
        }

        private void Print()
        {
            for (int i = 0; i < _len; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    var cell = _board[i, j];
                    var type = GetType(cell);
                    if (type == Type.Lumberyard)
                        Console.Write("#");
                    else if (type == Type.Tree)
                        Console.Write("|");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        class Cell
        {
            public Cell(int length = 2)
            {
                Types = new Type[length];
            }

            public Type[] Types { get; set; }
        }

        enum Type
        {
            Open = 0,
            Tree = 1,
            Lumberyard = 2
        }
    }
}
