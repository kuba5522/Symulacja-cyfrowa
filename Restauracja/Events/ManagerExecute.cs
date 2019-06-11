using System;
using System.Collections.Generic;


namespace Restauracja
{
    public class ManagerExecute:Event
    {
        private readonly Customer Customer;
        private readonly Queue<Customer> QueueWaiter;
        private readonly Manager Manager;

        public ManagerExecute(int executeTime, Customer customer, Queue<Customer> queueWaiter,
            Manager manager, int clock, Guid customerId) : base(clock, executeTime, customerId)
        {
            QueueWaiter = queueWaiter;
            Manager = manager;
            ExecuteTime = executeTime+clock;
            Customer = customer;
        }

        public override void Execute()
        {
            QueueWaiter.Enqueue(Customer);
            Manager.Customer = null;
            Console.WriteLine("Zakonczenie prowadzenia przez menagera");
        }
    }
}
