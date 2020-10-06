namespace thread_examples_bis
{
    class BankAccount
    {
        public int Id { get; set; }
        public double Saldo { get; private set; }
        public double Withdraw(double amount) {
            if (amount < 0) {
                amount = 0;
            }
            Saldo -= amount;
            return amount;
        }

        public double Deposit(double amount)
        {
            if (amount < 0)
            {
                amount = 0;
            }
            Saldo += amount;
            return amount;
        }
    }
}
