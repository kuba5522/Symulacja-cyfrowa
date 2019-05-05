using System;

namespace Restauracja
{
    internal class CashierExecute:Event
    {
        private Cashier Cashier;
        public CashierExecute(int executeTime, Cashier cashier) : base(Param.Clock, executeTime)
        {
            Cashier = cashier;
            ExecuteTime = executeTime + Param.Clock;
        }

        public override void Execute()
        {
            Cashier.Customer = null;
            Console.WriteLine("Zakończenie pobytu w restauracji");
        }
    }
}
