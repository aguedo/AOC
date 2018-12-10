using AOC.Common.Input;

namespace AOC.Common.Solution
{
    public abstract class BaseSolution : ISolution
    {
        protected MyFileStream _stream = new MyFileStream();        

        public abstract void FindSolution();

        public virtual void Dispose()
        {
            _stream.Dispose();
        }
    }
}
