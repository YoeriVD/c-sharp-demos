using System;
using System.Collections.Concurrent;
using System.Diagnostics;

static internal class Helpers
{
    private static ConcurrentDictionary<uint,uint> _fCache= new ConcurrentDictionary<uint, uint>();
    public static uint Fibonacci(uint i)
    {
        if (_fCache.ContainsKey(i)) return _fCache[i];
        if (i == 1 || i == 0) return i;
        var fibonacci = Fibonacci(i - 1) + Fibonacci(i - 2);
        _fCache.TryAdd(i, fibonacci);
        return fibonacci;
    }
    
    
    // public static uint Fibonacci(uint i)
    // {
    //     if (i == 1 || i == 0) return i;
    //     var fibonacci = Fibonacci(i - 1) + Fibonacci(i - 2);
    //     return fibonacci;
    // }
    public static void Clock(string methodName, Action work)
    {
        var sw = Stopwatch.StartNew();
        work();
        sw.Stop();
        Console.WriteLine($"{methodName} took {sw.ElapsedMilliseconds} ms");
    }
}