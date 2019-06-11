using System;
using System.Collections.Generic;

namespace Restauracja
{
    
    internal class NewGroup : Event
    {
        private readonly Customer Customer;
        private readonly List<Customer> QueueTable;
        private readonly Queue<Customer> QueueBuffet;
        private readonly List<Event> Events;

        public override void Execute()
        {
            
            Param.NumberOfGroups++;
            if (Customer.Choice)
                QueueTable.Add(Customer);
            else
                QueueBuffet.Enqueue(Customer);
            
            Console.WriteLine("Zaplanowanie pojawienia się następnej grupy");
            var Group = new NewGroup(new Time(ConditionalEvents.Mersenne.Random()).GaussianDistribution(205, 10), QueueTable, QueueBuffet, Events, Param.Clock,
                new Customer(ConditionalEvents.Mersenne.Random(), ConditionalEvents.Mersenne.Random()));
            Events.Add(Group);

        }
        public NewGroup(int executeTime, List<Customer> queueTable, Queue<Customer> queueBuffet, List<Event> events, int clock, Customer customer) : base(clock, executeTime, customer.Id)
        {
            Customer = customer;
            QueueTable = queueTable;
            QueueBuffet = queueBuffet;
            Events = events;
            ExecuteTime = executeTime+clock;
        }

    }
}
