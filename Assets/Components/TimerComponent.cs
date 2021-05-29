using System;

namespace Components
{
    public struct TimerComponent
    {
        public float Tick;
        public float StartTime;
        public Action Action;
        public bool Repeatable;
    }
}