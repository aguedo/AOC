using System.Collections.Generic;

namespace AOC.Common.DisjointSets
{
    public class DisjointSet<T>
    {
        public T Vaue { get; private set; }
        public DisjointSet<T> Parent { get; private set; }
        public int Rank { get; private set; }
        public int Count { get; private set; }

        public DisjointSet(T vaue)
        {
            Vaue = vaue;
            Rank = 1;
            Count = 1;
            Parent = null;
        }

        public DisjointSet<T> GetRepresentative()
        {
            var representative = this;
            var pathToCompress = new List<DisjointSet<T>>();
            while (representative.Parent != null)
            {
                pathToCompress.Add(representative);
                representative = representative.Parent;
            }

            foreach (var disjointSet in pathToCompress)
                disjointSet.Parent = representative;

            return representative;
        }

        public void Union(DisjointSet<T> other)
        {
            var representative = GetRepresentative();
            var otherRepresentative = other.GetRepresentative();

            if (representative == otherRepresentative)
                return;

            if (representative.Rank == other.Rank)
            {
                representative.Rank++;
                otherRepresentative.Parent = representative;
                representative.Count += otherRepresentative.Count;
            }
            else if (representative.Rank > other.Rank)
            {
                otherRepresentative.Parent = representative;
                representative.Count += otherRepresentative.Count;
            }
            else
            {
                representative.Parent = otherRepresentative;
                otherRepresentative.Count += representative.Count;
            }
        }
    }
}
