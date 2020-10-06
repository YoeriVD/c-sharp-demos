using System.Threading;

namespace thread_examples_bis
{
    class Bank
    {
        public bool Transfer(BankAccount from, BankAccount to, double amount) {
            bool lockTaken = false;
            bool retval = false;
            Monitor.TryEnter(from, 10, ref lockTaken);
            if (lockTaken)
            {
                bool lockTaken2 = false;

                Monitor.TryEnter(to, 10, ref lockTaken2);

                while (!Monitor.IsEntered(to))
                {
                    Monitor.Wait(from);
                    Monitor.TryEnter(to, 10, ref lockTaken2);
                }



                amount = from.Withdraw(amount);
                to.Deposit(amount);
                Monitor.Pulse(from);
                Monitor.Pulse(to);
                
                Monitor.Exit(from);
                Monitor.Exit(to);
                retval = true;
                
            }
            return retval;
        }
    }
}
