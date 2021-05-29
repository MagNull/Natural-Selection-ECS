﻿using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PersonEatFoodSystem : IEcsRunSystem
    {
        private EcsFilter<CollusionComponent> _filter;
        private EcsWorld _world;
        
        public void Run()
        {
            foreach (var e in _filter)
            {
                EcsEntity foodEntity;
                EcsEntity personEntity;
                
                
                if (_filter.Get1(e).Entity1.Has<PersonFoodComponent>()
                    && !_filter.Get1(e).Entity1.Has<PredatorComponent>()
                    && _filter.Get1(e).Entity2.Has<FoodComponent>()
                    && !_filter.Get1(e).Entity2.Has<PersonFoodComponent>())
                {
                    personEntity = _filter.Get1(e).Entity1;
                    foodEntity = _filter.Get1(e).Entity2;
                }
                else continue;

                personEntity.Get<PersonFoodComponent>().FoodAmount +=
                    foodEntity.Get<FoodComponent>().NutritionalValue;
                foodEntity.Replace(new DestroyedComponent());
            }
        }
    }
}