using AOC.Common.Input;

namespace AOC.Common.Solution
{
    abstract class BaseSolution : ISolution
    {
        protected MyFileStream _stream = new MyFileStream();

        public abstract void FindSolution();
    }
}
