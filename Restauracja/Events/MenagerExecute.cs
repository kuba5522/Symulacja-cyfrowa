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
        public MenagerExecute(int executeTime, Customer customer) : base(Param.Clock, executeTime)
        {
            ExecuteTime = executeTime+Param.Clock;
            Customer = customer;
        }

        public override void Execute()
        {
            Param.Menager.Free = true;
            Param.QueueWaiter.Enqueue(Customer);
            Param.Menager.Customer = null;
            Console.WriteLine("Zakonczenie prowadzenia przez menagera");
        }
    }
}
