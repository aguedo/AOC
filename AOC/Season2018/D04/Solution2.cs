using System.Collections.Generic;
using System.Linq;

namespace AOC.Season2018.D04
{
    class Solution2: Solution1
    {
        protected override KeyValuePair<int, GuardMinutes> FindGuard()
        {
            return _guardDictionary.OrderByDescending(t => t.Value.Minutes.Max()).First();
        }
    }
}
