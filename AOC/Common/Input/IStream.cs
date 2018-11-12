namespace AOC.Common.Input
{
    interface IStream
    {
        string ReadLine();
        void WriteLine(string line);
        bool EndOfStream { get; }
    }
}
