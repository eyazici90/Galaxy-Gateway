using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using System;

namespace Galaxy.Gateway.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestClass>();

            Console.ReadLine();

        }


        [SimpleJob(RunStrategy.Monitoring, launchCount: 1,
            warmupCount: 1, targetCount: 1)]
        public class TestClass
        {
            [Benchmark]
            private int TestMethod(int x, int y)
            {
                return x + y;
            }
        }
    }
}
