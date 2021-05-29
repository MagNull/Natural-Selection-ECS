using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Jobs
{
    [BurstCompile]
    public struct FindNearestJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<Vector3> PersonsPositions;

        [ReadOnly] 
        public NativeArray<Vector3> FoodPosition;

        [WriteOnly]
        public NativeArray<Vector3> ResultDirections;
        
        public void Execute(int index)
        {
            Vector3 nearest = new Vector3(0, 1234, 0);
            var personPosition = PersonsPositions[index];
            
            for (var i = 0; i < FoodPosition.Length; i++)
            {
                if ((personPosition - FoodPosition[i]).sqrMagnitude < (personPosition - nearest).sqrMagnitude) 
                    nearest = FoodPosition[i];
            }

            ResultDirections[index] = (nearest - personPosition).normalized;
        }
    }
}