using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class FromPlantToMeatChangeSystem : IEcsInitSystem
    {
        private EcsFilter<HerbivoreСomponent, PredatorComponent> _filter;
        private Configs _configs;
        private EcsWorld _world;

        public void Init()
        {
            CheckRatio();
            _world.NewEntity().Replace(new TimerComponent
            {
                StartTime = _configs.PlantMeatRatioCheckPeriod,
                Tick = _configs.PlantMeatRatioCheckPeriod,
                Action = CheckRatio,
                Repeatable = true
            });
        }

        private void CheckRatio()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var foodsAverage = ref entity.Get<FoodsAverage>();
                if (foodsAverage.MeatAverage / foodsAverage.PlantAverage >= _configs.RatioToChangeRation)
                {
                    entity.Del<HerbivoreСomponent>();
                }

                foodsAverage.MeatAverage = 0;
                foodsAverage.PlantAverage = 0;
            }
        }
    }
}