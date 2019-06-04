using System;

namespace Restauracja
{
    
    public class ConditionalEvents: Param
    {
        public static readonly Mersenne Mersenne = new Mersenne(1235);
        public static bool ExecuteConditionalEvents()
        {
            return ( GroupAssignmentToTables() || AllocationToBuffet() || WaiterAssignment() || CashierAssignment());
        }

        private static bool GroupAssignmentToTables()
        {
            if (Menager.Customer!=null) return false;
            foreach (var Customer in QueueTable)
            {
                foreach (var Table in Tables)
                {
                    if ((Table.Customer!=null) || (Table.NumberOfSeats < Customer.GroupSize)) continue;
                    Table.Customer = Customer;
                    Menager.Customer = Customer;
                    var Obj = new ManagerExecute(50, Customer, QueueWaiter, Menager, Clock, Customer.Id);
                    EventList.Add(Obj);
                    QueueTable.RemoveAll(x => x.Id == Customer.Id);
                    Console.WriteLine("Przydzielenie grupy do stolika");
                    return true;
                }
            }
            return false;
        }

        private static bool AllocationToBuffet()
        {
            if (QueueBuffet.Count == 0) return false;
            int n = 0;
            foreach (var Buffet1 in Buffet)
                if (Buffet1.NumberOfBusyPlace > 0)
                    n += Buffet1.NumberOfBusyPlace;
            foreach (var BuffetObj in Buffet)
            {
                if (BuffetObj.Customer == null && QueueBuffet.Peek().GroupSize+n <= 20)
                {
                    BuffetObj.Customer = QueueBuffet.Dequeue();
                    BuffetObj.NumberOfBusyPlace = BuffetObj.Customer.GroupSize;
                    var Obj = new BuffetExecute(new Time(Mersenne.Random(), Mersenne.Random()).GaussianDistribution(2900, 50), BuffetObj, QueueCashier, Buffet,
                        Clock);
                    EventList.Add(Obj);
                    Console.WriteLine("Przydzielenie grupy do bufetu");
                    return true;
                }
            }
            return false;
        }


        private static bool WaiterAssignment()
        {
            if (QueueWaiter.Count == 0) return false;
            foreach (var Waiter in Waiters)
            {
                if (Waiter.Customer == null)
                {
                    Waiter.Customer = QueueWaiter.Dequeue();
                    Console.WriteLine("Przydzielenie grupy do kelnera");
                    var Obj = Waiter.Customer.Meal == false
                        ? new WaiterExecute(new Time(Mersenne.Random()).ExponentialDistribution(270), Waiter, QueueWaiter, QueueCashier, Tables, EventList, Clock, Waiter.Customer.Id)
                        : new WaiterExecute(new Time(Mersenne.Random()).ExponentialDistribution(1780), Waiter, QueueWaiter, QueueCashier, Tables, EventList, Clock, Waiter.Customer.Id);
                    EventList.Add(Obj);
                    return true;
                }
            }
            return false;
        }

        private static bool CashierAssignment()
        {
            if (QueueCashier.Count == 0) return false;
            foreach (var Cashier in Cashiers)
            {
                if (Cashier.Customer != null) continue;
                Console.WriteLine("Przydzielenie grupy do kasjera");
                Cashier.Customer = QueueCashier.Dequeue();
                var Obj = new CashierExecute(new Time(Mersenne.Random()).ExponentialDistribution(1000), Cashier, Clock);
                EventList.Add(Obj);
                return true;
            }
            return false;
        }
    }
}
