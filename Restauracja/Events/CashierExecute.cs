using System;

namespace Restauracja
{
    internal class CashierExecute:Event
    {
        private readonly Cashier Cashier;
        public CashierExecute(int executeTime, Cashier cashier, int clock) : base(clock, executeTime)
        {
            Cashier = cashier;
            ExecuteTime = executeTime + clock;
        }

        public override void Execute()
        {
            Cashier.Customer = null;
            Console.WriteLine("Zakończenie pobytu w restauracji");
        }
    }
}
