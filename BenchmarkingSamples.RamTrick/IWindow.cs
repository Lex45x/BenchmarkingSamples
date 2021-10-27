namespace BenchmarkingSamples.RamTrick
{
    public interface IWindow
    {
        int Size { get; }
        decimal Max { get; }
        void Append(decimal value, long globalIndex);
    }
}