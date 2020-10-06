using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace thread_examples_bis
{
    internal class Program
    {
        private static readonly ThreadLocal<int> threadLocal = new ThreadLocal<int>(() => 3);

        [ThreadStatic]
        private static int threadStatic = 3; //initialization occurs only once! only the first thread sees 3!

        private static bool done;
        private static readonly object lockDone = new object();

        private static void Main(string[] args)
        {
            //Your code is already running in a Thread:
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            ////********************* EXAMPLE 01************************
            ////You can run a function in the synchronous way, from your Thread
            ////meaning that you have to wait for it to finish before you can continue
            //Ex01();
            //Console.WriteLine("After Ex01 sync");
            ////Or you can start a function in an asynchronous way,
            ////meaning that it runs on a different Thread
            ////while you can continue on in a parallel way.

            ////You can start a new Thread in different ways.
            ////You give a Thread something to do (a function to run)
            //Thread t = new Thread(Ex01);
            ////Then you start it
            //t.Start();
            ////This line could run before the Ex01 even starts,
            ////because Start SCHEDULES the Thread, but
            ////the OS will decide when to actually execute it.
            //Console.WriteLine("This line is after the Ex01 async, but does it get executed after the call? Maybe, maybe not...");
            ////******************* END EXAMPLE 01 ************************


            ////*************** EXAMPLE 02 ***************************
            ////Multiple Threads could get executed at the same time
            //// (or at least that's the illusion in case you only have one CPU)
            //new Thread(Ex02).Start();
            //Ex02();
            ////************ END EXAMPLE 02 ***************************


            //*************** EXAMPLE 03 ***************************
            // Race condition
            //Ex03();
            //************ END EXAMPLE 03 ***************************

            //*************** EXAMPLE 04 ***************************
            // Race condition with lock
            // Ex04();
            //************ END EXAMPLE 04 ***************************

            //*************** EXAMPLE 05 ***************************
            // new Thread vs ThreadPool vs Task
            //Ex05();
            //************ END EXAMPLE 05 ***************************

            //*************** EXAMPLE 06 ***************************
            // deadlocks
            // Ex06();
            //************ END EXAMPLE 06 ***************************

            //*************** EXAMPLE 07 ***************************
            // producer / consumer scenario (Monitor.Wait / Monitor.pulse)
            // Ex07();
            //************ END EXAMPLE 07 ***************************

            //*************** EXAMPLE 08 ***************************
            // producer / consumer scenario (BlockingCollection)
            Ex08();
            //************ END EXAMPLE 08 ***************************

            //*************** EXAMPLE 09 ***************************
            // CancellationTokens with Thread
            //Ex09();
            //************ END EXAMPLE 09 ***************************

            //*************** EXAMPLE 10 ***************************
            // CancellationTokens with ThreadPool
            //Ex10();
            //************ END EXAMPLE 10 ***************************


            //*************** EXAMPLE 11 ***************************
            // CancellationTokens with Task
            //Ex11();
            //************ END EXAMPLE 11 ***************************

            //*************** EXAMPLE 12 ***************************
            // ThreadStatic
            //Ex12();
            //************ END EXAMPLE 12 ***************************


            //*************** EXAMPLE 13 ***************************
            // ThreadLocal
            //Ex13();
            //************ END EXAMPLE 13 ***************************

            //*************** EXAMPLE 14 ***************************
            // ThreadPool does not reset ThreadLocal!
            //Ex14();
            //************ END EXAMPLE 14 ***************************

            //*************** EXAMPLE 15 ***************************
            // Timer
            //Ex15();
            //************ END EXAMPLE 15 ***************************

            //*************** EXAMPLE 16 ***************************
            // Exceptions do not propagate to the main thread
            //Ex16();
            //************ END EXAMPLE 16 ***************************

            //*************** EXAMPLE 17 ***************************
            // Exceptions should be handled on the same thread that caused them
            //Ex17();
            //************ END EXAMPLE 16 ***************************

            //*************** EXAMPLE 18 ***************************
            // Tasks can propagate exceptions on the main thread if inspected
            //but they return an aggregate exception
            //Ex18();
            //************ END EXAMPLE 18 ***************************

            //BarrierExample.Example();

            //SpinLockDemo.Example();

            //IoCompletionPorts();


            //*************** EXAMPLE 25 ***************************
            // Tasks
            //Ex25();
            //************ END EXAMPLE 25 ***************************


            Console.WriteLine("Press Enter to quit");
            Console.ReadLine();
        }

        private static void Ex25()
        {
            Action start = () =>
            {
                Console.WriteLine($"Started - {Thread.CurrentThread.ManagedThreadId} {Task.CurrentId}");
            };

            Action onCompleted = () =>
            {
                Console.WriteLine($"OnCompleted - {Thread.CurrentThread.ManagedThreadId} {Task.CurrentId}");
            };

            Action<Task> continuation = antecedentTask =>
            {
                Console.WriteLine($"Continuation - {Thread.CurrentThread.ManagedThreadId} {Task.CurrentId}");
            };

            var t = new Task(start);
            t.GetAwaiter().OnCompleted(onCompleted);
            t.ContinueWith(continuation);
            t.Start();
        }


        private static void Ex18()
        {
            Task t = null;
            try
            {
                t = Task.Run(() => { throw new NotImplementedException("ooooooops"); });
                //when you inspect the result of a task you get the exception back.
                //if you don't the exception is lost and the application continues
                //as if nothing happened
                t.Wait();
            }
            catch (AggregateException ex) //the exception you get is an aggregate exception
            {
                Console.WriteLine($"{ex.GetType().Name} - {ex.Message}");
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine($"\t{e.GetType().Name} - {e.Message}");
                }
            }
        }

        private static void Ex17()
        {
            //new Thread(() =>
            //{
            //    try
            //    {
            //        throw new NotImplementedException("ooooooops");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.GetType().Name);
            //    }
            //}).Start();


            //ThreadPool.QueueUserWorkItem(_ =>
            //{
            //    try
            //    {
            //        throw new NotImplementedException("ooooooops");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.GetType().Name);
            //    }
            //});

            Task.Run(() =>
            {
                try
                {
                    throw new NotImplementedException("ooooooops");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType().Name);
                }
            });
        }

        private static void Ex16()
        {
            try
            {
                new Thread(() => { throw new NotImplementedException("ooooooops"); }).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
            }
        }

        private static async Task IoCompletionPorts()
        {
            await ReadFilesInDirectory("C:\\");
        }

        private static async Task ReadFilesInDirectory(string directory)
        {
            foreach (var dirPath in Directory.GetDirectories(directory))
            {
                ReadFilesInDirectory(dirPath);
            }

            foreach (var filePath in Directory.GetFiles(directory))
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                ThreadPool.GetAvailableThreads(out var workerThreads, out var completionPortThreads);
                Console.WriteLine(
                    $"{Thread.CurrentThread.ManagedThreadId} {Thread.CurrentThread.IsThreadPoolThread} {workerThreads} {completionPortThreads}");
            }
        }

        private static void Ex15()
        {
            using (var timer =
                new Timer(
                    _ =>
                    {
                        Console.WriteLine(
                            $"{Thread.CurrentThread.ManagedThreadId} {Thread.CurrentThread.IsThreadPoolThread} Press E to End Timer");
                    }, null, 3000, 1000))
            {
                Console.WriteLine(".... Wait for it...");
                do
                {
                } while (Console.ReadKey().Key != ConsoleKey.E);
            }
        }

        private static void Ex14()
        {
            for (var i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(x =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine(
                        $"{Thread.CurrentThread.ManagedThreadId} - threadLocal.Value == {threadLocal.Value}");
                    threadLocal.Value = (int) x;
                    Thread.Sleep(100);
                    Console.WriteLine(
                        $"\t{Thread.CurrentThread.ManagedThreadId} - threadLocal.Value == {threadLocal.Value}");
                }, i);
            }
        }

        private static void Ex13()
        {
            new Thread(() =>
            {
                Console.WriteLine(
                    $"A - {Thread.CurrentThread.ManagedThreadId} - threadLocal.Value == {threadLocal.Value}");
                threadLocal.Value = 1;
                Thread.Sleep(1000);
                Console.WriteLine(
                    $"A - {Thread.CurrentThread.ManagedThreadId} - threadLocal.Value == {threadLocal.Value}");
            }).Start();
            new Thread(() =>
            {
                Console.WriteLine(
                    $"B - {Thread.CurrentThread.ManagedThreadId} - threadLocal.Value == {threadLocal.Value}");
                threadLocal.Value = 2;
                Console.WriteLine(
                    $"B - {Thread.CurrentThread.ManagedThreadId} - threadLocal.Value == {threadLocal.Value}");
            }).Start();
        }
        //https://blogs.msdn.microsoft.com/jfoscoding/2006/07/18/are-you-familiar-with-threadstatic/

        private static void Ex12()
        {
            new Thread(() =>
            {
                Console.WriteLine($"A - {Thread.CurrentThread.ManagedThreadId} - threadStatic == {threadStatic}");
                threadStatic = 1;
                Thread.Sleep(1000);
                Console.WriteLine($"A - {Thread.CurrentThread.ManagedThreadId} - threadStatic == {threadStatic}");
            }).Start();
            new Thread(() =>
            {
                Console.WriteLine($"B - {Thread.CurrentThread.ManagedThreadId} - threadStatic == {threadStatic}");
                threadStatic = 2;
                Console.WriteLine($"B - {Thread.CurrentThread.ManagedThreadId} - threadStatic == {threadStatic}");
            }).Start();
        }


        private static void Ex11()
        {
            var cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                var ct = cts.Token;
                Console.Clear();
                Console.WriteLine("Type A to abort");
                try
                {
                    var i = 0;
                    while (true)
                    {
                        Console.CursorTop = 3;
                        Console.CursorLeft = 3;
                        Console.Write(i++);
                        ct.ThrowIfCancellationRequested();
                    }
                }
                catch
                {
                    Console.WriteLine("kthxbye");
                    //undo stuff or just leave...
                }
            }, cts.Token);

            var k = Console.ReadKey();
            if (k.Key == ConsoleKey.A)
            {
                cts.Cancel();
            }
        }

        private static void Ex10()
        {
            var cts = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(o =>
            {
                var ct = (CancellationToken) o;
                Console.Clear();
                Console.WriteLine("Type A to abort");
                try
                {
                    var i = 0;
                    while (true)
                    {
                        Console.CursorTop = 3;
                        Console.CursorLeft = 3;
                        Console.Write(i++);
                        ct.ThrowIfCancellationRequested();
                    }
                }
                catch
                {
                    Console.WriteLine("kthxbye");
                    //undo stuff or just leave...
                }
            }, cts.Token);

            var k = Console.ReadKey();
            if (k.Key == ConsoleKey.A)
            {
                cts.Cancel();
            }
        }

        private static void Ex09()
        {
            var cts = new CancellationTokenSource();

            var t = new Thread(o =>
            {
                var ct = (CancellationToken) o;
                Console.Clear();
                Console.WriteLine("Type A to abort");
                try
                {
                    var i = 0;
                    while (true)
                    {
                        Console.CursorTop = 3;
                        Console.CursorLeft = 3;
                        Console.Write(i++);
                        ct.ThrowIfCancellationRequested();
                    }
                }
                catch
                {
                    Console.WriteLine("kthxbye");
                    //undo stuff or just leave...
                }
            });
            t.Start(cts.Token);

            var k = Console.ReadKey();
            if (k.Key == ConsoleKey.A)
            {
                cts.Cancel();
            }
        }

        private static void Ex08()
        {
            // A bounded collection. It can hold no more 
            // than 100 items at once.
            var dataItems = new BlockingCollection<int>(100);


            // A simple blocking consumer with no cancellation.
            for (var i = 0; i < 3; i++)
            {
                Task.Run(() =>
                {
                    while (!dataItems.IsCompleted)
                    {
                        lock (dataItems)
                        {
                            int? data = null;
                            // Blocks if dataItems.Count == 0
                            // IOE means that Take() was called on a completed collection.
                            // Some other thread can call CompleteAdding after we pass the
                            // IsCompleted check but before we call Take. 
                            // In this example, we can simply catch the exception since the 
                            // loop will break on the next iteration.
                            try
                            {
                                data = dataItems.Take();
                            }
                            catch (InvalidOperationException)
                            {
                            }

                            if (data != null)
                            {
                                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} took {data}");
                            }
                        }
                    }

                    Console.WriteLine("\r\nNo more items to take.");
                });
            }


            // A simple blocking producer with no cancellation.
            Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    // Blocks if numbers.Count == dataItems.BoundedCapacity
                    dataItems.Add(i);
                }

                // Let consumer know we are done.
                dataItems.CompleteAdding();
            });
        }

        private static void Ex07()
        {
            var buffer = new Buffer();
            var producerThread = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} pushing {i}");
                    buffer.Push(i);
                }

                buffer.SendComplete();
            });
            for (var i = 0; i < 3; i++)
            {
                new Thread(() =>
                {
                    while (!buffer.IsCompleted)
                    {
                        Monitor.TryEnter(buffer);
                        if (Monitor.IsEntered(buffer))
                        {
                            Console.WriteLine($"\t{Thread.CurrentThread.ManagedThreadId} pulling {buffer.Pull()}");
                        }
                    }
                }).Start();
            }

            producerThread.Start();
        }

        private static void Ex06()
        {
            BankAccount b1 = new BankAccount {Id = 1};
            b1.Deposit(100);
            BankAccount b2 = new BankAccount {Id = 2};
            b2.Deposit(100);

            Bank b = new Bank();

            var tasks = new List<Task>();
            var evt = new ManualResetEventSlim(false);

            var threadcount = 2000;

            //CountdownEvent countdownEvent = new CountdownEvent(threadcount);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 2000 / 2; i++)
            {
                new Thread(() =>
                {
                    while (!b.Transfer(b1, b2, 50))
                    {
                        Console.WriteLine("Something went wrong");
                    }

                    Interlocked.Decrement(ref threadcount);
                    if (threadcount <= 0)
                    {
                        evt.Set();
                    }
                }).Start();
                new Thread(() =>
                {
                    while (!b.Transfer(b2, b1, 50))
                    {
                        Console.WriteLine("something went wrong");
                    }

                    Interlocked.Decrement(ref threadcount);
                    if (threadcount <= 0)
                    {
                        evt.Set();
                    }
                }).Start();

                //ThreadPool.QueueUserWorkItem(_ =>
                //{
                //    while (!b.Transfer(b1, b2, 50))
                //    {
                //        Console.WriteLine("Something went wrong");
                //    }
                //    Interlocked.Decrement(ref threadcount);
                //    if (threadcount <= 0) evt.Set();
                //});
                //ThreadPool.QueueUserWorkItem(_ =>
                //{
                //    while (!b.Transfer(b2, b1, 50))
                //    {
                //        Console.WriteLine("something went wrong");
                //    }
                //    Interlocked.Decrement(ref threadcount);
                //    if (threadcount <= 0) evt.Set();
                //});


                //new Thread(() =>
                //{
                //    while (!b.Transfer(b1, b2, 50))
                //    {
                //        Console.WriteLine("Something went wrong");
                //    }
                //    countdownEvent.Signal();

                //}).Start();
                //new Thread(() =>
                //{
                //    while (!b.Transfer(b2, b1, 50))
                //    {
                //        Console.WriteLine("something went wrong");
                //    }
                //    countdownEvent.Signal();
                //}).Start();


                //ThreadPool.QueueUserWorkItem(_ =>
                //{
                //    while (!b.Transfer(b1, b2, 50))
                //    {
                //        Console.WriteLine("Something went wrong");
                //    }
                //    countdownEvent.Signal();

                //});
                //ThreadPool.QueueUserWorkItem(_ =>
                //{
                //    while (!b.Transfer(b2, b1, 50))
                //    {
                //        Console.WriteLine("something went wrong");
                //    }
                //    countdownEvent.Signal();
                //});


                //tasks.Add(Task.Run(() =>
                //{
                //    while (!b.Transfer(b1, b2, 50))
                //    {
                //        Console.WriteLine("something went wrong");
                //    }
                //}));
                //tasks.Add(Task.Run(() =>
                //{
                //    while (!b.Transfer(b2, b1, 50))
                //    {
                //        Console.WriteLine("something went wrong");
                //    }
                //}));
            }

            //Task.WhenAll(tasks).ContinueWith(_ =>
            //{
            //    Console.WriteLine($"b1.Saldo == {b1.Saldo}, b2.Saldo == {b2.Saldo} in {sw.ElapsedMilliseconds}");
            //});

            evt.Wait();

            //countdownEvent.Wait();
            Console.WriteLine($"b1.Saldo == {b1.Saldo}, b2.Saldo == {b2.Saldo} after {sw.ElapsedMilliseconds}");
        }

        private static void Ex04()
        {
            //DoStuff();
            //DoStuff();

            //ThreadPool.QueueUserWorkItem(_=>DoStuffLock());
            //new Thread(DoStuffLock).Start();
            Task.Run(DoStuffLock);
            DoStuffLock();
        }

        private static void DoStuffLock()
        {
            lock (lockDone)
            {
                if (!done)
                {
                    Console.WriteLine(
                        $"done (on thread {Thread.CurrentThread.ManagedThreadId} - {Thread.CurrentThread.IsThreadPoolThread})!");
                    done = true;
                }
            }
        }

        private static void Ex05()
        {
            for (var i = 0; i < 1000; i++)
            {
                //new Thread(() => Console.WriteLine($"{Thread.CurrentThread.IsThreadPoolThread} - {Thread.CurrentThread.ManagedThreadId}")).Start();
                //ThreadPool.QueueUserWorkItem(_ => Console.WriteLine($"{Thread.CurrentThread.IsThreadPoolThread} - {Thread.CurrentThread.ManagedThreadId}"));
                Task.Run(() =>
                    Console.WriteLine(
                        $"{Thread.CurrentThread.IsThreadPoolThread} - {Thread.CurrentThread.ManagedThreadId}"));
            }
        }

        private static void Ex03()
        {
            //DoStuff();
            //DoStuff();

            new Thread(DoStuff).Start();
            DoStuff();
        }

        private static void DoStuff()
        {
            if (!done)
            {
                Console.WriteLine($"done (on thread {Thread.CurrentThread.ManagedThreadId})!");
                done = true;
            }
        }

        private static void Ex01()
        {
            Console.WriteLine($"Hi from Ex01, running on Thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Ex02()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"Hi from Ex02, {i} - running on Thread: {Thread.CurrentThread.ManagedThreadId}");
                for (var j = 0; j < 10000000; j++)
                {
                    //simulating that the CPU is veeeeeeeeeeery busy
                }
            }
        }
    }
}