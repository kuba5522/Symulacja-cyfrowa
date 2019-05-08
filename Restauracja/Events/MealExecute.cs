using System;
using System.Collections.Generic;
using System.Linq;

namespace Restauracja
{
    internal class MealExecute:Event
    {
        private readonly Waiter Waiter;
        private readonly Queue<Customer> QueueCashier;
        private readonly List<Table> Tables;
        public MealExecute(int executeTime, Waiter waiter, Queue<Customer> queueCashier, List<Table> tables, int clock) : base(clock, executeTime)
        {
            Waiter = waiter;
            QueueCashier = queueCashier;
            Tables = tables;
            ExecuteTime = executeTime + clock;
        }

        public override void Execute()
        {
            QueueCashier.Enqueue(Waiter.Customer);
            foreach (var Table in Tables)
            {
                if (Table.Customer == null || Table.NumberOfSeats != Waiter.Customer.Seats) continue;
                Table.Customer = null;
                break;
            }
            Waiter.Customer = null;
            Console.WriteLine("Zakończenie spozyswania posiłku, zwolnienie stolika");
        }
    }
}
