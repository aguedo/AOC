using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D14
{
    class Solution2 : BaseSolution
    {

        private int[] _totalArray;
        private Node _last;
        private int _count;

        public override void FindSolution()
        {
            var total = 607331;
            var tempTotal = total.ToString();
            _totalArray = new int[tempTotal.Length];
            for (int i = 0; i < tempTotal.Length; i++)
                _totalArray[i] = int.Parse(tempTotal[i].ToString());

            var current1 = new Node(3);
            var current2 = new Node(7);
            _last = current2;
            var firt = current1;

            current1.Next = current2;
            current1.Prev = current2;
            current2.Next = current1;
            current2.Prev = current1;

            _count = 2;
            while (true)
            {
                var temp = current1.Value + current2.Value;
                if (temp >= 10)
                {
                    var newNode1 = new Node(1);
                    _count++;
                    newNode1.Prev = _last;
                    _last.Next = newNode1;
                    _last = newNode1;
                    if (CheckCurrent())
                        return;                    
                }

                var newNode2 = new Node(temp % 10);
                _count++;
                newNode2.Prev = _last;
                _last.Next = newNode2;
                _last = newNode2;

                _last.Next = firt;
                firt.Prev = _last;

                current1 = MoveNext(current1);
                current2 = MoveNext(current2);

                if (CheckCurrent())
                    return;
            }

        }

        private bool CheckCurrent()
        {
            if (_count < _totalArray.Length)
                return false;

            var tempNode = _last;
            for (int i = _totalArray.Length - 1; i >= 0; i--)
            {
                if (tempNode.Value != _totalArray[i])
                    return false;
                tempNode = tempNode.Prev;
            }

            Console.WriteLine(_count - _totalArray.Length);
            return true;
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
