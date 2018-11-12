using System;

namespace AOC.Common.Input
{
    class InputHelper
    {
        private readonly IStream _stream;

        public InputHelper(IStream stream)
        {
            _stream = stream;
        }

        public T[] ReadArray<T>(Converter<string, T> converter, char delimiter = ' ', bool skipCount = false)
        {
            if (skipCount)
                Convert.ToInt32(_stream.ReadLine());
            return Array.ConvertAll(_stream.ReadLine().Split(delimiter), converter);
        }

        public int[] ReadArrayInt32(char delimiter = ' ', bool skipCount = false)
        {
            return ReadArray(t => Convert.ToInt32(t), delimiter, skipCount);
        }
    }
}
