using System;
using System.Collections.Generic;

namespace Restauracja
{
    internal class BuffetExecute:Event
    {
        private readonly Buffet Buffet;
        private readonly Queue<Customer> QueueCashier;
        public BuffetExecute(int executeTime, Buffet buffet, Queue<Customer> queueCashier, int clock) : base(clock, executeTime)
        {
            Buffet = buffet;
            QueueCashier = queueCashier;
            ExecuteTime = executeTime + clock;
        }

        public override void Execute()
        {
            QueueCashier.Enqueue(Buffet.Customer);
            Buffet.Customer = null;
            Console.WriteLine("Zaknończneinie pobytu w bufecie");
        }
    }
}
