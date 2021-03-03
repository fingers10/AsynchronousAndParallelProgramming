using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AdvancedTopics
{
    class Program
    {
        static object syncRoot = new object();
        static object lock1 = new object();
        static object lock2 = new object();
        static async Task Main(string[] args)
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
            //    .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            //    .WithDegreeOfParallelism(1)
            //    .WithCancellation(new CancellationToken(canceled: true))
            //    .Select(Compute)
            //    .Take(10);

            //result.ForAll(Console.WriteLine);

            //decimal total = 0;
            //for (int i = 0; i < 100; i++)
            //{
            //    total += Compute(i);
            //}

            //Console.WriteLine(total);

            //decimal total = 0;
            //Parallel.For(0, 100, (i) =>
            //{
            //    total += Compute(i);
            //});

            //Console.WriteLine(total);

            //decimal total = 0;
            //Parallel.For(0, 100, (i) =>
            //{
            //    lock (syncRoot)
            //    {
            //        total += Compute(i);
            //    }
            //});

            //Console.WriteLine(total);

            //decimal total = 0;
            //Parallel.For(0, 100, (i) =>
            //{
            //    var result = Compute(i);
            //    lock (syncRoot)
            //    {
            //        total += result;
            //    }
            //});

            //Console.WriteLine(total);

            //int total = 0;
            //Parallel.For(0, 100, (i) =>
            //{
            //    var result = Compute(i);
            //    Interlocked.Add(ref total, (int)result);
            //});

            //Console.WriteLine(total);

            //var t1 = Task.Run(() =>
            //{
            //    lock (lock1)
            //    {
            //        Thread.Sleep(1);
            //        lock (lock2)
            //        {
            //            Console.WriteLine("Hello");
            //        }
            //    }
            //});
            //var t2 = Task.Run(() =>
            //{
            //    lock (lock2)
            //    {
            //        Thread.Sleep(1);
            //        lock (lock1)
            //        {
            //            Console.WriteLine("World!");
            //        }
            //    }
            //});

            //await Task.WhenAll(t1, t2);

            var queue = new Queue();
            queue.Enqueue(1);
            queue.Dequeue();
            queue.Peek();

            var concurrentQueue = new ConcurrentQueue<int>();
            concurrentQueue.Enqueue(1);
            var success = concurrentQueue.TryDequeue(out var result);
            success = concurrentQueue.TryPeek(out result);

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
