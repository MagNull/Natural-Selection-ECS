using Components.Common_Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Common_Systems
{
    public class TimerSystem : IEcsRunSystem
    {
        private EcsFilter<TimerComponent> _filter;
        
        public void Run()
        {
            foreach (var e in _filter)
            {
                ref var timer = ref _filter.Get1(e);
                if (timer.Tick <= 0)
                {
                    timer.Action.Invoke();
                    if (timer.Repeatable)
                    {
                        timer.Tick = timer.StartTime;
                    }
                    else
                    {
                        _filter.GetEntity(e).Destroy();
                    }
                }
                else timer.Tick -= Time.deltaTime;
            }
        }
    }
}