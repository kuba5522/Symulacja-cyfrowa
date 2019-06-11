using System;
using System.Globalization;
using System.Linq;


namespace Restauracja
{
    internal class Restauracja
    {
        public static void Main()
        {
            /////////////Simulation parameters/////////////
            const int numberOfSimulations = 1;
            const int numberOfCustomerReceptions = 5000;
            const bool stepMode = false;
            const bool toExcel = false;
            Param.includeInitialPhase = false;
            /////////////////////////////////////////////
            Param.Initialization();
            var y = 0;
            for (var i = 0; i < numberOfSimulations; i++)
            {
                var NewGroupArrival = new NewGroup(0, Param.QueueTable, Param.QueueBuffet, Param.EventList, Param.Clock, new Customer(ConditionalEvents.Mersenne.Random(), ConditionalEvents.Mersenne.Random()));
                Param.EventList.Add(NewGroupArrival);
                var Sum1 = 0;                                                                                                                   //A, initiation by adding the first event
                var Sum2 = 0;
                var n = 0;
                var k = 1;
                var Excel = new Excel(AppDomain.CurrentDomain.BaseDirectory + "test.xlsx", i+1);        //FILE WITH THIS NAME AND MAX NUMBERS OF SHEETS MUST EXIST BEFORE THE SIMULATION STARTS IN 
                                                                                                                  //../Restauracja\bin\Debug     FOLDER

                while (Param.NumberOfGroups <= numberOfCustomerReceptions)                  //Main simulation loop  
                {
                    Param.Clock = Param.EventList.Min(r => r.ExecuteTime);                                                                       //B, execution of the earliest event/events
                    Param.EventList.Where(time => time.ExecuteTime.Equals(Param.Clock)).ToList()
                        .ForEach(obj => obj.Executing());
                    Param.EventList.RemoveAll(time => time.ExecuteTime == Param.Clock);

                    bool X;
                    do
                    {
                        X = ConditionalEvents.ExecuteConditionalEvents();                                                                     //C, execution of all ConditionalEvents
                    } while (X);

                    
                    if (!Param.includeInitialPhase && Param.NumberOfGroups<1400) continue;                //determined experimentally
                    /////////////////displaying/////////////////
                    Console.WriteLine();
                    Sum1 += Param.QueueTable.Count;
                    Sum2 += Param.QueueCashier.Count;
                    n++;
                    Param.ShowCustomersInQueues();
                    Param.ShowObjInLists();
                    Console.WriteLine("\nLiczba eventow czasowych: " + Param.EventList.Count);
                    foreach (var Event in Param.EventList)
                        Console.Write("|" + Event.ExecuteTime + "| ");
                    Console.WriteLine();
                    Console.WriteLine("____________________________________________________________________");
                    if (stepMode)
                        Console.ReadLine();
                    if (toExcel && Param.NumberOfGroups % 20 == 0)
                    {
                        Excel.WriteToCell(k, 1, Param.QueueTable.Count.ToString());
                        Excel.WriteToCell(k, 2, Param.QueueBuffet.Count.ToString());
                        Excel.WriteToCell(k, 3, Param.QueueWaiter.Count.ToString());
                        Excel.WriteToCell(k++, 4, Param.QueueCashier.Count.ToString());
                    }
                }

                var Result1 = AverageWaitingTimeForATable();
                var Result2 = AverageQueueLength(Sum1, n);
                var Result3 = AverageWaitTimeForWaiterServices();
                var Result4 = AverageQueueLength(Sum2, n);

                if (toExcel)
                {
                    Excel.WriteToCell(++y, 6, Result1.ToString(CultureInfo.InvariantCulture));
                    Excel.WriteToCell(y, 7, Result2.ToString(CultureInfo.InvariantCulture));
                    Excel.WriteToCell(y, 8, Result3.ToString(CultureInfo.InvariantCulture));
                    Excel.WriteToCell(y, 9, Result4.ToString(CultureInfo.InvariantCulture));
                }

                Console.WriteLine("Sredni czas oczekiwania na stolik: " + Result1);

                Console.WriteLine("Średnia długość kolejki do stolików: " + Result2);
            
                Console.WriteLine("Średi czas oczekiwania na obsługę przez kelnera:  " + Result3);
                
                Console.WriteLine("Średnia długość kolejki do kasjerów: " + Result4);
                

                Excel.Close();
                //Console.ReadLine();
                Param.Clear();
            }
        }



        //funkcje zwracające wyniki symulacji

        private static double AverageWaitingTimeForATable()
        {
            int Sum = 0;
            int n = 0;
            for (int j = 0; j < Param.PastEventList.Count; j++)
            {
                if (Param.PastEventList[j].GetType() == typeof(NewGroup))
                {
                    for (int l = j; l < Param.PastEventList.Count; l++)
                    {
                        if (Param.PastEventList[l].Id == Param.PastEventList[j].Id && Param.PastEventList[l].GetType() == typeof(ManagerExecute))
                        {
                            Sum += Param.PastEventList[l].StartTime - Param.PastEventList[j].ExecuteTime;
                            n++;
                            break;

                        }
                    }
                }
                
            }
            return (double)Sum / n;
        }

        private static double AverageQueueLength(int sum, int n)
        {
            return (double) sum / n;
        }

        private static double AverageWaitTimeForWaiterServices()
        {
            int Sum = 0;
            int n = 0;
            for (int j = 0; j < Param.PastEventList.Count; j++)
            {
                if (Param.PastEventList[j].GetType() == typeof(ManagerExecute))
                {
                    for (int l = j; l < Param.PastEventList.Count; l++)
                    {
                        if (Param.PastEventList[l].Id == Param.PastEventList[j].Id && Param.PastEventList[l].GetType() == typeof(WaiterExecute))
                        {
                            Sum += Param.PastEventList[l].StartTime - Param.PastEventList[j].ExecuteTime;
                            n++;
                            break;

                        }
                    }
                }
                if (Param.PastEventList[j].GetType() == typeof(WaiterExecute))
                {
                    for (int l = j; l < Param.PastEventList.Count; l++)
                    {
                        if (Param.PastEventList[l].Id == Param.PastEventList[j].Id && Param.PastEventList[l].GetType() == typeof(WaiterExecute))
                        {
                            Sum += Param.PastEventList[j].ExecuteTime - Param.PastEventList[l].StartTime;
                            n++;
                            break;

                        }
                    }
                }
                
            }
            return (double) Sum / n;
        }
    }
}