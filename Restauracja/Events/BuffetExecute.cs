using System;

namespace Restauracja
{
    internal class BuffetExecute:Event
    {
        private readonly Buffet Buffet;
        public BuffetExecute(int executeTime, Buffet buffet) : base(Param.Clock, executeTime)
        {
            Buffet = buffet;
            ExecuteTime = executeTime + Param.Clock;
        }

        public override void Execute()
        {
            Param.QueueCashier.Enqueue(Buffet.Customer);
            Buffet.Customer = null;
            Console.WriteLine("Zaknończneinie pobytu w bufecie");
        }
    }
}
