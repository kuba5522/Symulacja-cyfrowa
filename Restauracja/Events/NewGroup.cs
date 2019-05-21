using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;

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
            Param.NumberOfGroups++;
            if (Customer.Choice)
                QueueTable.Add(Customer);
            else
                QueueBuffet.Enqueue(Customer);
            Console.WriteLine("Zaplanowanie pojawienia się następnej grupy");
            var Group = new NewGroup(new Time().GaussianDistribution(220, 10), QueueTable, QueueBuffet, Events, Param.Clock);
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
