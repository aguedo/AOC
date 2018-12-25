using AOC.Common.DisjointSets;
using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Season2018.D25
{
    class Solution1 : BaseSolution
    {
        private List<DisjointSet<int[]>> _sets = new List<DisjointSet<int[]>>();

        public override void FindSolution()
        {
            ReadPoints();
            FindSets();            

            var count = 0;
            foreach (var set in _sets)
            {
                if (set.Parent == null)
                    count++;
            }

            Console.WriteLine(count);
        }

        private void FindSets()
        {
            for (int i = 0; i < _sets.Count; i++)
            {
                var set = _sets[i];
                for (int j = 0; j < _sets.Count; j++)
                {
                    var other = _sets[j];
                    if (set.GetRepresentative() != other.GetRepresentative() && InDistance(set.Vaue, other.Vaue))
                        set.Union(other);
                }
            }
        }

        private bool InDistance(int[] arr1, int[] arr2)
        {
            var distance = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                var value1 = arr1[i];
                var value2 = arr2[i];
                if ((value1 > 0 && value2 > 0) || (value1 < 0 && value2 < 0))
                    distance += Math.Abs(Math.Abs(value1) - Math.Abs(value2));
                else
                    distance += Math.Abs(value1) + Math.Abs(value2);
            }

            return distance <= 3;
        }

        private void ReadPoints()
        {
            var lines = _stream.ReadStringDocument();
            for (int i = 0; i < lines.Count; i++)
            {
                var point = Array.ConvertAll(lines[i].Split(','), int.Parse);
                var set = new DisjointSet<int[]>(point);
                _sets.Add(set);
            }
        }
    }
}
