using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class MutationSystem : IEcsRunSystem
    {
        private EcsFilter<BornComponent> _filter;
        private Configs _configs;
        
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                SpeedMutation(entity);
                //SizeMutation(entity);
                PredatorMutation(entity);
                PoisonousMutation(entity);
                entity.Del<BornComponent>();
            }
        }

        private void SpeedMutation(EcsEntity entity)
        {
            bool roll = Random.value < _configs.MutationChance / 100;
            if (roll)
            {
                float delta = (Random.value - Random.value);
                float bonusSpeed = delta * _configs.SpeedMutationFault;
                entity.Get<MoveComponent>().Speed += bonusSpeed;
                entity.Get<MoveComponent>().Speed = Mathf.Clamp(entity.Get<MoveComponent>().Speed, 0, float.MaxValue);
                Color newColor = entity.Get<ViewComponent>().View.GetComponent<MeshRenderer>().material.color;
                newColor.r += .05f * bonusSpeed;
                newColor.b -= .05f * bonusSpeed;
                entity.Get<ViewComponent>().View.GetComponent<MeshRenderer>().material.color = newColor;

            }
        }

        private void SizeMutation(EcsEntity entity)
        {
            bool roll = Random.value < (float)_configs.MutationChance / 100;
            if (roll)
            {
                float delta = (Random.value - Random.value);
                float bonusSize = delta * _configs.SizeMutationFault;
                Transform transform = entity.Get<ViewComponent>().View.transform;

                float speed = entity.Get<MoveComponent>().Speed;
                entity.Get<MoveComponent>().Speed += _configs.SizeSpeedCoefficient *
                                                     (speed * transform.localScale.y / (transform.localScale.y + bonusSize) 
                                                            - speed);
                
                transform.localScale += Vector3.one * bonusSize;
                transform.position = new Vector3(transform.position.x, transform.localScale.y, transform.position.z);
            }
        }

        private void PredatorMutation(EcsEntity entity)
        {
            bool roll = Random.value < _configs.PredatorMutationChance / 100;
            if (roll)
            {
                entity.Replace(new PredatorComponent
                {
                    Rapacity = 0,
                    Predatoriness = .1f,
                    PredatorExperience = 0
                });
                entity.Get<ViewComponent>().View.GetComponent<MeshRenderer>().material.color = Color.black;
                entity.Get<PersonFoodComponent>().FoodAmount = 2 + _configs.PredatoryFoodNeed;
            }
        }

        private void PoisonousMutation(EcsEntity entity)
        {
            bool roll = Random.value < _configs.MutationChance / 100;
            if (roll)
            {
                if (!entity.Has<PoisonousComponent>())
                {
                    entity.Replace(new PoisonousComponent
                    {
                        Toxicity = Random.Range(.1f,1f)
                    });
                    entity.Get<PersonFoodComponent>().FoodAmount = 3;
                }
                else
                {
                    float bonusToxicity = (Random.value - Random.value);
                    ref var toxicity = ref entity.Get<PoisonousComponent>().Toxicity;
                    toxicity += bonusToxicity * _configs.PoisonousMutationFault;
                    toxicity = Mathf.Clamp(toxicity, 0, 1);
                }
                Color newColor = entity.Get<ViewComponent>().View.GetComponent<MeshRenderer>().material.color;
                newColor.g = .5f + .5f * entity.Get<PoisonousComponent>().Toxicity;
                newColor.g = Mathf.Clamp(newColor.g, 0.5f, 2);
                entity.Get<ViewComponent>().View.GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
    }
}