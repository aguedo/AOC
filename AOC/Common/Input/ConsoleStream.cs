using System;

namespace AOC.Common.Input
{
    class ConsoleStream : IStream
    {
        public bool EndOfStream => false;

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
