﻿using System;
using UnityEngine;

namespace Components
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
         public byte MutationChance;
         
         [Header("Food Configs")]
         public GameObject FoodPrefab;
         public int FoodSpawnAmount;
         public float FoodSpawnRate;
         public int NoneActiveStartFood;
         
         [Header("Tick Rates")]
         public int DieTickRate;
         public int ReplicateTickRate;
         
         [Header("Speed Mutation Configs")]
         public float FoodSpeedCoefficient;
         public float SpeedMutationFault;
         
         [Header("Size Mutation Configs")]
         public float SizeSpeedCoefficient;
         public float SizeMutationFault;
         public float FoodSizeCoefficient;
         public float SizeThresholdToEat;

         [Header("Predator Mutation Configs")] 
         public int PredatorMutationChance;

         [Header("Poisonous Mutation Configs")] 
         public float FoodPerPoisonous;
    }
}