using AOC.Common.Solution;

namespace AOC.Season2017.D01
{
    class Solution2 : BaseSolution
    {
        private string line;

        public int Result { get; set; }

        public Solution2()
        { }

        public override void FindSolution()
        {
            line = _stream.ReadLine();
            Result = 0;
            var half = line.Length / 2;
            for (int i = 0; i < line.Length / 2 - 1; i++)
            {
                var current = line[i];
                var next = line[i + half];
                if (current == next)
                    Result += 2 * int.Parse(current.ToString());
            }
        }
    }
}
