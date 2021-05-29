using System.Collections.Generic;
using System.Linq;
using Components;
using Jobs;
using Leopotam.Ecs;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Systems
{
    public class FindPlantFoodSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, ViewComponent>.Exclude<PredatorComponent> _persons;
        private EcsFilter<FoodComponent, ViewComponent> _food;
        
        
            
        public void Run()
        {
            List<Vector3> personsPositions = new List<Vector3>();
            List<EcsEntity> personsEntity = new List<EcsEntity>();
            foreach (var person in _persons)
            {
                // if (_food.Get2(0).View == null)
                // {
                //     _persons.Get1(person).Direction = Vector3.zero;
                //     continue;
                // }
                
                // Vector3 position = _persons.Get2(person).View.transform.position;
                // Vector3 nearestFood = new Vector3(0, int.MaxValue, 0);
                
                // foreach (var f in _food)
                // {
                //     if (_food.Get2(f).View.activeSelf && (_food.Get2(f).View.transform.position - position).sqrMagnitude <
                //         (nearestFood - position).sqrMagnitude) nearestFood = _food.Get2(f).View.transform.position;
                // }

                //_persons.Get1(person).Direction = (nearestFood - position).normalized;
                var entity = _persons.GetEntity(person);
                personsPositions.Add(entity.Get<ViewComponent>().View.transform.position);
                personsEntity.Add(entity);
            }
            
            List<Vector3> foodPositions = new List<Vector3>();
            foreach (var f in _food)
            {
                if (_food.Get2(f).View.activeSelf && !_food.GetEntity(f).Has<PersonFoodComponent>()) foodPositions.Add(_food.Get2(f).View.transform.position);
            }
            
            List<Vector3> directions = FindNearest(personsPositions.ToNativeArray(Allocator.TempJob), foodPositions.ToNativeArray(Allocator.TempJob));
            for (int i = 0; i < personsEntity.Count; i++)
            {
                personsEntity[i].Get<MoveComponent>().Direction = directions[i];
            }
        }
        
        private List<Vector3> FindNearest(NativeArray<Vector3> persons, NativeArray<Vector3> foodPositions)
        {
            NativeArray<Vector3> resultArray = new NativeArray<Vector3>(persons.Length, Allocator.TempJob);
            var newJob = new FindNearestJob()
            {
                PersonsPositions = persons,
                FoodPosition = foodPositions,
                ResultDirections = resultArray
            };

            JobHandle handle = newJob.Schedule(persons.Length, 6);
            handle.Complete();
            List<Vector3> result = resultArray.ToList();
            
            resultArray.Dispose();
            foodPositions.Dispose();
            persons.Dispose();

            return result;
        }
    }
}