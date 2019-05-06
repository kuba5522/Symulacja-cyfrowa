using System;
using System.Linq;

namespace Restauracja
{
    internal class MealExecute:Event
    {
        private readonly Waiter Waiter;
        public MealExecute(int executeTime, Waiter waiter) : base(Param.Clock, executeTime)
        {
            Waiter = waiter;
            ExecuteTime = executeTime + Param.Clock;
        }

        public override void Execute()
        {
            Param.QueueCashier.Enqueue(Waiter.Customer);
            foreach (var Table in Param.Tables)
            {
                if (Table.Customer != null && Table.NumberOfSeats == Waiter.Customer.Seats)
                {
                    Table.Customer = null;
                    break;
                }
            }
            Waiter.Customer = null;
            Console.WriteLine("Zakończenie spozyswania posiłku");
        }
    }
}
