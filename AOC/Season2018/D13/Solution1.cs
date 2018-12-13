using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D13
{
    class Solution1 : BaseSolution
    {
        private char[,] _board;
        private bool[,] _pos;
        private int _len;
        private List<Cart> _carts = new List<Cart>();

        public override void FindSolution()
        {
            var lines = _stream.ReadStringDocument();
            _len = lines[0].Length;
            _board = new char[_len, _len];
            _pos = new bool[_len, _len];

            for (int i = 0; i < _len; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    var current = lines[i][j];
                    _board[i, j] = current;
                    
                    if (current == '^')
                    {
                        var cart = new Cart { X = i, Y = j };
                        cart.Direction = Directions.Up;
                        _board[i, j] = '|';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                    else if (current == '>')
                    {
                        var cart = new Cart { X = i, Y = j };
                        cart.Direction = Directions.Right;
                        _board[i, j] = '-';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                    else if (current == 'v')
                    {
                        var cart = new Cart { X = i, Y = j };
                        cart.Direction = Directions.Down;
                        _board[i, j] = '|';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                    else if (current == '<')
                    {
                        var cart = new Cart { X = i, Y = j };
                        cart.Direction = Directions.Left;
                        _board[i, j] = '-';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                }
            }


            while (Move())
            {

            }
        }

        private bool Move()
        {
            foreach (var cart in _carts.OrderBy(t => t.Y).ThenBy(t => t.X))
            {
                var current = _board[cart.X, cart.Y];
                var nextX = -1;
                var nextY = -1;
                

                if (_pos[nextX, nextY])
                {
                    Console.WriteLine($"{nextY},{nextY}");
                    return false;
                }

            }
            return false;
        }

        class Cart
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Directions Direction { get; set; }
            public int Intersactions { get; set; }

            public void Move(char current)
            {
                if (current == '+')
                {
                    var intersection = Intersactions % 3;
                    if (intersection == 0)
                    {
                        MoveLeft();
                        Direction = Directions.Left;
                    }
                    else if (intersection == 2)
                    {
                        MoveLeft();
                        Direction = Directions.Left;
                    }
                }

                if (Direction == Directions.Up)
                {
                    if (current == '/')
                    {
                        MoveUpRight();
                    }
                    else if (current == '\\')
                    {
                        MoveUpLeft();
                    }
                    else
                        MoveUp();
                }
            }

            public void MoveUp()
            {
                X -= 1;
            }
            public void MoveDown()
            {
                X += 1;
            }
            public void MoveLeft()
            {
                Y -= 1;
            }
            public void MoveRight()
            {
                Y += 1;
            }
            public void MoveUpLeft()
            {
                MoveUp();
                MoveLeft();
                Direction = Directions.Left;
            }
            public void MoveUpRight()
            {
                MoveUp();
                MoveRight();
                Direction = Directions.Right;
            }
            public void MoveDownLeft()
            {
                MoveDown();
                MoveLeft();
                Direction = Directions.Left;
            }
            public void MoveDownRight()
            {
                MoveDown();
                MoveRight();
                Direction = Directions.Right;
            }



            public void MoveLeftUp()
            {
                MoveUp();
                MoveLeft();
                Direction = Directions.Up;
            }
            public void MoveRightUp()
            {
                MoveUp();
                MoveRight();
                Direction = Directions.Up;
            }
            public void MoveLeftDown()
            {
                MoveDown();
                MoveLeft();
                Direction = Directions.Down;
            }
            public void MoveRightDown()
            {
                MoveDown();
                MoveRight();
                Direction = Directions.Down;
            }
        }

        enum Directions
        {
            Up = 1,
            Right = 2,
            Down = 3,
            Left = 4
        }
    }
}
