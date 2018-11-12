using AOC.Common.Input;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2017.D02
{
    class Solution1 : BaseSolution
    {
        public int Result { get; set; }

        public Solution1(): base (new MyFileStream())
        { }

        public override void Solve()
        {
            while (!_stream.EndOfStream)
            {
                var array = _inputHelper.ReadArrayInt32(delimiter: '\t');
                var min = array.Min();
                var max = array.Max();
                var diff = max - min;
                Result += diff;
            }
        }
    }
}
