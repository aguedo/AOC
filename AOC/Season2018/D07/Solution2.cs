using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D07
{
    class Solution2 : BaseSolution
    {
        public override void FindSolution()
        {
            var lines = _stream.ReadStringDocument();
            var dict = new Dictionary<string, HashSet<string>>();

            var left = new HashSet<string>();
            var all = new HashSet<string>();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var temp = line.Split(' ');
                var s1 = temp[1];
                var s2 = temp[7];

                if (!dict.ContainsKey(s2))
                    dict[s2] = new HashSet<string>();
                dict[s2].Add(s1);

                all.Add(s2);
                all.Add(s1);
                left.Add(s2);
            }

            var done = new HashSet<string>();
            var ready = new HashSet<string>();

            var steps = new Dictionary<string, int>();
            foreach (var item in all)
            {
                if (!left.Contains(item))
                {
                    ready.Add(item);
                }

                steps[item] = GetTime(item[0]);
            }

            var seconds = 0;

            while (steps.Any(t => t.Value > 0))
            {
                foreach (var item in dict)
                {
                    if (!done.Contains(item.Key))
                    {
                        if (item.Value.Count == 0 || item.Value.All(t => done.Contains(t)))
                            ready.Add(item.Key);
                    }
                }

                if (ready.Count > 0)
                {
                    var next = ready.OrderBy(t => t).Take(5);
                    foreach (var item in next)
                    {
                        steps[item] -= 1;
                        if (steps[item] == 0)
                        {
                            ready.Remove(item);
                            done.Add(item);
                        }
                        
                    }

                    seconds++;
                }
            }


            Console.WriteLine(seconds);
        }

        private int GetTime(char c)
        {
            return (int)c - 64 + 60; 
        }
    }
}
