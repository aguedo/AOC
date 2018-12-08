using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Common.Graphs
{
    /// <typeparam name="K">Node id type</typeparam>
    /// <typeparam name="T">Node value type</typeparam>
    public class Graph<K, T>
    {
        private Dictionary<K, HashSet<Node<K, T>>> _adjacents;
        private Dictionary<K, Node<K, T>> _nodes;

        public Graph()
        {
            _adjacents = new Dictionary<K, HashSet<Node<K, T>>>();
            _nodes = new Dictionary<K, Node<K, T>>();
        }

        public Graph(int count)
        {
            _adjacents = new Dictionary<K, HashSet<Node<K, T>>>(count);
            _nodes = new Dictionary<K, Node<K, T>>(count);
        }

        public int Count => _nodes.Count;

        public bool IsDirected { get; set; } = false;

        public void AddEdge(Node<K, T> node1, Node<K, T> node2)
        {
            AddNode(node1);
            AddNode(node2);

            AddSingleEdge(node1, node2);
            if (!IsDirected)
                AddSingleEdge(node2, node1);
        }        

        public void AddNode(Node<K, T> node)
        {
            if (!_nodes.ContainsKey(node.Id))
                _nodes[node.Id] = node;
        }

        public Node<K, T> GetNode(K id)
        {
            return _nodes[id];
        }

        public IEnumerable<Node<K, T>> GetAllNodes()
        {
            return _nodes.Values;
        }

        public HashSet<Node<K, T>> GetAdjacents(K id)
        {
            return _adjacents[id];
        }

        private void AddSingleEdge(Node<K, T> node1, Node<K, T> node2)
        {
            if (!_adjacents.TryGetValue(node1.Id, out HashSet<Node<K, T>> set))
            {
                set = new HashSet<Node<K, T>>();
                _adjacents[node1.Id] = set;
            }
            set.Add(node2);
        }
    }

    public class Node<K, T>
    {
        public Node(K id)
        {
            Id = id;
        }

        public K Id { get; private set; }
        public T Value { get; set; }
    }
}
