using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restauracja
{
    public class MenagerExecute:Event
    {
        private readonly Customer Customer;
        private readonly Table Table;
        public MenagerExecute(int executeTime, Customer customer, Table table) : base(Param.Clock, executeTime)
        {
            Table = table;
            ExecuteTime = executeTime+Param.Clock;
            Customer = customer;
        }

        public override void Execute()
        {
            Table.Customer = Customer;
            Param.QueueWaiter.Enqueue(Customer);
            Param.Menager.Customer = null;
            Console.WriteLine("Zakonczenie prowadzenia przez menagera");
        }
    }
}
