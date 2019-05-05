using System;
using System.Linq;

namespace Restauracja
{
    internal class WaiterExecute:Event
    {
        private readonly Waiter Waiter;
        public WaiterExecute(int executeTime, Waiter waiter) : base(Param.Clock, executeTime)
        {
            Waiter = waiter;
            ExecuteTime = executeTime + Param.Clock;
        }

        public override void Execute()
        {
            if (Waiter.Customer.Meal)
            {
                var Obj = new MealExecute(new Time().ExponentialDistribution(2020), Waiter);
                Param.EventList.Add(Obj);
            }
            else
            {
                Waiter.Customer.Meal = true;
                Param.QueueWaiter.Enqueue(Waiter.Customer);
                Waiter.Customer = null;
            }
            Console.WriteLine("Zakonczenie obsługi kelnera");
        }
    }
}
