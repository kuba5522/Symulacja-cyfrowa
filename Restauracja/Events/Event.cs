using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Restauracja
{
    public abstract class Event
    {
        public int StartTime { get; set; }
        public int ExecuteTime { get; set; }

        protected Event(int startTime, int executeTime)
        {
            StartTime = startTime;
            ExecuteTime = executeTime;
        }
        public abstract void Execute();

        public virtual void Executing()
        {
            Execute();
            Console.WriteLine("Current time: "+StartTime);
            //tu coś jeszcze będzie
        }
    }
}
