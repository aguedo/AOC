using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Common.Testing
{
    class TestRegex
    {
        public void Test()
        {
            var line = "";
            var pattern = @"";
            var groups = Regex.Match(line, pattern).Groups;

            var match = Regex.Match(line, pattern);

        }
    }
}
