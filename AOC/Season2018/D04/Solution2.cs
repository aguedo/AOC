using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D04
{
    class Solution2: BaseSolution
    {
        public override void FindSolution()
        {
            // Todo: refactor.
            var list = _stream.ReadStringDocument();

            var pattern1 = @".(\d\d\d\d)-(\d\d)-(\d\d).(\d\d):(\d\d)";
            var pattern2 = @".(\d\d\d\d)-(\d\d)-(\d\d).(\d\d):(\d\d).\sGuard\s#(\d+)\s.*$";

            var actions = new Dictionary<DateTime, int[]>();

            for (int i = 0; i < list.Count; i++)
            {
                var line = list[i];
                if (line[line.Length - 1] == 't')
                {
                    var temp = Regex.Match(line, pattern2).Groups;
                    var time = new DateTime(int.Parse(temp[1].Value), int.Parse(temp[2].Value), int.Parse(temp[3].Value),
                        int.Parse(temp[4].Value), int.Parse(temp[5].Value), 0);
                    actions[time] = new int[] { 1, int.Parse(temp[6].Value) };
                }
                else if (line[line.Length - 2] == 'e')
                {
                    var temp = Regex.Match(line, pattern1).Groups;
                    var time = new DateTime(int.Parse(temp[1].Value), int.Parse(temp[2].Value), int.Parse(temp[3].Value),
                        int.Parse(temp[4].Value), int.Parse(temp[5].Value), 0);
                    actions[time] = new int[] { 2 };
                }
                else
                {
                    var temp = Regex.Match(line, pattern1).Groups;
                    var time = new DateTime(int.Parse(temp[1].Value), int.Parse(temp[2].Value), int.Parse(temp[3].Value),
                        int.Parse(temp[4].Value), int.Parse(temp[5].Value), 0);
                    actions[time] = new int[] { 3 };
                }
            }

            var dict = new Dictionary<int, Item>();

            Item currentTuple = null;
            var sleepMin = -1;
            foreach (var item in actions.OrderBy(t => t.Key))
            {
                var current = item.Value;
                if (current[0] == 1)
                {
                    if (!dict.ContainsKey(current[1]))
                        dict[current[1]] = new Item();
                    currentTuple = dict[current[1]];
                }
                else if (current[0] == 2)
                {
                    sleepMin = item.Key.Minute;
                }
                else if (sleepMin >= 0)
                {
                    var wakesupMin = item.Key.Minute;
                    for (int i = sleepMin; i < wakesupMin; i++)
                    {
                        currentTuple.Total += 1;
                        currentTuple.Minutes[i] += 1;
                    }
                }
            }

            var person = dict.OrderByDescending(t => t.Value.Minutes.Max()).First();
            var max = person.Value.Minutes.Max();
            var minutes = -1;
            for (int i = 0; i < person.Value.Minutes.Length; i++)
            {
                if (person.Value.Minutes[i] == max)
                {
                    var result = person.Key * i;
                    Console.WriteLine(result);
                    return;
                }
            }

        }

        class Item
        {
            public int Total { get; set; }
            public int[] Minutes { get; set; } = new int[60];
        }
    }
}
