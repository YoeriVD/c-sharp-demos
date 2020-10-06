using System;
using System.Diagnostics;

static internal class Helpers
{
    public static uint Fibonacci(uint i)
    {
        if (i == 1 || i == 0) return i;
        var fibonacci = Fibonacci(i - 1) + Fibonacci(i - 2);
        return fibonacci;
    }
    public static void Clock(string methodName, Action work)
    {
        var sw = Stopwatch.StartNew();
        work();
        sw.Stop();
        Console.WriteLine($"{methodName} took {sw.ElapsedMilliseconds} ms");
    }
}