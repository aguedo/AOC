using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Season2018.D04
{
    class Solution1: BaseSolution
    {
        private const string _pattern1 = @".(.+)\].*";
        private const string _pattern2 = @".(.+)\]\sGuard\s#(\d+)\s.*";
        private Dictionary<DateTime, int[]> _actions = new Dictionary<DateTime, int[]>();
        protected Dictionary<int, GuardMinutes> _guardDictionary = new Dictionary<int, GuardMinutes>();

        public override void FindSolution()
        {
            CreateActions();
            ComputeMinutes();
            ComputeSolution();
        }

        protected virtual KeyValuePair<int, GuardMinutes> FindGuard()
        {
            return _guardDictionary.OrderByDescending(t => t.Value.Total).First();
        }
        
        private void ComputeMinutes()
        {
            GuardMinutes currentGuard = null;
            var sleepMin = 0;
            foreach (var action in _actions.OrderBy(t => t.Key))
            {
                var currentAction = action.Value;
                if (currentAction[0] == 1)
                {
                    if (!_guardDictionary.TryGetValue(currentAction[1], out currentGuard))
                    {
                        currentGuard = new GuardMinutes();
                        _guardDictionary[currentAction[1]] = currentGuard;
                    }
                }
                else if (currentAction[0] == 2)
                {
                    sleepMin = action.Key.Minute;
                }
                else
                {
                    var wakesupMin = action.Key.Minute;
                    for (int i = sleepMin; i < wakesupMin; i++)
                    {
                        currentGuard.Total += 1;
                        currentGuard.Minutes[i] += 1;
                    }
                }
            }
        }

        private void CreateActions()
        {
            while (!_stream.EndOfStream)
            {
                var line = _stream.ReadLine();
                if (line[line.Length - 1] == 't')
                {
                    var groups = Regex.Match(line, _pattern2).Groups;
                    var dateTime = DateTime.Parse(groups[1].Value);
                    _actions[dateTime] = new int[] { 1, int.Parse(groups[2].Value) };
                    
                }
                else
                {
                    var groups = Regex.Match(line, _pattern1).Groups;
                    var dateTime = DateTime.Parse(groups[1].Value);                    
                    var action = line[line.Length - 2] == 'e' ? 2 : 3;
                    _actions[dateTime] = new int[] { action };
                }
            }
        }       

        private void ComputeSolution()
        {
            var guard = FindGuard();
            var guardMinutes = guard.Value.Minutes;
            var guardId = guard.Key;
            var max = guardMinutes.Max();
            var minute = Array.IndexOf(guardMinutes, max);
            var result = guardId * minute;
            Console.WriteLine(result);
        }

        /// <summary>
        /// Live implementation
        /// </summary>
        public void FindSolutionOld()
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

            var dict = new Dictionary<int, GuardMinutes>();

            GuardMinutes currentTuple = null;
            var sleepMin = -1;
            foreach (var item in actions.OrderBy(t => t.Key))
            {
                var current = item.Value;
                if (current[0] == 1)
                {
                    if (!dict.ContainsKey(current[1]))
                        dict[current[1]] = new GuardMinutes();
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

            var person = dict.OrderByDescending(t => t.Value.Total).First();
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
    }

    class GuardMinutes
    {
        public int Total { get; set; }
        public int[] Minutes { get; set; } = new int[60];
    }
}
