using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Components
{
    public class LookAtMovementSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, ViewComponent> _filter;
        
        public void Run()
        {
            foreach (var e in _filter)
            {
                _filter.Get2(e).View.transform.rotation = Quaternion.LookRotation(_filter.Get1(e).Direction);
            }
        }
    }
}