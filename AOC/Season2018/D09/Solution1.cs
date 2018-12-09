using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Season2018.D09
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            FindScore(427, 70723);
        }

        protected void FindScore(int playerCount, int pointCount)
        {
            var current = new Node(0);
            current.Next = current;
            current.Prev = current;
            var players = new Dictionary<int, long>();
            for (int i = 1; i <= pointCount; i++)
            {
                if (i % 23 == 0)
                {
                    var player = i % playerCount;
                    if (!players.ContainsKey(player))
                        players[player] = 0;
                    players[player] += i;
                    for (int j = 0; j < 7; j++)
                        current = current.Prev;

                    players[player] += current.Value;
                    current.Prev.Next = current.Next;
                    current = current.Next;
                }
                else
                {
                    var node = new Node(i);
                    node.Next = current.Next.Next;
                    current.Next.Next = node;
                    node.Prev = current.Next;
                    node.Next.Prev = node;
                    current = node;
                }
            }

            var winning = players.Max(t => t.Value);
            Console.WriteLine(winning);
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

