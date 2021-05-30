using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class AgingSystem : IEcsInitSystem
    {
        private Configs _configs;
        private EcsWorld _world;

        private EcsFilter<PersonFoodComponent> _filter;
        
        public void Init()
        {
            _world.NewEntity().Replace(new TimerComponent
            {
                StartTime = _configs.DieTickRate,
                Tick = _configs.DieTickRate,
                Action = DieTick,
                Repeatable = true
            });
        }

        private void DieTick()
        {
            foreach (var i in _filter)
            {
                float fromSpeed = _filter.GetEntity(i).Get<MoveComponent>().Speed * _configs.FoodSpeedCoefficient;
                // float fromSize =
                //     Mathf.Pow(_filter.GetEntity(i).Get<ViewComponent>().View.transform.localScale.y,2) * _configs.FoodSizeCoefficient 
                //                   - _configs.FoodSizeCoefficient + 1;
                float fromPoisonous = _filter.GetEntity(i).Has<PoisonousComponent>() ? 
                    _configs.FoodPerPoisonous * _filter.GetEntity(i).Get<PoisonousComponent>().Toxicity : 0;
                float fromPredatory = 
                    _filter.GetEntity(i).Has<PredatorComponent>() ? _filter.GetEntity(i).Get<PredatorComponent>().Rapacity : 0;
                _filter.Get1(i).FoodAmount -= fromPredatory + fromSpeed + fromPoisonous;

                if (_filter.Get1(i).FoodAmount <= 0) _filter.GetEntity(i).Replace(new DestroyedComponent());
            }
        }
    }
}