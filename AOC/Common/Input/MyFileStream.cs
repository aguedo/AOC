using System;
using System.Collections.Generic;
using System.IO;

namespace AOC.Common.Input
{
    public class MyFileStream : IDisposable
    {
        private readonly StreamReader _reader = new StreamReader(File.OpenRead("input.txt"));
        private readonly StreamWriter _writer = new StreamWriter(File.OpenWrite("output.txt"));

        public bool EndOfStream => _reader.EndOfStream;

        public string ReadLine()
        {
            return _reader.ReadLine();
        }
        public int ReadInt32Line()
        {
            return ReadLine(t => Convert.ToInt32(t));
        }
        public long ReadLongLine()
        {
            return ReadLine(t => Convert.ToInt64(t));
        }
        public T ReadLine<T>(Converter<string, T> converter)
        {
            return converter(_reader.ReadLine());
        }

        public T[] ReadArrayLine<T>(Converter<string, T> converter, char delimiter = ' ', bool skipCount = false)
        {
            if (skipCount)
                _reader.ReadLine();
            return Array.ConvertAll(_reader.ReadLine().Split(delimiter), converter);
        }
        public int[] ReadInt32ArrayLine(char delimiter = ' ', bool skipCount = false)
        {
            return ReadArrayLine(t => Convert.ToInt32(t), delimiter, skipCount);
        }
        public long[] ReadLongArrayLine(char delimiter = ' ', bool skipCount = false)
        {
            return ReadArrayLine(t => Convert.ToInt64(t), delimiter, skipCount);
        }


        public List<T> ReadDocument<T>(Converter<string, T> converter)
        {
            var list = new List<T>();
            while (!_reader.EndOfStream)
                list.Add(converter(_reader.ReadLine()));
            return list;
        }
        public List<string> ReadStringDocument()
        {
            return ReadDocument(t => t);
        }
        public List<long> ReadLongDocument()
        {
            return ReadDocument(t => Convert.ToInt64(t));
        }
        public List<int> ReadInt32Document()
        {
            return ReadDocument(t => Convert.ToInt32(t));
        }
        public List<int[]> ReadArrayDocument()
        {
            return ReadDocument(t => ReadInt32ArrayLine());
        }
        

        public void WriteLine(string line)
        {
            _writer.WriteLine(line);
        }

        public void Dispose()
        {
            _reader.Dispose();
            _writer.Dispose();
        }
    }
}
