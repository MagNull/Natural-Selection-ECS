using System.Collections.Generic;
using System.Linq;
using Components;
using Jobs;
using Leopotam.Ecs;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Systems
{
    public class FindPredatorFoodSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, ViewComponent, PredatorComponent>.Exclude<HerbivoreСomponent> _persons;
        private EcsFilter<PersonFoodComponent, ViewComponent> _food;

        public void Run()
        {
            List<Vector3> personsPositions = new List<Vector3>();
            List<EcsEntity> personsEntity = new List<EcsEntity>();
            foreach (var person in _persons)
            {
                var entity = _persons.GetEntity(person);
                personsPositions.Add(entity.Get<ViewComponent>().View.transform.position);
                personsEntity.Add(entity);
            }
            
            List<Vector3> foodPositions = new List<Vector3>();
            foreach (var f in _food)
            {
                if (_food.Get2(f).View.activeSelf) foodPositions.Add(_food.Get2(f).View.transform.position);
            }

            foodPositions = foodPositions.Where(x => !personsPositions.Contains(x)).ToList();
            
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