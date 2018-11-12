using AOC.Common.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Common.Solution
{
    abstract class BaseSolution : ISolution
    {
        protected readonly IStream _stream;
        protected readonly InputHelper _inputHelper;

        public BaseSolution(IStream stream)
        {
            _stream = stream;
            _inputHelper = new InputHelper(stream);
        }

        public abstract void Solve();
    }
}
