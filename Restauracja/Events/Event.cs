using System;

namespace Restauracja
{
    public abstract class Event
    {
        public int StartTime { get; set; }
        public int ExecuteTime { get; set; }
        public Guid Id { get; set; }

        protected Event(int startTime, int executeTime, Guid id)
        {
            StartTime = startTime;
            ExecuteTime = executeTime;
            Id = id;
        }

        protected Event(int startTime, int executeTime)
        {
            StartTime = startTime;
            ExecuteTime = executeTime;
        }

        public abstract void Execute();

        public virtual void Executing()
        {
            Execute();
            if (Param.includeInitialPhase)
                    Param.PastEventList.Add(this);
            else
                if (Param.NumberOfGroups > 1400)
                    Param.PastEventList.Add(this);
        }
    }
}
