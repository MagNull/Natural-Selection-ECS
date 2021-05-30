using Components;
using Leopotam.Ecs;
using Services;
using UnityComponents;
using UnityEngine;

namespace Systems
{
    public class PersonsSpawnSystem : IEcsInitSystem
    {
        private Configs _configs;
        private EcsWorld _world;
        private FoodAndPersonPools _pools;
        
        public void Init()
        {
            _pools.PersonPool = new ObjectPool<GameObject>(SpawnPerson);
            for (int i = 0; i < _configs.NoneActiveStartPerson; i++)
            {
                GameObject person = SpawnPerson();
                person.SetActive(false);
                _pools.PersonPool.Return(person);
            }
            for (int i = 0; i < _configs.StartPersonsAmount; i++)
            {
                Vector3 position = new Vector3(
                    Random.Range(-_configs.EnvironmentPlane.transform.localScale.x / 2, 
                        _configs.EnvironmentPlane.transform.localScale.x / 2),
                    1,
                    Random.Range(-_configs.EnvironmentPlane.transform.localScale.y / 2, 
                        _configs.EnvironmentPlane.transform.localScale.y / 2));
                
                EcsEntity entity = _world.NewEntity();
                GameObject person = _pools.PersonPool.Get();
                
                entity.Replace(new ViewComponent
                    {View = person.gameObject});
                entity.Replace(new MoveComponent
                {
                    Speed = _configs.StartPersonsSpeed,
                    Rigidbody = person.GetComponent<Rigidbody>()
                });
                entity.Replace(new FoodComponent
                {
                    NutritionalValue = _configs.PersonNutritionalValue,
                });
                person.GetComponent<LinkedEntity>().Link(entity);
                entity.Replace(new PersonFoodComponent {FoodAmount = 1});
                entity.Replace(new MeatFoodComponent());
                entity.Replace(new HerbivoreСomponent());
                entity.Replace(new NoReplicateTimeComponent());
                
                person.transform.position = position;
                person.SetActive(true);
            }
        }

        private GameObject SpawnPerson()
        {
            GameObject person = GameObject.Instantiate(
                _configs.PersonPrefab, Vector3.zero, Quaternion.identity);
            return person;
        }
    }
}