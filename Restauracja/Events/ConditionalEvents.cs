using System;

namespace Restauracja
{
    public class ConditionalEvents: Param
    {
        public static bool ExecuteConditionalEvents()
        {
            return (false || GroupAssignmentToTables() || AllocationToBuffet() || WaiterAssignment() || CashierAssignment());
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
                    Menager.Customer.Seats = Table.NumberOfSeats;
                    var Obj = new MenagerExecute(40, Customer, Table);
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
            foreach (var BuffetObj in Buffet)
            {
                if (BuffetObj.Customer != null) continue;
                BuffetObj.Customer = QueueBuffet.Dequeue();
                var Obj = new BuffetExecute(new Time().GaussianDistribution(2900, 80), BuffetObj);
                EventList.Add(Obj);
                Console.WriteLine("Przydzielenie grupy do bufetu");
                return true;
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
                        ? new WaiterExecute(new Time().ExponentialDistribution(370), Waiter)
                        : new WaiterExecute(new Time().ExponentialDistribution(2000), Waiter);
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
                var Obj = new CashierExecute(new Time().ExponentialDistribution(220), Cashier);
                EventList.Add(Obj);
                return true;
            }
            return false;
        }
    }
}
