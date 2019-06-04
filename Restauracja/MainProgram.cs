using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


namespace Restauracja
{
    internal class Restauracja
    {
        public static void Main()
        {
            /////////////Parametry symulacji/////////////
            const int numberOfSimulations = 1;
            const int numberOfCustomerReceptions = 4000;
            const bool stepMode = false;
            const bool toExcel = true;
            const bool includeInitialPhase = true;
            /////////////////////////////////////////////
            Param.Initialization();
            int y = 0;
            for (var i = 0; i < numberOfSimulations; i++)
            {
                 var NewGroupArrival = new NewGroup(0, Param.QueueTable, Param.QueueBuffet, Param.EventList, Param.Clock,
                   new Customer(ConditionalEvents.Mersenne.Random(), ConditionalEvents.Mersenne.Random()));              //A
                Param.EventList.Add(NewGroupArrival);
                int[] QueueToTable = new int[100 * numberOfCustomerReceptions];
                int[] QueueToCashier = new int[100 * numberOfCustomerReceptions];
                var k = 1;
                var Excel = new Excel(AppDomain.CurrentDomain.BaseDirectory + "sym6.xlsx", 1);
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

                    
                    if (!includeInitialPhase && Param.NumberOfGroups<1100) continue;                //wyznaczone doświadczalnie
                    //wyświetlanie////////////////////////////////////
                    Console.WriteLine();
                    QueueToTable[k] = Param.QueueTable.Count;
                    QueueToCashier[k] = Param.QueueCashier.Count;
                    Param.ShowCustomersInQueues();
                    Param.ShowObjInLists();
                    Console.WriteLine("\nLiczba eventow czasowych: " + Param.EventList.Count);
                    foreach (var Event in Param.EventList)
                    {
                        Console.Write("|" + Event.ExecuteTime + "| ");
                    }

                    Console.WriteLine();
                    Console.WriteLine("____________________________________________________________________");
                    if (stepMode)
                        Console.ReadLine();
                    if (toExcel && Param.NumberOfGroups % 10 == 0)
                    {
                        Excel.WriteToCell(k, 1, Param.QueueTable.Count.ToString());
                        Excel.WriteToCell(k, 2, Param.QueueBuffet.Count.ToString());
                        Excel.WriteToCell(k, 3, Param.QueueWaiter.Count.ToString());
                        Excel.WriteToCell(k++, 4, Param.QueueCashier.Count.ToString());
                    }

                    
                }
                QueueToTable[k] = -1;
                QueueToCashier[k] = -1;

                
                Excel.WriteToCell(++y, 6, AverageWaitingTimeForATable(numberOfCustomerReceptions).ToString());
                Excel.WriteToCell(y, 7, AverageQueueLengthWaitingForATable(QueueToTable).ToString());
                Excel.WriteToCell(y, 8, AverageWaitTimeForWaiterService(numberOfCustomerReceptions).ToString());
                Excel.WriteToCell(y, 9, AverageQueueLengthToCashier(QueueToCashier).ToString());
                /*
                Console.WriteLine("Sredni czas oczekiwania na stolik: " + AverageWaitingTimeForATable(numberOfCustomerReceptions));

                Console.WriteLine("Średnia długość kolejki do stolików: "+ AverageQueueLengthWaitingForATable(QueueToTable));
            
                Console.WriteLine("Średi czas oczekiwania na obsługę przez kelnera:  "+AverageWaitTimeForWaiterService(numberOfCustomerReceptions));
                
                Console.WriteLine("Średnia długość kolejki do kasjerów: " + AverageQueueLengthToCashier(QueueToCashier));
                */
                //Excel.Close();
                //Console.ReadLine();
                Param.Clear();
            }
        }



        //funkcje zwracające wyniki symulacji

        private static double AverageWaitingTimeForATable(int numberOfCustomerReceptions)
        {
            int[] Tab = new int[10*numberOfCustomerReceptions];
            int h = 0;
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
                            Tab[h++] = Param.PastEventList[l].StartTime - Param.PastEventList[j].ExecuteTime;
                            break;

                        }
                    }
                }
                Tab[h] = -1;
            }

            while (Tab[n] != -1)
                Sum += Tab[n++];
            

            return (double)Sum / n;
            
        }

        private static double AverageQueueLengthWaitingForATable(IReadOnlyList<int> queueToTable)
        {
            int n = 0;
            int Sum = 0;
            while (queueToTable[n] != -1)
                Sum += queueToTable[n++];

            return (double) Sum / n;
        }

        private static double AverageWaitTimeForWaiterService(int numberOfCustomerReceptions)
        {
            int[] Tab1 = new int[10 * numberOfCustomerReceptions];
            int h = 0;
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
                            Tab1[h++] = Param.PastEventList[l].StartTime - Param.PastEventList[j].ExecuteTime;
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
                            Tab1[h++] = Param.PastEventList[j].ExecuteTime - Param.PastEventList[l].StartTime;
                            Param.PastEventList.RemoveAt(l); Param.PastEventList.RemoveAt(j);
                            break;

                        }
                    }
                }
                Tab1[h] = -1;
            }
            while (Tab1[n] != -1)
                Sum += Tab1[n++];

            return (double) Sum / n;
        }

        private static double AverageQueueLengthToCashier(IReadOnlyList<int> queueToCashier)
        {
            int n = 0;
            int Sum = 0;
            while (queueToCashier[n] != -1)
                Sum += queueToCashier[n++];

            return (double) Sum / n;
        }
    }
}