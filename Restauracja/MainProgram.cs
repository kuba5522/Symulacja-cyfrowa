using System;
using System.Linq;


namespace Restauracja
{
    internal class Restauracja
    {
        public static void Main()
        {
            bool x;
            Param.Initialization();
            var NewGroupArrival = new NewGroup();              //A
            Param.EventList.Add(NewGroupArrival);
            Console.WriteLine("Tryb pracy krokowy(1) czy ciągły(2)? ");
            var StepMode = int.Parse(Console.ReadLine()) == 1;
            while (Param.Clock < 200000)
            {
                Param.Clock = Param.EventList.Min(r => r.ExecuteTime);
                Console.WriteLine("Clock: "+Param.Clock);                         //B
                Param.EventList.Where(time => time.ExecuteTime.Equals(Param.Clock)).ToList().ForEach(obj => obj.Executing());
                Param.EventList.RemoveAll(time => time.ExecuteTime == Param.Clock);
                do
                {
                    x = ConditionalEvents.ExecuteConditionalEvents();
                } while (x);
                Console.WriteLine();
                Param.ShowCustomersInQueues();
                Param.ShowObjInLists();
                Console.WriteLine("\nLiczba evbentow czasowych: " + Param.EventList.Count);
                foreach (var Event in Param.EventList)
                {
                    Console.Write("|"+Event.ExecuteTime + "| ");            //C
                }
                Console.WriteLine();
                Console.WriteLine("____________________________________________________________________");
                if (StepMode == true) Console.ReadLine();
            }
        }
    }
}