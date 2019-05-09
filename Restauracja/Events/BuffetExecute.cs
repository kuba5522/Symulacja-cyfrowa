using System;
using System.Collections.Generic;
using System.Linq;

namespace Restauracja
{
    internal class BuffetExecute:Event
    {
        private readonly Buffet Buffet;
        private readonly Queue<Customer> QueueCashier;
        private readonly List<Buffet> Buffets;
        public BuffetExecute(int executeTime, Buffet buffet, Queue<Customer> queueCashier,List<Buffet> buffets, int clock) : base(clock, executeTime)
        {

            Buffet = buffet;
            QueueCashier = queueCashier;
            Buffets = buffets;
            ExecuteTime = executeTime + clock;
        }

        public override void Execute()
        {
            QueueCashier.Enqueue(Buffet.Customer);
            Buffets.Where(x => x.Customer == Buffet.Customer).ToList().ForEach(p => p.NumberOfBusyPlace = 0);
            Buffet.Customer = null;
            Console.WriteLine("Zaknończneinie pobytu w bufecie");
        }
    }
}
