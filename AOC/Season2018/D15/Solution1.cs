﻿using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D15
{
    class Solution1 : BaseSolution
    {
        private Cell[,] _board;
        private int _len;
        private List<Cell> _players = new List<Cell>();
        private int _rounds;

        public override void FindSolution()
        {
            // Todo: refactor
            // Improve the method NextRound to use the variable found

            ReadBoard();

            while (true)
            {
                //Print();
                var completed = NextRound();

                if (completed)
                    break;
            }

            Print();
            var totalHP = _players.Sum(t => t.HP);
            Console.WriteLine($"rounds: {_rounds}");
            Console.WriteLine($"hp: {totalHP}");
            var solution = totalHP * _rounds;
            Console.WriteLine(solution);
        }


        private bool NextRound()
        {
            if (_players.GroupBy(t => t.Type).Count() == 1)
                return true;
            
            //var found = false;
            foreach (var player in _players.OrderBy(t => t.Y).ThenBy(t => t.X).ToArray())
            {
                if (player.Type == Type.Space)
                    continue;

                FindTargets(player);
                if (_target == null)
                    continue;

                //found = true;
                if (_attack)
                    Attack();
                else
                {
                    Move();
                    TryAttack();
                }
            }

            //_rounds++;

            if (_players.GroupBy(t => t.Type).Count() > 1)
                _rounds++;

            return _players.GroupBy(t => t.Type).Count() == 1;
        }

        private void TryAttack()
        {
            _target = null;
            var up = _player.Y > 0 ? _board[_player.Y - 1, _player.X] : null;
            var validUp = ValidAdj(up);

            var left = _player.X > 0 ? _board[_player.Y, _player.X - 1] : null;
            var validLeft = ValidAdj(left);

            var right = _player.X < _len - 1 ? _board[_player.Y, _player.X + 1] : null;
            var validRight = ValidAdj(right);

            var down = _player.Y < _len - 1 ? _board[_player.Y + 1, _player.X] : null;
            var validDown = ValidAdj(down);

            if (validUp || validRight || validLeft || validDown)
            {
                Attack();
            }
        }

        private void Move()
        {
            var tempX = _start.X;
            var tempY = _start.Y;
            _board[_start.Y, _start.X] = _player;
            _board[_player.Y, _player.X] = _start;

            _start.Y = _player.Y;
            _start.X = _player.X;
            _player.Y = tempY;
            _player.X = tempX;
        }

        private void Attack()
        {
            _target.HP -= 3;
            if (_target.HP <= 0)
                KillTarget();
        }

        private void KillTarget()
        {
            _players.Remove(_target);
            _target.Type = Type.Space;
        }

        private bool[,] _map;
        private Cell _player;
        private int _targetDist;
        private Cell _target;
        private Cell _start;
        private bool _attack;
        private Queue<(int x, int y, int dist, Cell start)> _queue = new Queue<(int, int y, int, Cell)>();
        private void FindTargets(Cell player)
        {
            _player = player;
            _map = new bool[_len, _len];
            _target = null;
            _start = null;
            _targetDist = int.MaxValue;
            _attack = false;
            _queue.Clear();

            var up = player.Y > 0 ? _board[player.Y - 1, player.X] : null;
            var validUp = ValidAdj(up);

            var left = player.X > 0 ? _board[player.Y, player.X - 1] : null;
            var validLeft = ValidAdj(left);

            var right = player.X < _len - 1 ? _board[player.Y, player.X + 1] : null;
            var validRight = ValidAdj(right);

            var down = player.Y < _len - 1 ? _board[player.Y + 1, player.X] : null;
            var validDown = ValidAdj(down);

            if (validUp || validRight || validLeft || validDown)
                return;

            while (_queue.Count > 0)
            {
                var top = _queue.Dequeue();
                Visit(top.x, top.y, top.dist, top.start);
            }
        }

        private bool ValidAdj(Cell cell)
        {
            if (cell.Type == Type.Space)
                _queue.Enqueue((cell.X, cell.Y, 1, cell));
            if (cell.Type == Type.Wall || cell.Type == Type.Space)
                return false;
            if (cell.Type != _player.Type)
            {
                if (_target == null || cell.HP < _target.HP)
                {
                    _target = cell;
                    _attack = true;
                }                
                return true;
            }
            return false;
        }

        private void Visit(int x, int y, int dist, Cell start)
        {
            if (dist > _targetDist || x < 0 || x >= _len || y < 0 || y >= _len || _map[y, x])
                return;

            _map[y, x] = true;
            var cell = _board[y, x];
            if (cell.Type != Type.Space)
                return;

            CheckAdjs(cell, dist, start);

            _queue.Enqueue((x + 1, y, dist + 1, start));
            _queue.Enqueue((x - 1, y, dist + 1, start));
            _queue.Enqueue((x, y + 1, dist + 1, start));
            _queue.Enqueue((x, y - 1, dist + 1, start));
        }

        private void CheckAdjs(Cell cell, int dist, Cell start)
        {
            var left = cell.X > 0 ? _board[cell.Y, cell.X - 1] : null;
            if (ValidTarget(left))
            {
                Consider(cell, dist, start);
                return;
            }

            var right = cell.X < _len - 1 ? _board[cell.Y, cell.X + 1] : null;
            if (ValidTarget(right))
            {
                Consider(cell, dist, start);
                return;
            }

            var up = cell.Y > 0 ? _board[cell.Y - 1, cell.X] : null;
            if (ValidTarget(up))
            {
                Consider(cell, dist, start);
                return;
            }

            var down = cell.Y < _len - 1 ? _board[cell.Y + 1, cell.X] : null;
            if (ValidTarget(down))
            {
                Consider(cell, dist, start);
                return;
            }
        }

        private void Consider(Cell cell, int dist, Cell start)
        {
            if (_target == null)
            {
                SelectTarget(cell, dist, start);
            }
            else
            {
                if (dist < _targetDist)
                {
                    SelectTarget(cell, dist, start);
                }
                else if (dist == _targetDist)
                {
                    if (cell.Y <= _target.Y)
                    {
                        if (cell.Y < _target.Y || cell.X < _target.X)
                            SelectTarget(cell, dist, start);
                        else if (cell.X == _target.X && cell.Y == _target.Y)
                        {
                            if (start.Y <= _start.Y)
                            {
                                if (start.Y < _start.Y || start.X < _start.X)
                                    SelectTarget(cell, dist, start);
                            }
                        }
                    }
                    
                }
            }
        }

        private bool ValidTarget(Cell cell)
        {
            return cell != null && cell.Type != Type.Wall && cell.Type != Type.Space && cell.Type != _player.Type;
        }

        private void SelectTarget(Cell target, int dist, Cell start)
        {
            _target = target;
            _targetDist = dist;
            _start = start;
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
                    var curr = line[j];
                    var cell = new Cell
                    {
                        X = j,
                        Y = i
                    };
                    if (curr == '#')
                        cell.Type = Type.Wall;
                    else if (curr == 'G')
                    {
                        cell.Type = Type.G;
                        cell.HP = 200;
                        _players.Add(cell);
                    }
                    else if (curr == 'E')
                    {
                        cell.Type = Type.E;
                        cell.HP = 200;
                        _players.Add(cell);
                    }
                    else
                        cell.Type = Type.Space;
                    _board[i, j] = cell;
                }
            }
        }

        private void Print()
        {
            Console.WriteLine();
            Console.WriteLine($"After round {_rounds}");
            var elf = _players.Where(t => t.Type == Type.E);
            Console.Write("E -> ");
            foreach (var item in elf)
                Console.Write($"{item.HP}, ");
            Console.WriteLine();
            var gob = _players.Where(t => t.Type == Type.G);
            Console.Write("G -> ");
            foreach (var item in gob)
                Console.Write($"{item.HP}, ");
            Console.WriteLine();
            for (int i = 0; i < _len; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    _board[i, j].Print();
                }
                Console.WriteLine();
            }
        }

        class Cell
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int HP { get; set; }
            public Type Type { get; set; }

            internal void Print()
            {
                if (Type == Type.Wall)
                    Console.Write("#");
                else if (Type == Type.Space)
                    Console.Write(".");
                else if (Type == Type.G)
                    Console.Write("G");
                else if (Type == Type.E)
                    Console.Write("E");
            }
        }

        enum Type
        {
            Wall = 1,
            Space = 2,
            E = 3,
            G = 4
        }
    }  

    
}
