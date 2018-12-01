using AOC.Common.Solution;
using System;

namespace AOC.Season2017.D03
{
    class Solution1 : BaseSolution
    {
        private int _position;
        private int _lowerSqrt;
        private int _upperSqrt;
        private int _lowerPow;
        private int _upperPow;

        public int Result { get; set; }

        public Solution1()
        { }

        public override void FindSolution()
        {
            _position = int.Parse(_stream.ReadLine());
            ComputeSqrts();

            if (_position <= _lowerPow + _lowerSqrt)
            {
                var sideLength = _lowerSqrt;
                var dist1 = 0;
                var sideDist = _position - _lowerPow;
                var middle = sideLength / 2;

                if (sideLength % 2 == 0)
                {
                    if (sideDist > middle)
                    {
                        dist1 = sideDist - middle - 1;
                    }
                    else
                    {
                        dist1 = middle - sideDist + 1;
                    }
                }
                else
                {
                    if (sideDist > middle)
                    {
                        dist1 = sideDist - middle - 1;
                    }
                    else
                    {
                        dist1 = middle - sideDist + 1;
                    }
                }
                var dist2 = _upperSqrt / 2;
                var solution = Math.Abs(dist1) + dist2;
                Console.WriteLine(solution);
            }
            else
            {
                var sideLength = _upperSqrt;
                var dist1 = 0;
                var sideDist = _upperPow - _position;
                var middle = sideLength / 2;
                if (sideLength % 2 == 0)
                {
                    if (sideDist >= middle - 1)
                    {
                        dist1 = sideDist - middle + 1;
                    }
                    else
                    {
                        dist1 = middle - sideDist - 1;
                    }
                }
                else
                {
                    if (sideDist >= middle)
                    {
                        dist1 = sideDist - middle;
                    }
                    else
                    {
                        dist1 = middle - sideDist;
                    }
                }

                var dist2 = _upperSqrt / 2;
                var solution = dist1 + dist2;
                Console.WriteLine(solution);
            }

            Console.ReadLine();
        }

        private void ComputeSqrts()
        {
            var sqrt = Math.Sqrt(_position);
            var roundSqrt = Math.Round(sqrt, 0);
            if (sqrt > roundSqrt)
            {
                _lowerSqrt = (int)roundSqrt;
                _upperSqrt = _lowerSqrt + 1;
            }
            else
            {
                _upperSqrt = (int)roundSqrt;
                _lowerSqrt = _upperSqrt - 1;
            }

            _lowerPow = (int)Math.Pow(_lowerSqrt, 2);
            _upperPow = (int)Math.Pow(_upperSqrt, 2);            
        }


    }
}
