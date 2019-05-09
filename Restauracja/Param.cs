using System;
using System.Collections.Generic;
using System.Linq;

namespace Restauracja
{
    public class Param
    {
        public static int Clock = 0;
        private const int N2 = 8;
        private const int N3 = 14;
        private const int N4 = 4;
        private const int NumberOfTables = N2 + N3 + N4;
        private const int CapacityBuffet = 20;
        private const int NumberOfCashiers = 6;
        private const int NumberOfWaiters = 6;
        public static int NumberOfGroups { get; set; }

        public static Manager Menager { get; set; } = new Manager();

        public static List<Waiter> Waiters { get; set; } = new List<Waiter>();
        public static List<Table> Tables { get; set; } = new List<Table>();
        public static List<Cashier> Cashiers { get; set; } = new List<Cashier>();
        public static List<Buffet> Buffet { get; set; } = new List<Buffet>();

        public static List<Event> EventList = new List<Event>();
        public static List<Event> PastEventList = new List<Event>();


        public static List<Customer> QueueTable { get; set; } = new List<Customer>();
        public static Queue<Customer> QueueWaiter { get; set; } = new Queue<Customer>();
        public static Queue<Customer> QueueBuffet { get; set; } = new Queue<Customer>();
        public static Queue<Customer> QueueCashier { get; set; } = new Queue<Customer>();


        public static void Initialization()
        {
            AddToList(Waiters, NumberOfWaiters);
            AddToList(Tables, NumberOfTables);
            AddToList(Cashiers, NumberOfCashiers);
            AddToList(Buffet, CapacityBuffet);
            AllocationOfSeatsToTables(Tables, N2, N3, N4);
            NumberOfGroups = 0;
        }

        private static void AddToList<T>(ICollection<T> objects, int number=1) where T : new()
        {
            for (int I = 0; I < number; I++)
                objects.Add(new T());
        }

        private static void AllocationOfSeatsToTables(IReadOnlyList<Table> tables, int n2, int n3, int n4)
        {

            for (var I = 0; I < n2; I++)
                tables[I].NumberOfSeats = 2;
            for (var I = n2; I < n3+n2; I++)
                tables[I].NumberOfSeats = 3;
            for (var I = n2+n3; I < n2+n3+n4; I++)
                tables[I].NumberOfSeats = 4;
        }
        public static void ShowCustomersInQueues()
        {
            Console.WriteLine("Queue to Table "+ QueueTable.Count + " | Queue to Buffet " + QueueBuffet.Count + " | Queue to Waiter " + QueueWaiter.Count + " | Queue to Cashier " + QueueCashier.Count + " |");       
        }

        public static void ShowObjInLists()
        {
            int x = 0;
            int y = 0;
            int z = 0;
            int n = 0;
            int m = 0;
            int p = 0;
            foreach (var T in Tables)
            {
                if (T.Customer != null)
                {
                    if (T.NumberOfSeats == 2)
                        x++;
                    else if (T.NumberOfSeats == 3)
                        y++;
                    else
                        z++;
                }
            }
            foreach(var T in Buffet)
            {
                if (T.NumberOfBusyPlace > 0)
                    n+=T.NumberOfBusyPlace;
            }
            foreach (var T in Waiters)
            {
                if (T.Customer != null)
                    m++;
            }
            foreach (var T in Cashiers)
            {
                if (T.Customer != null)
                    p++;
            }
            Console.WriteLine("Ilośc miejsc w stolikach: 2os:"+x+"/"+N2+" 3os:"+y+"/"+N3+" 4os:"+z+"/"+N4);
            Console.WriteLine("Ilośc osób w bufecie "+ n+"/"+CapacityBuffet);
            Console.WriteLine("Ilośc zajetych kelnerow:"+m+"/"+NumberOfWaiters);
            Console.WriteLine("Ilośc zajetych kelnerow:" + p + "/" + NumberOfCashiers);
        }
    }
}
