using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PredatorEatPersonSystem : IEcsRunSystem
    {
        private Configs _configs;
        private EcsFilter<CollusionComponent> _collusions;

        public void Run()
        {
            foreach (var collusion in _collusions)
            {
                var entity1 = _collusions.Get1(collusion).Entity1;
                var entity2 = _collusions.Get1(collusion).Entity2;
                if (entity2.Has<PersonFoodComponent>())
                {
                    if (entity1.Has<PredatorComponent>() && !entity2.Has<PredatorComponent>())
                    {
                        entity1.Get<PersonFoodComponent>().FoodAmount += entity2.Get<FoodComponent>().NutritionalValue;
                        if (entity2.Has<PoisonousComponent>())
                        {
                            bool roll = Random.Range(0f, 1f) < entity1.Get<PoisonousComponent>().Toxicity;
                            if(roll)entity1.Replace(new DestroyedComponent());
                        }
                        entity2.Replace(new DestroyedComponent());
                    }
                }
            }
        }
    }
}