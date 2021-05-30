using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Jobs
{
    [BurstCompile]
    public struct FindNearestAllJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<Vector3> PersonsPositions;

        [ReadOnly] 
        public NativeArray<int> PersonsPredatorExperiences;

        [ReadOnly] 
        public NativeArray<Vector3> FoodPositions;

        [ReadOnly] 
        public NativeArray<int> FoodPredatorExperiences;

        [WriteOnly]
        public NativeArray<Vector3> ResultDirections;
        
        public void Execute(int index)
        {
            Vector3 nearestFoodPosition = new Vector3(0, 1234, 0);
            int personExperience = PersonsPredatorExperiences[index];
            Vector3 personPosition = PersonsPositions[index];
            
            for (var i = 0; i < FoodPositions.Length; i++)
            {
                if (personExperience > FoodPredatorExperiences[i]
                    && personPosition != FoodPositions[i]
                    && (personPosition - FoodPositions[i]).sqrMagnitude <
                    (personPosition - nearestFoodPosition).sqrMagnitude)
                {
                    nearestFoodPosition = FoodPositions[i];
                }
            }
            ResultDirections[index] = (nearestFoodPosition - personPosition).normalized;
        }
    }
}