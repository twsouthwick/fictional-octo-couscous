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

            // First fill random area with potentially larger squares of true
            for (int i = 0; i < Size / 2; i++)
            {
                var width = random.Next(0, Size / random.Next(5, Size / 2));
                var height = random.Next(0, Size / random.Next(5, Size / 2));

                for (int x = 0; x + width < Size && x < width; x++)
                {
                    for (int y = 0; y + height < Size && y < height; y++)
                    {
                        _matrix[x, y] = true;
                    }
                }
            }
        }

        [Benchmark]
        public LargestSquareResult GetLargestSquare_Naive() => _matrix.GetLargestSquare_Naive();
        [Benchmark]
        public LargestSquareResult GetLargestSquare_Accumulating() => _matrix.GetLargestSquare_Accumulating();
    }
}
