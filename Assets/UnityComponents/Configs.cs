using UnityEngine;

namespace UnityComponents
{
    [CreateAssetMenu(fileName = "Configs")]
    public class Configs : ScriptableObject
    {
         public Transform EnvironmentPlane;
         
         [Header("Person Configs")]
         public int StartPersonsAmount;
         public float StartPersonsSpeed;
         public int NoneActiveStartPerson;
         public GameObject PersonPrefab;
         public byte FoodToReplicate;
         public byte PersonNutritionalValue;
         public float MutationChance;
         
         [Header("Food Configs")]
         public GameObject FoodPrefab;
         public int FoodSpawnAmount;
         public int NoneActiveStartFood;
         
         [Header("Tick Rates")]
         public float AgingTickRate;
         public float ReplicateTickRate;
         public float PlantMeatRatioCheckPeriod;
         public float FoodSpawnRate;
         
         [Header("Speed Mutation Configs")]
         public float FoodSpeedCoefficient;
         public float SpeedMutationFault;
         
         [Header("Size Mutation Configs")]
         public float SizeSpeedCoefficient;
         public float SizeMutationFault;
         public float FoodSizeCoefficient;
         public float SizeThresholdToEat;

         [Header("Predator Mutation Configs")]
         public float RatioToChangeRation;
         public float TimeToBecamePredator;
         public float KillsToBecameOnlyCarnivorous;
         public float RapacityPerKill;
         public float ExperienceDeltaToEat;

         [Header("Poisonous Mutation Configs")] 
         public float FoodPerPoisonous;
         public float PoisonousMutationFault;
         public float KillPoisonThreshold;
    }
}