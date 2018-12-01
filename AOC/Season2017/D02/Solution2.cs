using AOC.Common.Input;
using AOC.Common.Solution;
using System;

namespace AOC.Season2017.D02
{
    class Solution2 : BaseSolution
    {
        public int Result { get; set; }

        public Solution2()
        { }

        public override void FindSolution()
        {
            while (!_stream.EndOfStream)
            {
                var array = _stream.ReadArrayInt32(delimiter: '\t');
                Result += Compute(array);
            }
        }

        private int Compute(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    var min = Math.Min(array[i], array[j]);
                    var max = Math.Max(array[i], array[j]);
                    if (max % min == 0)
                        return max / min;
                }
            }

            return 0;
        }
    }
}
