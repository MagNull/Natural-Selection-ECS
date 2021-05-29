using Components;
using Leopotam.Ecs;
using Services;
using UnityEngine;

namespace Systems
{
    public class DestroySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyedComponent> _filter;
        private EcsFilter<CollusionComponent> _collusions;
        private FoodAndPersonPools _pools;
        
        public void Run()
        {
            foreach (var e in _filter)
            {
                var entity = _filter.GetEntity(e);
                entity.Del<DestroyedComponent>();
                if (entity.Has<ViewComponent>())
                {
                    GameObject view = entity.Get<ViewComponent>().View;
                    if (entity.Has<FoodComponent>() && !entity.Has<PersonFoodComponent>())
                    {
                        view.SetActive(false);
                        _pools.FoodPool.Return(view);
                    }
                    else if (entity.Has<PersonFoodComponent>())
                    {
                        view.SetActive(false);
                        _pools.PersonPool.Return(view);
                    }
                }
                entity.Destroy();
            }

            foreach (var collusion in _collusions)
            {
                _collusions.GetEntity(collusion).Destroy();
            }
        }
    }
}