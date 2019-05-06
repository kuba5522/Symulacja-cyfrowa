using System;
using System.Collections.Generic;

namespace Restauracja
{
    public class NewGroup : Event
    {
        public override void Execute()
        {
            var Customer = new Customer();
            if (Customer.Choice)
                Param.QueueTable.Add(Customer);
            else
                Param.QueueBuffet.Enqueue(Customer);
            Console.WriteLine("Zaplanowanie pojawienia się następnej grupy");
            var Group = new NewGroup(new Time().GaussianDistribution(1500, 200));
            Param.EventList.Add(Group);

        }
        public NewGroup(int executeTime) : base(Param.Clock, executeTime)
        {

            ExecuteTime = executeTime+Param.Clock;
        }

    }
}
