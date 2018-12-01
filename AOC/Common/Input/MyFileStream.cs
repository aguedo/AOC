using System;
using System.IO;

namespace AOC.Common.Input
{
    class MyFileStream : IDisposable
    {
        private readonly StreamReader _reader = new StreamReader(File.OpenRead("input.txt"));
        private readonly StreamWriter _writer = new StreamWriter(File.OpenWrite("output.txt"));

        public bool EndOfStream => _reader.EndOfStream;

        public string ReadLine()
        {
            return _reader.ReadLine();
        }

        public int ReadInt32()
        {
            return ReadLine(t => int.Parse(_reader.ReadLine()));
        }

        public long ReadLong()
        {
            return ReadLine(t => long.Parse(_reader.ReadLine()));
        }

        public T ReadLine<T>(Converter<string, T> converter)
        {
            return converter(_reader.ReadLine());
        }

        public T[] ReadArray<T>(Converter<string, T> converter, char delimiter = ' ', bool skipCount = false)
        {
            if (skipCount)
                _reader.ReadLine();
            return Array.ConvertAll(_reader.ReadLine().Split(delimiter), converter);
        }

        public int[] ReadArrayInt32(char delimiter = ' ', bool skipCount = false)
        {
            return ReadArray(t => Convert.ToInt32(t), delimiter, skipCount);
        }

        public void WriteLine(string line)
        {
            _writer.WriteLine();
        }

        public void Close()
        {
            _reader.Dispose();
            _writer.Dispose();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
