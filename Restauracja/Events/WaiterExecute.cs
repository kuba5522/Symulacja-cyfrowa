using System;
using System.Collections.Generic;
using System.Linq;

namespace Restauracja
{
    internal class WaiterExecute:Event
    {
        private readonly Waiter Waiter;
        private readonly Queue<Customer> QueueWaiter;
        private readonly List<Event> Events;
        private readonly Queue<Customer> QueueCashier; 
        private readonly List<Table> Tables;

        public WaiterExecute(int executeTime, Waiter waiter, Queue<Customer> queueWaiter, Queue<Customer> queueCashier, List<Table> tables, List<Event> events, int clock) : base(clock, executeTime)
        {
            Waiter = waiter;
            QueueWaiter = queueWaiter;
            Events = events;
            Tables = tables;
            QueueCashier = queueCashier;
            ExecuteTime = executeTime + clock;
        }

        public override void Execute()
        {
            if (Waiter.Customer.Meal)
            {
                var Obj = new MealExecute(new Time().ExponentialDistribution(2020), Waiter, QueueCashier, Tables, Param.Clock );
                Events.Add(Obj);
            }
            else
            {
                Waiter.Customer.Meal = true;
                QueueWaiter.Enqueue(Waiter.Customer);
                Waiter.Customer = null;

            }
            Console.WriteLine("Zakonczenie obsługi kelnera");
        }
    }
}
