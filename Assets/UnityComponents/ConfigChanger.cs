using System;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents
{
    public class ConfigChanger : MonoBehaviour
    {
        [HideInInspector] public Configs DefaultConfigs;
        public Configs CurrentConfigs;
        

        #region Person Configs
        
        public void ChangeStartPersonAmount(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.StartPersonsAmount = int.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeStartPersonSpeed(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.StartPersonsSpeed = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeFoodToReplicate(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.FoodToReplicate = byte.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangPersonNutritionalValue(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.PersonNutritionalValue = byte.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeMutationChance(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.MutationChance = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        

        #endregion

        #region Food Configs

        public void ChangeFoodSpawnAmount(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.FoodSpawnAmount = int.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }

        #endregion

        #region Tick Rates

        public void ChangeAgingTickRate(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.AgingTickRate = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeReplicateTickRate(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.ReplicateTickRate = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangePlantMeatRatioCheckTickRate(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.PlantMeatRatioCheckPeriod = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeFoodSpawnRate(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.FoodSpawnRate = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        #endregion

        #region Speed Mutation Configs

        public void ChangeInfluenceSpeedOnFoodNeed(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.FoodSpeedCoefficient = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeSpeedMutationFault(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.SpeedMutationFault = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        #endregion

        #region Predator Mutation Configs

        public void ChangeRatioToChangeRation(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.RatioToChangeRation = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeTimeToBecamePredator(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.TimeToBecamePredator = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeKillsToBecameOnlyCarnivorous(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.KillsToBecameOnlyCarnivorous = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeRapacityPerKil(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.RapacityPerKill = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        

        #endregion

        #region Poisonous Mutation Configs

        public void ChangeFoodPerPoisonous(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.FoodPerPoisonous = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangePoisonousMutationFault(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.PoisonousMutationFault = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }
        
        public void ChangeKillPoisonThreshold(TMP_InputField inputField)
        {
            try
            {
                CurrentConfigs.KillPoisonThreshold = float.Parse(inputField.text);
                inputField.textComponent.color = Color.black;
            }
            catch
            {
                inputField.text = "Incorrect input";
                inputField.textComponent.color = Color.red;
            }
        }

        #endregion
    }
}