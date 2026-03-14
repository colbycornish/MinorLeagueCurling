using UnityEngine;
using UnityEngine.Events;

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

        [Header("FX Events")]
        public GameObject fxMax;
        public GameObject fx75;
        public GameObject fx50;
        public GameObject fx25;
        public GameObject fxMin;
        // // public UnityEvent onPowerChanged;
        // public UnityEvent _OnMaxPowerReached;
        // public UnityEvent _On75PowerReached;
        // public UnityEvent _On50PowerReached;
        // public UnityEvent _On25PowerReached;
        // public UnityEvent _OnMinPowerReached;

        void Update()
        {
            // For testing purposes, we can use the up and down arrow keys to adjust power
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                IncreasePower();
                UpdatePowerDisplay();
                // PlayPowerFXDisplay();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                DecreasePower();
                UpdatePowerDisplay();
                // PlayPowerFXDisplay();
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void OnEnable()
        {
            UpdatePowerDisplay();
            PlayPowerFXDisplay();
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
            PlayPowerFXDisplay();
            // UpdatePowerDisplay();

        }

        public void DecreasePower(){
            currentPower = currentPower - 0.04f;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
            PlayPowerFXDisplay();
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
            int numberOfChildren = PowerMeterBarArea.transform.childCount;
            float currentPowerPct = (currentPower - minPower) / (maxPower - minPower);

            float numberOfActivesF = numberOfChildren * currentPowerPct;
            int numberOfActives = Mathf.CeilToInt(numberOfActivesF) + 2;
            numberOfActives = Mathf.Clamp(numberOfActives, 2, numberOfChildren);

            int index = numberOfActives - numberOfChildren;

            foreach(Transform child in PowerMeterBarArea.transform){
                if (index >=0){
                    child.gameObject.SetActive(true);
                }
                else {
                    child.gameObject.SetActive(false);
                }
                index++;
                
            }

            UpdateMaxPowerDisplay();
        }



        public void PlayPowerFXDisplay(){
            float pct75 = 0.75f * (maxPower - minPower) + minPower;
            float pct50 = 0.50f * (maxPower - minPower) + minPower;
            float pct25 = 0.25f * (maxPower - minPower) + minPower;


            if (fxMax != null)
            {
                fxMax.SetActive(currentPower == maxPower );
            }

            if (fx75 != null)
            {
                fx75.SetActive(currentPower >= pct75);
            }

            if (fx50 != null)
            {
                fx50.SetActive(currentPower >= pct50);
            }

            if (fx25 != null)
            {
                fx25.SetActive(currentPower >= pct25);
            }

            if (fxMin != null)
            {
                fxMin.SetActive(currentPower == minPower);
            }
            
        }
    }
}