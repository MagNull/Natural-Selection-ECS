using Components.Common_Components;
using Components.Person_Components;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;

namespace Systems.Common_Systems
{
    public class MutationSystem : IEcsRunSystem
    {
        private EcsFilter<BornComponent> _filter;
        private EcsFilter<NoReplicateTimeComponent> _noReplicatetimers;
        private Configs _configs;
        
        
        public void Run()
        {
            BecamePredator();
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                SpeedMutation(entity);
                //SizeMutation(entity);
                PoisonousMutation(entity);
                entity.Del<BornComponent>();
            }
        }

        private void BecamePredator()
        {
            foreach (var timer in _noReplicatetimers)
            {
                if (_noReplicatetimers.Get1(timer).Time >= _configs.TimeToBecamePredator)
                {
                    var entity = _noReplicatetimers.GetEntity(timer);
                    PredatorMutation(entity);
                    entity.Del<NoReplicateTimeComponent>();
                }
                else
                {
                    _noReplicatetimers.Get1(timer).Time += Time.deltaTime;
                }
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
            if (!entity.Has<PredatorComponent>())
            {
                entity.Replace(new PredatorComponent
                {
                    Rapacity = 0,
                    Predatoriness = .1f,
                    PredatorExperience = 1
                });
                entity.Get<ViewComponent>().View.GetComponent<MeshRenderer>().material.color = Color.black;
                entity.Get<ViewComponent>().View.transform.GetChild(0).gameObject.SetActive(true);
                entity.Get<ViewComponent>().View.transform.GetChild(0).transform.localScale =
                    Vector3.one * entity.Get<PredatorComponent>().Predatoriness;
                entity.Replace(new FoodsAverage());
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