using System;
using System.Collections.Generic;

namespace Restauracja
{
    internal class MealExecute:Event
    {
        private readonly Customer Customer;
        private readonly Queue<Customer> QueueCashier;
        private readonly List<Table> Tables;
        public MealExecute(int executeTime, Customer customer, Queue<Customer> queueCashier, List<Table> tables, int clock) : base(clock, executeTime)
        {
            QueueCashier = queueCashier;
            Tables = tables;
            Customer = customer;
            ExecuteTime = executeTime + clock;
        }

        public override void Execute()
        {
            QueueCashier.Enqueue(Customer);
            foreach (var Table in Tables)
            {
                if (Table.Customer != null && Table.Customer.Id == Customer.Id)
                {
                    Table.Customer = null;
                    break;
                }
            }
            Console.WriteLine("Zakończenie spozyswania posiłku, zwolnienie stolika");
        }
    }
}
