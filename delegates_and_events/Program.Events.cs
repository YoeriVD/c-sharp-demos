using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace delegates_and_events
{
    class MyItemAddedListener
    {
        private int _callCount = 0;

        public void ListOnItemAdded(object sender, EventArgs eventArgs)
        {
            _callCount++;
            Console.WriteLine($"number of items added {_callCount}");
        }

        ~MyItemAddedListener() => Console.WriteLine($"{nameof(MyItemAddedListener)} is now being garbage collected.");
    }

    internal static partial class Program
    {
        private static async Task Events()
        {
            var list = new ObservableList<string>();
            AddListener(list);
            // list.ItemAdded = null; // not possible! the only real difference with delegates. 

            list.Add("some item");
            list.Add("some item");
            list.Add("some item");
            list.Add("some item");

            var input = "go";
            while (!string.Equals(input, "q", StringComparison.Ordinal))
            {
                foreach (var listener in list.GetEventListeners())
                {
                    Console.Write("Listener that is still in memory: ");
                    Console.WriteLine(listener.Target);
                }

                Console.Write("Force start GC..");
                GC.Collect();
                await Task.Delay(1000);

                input = Console.ReadLine();
            }
            
            while (true)
            {
                foreach (var listener in list.GetEventListeners())
                {
                    Console.Write("Removing listener: ");
                    Console.WriteLine(listener.Target);
                    list.ItemAdded -= listener as ItemAddedDelegate;
                }

                GC.Collect();

                await Task.Delay(1000);
            }
        }

        private static void AddListener(ObservableList<string> list)
        {
            var listener = new MyItemAddedListener();
            list.ItemAdded += listener.ListOnItemAdded;
            //on exit of method, no reference to listener exists
            // a 'closure' is created
        }
    }
}