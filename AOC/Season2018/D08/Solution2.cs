using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D08
{
    class Solution2 : BaseSolution
    {
        int[] _lines;

        public override void FindSolution()
        {
            _lines = _stream.ReadInt32ArrayLine();
            var temp = Read(0);
            Console.WriteLine(temp.value);
        }

        private (int next, int value) Read(int start)
        {
            var child = _lines[start];
            var meta = _lines[start + 1];

            var next = start + 2;
            var item = new Item { Next = start + 2 };

            var result = 0;
            if (child == 0)
            {
                for (int i = 0; i < meta; i++)
                {
                    result += _lines[item.Next + i];
                }
            }
            else
            {
                var dict = new Dictionary<int, int>();            

                for (int i = 0; i < child; i++)
                {
                    var temp = Read(item.Next);
                    item.Next = temp.next;
                    dict[i + 1] = temp.value;
                }

                for (int i = 0; i < meta; i++)
                {
                    var temp = _lines[item.Next + i];
                    if (dict.ContainsKey(temp))
                        result += dict[temp];
                }
            }

            return (item.Next + meta, result);
        }

        class Item
        {
            public int Next { get; set; }
            public int Value { get; set; }
        }
    }
}
