using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D09
{
    class Solution2 : BaseSolution
    {
        public override void FindSolution()
        {
            var players = 427;
            var points = 70723 * 100;

            var node0 = new Node(0);
            var node1 = new Node(1);
            var node2 = new Node(2);
            var node3 = new Node(3);

            node0.Next = node2;
            node0.Prev = node3;
            node1.Next = node3;
            node1.Prev = node2;
            node2.Next = node1;
            node2.Prev = node0;
            node3.Next = node0;
            node3.Prev = node1;


            var current = node3;

            var dict = new Dictionary<int, long>();

            for (int i = 4; i <= points; i++)
            {
                if (i % 23 == 0)
                {
                    var player = i % players;
                    if (!dict.ContainsKey(player))
                        dict[player] = 0;
                    dict[player] += i;
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Prev;
                    }

                    dict[player] += current.Value;
                    current.Prev.Next = current.Next;
                    current = current.Next;
                }
                else
                {
                    var node = new Node(i);
                    var next = current.Next;
                    node.Next = next.Next;
                    next.Next = node;
                    node.Prev = next;
                    node.Next.Prev = node;
                    current = node;
                }
            }

            var win = dict.Max(t => t.Value);
            Console.WriteLine(win);

        }

        class Node
        {
            public Node(int value)
            {
                Value = value;
            }

            public int Value { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
        }
    }

}
