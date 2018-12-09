using System;
using System.Collections.Generic;

namespace AOC.Common.Graphs
{
    public class DFS<K, T>
    {
        private Graph<K, T> _graph;
        private HashSet<Node<K, T>> _visited = new HashSet<Node<K, T>>();

        public DFS(Graph<K, T> graph)
        {
            _graph = graph;
        }

        public void Run()
        {
            foreach (var node in _graph.GetAllNodes())
            {
                if (!_visited.Contains(node))
                    Visit(node);
            }
        }

        private void Visit(Node<K, T> node)
        {
            _visited.Add(node);
            foreach (var adj in _graph.GetAdjacents(node))
            {
                if (!_visited.Contains(adj))
                    Visit(adj);
            }

            DoSomething(node);
        }

        private void DoSomething(Node<K, T> node)
        {
            Console.Write(node.Id);
        }
    }
}
