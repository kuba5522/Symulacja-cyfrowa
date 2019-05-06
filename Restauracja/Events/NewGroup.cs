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
            var Group = new NewGroup(new Time().GaussianDistribution(200, 100));
            Param.EventList.Add(Group);

        }
        public NewGroup(int executeTime=0) : base(Param.Clock, executeTime)
        {

            ExecuteTime = executeTime+Param.Clock;
        }

    }
}
