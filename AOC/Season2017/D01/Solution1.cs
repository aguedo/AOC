using AOC.Common.Input;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2017.D01
{
    class Solution1 : BaseSolution
    {
        public int Result { get; set; }

        public Solution1(): base (new MyFileStream())
        { }

        public override void Solve()
        {
            var line = _stream.ReadLine();
            Result = 0;
            for (int i = 0; i < line.Length - 1; i++)
            {
                var current = line[i];
                var next = line[i + 1];
                if (current == next)
                    Result += int.Parse(current.ToString());
            }
            if (line[line.Length - 1] == line[0])
                Result += int.Parse(line[0].ToString());
        }
    }
}
