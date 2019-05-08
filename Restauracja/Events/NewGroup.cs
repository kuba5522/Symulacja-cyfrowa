using System;
using System.Collections.Generic;

namespace Restauracja
{
    internal class NewGroup : Event
    {
        private Customer Customer;
        private readonly List<Customer> QueueTable;
        private readonly Queue<Customer> QueueBuffet;
        private readonly List<Event> Events;

        public override void Execute()
        {
            Customer = new Customer();
            if (Customer.Choice)
                QueueTable.Add(Customer);
            else
                QueueBuffet.Enqueue(Customer);
            Console.WriteLine("Zaplanowanie pojawienia się następnej grupy");
            var Group = new NewGroup(new Time().GaussianDistribution(10000, 200), QueueTable, QueueBuffet, Events, Param.Clock);
            Events.Add(Group);

        }
        public NewGroup(int executeTime, List<Customer> queueTable, Queue<Customer> queueBuffet, List<Event> events, int clock) : base(clock, executeTime)
        {
            QueueTable = queueTable;
            QueueBuffet = queueBuffet;
            Events = events;
            ExecuteTime = executeTime+clock;
        }

    }
}
