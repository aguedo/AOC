using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D13
{
    class Solution2 : BaseSolution
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
                        var cart = new Cart { Y = i, X = j };
                        cart.Direction = Directions.Up;
                        _board[i, j] = '|';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                    else if (current == '>')
                    {
                        var cart = new Cart { Y = i, X = j };
                        cart.Direction = Directions.Right;
                        _board[i, j] = '-';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                    else if (current == 'v')
                    {
                        var cart = new Cart { Y = i, X = j };
                        cart.Direction = Directions.Down;
                        _board[i, j] = '|';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                    else if (current == '<')
                    {
                        var cart = new Cart { Y = i, X = j };
                        cart.Direction = Directions.Left;
                        _board[i, j] = '-';
                        _carts.Add(cart);
                        _pos[i, j] = true;
                    }
                }
            }


            while (_carts.Count > 1)
            {
                Move();
            }

            var last = _carts[0];
            Console.WriteLine($"{last.X},{last.Y}");
        }

        private bool Move()
        {
            foreach (var cart in _carts.OrderBy(t => t.Y).ThenBy(t => t.X).ToArray())
            {
                var current = _board[cart.Y, cart.X];
                //_pos[cart.Y, cart.X] = false;
                cart.Move(current);

                if (_pos[cart.Y, cart.X])
                {
                    //_pos[cart.Y, cart.X] = false;
                    _carts.RemoveAll(t => t.X == cart.X && t.Y == cart.Y);
                }
                //else
                //    _pos[cart.Y, cart.X] = true;

                // Todo: avoid this last loops
                for (int i = 0; i < _len; i++)
                {
                    for (int j = 0; j < _len; j++)
                    {
                        _pos[i, j] = false;
                    }
                }

                foreach (var item in _carts)
                {
                    _pos[item.Y, item.X] = true;
                }
            }

            return true;
        }

        class Cart
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Directions Direction { get; set; }
            public int Intersactions { get; set; }

            public void Move(char current)
            {
                if (Direction == Directions.Up)
                {
                    if (current == '/')
                    {
                        MoveRight();
                        Direction = Directions.Right;
                    }
                    else if (current == '\\')
                    {
                        MoveLeft();
                        Direction = Directions.Left;
                    }
                    else if (current == '+')
                    {
                        var intersection = Intersactions % 3;
                        Intersactions++;
                        if (intersection == 0)
                        {
                            MoveLeft();
                            Direction = Directions.Left;
                            return;
                        }
                        else if (intersection == 2)
                        {
                            MoveRight();
                            Direction = Directions.Right;
                            return;
                        }
                        else
                            MoveUp();
                    }
                    else
                        MoveUp();
                }
                else if (Direction == Directions.Right)
                {
                    if (current == '/')
                    {
                        MoveUp();
                        Direction = Directions.Up;
                    }
                    else if (current == '\\')
                    {
                        MoveDown();
                        Direction = Directions.Down;
                    }
                    else if (current == '+')
                    {
                        var intersection = Intersactions % 3;
                        Intersactions++;
                        if (intersection == 0)
                        {
                            MoveUp();
                            Direction = Directions.Up;
                            return;
                        }
                        else if (intersection == 2)
                        {
                            MoveDown();
                            Direction = Directions.Down;
                            return;
                        }
                        else
                            MoveRight();
                    }
                    else
                        MoveRight();
                }
                else if (Direction == Directions.Down)
                {
                    if (current == '/')
                    {
                        MoveLeft();
                        Direction = Directions.Left;
                    }
                    else if (current == '\\')
                    {
                        MoveRight();
                        Direction = Directions.Right;
                    }
                    else if (current == '+')
                    {
                        var intersection = Intersactions % 3;
                        Intersactions++;
                        if (intersection == 0)
                        {
                            MoveRight();
                            Direction = Directions.Right;
                            return;
                        }
                        else if (intersection == 2)
                        {
                            MoveLeft();
                            Direction = Directions.Left;
                            return;
                        }
                        else
                            MoveDown();
                    }
                    else
                        MoveDown();
                }
                else if (Direction == Directions.Left)
                {
                    if (current == '/')
                    {
                        MoveDown();
                        Direction = Directions.Down;
                    }
                    else if (current == '\\')
                    {
                        MoveUp();
                        Direction = Directions.Up;
                    }
                    else if (current == '+')
                    {
                        var intersection = Intersactions % 3;
                        Intersactions++;
                        if (intersection == 0)
                        {
                            MoveDown();
                            Direction = Directions.Down;
                            return;
                        }
                        else if (intersection == 2)
                        {
                            MoveUp();
                            Direction = Directions.Up;
                            return;
                        }
                        else
                            MoveLeft();
                    }
                    else
                        MoveLeft();
                }
            }

            public void MoveUp()
            {
                Y -= 1;
            }
            public void MoveDown()
            {
                Y += 1;
            }
            public void MoveLeft()
            {
                X -= 1;
            }
            public void MoveRight()
            {
                X += 1;
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
