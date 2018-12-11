﻿using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D11
{
    class Solution2 : BaseSolution
    {
        private long[,] _grid;
        private int _len = 300;

        public override void FindSolution()
        {
            var serial = 8772;
            _grid = new long[_len, _len];

            for (int i = 0; i < _len; i++)
            {
                var rackId = i + 1 + 10;
                for (int j = 0; j < _len; j++)
                {
                    var power = rackId * (j + 1);
                    power += serial;
                    power *= rackId;
                    power = power / 100 % 10;
                    power -= 5;

                    _grid[i, j] = power;

                }
            }

            var x = -1;
            var y = -1;
            var size = -1;
            var max = long.MinValue;

            var temp = _grid[200, 200];

            for (int i = 0; i < _len; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    for (int k = 0; k < _len; k++)
                    {
                        var power = GetPower(i, j, k + 1);
                        if (power > max)
                        {
                            max = power;
                            x = i + 1;
                            y = j + 1;
                            size = k + 1;
                        }
                    }                    
                }
            }

            Console.WriteLine(x);
            Console.WriteLine(y);
        }

        private long GetPower(int x, int y, int size)
        {
            long result = 0;
            for (int i = x; i < x + size && i < _len; i++)
            {
                for (int j = y; j < y + size && j < _len; j++)
                {
                    result += _grid[i, j];
                }
            }

            return result;
        }
    }
}
