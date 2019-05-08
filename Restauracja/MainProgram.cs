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
            var NewGroupArrival = new NewGroup(0,Param.QueueTable, Param.QueueBuffet, Param.EventList, Param.Clock);              //A
            Param.EventList.Add(NewGroupArrival);
            Console.WriteLine("Tryb pracy krokowy(1) czy ciągły(2)? ");
            var StepMode = int.Parse(Console.ReadLine()) == 1;
            while (Param.Clock < 10000000)
            {
                Param.Clock = Param.EventList.Min(r => r.ExecuteTime);
                Console.WriteLine("Clock: "+Param.Clock);                         //B
                Param.EventList.Where(time => time.ExecuteTime.Equals(Param.Clock)).ToList().ForEach(obj => obj.Executing());
                Param.EventList.RemoveAll(time => time.ExecuteTime == Param.Clock);
                do
                {
                    x = ConditionalEvents.ExecuteConditionalEvents();                   //C
                } while (x);
                //wyświetlanie
                Console.WriteLine();
                Param.ShowCustomersInQueues();
                Param.ShowObjInLists();
                Console.WriteLine("\nLiczba evbentow czasowych: " + Param.EventList.Count);
                foreach (var Event in Param.EventList)
                {
                    Console.Write("|"+Event.ExecuteTime + "| ");            
                }
                Console.WriteLine();
                Console.WriteLine("____________________________________________________________________");
                if (StepMode == true) Console.ReadLine();
            }
        }
    }
}