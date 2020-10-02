using System;

namespace delegates_and_events
{
    internal static partial class Program
    {
        private static void MyLogFunction(string message)
        {
            Console.WriteLine($"The list reports the following message: {message}");
        }
        private static void Delegates()
        {
            var list = new LoggerList<int>();

            // as a function pointer
            list.Log += MyLogFunction;

            // anonymous method
            list.Log += delegate(string message) { Console.WriteLine($"Anonymous methods (yeey): {message}"); };
            // lambda
            list.Log += message => Console.WriteLine($"Lambda's yeey!, {message}");
            // function pointer to an existing function
            list.Log += Console.WriteLine;

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}