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
        private EcsFilter<PredatorComponent, MoveComponent, ViewComponent>.Exclude<HerbivoreСomponent> _persons;
        private EcsFilter<MeatFoodComponent, ViewComponent> _food;

        public void Run()
        {
            NativeArray<Vector3> personsPositions =
                new NativeArray<Vector3>(_persons.GetEntitiesCount(), Allocator.TempJob);
            NativeArray<int> personsPredatorExperiences =
                new NativeArray<int>(_persons.GetEntitiesCount(), Allocator.TempJob);
            EcsEntity[] personsEntities = new EcsEntity[_persons.GetEntitiesCount()]; 
            GetPersonsData(personsPositions, personsPredatorExperiences, personsEntities);
            
            NativeArray<Vector3> foodPositions = 
                new NativeArray<Vector3>(_food.GetEntitiesCount(), Allocator.TempJob);
            NativeArray<int> foodPredatorExperiences =
                new NativeArray<int>(_food.GetEntitiesCount(), Allocator.TempJob);
            GetFoodData(foodPredatorExperiences, foodPositions);
            
            List<Vector3> directions = FindNearest(personsPositions, personsPredatorExperiences, 
                foodPositions, foodPredatorExperiences);
            for (int i = 0; i < personsEntities.Length; i++)
            {
                personsEntities[i].Get<MoveComponent>().Direction = directions[i];
            }
        }
        
        private void GetFoodData(NativeArray<int> predatorExperiences, NativeArray<Vector3> foodPositions)
        {
            for (int i = 0; i < _food.GetEntitiesCount(); i++)
            {
                int predatorExperienceValue = 0;
                if (_food.GetEntity(i).Has<PredatorComponent>())
                {
                    predatorExperienceValue = _food.GetEntity(i).Get<PredatorComponent>().PredatorExperience;
                }

                if (!_food.Get2(i).View.activeSelf) predatorExperienceValue = -1;
                predatorExperiences[i] = predatorExperienceValue;
                foodPositions[i] = _food.Get2(i).View.transform.position;
            }
        }

        private void GetPersonsData(NativeArray<Vector3> personsPositions,
            NativeArray<int> predatorExperiences, EcsEntity[] personsEntity)
        {
            for (int i = 0; i < _persons.GetEntitiesCount(); i++)
            {
                personsPositions[i] = _persons.Get3(i).View.transform.position;
                predatorExperiences[i] = _persons.Get1(i).PredatorExperience;
                personsEntity[i] = _persons.GetEntity(i);
            }
        }

        private List<Vector3> FindNearest(NativeArray<Vector3> personsPositions,
            NativeArray<int> personPredatorExperiences, 
            NativeArray<Vector3> foodPositions,
            NativeArray<int> foodPredatorExperiences)
        {
            NativeArray<Vector3> resultArray = new NativeArray<Vector3>(personsPositions.Count(), Allocator.TempJob);
            var newJob = new FindNearestAllJob()
            {
                PersonsPositions = personsPositions,
                PersonsPredatorExperiences = personPredatorExperiences,
                FoodPositions = foodPositions,
                FoodPredatorExperiences = foodPredatorExperiences,
                ResultDirections = resultArray
            };

            JobHandle handle = newJob.Schedule(personsPositions.Count(), 6);
            handle.Complete();
            List<Vector3> result = resultArray.ToList();
            
            resultArray.Dispose();
            foodPositions.Dispose();
            foodPredatorExperiences.Dispose();
            personPredatorExperiences.Dispose();
            personsPositions.Dispose();

            return result;
        }
    }
}