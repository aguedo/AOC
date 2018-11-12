using System.IO;

namespace AOC.Common.Input
{
    class MyFileStream : IStream
    {
        private readonly StreamReader _reader = new StreamReader(File.OpenRead("input.txt"));
        private readonly StreamWriter _writer = new StreamWriter(File.OpenWrite("output.txt"));

        public bool EndOfStream => _reader.EndOfStream;

        public string ReadLine()
        {
            return _reader.ReadLine();
        }

        public void WriteLine(string line)
        {
            _writer.WriteLine();
        }
    }
}
