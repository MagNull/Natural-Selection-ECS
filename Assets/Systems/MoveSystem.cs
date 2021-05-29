using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Systems
{
    public class MoveSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent> _filter;

        public void Run()
        {
            foreach (var e in _filter)
            {
                _filter.Get1(e).Direction.y = 0;
                var moveComponent = _filter.Get1(e);
                moveComponent.Rigidbody.velocity = moveComponent.Direction * moveComponent.Speed;
            }
        }
    }
}