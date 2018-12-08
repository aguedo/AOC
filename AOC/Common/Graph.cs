using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Common
{
    public class Graph<K, T>
    {
        private Dictionary<K, List<Node<K, T>>> _adjacents;
        private Dictionary<K, Node<K, T>> _nodes;
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
