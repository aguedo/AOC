using AOC.Common.Testing;

namespace AOC
{
    class Program
    {
        static void Main(string[] args)
        {
            //var test = new TestRegex();
            //test.Test();

            using (var solution = new Season2018.D10.Solution1())
                solution.FindSolution();
        }
    }
}
