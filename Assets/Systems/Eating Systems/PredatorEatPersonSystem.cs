using Components.Common_Components;
using Components.Food_Components;
using Components.Person_Components;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;

namespace Systems.Eating_Systems
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
                if (entity2.Has<MeatFoodComponent>() &&
                    entity1.Has<PredatorComponent>())
                {
                    if (!entity2.Has<PredatorComponent>() ||
                        entity1.Get<PredatorComponent>().PredatorExperience - 
                        entity2.Get<PredatorComponent>().PredatorExperience >= _configs.ExperienceDeltaToEat)
                    {
                        float foodValue = entity2.Get<FoodComponent>().NutritionalValue *
                                          entity1.Get<PredatorComponent>().Predatoriness;
                        entity1.Get<PersonFoodComponent>().FoodAmount += foodValue;
                        entity1.Get<FoodsAverage>().MeatAverage += foodValue;
                        
                        IncreasePredatorParams(entity1);
                        
                        if (entity2.Has<PoisonousComponent>())
                        {
                            bool roll = _configs.KillPoisonThreshold <= entity2.Get<PoisonousComponent>().Toxicity;
                            if(roll)entity1.Replace(new DestroyedComponent());
                        }
                        
                        entity2.Replace(new DestroyedComponent());
                    }
                }
            }
        }

        private void IncreasePredatorParams(EcsEntity predator)
        {
            predator.Get<PredatorComponent>().Predatoriness += 1 / _configs.KillsToBecameOnlyCarnivorous;
            predator.Get<PredatorComponent>().Predatoriness = Mathf.Clamp(predator.Get<PredatorComponent>().Predatoriness, .1f, 1);
            predator.Get<ViewComponent>().View.transform.GetChild(0).localScale =
                Vector3.one * predator.Get<PredatorComponent>().Predatoriness;
            predator.Get<PredatorComponent>().PredatorExperience++;
            predator.Get<PredatorComponent>().Rapacity += _configs.RapacityPerKill;
        }
    }
}