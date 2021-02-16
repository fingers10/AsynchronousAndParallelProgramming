using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AdvancedTopics
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // step-1. Sequential execution
            //var result = Enumerable.Range(0, 100)
            //    .Select(Compute)
            //    .Sum();

            //Console.WriteLine(result);

            // step-2. Parallel execution
            //var result = Enumerable.Range(0, 100)
            //    .AsParallel()
            //    .Select(Compute)
            //    .Sum();

            //Console.WriteLine(result);

            //// step-3. Checking order
            //var result = Enumerable.Range(0, 100)
            //    .AsParallel()
            //    .Select(Compute)
            //    .Take(10);

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            // step-4. Maintaing sequence
            //var result = Enumerable.Range(0, 100)
            //    .AsParallel()
            //    .AsOrdered()
            //    .Select(Compute)
            //    .Take(10);

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            // step-5. Using Parallel Operation to Loop all instead of normal foreach
            //var result = Enumerable.Range(0, 100)
            //    .AsParallel()
            //    .AsOrdered()
            //    .Select(Compute)
            //    .Take(10);

            //result.ForAll(Console.WriteLine);

            // step-6. Other options to explore.
            //var result = Enumerable.Range(0, 100)
            //    .AsParallel()
            //    .AsOrdered()
            //    .WithDegreeOfParallelism(1)
            //    .WithCancellation(new CancellationToken(canceled: true))
            //    .Select(Compute)
            //    .Take(10);

            //result.ForAll(Console.WriteLine);

            Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds}ms to run");
        }

        static Random random = new Random();
        static decimal Compute(int value)
        {
            var randomMilliseconds = random.Next(10, 50);
            var end = DateTime.Now + TimeSpan.FromMilliseconds(randomMilliseconds);

            // This will spin for a while...
            while (DateTime.Now < end) { }

            return value + 0.5m;
        }
    }
}
