using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace threads_examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //RunProcessExample();


            var numbers = Enumerable
                .Range(10, 34)
                .Select(i => (uint) i)
                .ToArray();

            // Helpers.Clock(nameof(Sync), () => Sync(numbers));
            // Helpers.Clock(nameof(UsingThreads), () => UsingThreads(numbers.Reverse().ToArray()));
             //Helpers.Clock(nameof(UsingThreadPool), () => UsingThreadPool(numbers));
            Helpers.Clock(nameof(AsyncCall), () => AsyncCall(numbers));
        }

        private static void RunProcessExample()
        {
            foreach (var process1 in Process
                .GetProcesses()
                .Where(p => !string.IsNullOrWhiteSpace(p.ProcessName))
                .OrderBy(p => p.ProcessName))
            {
                Console.WriteLine("process: " + process1.ProcessName);
            }

            using var process = Process.Start("docker", "run hello-world");
            Console.WriteLine("number of threads in docker " + process.Threads.Count);
            process.WaitForExit();
            var rider = Process.GetProcessesByName("rider").First();
            Console.WriteLine($"number of threads in {nameof(rider)} " + rider.Threads.Count);
        }


        public static void Sync(params uint[] numbers)
        {
            foreach (var number in numbers)
            {
                var result = Helpers.Fibonacci(number);
                Console.WriteLine($"{nameof(Sync)}: {number}! = {result}");
            }
        }

        public static void UsingThreads(params uint[] numbers)
        {
            var cd = new CountdownEvent(numbers.Length);
            foreach (var number in numbers)
            {
                var t = new Thread(() =>
                {
                    var result = Helpers.Fibonacci(number);
                    Console.WriteLine(
                        $"{nameof(UsingThreads)} {Thread.CurrentThread.ManagedThreadId} (pool: {Thread.CurrentThread.IsThreadPoolThread}):  {number}! = {result}"
                    );
                    cd.Signal();
                });
                t.Start();
            }

            cd.Wait();
        }

        private static object _lock = new object();

        public static void UsingThreadPool(params uint[] numbers)
        {
            var threadIds = new ConcurrentBag<int>();
            var cd = new CountdownEvent(numbers.Length);

            var processorCount = Environment.ProcessorCount;
            Console.WriteLine("Number of processors:" + processorCount);
            // You cannot set the maximum number of worker threads or I/O completion threads to a number smaller
            // than the number of processors on the computer. 
            // ThreadPool.SetMaxThreads(processorCount, processorCount);
            ThreadPool.GetMinThreads(out var minWorkerThreads, out var minCompletionThreads);
            Console.WriteLine($"minimum threads {minWorkerThreads}");
            ThreadPool.GetAvailableThreads(out var aWorkerThreads, out var aCompletionThreads);
            Console.WriteLine($"available threads {aWorkerThreads}");

            foreach (var number in numbers)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    var result = Helpers.Fibonacci(number);
                    lock (_lock)
                    {
                        Console.WriteLine(
                            $"{nameof(UsingThreadPool)} {threadId} (pool: {Thread.CurrentThread.IsThreadPoolThread}, reuse: {threadIds.Contains(threadId)}):  {number}! = {result}"
                        );
                    }

                    threadIds.Add(threadId);
                    cd.Signal();
                });
            }

            cd.Wait();
        }

        // NOT SUPPORTED ON .NET CORE
        public static void AsyncCall(params uint[] numbers)
        {
            var caller = new AsyncMethodCaller(Helpers.Fibonacci); //delegate
            foreach (var number in numbers)
            {
                caller.BeginInvoke(number,
                    (result) => { Console.WriteLine($"{nameof(AsyncCall)}: {number}! = {result}"); }, null);
            }
        }
    }

    public delegate uint AsyncMethodCaller(uint i);
}