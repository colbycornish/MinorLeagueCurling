using UnityEngine;

namespace CurlingUI.v3 {
    public class PowerMeter : MonoBehaviour
    {
        [Header("Game Objects")]
        public GameObject PowerMeterBarArea;
        public GameObject PowerMeterSingleBar;
        public GameObject MaxPowerIndicator;

        [Header("Power Meter Settings")]
        public float currentPower = 0.6f;
        public float maxPower = 1.0f;
        public float minPower = 0.6f;
        
        [Header("Visualization Settings")]
        public int numberOfVisualSteps = 25;
        public int numberOfUserSteps = 4;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void OnEnable()
        {
            
        }

        // Update is called once per frame
        void OnDisable()
        {
            
        }

        public void SetPowerLimits(
            float current,
            float min = 0.6f,
            float max = 1f
        ){
            this.currentPower = current;
            this.minPower = min;
            this.maxPower = max;
            // UpdatePowerDisplay();
        }

        public void UpdateMaxPowerDisplay(){
            MaxPowerIndicator.SetActive(currentPower == maxPower);
        }

        /// <summary>
        /// Helpers
        /// </summary>

        public float GetPower()
        {
            return currentPower;
        }

        /// <summary>
        /// Change Power
        /// </summary>
        public void UpdateCurrentPower(float current){
            this.currentPower = Mathf.Clamp(current, minPower, maxPower);
            // UpdatePowerDisplay();
        }


        public void IncreasePower(){
            currentPower = currentPower + 0.04f;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
            // UpdatePowerDisplay();

        }

        public void DecreasePower(){
            currentPower = currentPower - 0.04f;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
            // UpdatePowerDisplay();
        }


        public void BuildPowerMeter(){
            // Remove all the existing power bar items
            // foreach (Transform child in powerMeterBar.transform)
            // {
            //     GameObject.Destroy(child.gameObject);
            // }

            // int numBarsPerColor = 4;
            // foreach(string hexCode in hexColors){
            //     for(int i = 0; i < numBarsPerColor; i++){
            //         // instantiate the bar
            //         GameObject barItem = Instantiate(powerMeterSingleBarPrefab, powerMeterBar.transform);

            //         // set the bar's color
            //         PowerMeterSingleBar controller = barItem.GetComponent<PowerMeterSingleBar>();
            //         controller.SetHexColor(hexCode: hexCode);

            //         // add the bar into the power meter
            //         barItem.transform.SetParent(powerMeterBar.transform);
            //     }
            // }
        }

        public void UpdatePowerDisplay(){

            // Hide / Show the bars, according to the amount of power applied;
            // int numberOfChildren = powerMeterBar.transform.childCount;
            // float currentPowerPct = (currentPower - minPower) / (maxPower - minPower);

            // float numberOfActivesF = numberOfChildren * currentPowerPct;
            // int numberOfActives = Mathf.CeilToInt(numberOfActivesF) + 2;
            // numberOfActives = Mathf.Clamp(numberOfActives, 2, numberOfChildren);

            // int index = numberOfActives - numberOfChildren;

            // foreach(Transform child in powerMeterBar.transform){
            //     if (index >=0){
            //         child.gameObject.SetActive(true);
            //     }
            //     else {
            //         child.gameObject.SetActive(false);
            //     }
            //     index++;
                
            // }

            // UpdateMaxPowerDisplay();
        }
    }
}