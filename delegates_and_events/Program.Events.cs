using System;

namespace delegates_and_events
{
    internal static partial class Program
    {
        private static void Events()
        {
            var list = new ObservableList<string>();
            list.ItemAdded += ListOnItemAdded;
            // list.ItemAdded = null; // not possible! the only real difference with delegates. 
        }

        private static void ListOnItemAdded(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}