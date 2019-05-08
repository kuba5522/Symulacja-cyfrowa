using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restauracja
{
    public class ManagerExecute:Event
    {
        private readonly Customer Customer;
        private readonly Table Table;
        private readonly Queue<Customer> QueueWaiter;
        private readonly Manager Manager;

        public ManagerExecute(int executeTime, Customer customer, Table table, Queue<Customer> queueWaiter, Manager manager, int clock) : base(clock, executeTime)
        {
            Table = table;
            QueueWaiter = queueWaiter;
            Manager = manager;
            ExecuteTime = executeTime+clock;
            Customer = customer;
        }

        public override void Execute()
        {
            Table.Customer = Customer;
            QueueWaiter.Enqueue(Customer);
            Manager.Customer = null;
            Console.WriteLine("Zakonczenie prowadzenia przez menagera");
        }
    }
}
