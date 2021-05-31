using System;

namespace Components.Common_Components
{
    public struct TimerComponent
    {
        public float Tick;
        public float StartTime;
        public Action Action;
        public bool Repeatable;
    }
}