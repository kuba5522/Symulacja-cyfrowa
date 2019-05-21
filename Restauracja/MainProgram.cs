using System;
using System.Linq;


namespace Restauracja
{
    internal class Restauracja
    {
        public static void Main()
        {
            /////////////Parametry symulacji/////////////
            const int numberOfSimulations = 1;
            const int numberOfCustomerReceptions = 2000;
            const bool stepMode = false;
            const bool toExcel = false;
            const bool includeInitialPhase = true;
            /////////////////////////////////////////////
            var k = 1;
            Param.Initialization();
            var NewGroupArrival = new NewGroup(0,Param.QueueTable, Param.QueueBuffet, Param.EventList, Param.Clock);              //A
            Param.EventList.Add(NewGroupArrival);
            for (var i = 0; i < numberOfSimulations; i++)
            {
                var Excel = new Excel(AppDomain.CurrentDomain.BaseDirectory + "wynik.xlsx", i+1);
                while (Param.NumberOfGroups <= numberOfCustomerReceptions)
                {
                    Param.Clock = Param.EventList.Min(r => r.ExecuteTime);                                                                       //B
                    Param.EventList.Where(time => time.ExecuteTime.Equals(Param.Clock)).ToList()
                        .ForEach(obj => obj.Executing());
                    Param.EventList.RemoveAll(time => time.ExecuteTime == Param.Clock);
                    bool x;
                    do
                    {
                        x = ConditionalEvents.ExecuteConditionalEvents();                                                                     //C
                    } while (x);

                    if (!includeInitialPhase && numberOfCustomerReceptions<50) continue;                //wyznaczone doświadczalnie
                    //wyświetlanie////////////////////////////////////
                    Console.WriteLine();
                    Param.ShowCustomersInQueues();
                    Param.ShowObjInLists();
                    Console.WriteLine("\nLiczba evbentow czasowych: " + Param.EventList.Count);
                    foreach (var Event in Param.EventList)
                    {
                        Console.Write("|" + Event.ExecuteTime + "| ");
                    }

                    Console.WriteLine();
                    Console.WriteLine("____________________________________________________________________");
                    if (stepMode)
                        Console.ReadLine();
                    if (toExcel)
                        Excel.WriteToCell(k++, 1, Param.QueueBuffet.Count.ToString());
                }
                Excel.Close();
            }
        }
    }
}