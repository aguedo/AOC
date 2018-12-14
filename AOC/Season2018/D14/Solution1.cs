using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D14
{
    class Solution1 : BaseSolution
    {
        public override void FindSolution()
        {
            var total = 607331;
            var current1 = new Node(3);
            var current2 = new Node(7);
            var last = current2;
            var firt = current1;

            current1.Next = current2;
            current1.Prev = current2;
            current2.Next = current1;
            current2.Prev = current1;

            var count = 2;
            while (true)
            {
                var temp = current1.Value + current2.Value;
                if (temp >= 10)
                {
                    var newNode1 = new Node(1);
                    count++;
                    newNode1.Prev = last;
                    last.Next = newNode1;
                    last = newNode1;
                    if (count == total)
                    {
                        current1 = MoveNext(current1);
                        current2 = MoveNext(current2);
                        last.Next = firt;
                        break;
                    }
                }

                var newNode2 = new Node(temp % 10);
                count++;
                newNode2.Prev = last;
                last.Next = newNode2;
                last = newNode2;

                last.Next = firt;

                current1 = MoveNext(current1);
                current2 = MoveNext(current2);

                if (count == total)
                    break;
            }

            var nextCount = 10;
            var result = "";
            while (true)
            {
                var temp = current1.Value + current2.Value;
                if (temp >= 10)
                {
                    var newNode1 = new Node(1);
                    result += 1;
                    count++;
                    if (count == total + nextCount)
                    {
                        Console.WriteLine(result);
                        return;
                    }
                    newNode1.Prev = last;
                    last.Next = newNode1;
                    last = newNode1;
                }

                var newNode2 = new Node(temp % 10);
                count++;
                result += newNode2.Value;
                newNode2.Prev = last;
                last.Next = newNode2;
                last = newNode2;

                last.Next = firt;

                current1 = MoveNext(current1);
                current2 = MoveNext(current2);
                if (count == total + nextCount)
                {
                    Console.WriteLine(result);
                    return;
                }
            }

        }

        private Node MoveNext(Node node)
        {
            var steps = node.Value + 1;
            for (int i = 0; i < steps; i++)
                node = node.Next;

            return node;
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
