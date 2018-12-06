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
            var line = "this this this sdf";
            var pattern = @"\s?(\w+)(\s(\1))+";

            foreach (Match match in Regex.Matches(line, pattern))
            {
                var groups = match.Groups;
            }

            var temp = Regex.Replace(line, pattern, "hello");

            var matchEvaluator = new MatchEvaluator(Evaluator);
            var temp1 = Regex.Replace(line, pattern, matchEvaluator);

            var groups1 = Regex.Match(line, pattern).Groups;

        }

        private string Evaluator(Match match)
        {
            var word = match.Groups[1].Value;
            return word;
        }
    }
}
