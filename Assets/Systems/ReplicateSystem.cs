using Components;
using Leopotam.Ecs;
using Services;
using UnityComponents;
using UnityEngine;

namespace Systems
{
    public class ReplicateSystem : IEcsInitSystem
    {
        private EcsFilter<PersonFoodComponent> _filter;
        private Configs _configs;
        private EcsWorld _world;
        private FoodAndPersonPools _pools;
        
        public void Init()
        {
            _world.NewEntity().Replace(new TimerComponent
            {
                StartTime = _configs.ReplicateTickRate,
                Tick = _configs.ReplicateTickRate,
                Action = ReplicateTick,
                Repeatable = true
            });
        }

        private void ReplicateTick()
        {
            foreach (var i in _filter)
            {
                ref var food = ref _filter.Get1(i);
                if (food.FoodAmount >= _configs.FoodToReplicate)
                {
                    food.FoodAmount--;
                    GameObject parentView = _filter.GetEntity(i).Get<ViewComponent>().View;
                    LinkedEntity person = _pools.PersonPool.Get().GetComponent<LinkedEntity>();
                    person.transform.position = parentView.transform.position - parentView.transform.forward * 1.5f;
                    person.gameObject.GetComponent<MeshRenderer>().material =
                        parentView.GetComponent<MeshRenderer>().material;
                    var entity = _filter.GetEntity(i).Copy();
                    entity.Replace(new ViewComponent {View = person.gameObject});
                    entity.Get<MoveComponent>().Rigidbody = person.GetComponent<Rigidbody>();
                    entity.Get<PersonFoodComponent>().FoodAmount = 2;
                    entity.Replace(new BornComponent());
                    person.Link(entity);
                    person.gameObject.SetActive(true);
                }
            }
        }
    }
}