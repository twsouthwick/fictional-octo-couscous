using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Taylor
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ZeroArrayBenchmarks>();

        }
    }

    [CoreJob]
    public class ZeroArrayBenchmarks
    {
        private BoolMatrix _matrix;

        [Params(10, 100, 1_000, 10_000)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = new BoolMatrix(Size);
        }

        [Benchmark]
        public LargestSquareResult GetLargestSquare() => _matrix.GetLargestSquare();
    }
}
