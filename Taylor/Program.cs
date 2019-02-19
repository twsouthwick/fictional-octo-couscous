using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace Taylor
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FalseArrayBenchmarks>();
            BenchmarkRunner.Run<TrueArrayBenchmarks>();
            BenchmarkRunner.Run<RandomArrayBenchmarks>();
        }
    }

    [CoreJob]
    public class FalseArrayBenchmarks
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
        public LargestSquareResult GetLargestSquare_Naive() => _matrix.GetLargestSquare_Naive();
        [Benchmark]
        public LargestSquareResult GetLargestSquare_Accumulating() => _matrix.GetLargestSquare_Accumulating();
    }


    [CoreJob]
    public class TrueArrayBenchmarks
    {
        private BoolMatrix _matrix;

        [Params(10, 100, 1_000, 10_000)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = new BoolMatrix(Size);
            for (int i = 0; i < Size; i++)
                for (int j = 0; i < Size; i++)
                    _matrix[i, j] = true;
        }

        [Benchmark]
        public LargestSquareResult GetLargestSquare_Naive() => _matrix.GetLargestSquare_Naive();
        [Benchmark]
        public LargestSquareResult GetLargestSquare_Accumulating() => _matrix.GetLargestSquare_Accumulating();
    }


    [CoreJob]
    public class RandomArrayBenchmarks
    {
        private BoolMatrix _matrix;

        [Params(10, 100, 1_000, 10_000)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = new BoolMatrix(Size);

            var random = new Random();
            for (int i = 0; i < Size/2; i++)
            {
                _matrix[random.Next(0,Size), random.Next(0, Size)] = true;
            }
        }

        [Benchmark]
        public LargestSquareResult GetLargestSquare_Naive() => _matrix.GetLargestSquare_Naive();
        [Benchmark]
        public LargestSquareResult GetLargestSquare_Accumulating() => _matrix.GetLargestSquare_Accumulating();
    }
}
