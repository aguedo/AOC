using AOC.Common.Solution;
using System.Linq;

namespace AOC.Season2017.D02
{
    class Solution1 : BaseSolution
    {
        public int Result { get; set; }

        public Solution1()
        { }

        public override void FindSolution()
        {
            while (!_stream.EndOfStream)
            {
                var array = _stream.ReadArrayInt32(delimiter: '\t');
                var min = array.Min();
                var max = array.Max();
                var diff = max - min;
                Result += diff;
            }
        }
    }
}
