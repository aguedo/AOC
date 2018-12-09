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

        public Graph(bool isDirected)
        {
            _adjacents = new Dictionary<K, HashSet<Node<K, T>>>();
            _nodes = new Dictionary<K, Node<K, T>>();
            IsDirected = isDirected;
        }        

        public Graph(int count, bool isDirected)
        {
            _adjacents = new Dictionary<K, HashSet<Node<K, T>>>(count);
            _nodes = new Dictionary<K, Node<K, T>>(count);
            IsDirected = isDirected;
        }

        public int Count => _nodes.Count;

        public bool IsDirected { get; set; }

        public void AddEdge(K id1, K id2, T value1 = default(T), T value2 = default(T))
        {
            var node1 = GetNode(id1) ?? new Node<K, T>(id1, value1);
            var node2 = GetNode(id2) ?? new Node<K, T>(id2, value2);
            AddEdge(node1, node2);
        }

        public void AddEdge(Node<K, T> node1, Node<K, T> node2)
        {
            AddNode(node1);
            AddNode(node2);

            AddSingleEdge(node1, node2);
            if (!IsDirected)
                AddSingleEdge(node2, node1);
        }

        public void RemoveEdge(Node<K, T> node1, Node<K, T> node2)
        {
            RemoveSingleEdge(node1, node2);
            if (!IsDirected)
                RemoveSingleEdge(node2, node1);
        }        

        public void AddNode(Node<K, T> node)
        {
            if (!_nodes.ContainsKey(node.Id))
                _nodes[node.Id] = node;
        }

        public Node<K, T> GetNode(K id)
        {
            return _nodes.TryGetValue(id, out Node<K, T> node) ? node : null;
        }

        public IEnumerable<Node<K, T>> GetAllNodes()
        {
            return _nodes.Values;
        }

        public IEnumerable<Node<K, T>> GetAdjacents(Node<K, T> node)
        {
            return GetAdjacents(node.Id);
        }

        public IEnumerable<Node<K, T>> GetAdjacents(K id)
        {             
            return _adjacents.TryGetValue(id, out HashSet<Node<K, T>> adjacents) ? adjacents : Enumerable.Empty<Node<K, T>>();
        }

        public bool ContainsEdge(Node<K, T> node1, Node<K, T> node2)
        {
            return ContainsEdge(node1.Id, node2.Id);
        }

        public bool ContainsEdge(K id1, K id2)
        {
            var containsDirect = _adjacents[id1] != null && _adjacents[id1].Any(t => t.Id.Equals(id2));
            if (containsDirect)
                return true;
            return !IsDirected ? _adjacents[id2] != null && _adjacents[id2].Any(t => t.Id.Equals(id1)) : false;
        }

        public bool ContainsNode(Node<K, T> node)
        {
            return ContainsNode(node.Id);
        }

        public bool ContainsNode(K id)
        {
            return _nodes.ContainsKey(id);
        }

        public bool IsLeaf(Node<K, T> node)
        {
            return !_adjacents.TryGetValue(node.Id, out HashSet<Node<K, T>> adj) || adj.Count == 0;
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

        private void RemoveSingleEdge(Node<K, T> node1, Node<K, T> node2)
        {
            if (_adjacents.TryGetValue(node1.Id, out HashSet<Node<K, T>> set))
                set.Remove(node2);
        }
    }

    public class Node<K, T>
    {    
        public K Id { get; private set; }
        public T Value { get; set; }

        public Node(K id, T value = default(T))
        {
            Id = id;
            Value = value;
        }
    }
}
