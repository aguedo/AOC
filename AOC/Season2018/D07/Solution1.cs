using AOC.Common.Graphs;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC.Season2018.D07
{
    class Solution1 : BaseSolution
    {
        private Graph<char, int> _graph = new Graph<char, int>(true);
        private StringBuilder _solution = new StringBuilder();

        public override void FindSolution()
        {
            BuildGraph();
            BuildSolution();

            Console.WriteLine(_solution.ToString());
        }

        private void BuildSolution()
        {
            var leaves = new SortedSet<Node<char, int>>(new NodeComparer());
            var visited = new HashSet<Node<char, int>>();

            while (visited.Count < _graph.Count)
            {
                Node<char, int> top = null;
                if (leaves.Count > 0)
                {
                    top = leaves.First();
                    leaves.Remove(top);
                    visited.Add(top);
                    _solution.Append(top.Id);
                }

                foreach (var node in _graph.GetAllNodes())
                {
                    if (top != null)
                        _graph.RemoveEdge(node, top);

                    if (visited.Contains(node))
                        continue;

                    if (_graph.IsLeaf(node))
                        leaves.Add(node);
                }
            }
        }

        private void BuildGraph()
        {
            var lines = _stream.ReadStringDocument();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var temp = line.Split(' ');
                var n1 = temp[1][0];
                var n2 = temp[7][0];
                _graph.AddEdge(n2, n1);
            }
        }

        public void FindSolutionOld()
        {
            var steps = new HashSet<string>();
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
            var result = "";
            var ready = new HashSet<string>();

            foreach (var item in all)
            {
                if (!left.Contains(item))
                {
                    ready.Add(item);
                }
            }

            for (int i = 0; i < all.Count; i++)
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
                    var next = ready.OrderBy(t => t).First();
                    result += next;
                    done.Add(next);
                    ready.Remove(next);
                }
            }

            Console.WriteLine(result.Length);
            Console.WriteLine(dict.Count);
            Console.WriteLine(result);
        }
    }

    internal class NodeComparer : IComparer<Node<char, int>>
    {
        public int Compare(Node<char, int> x, Node<char, int> y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}
