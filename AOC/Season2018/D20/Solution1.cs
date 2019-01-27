using AOC.Common.Graphs;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D20
{
    class Solution1 : BaseSolution
    {
        private string _line;
        private Graph<string, int> _graph = new Graph<string, int>(false);
        private Dictionary<int, int> _branchDictionay = new Dictionary<int, int>();

        public override void FindSolution()
        {
            _line = _stream.ReadLine();
            BuildBranchDictionary(1);

            BuildGraph(1, 0, 0);
            FindPath();
            Console.WriteLine("OK");
        }

        private void FindPath()
        {
            var start = _graph.GetNode("00");
            BFS(start);
        }

        private void BFS(Node<string, int> start)
        {
            var visited = new HashSet<Node<string, int>>();
            var queue = new Queue<(Node<string, int> node, int distance)>();
            queue.Enqueue((start, 0));
            visited.Add(start);
            var max = int.MinValue;

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                var distance = next.distance;
                if (distance > max)
                    max = distance;
                var adjacents = _graph.GetAdjacents(next.node.Id);
                foreach (var adj in adjacents)
                {
                    if (visited.Add(adj))
                        queue.Enqueue((adj, distance + 1));
                }
            }

            Console.WriteLine(max);
        }

        private int calls = 0;

        private (int outX, int outY, int outIndex) BuildGraph(int index, int x, int y)
        {
            calls++;
            if (index >= _line.Length - 1)
                return (index, x, y);

            var current = _line[index];
            var currentX = x;
            var currentY = y;
            
            while (current == 'N' || current == 'S' || current == 'W' || current == 'E')
            {
                var nextX = currentX;
                var nextY = currentY;
                if (current == 'N')
                {
                    nextY++;
                }
                else if (current == 'S')
                {
                    nextY--;
                }
                else if (current == 'W')
                {
                    nextX--;
                }
                else if (current == 'E')
                {
                    nextX++;
                }

                if (_graph.ContainsEdge($"{currentX}{currentY}", $"{nextX}{nextY}"))
                {
                }

                _graph.AddEdge($"{currentX}{currentY}", $"{nextX}{nextY}");
                index++;
                if (index == _line.Length - 1)
                    return (index, currentX, currentY);
                current = _line[index];
                currentX = nextX;
                currentY = nextY;
            }

            if (current == ')' || current == '|')
            {
                return (currentX, currentY, index);
            }

            if (current == '(')
            {
                var branchEnd = _branchDictionay[index];
                while (true)
                {
                    var node = BuildGraph(index + 1, currentX, currentY);
                    if (node.outIndex >= _line.Length - 1)
                        return (index, currentX, currentY);

                    if (_line[node.outIndex] == ')')
                        return BuildGraph(branchEnd + 1, node.outX, node.outY);
                    else if (_line[node.outIndex] == '|')
                    {
                        BuildGraph(branchEnd + 1, node.outX, node.outY);
                        if (_line[node.outIndex + 1] == ')')
                            return BuildGraph(branchEnd + 1, currentX, currentY);
                        index = node.outIndex;
                    }
                    else
                    {
                        throw new Exception("Invalid Exception");
                    }
                }                
            }
            
            return BuildGraph(index + 1, currentX, currentY);
        }

        private int BuildBranchDictionary(int index)
        {
            if (index >= _line.Length)
                return -1;

            var current = _line[index];

            if (current == ')')
                return index;

            if (current == '(')
            {
                var closingIndex = BuildBranchDictionary(index + 1);
                _branchDictionay[index] = closingIndex;
                return BuildBranchDictionary(closingIndex + 1);
            }

            return BuildBranchDictionary(index + 1);
        }
    }
}
