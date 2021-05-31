using Components.Common_Components;
using Components.Food_Components;
using Leopotam.Ecs;
using Services;
using UnityComponents;
using UnityEngine;

namespace Systems.Common_Systems
{
    public class FoodSpawnSystem : IEcsInitSystem
    {
        private Configs _configs;
        private EcsWorld _world;
        private FoodAndPersonPools _pools;
        
        public void Init()
        {
            _pools.FoodPool = new ObjectPool<GameObject>(SpawnFood);
            for (int i = 0; i < _configs.NoneActiveStartFood; i++)
            {
               GameObject food = _pools.FoodPool.Get();
               food.SetActive(false);
               _pools.FoodPool.Return(food);
            }
            FoodWave();
            _world.NewEntity().Replace(new TimerComponent
            {
                StartTime = _configs.FoodSpawnRate,
                Tick = _configs.FoodSpawnRate,
                Action = FoodWave,
                Repeatable = true
            });
        }

        private void FoodWave()
        {
            for (int i = 0; i < _configs.FoodSpawnAmount; i++)
            {
                Vector3 position = new Vector3(
                    Random.Range(-_configs.EnvironmentPlane.transform.localScale.x / 2, 
                        _configs.EnvironmentPlane.transform.localScale.x / 2),
                    .4f,
                    Random.Range(-_configs.EnvironmentPlane.transform.localScale.y / 2, 
                        _configs.EnvironmentPlane.transform.localScale.y / 2));
                GameObject foodView = _pools.FoodPool.Get();
                
                EcsEntity entity = _world.NewEntity();
                foodView.GetComponent<LinkedEntity>().Link(entity);
                
                entity.Replace(new ViewComponent
                    {View = foodView.gameObject});
                entity.Replace(new FoodComponent {NutritionalValue = 1});
                
                foodView.transform.position = position;
                foodView.SetActive(true);
            }
        }

        private GameObject SpawnFood()
        {
            GameObject food = GameObject.Instantiate(
                _configs.FoodPrefab, Vector3.zero, Quaternion.identity);
            return food;
        }
    }
}