

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.Tools;

public class PowerMeterController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public GameObject powerMeterBar;
    public GameObject powerMeterSingleBarPrefab;
    public GameObject maxTextArea;
    public GameObject keyIconItem;

    [Header("User Steps")]
    public int numberOfVisualSteps = 32;
    public int numberOfUserSteps = 4;

    [Header("Power Variables")]
    public float currentPower = 0.6f;
    public float minPower = 0.6f;
    public float maxPower = 1.0f;

    [Header("Variables")]
    private bool powerSelected = false;
    private bool isActive = false;

    public List<string> hexColors;


    /// <summary>
    /// Initilization
    /// </summary>
    public void Start(){
        DefineHexColors();
        BuildPowerMeter();
        UpdatePowerDisplay();
    }


    public void SetPowerLimits(
        float current,
        float min = 0.6f,
        float max = 1f
    ){
        this.currentPower = current;
        this.minPower = min;
        this.maxPower = max;
    }

    

    public void DefineHexColors(){
        hexColors.Add("059C61"); // Green
        hexColors.Add("1FBC60"); 
        hexColors.Add("71DD20"); 
        hexColors.Add("C1F52D"); 
        hexColors.Add("F5872D"); // Orange
        hexColors.Add("F56A2D"); 
        hexColors.Add("F54C2D"); // Red
        hexColors.Add("F52D2D"); 
    }

    public void BuildPowerMeter(){
        // Remove all the existing power bar items
        foreach (Transform child in powerMeterBar.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int numBarsPerColor = 4;
        foreach(string hexCode in hexColors){
            for(int i = 0; i < numBarsPerColor; i++){
                // instantiate the bar
                GameObject barItem = Instantiate(powerMeterSingleBarPrefab, powerMeterBar.transform);

                // set the bar's color
                PowerMeterSingleBar controller = barItem.GetComponent<PowerMeterSingleBar>();
                controller.SetHexColor(hexCode: hexCode);

                // add the bar into the power meter
                barItem.transform.SetParent(powerMeterBar.transform);
            }
        }
    }

    /// <summary>
    /// Change Power
    /// </summary>
    public void UpdateCurrentPower(float current){
        this.currentPower = Mathf.Clamp(current, minPower, maxPower);
        UpdatePowerDisplay();
    }


    public void IncreasePower(){
        currentPower = currentPower + 0.04f;
        currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
        UpdatePowerDisplay();

    }

    public void DecreasePower(){
        currentPower = currentPower - 0.04f;
        currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
        UpdatePowerDisplay();
    }

    /// <summary>
    /// Update Display
    /// </summary>
    public void UpdatePowerDisplay(){

        // Hide / Show the bars, according to the amount of power applied;
        int numberOfChildren = powerMeterBar.transform.childCount;
        float currentPowerPct = (currentPower - minPower) / (maxPower - minPower);

        float numberOfActivesF = numberOfChildren * currentPowerPct;
        int numberOfActives = Mathf.CeilToInt(numberOfActivesF) + 2;
        numberOfActives = Mathf.Clamp(numberOfActives, 2, numberOfChildren);

        int index = numberOfActives - numberOfChildren;

        foreach(Transform child in powerMeterBar.transform){
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

    public void UpdateMaxPowerDisplay(){
        if (currentPower == maxPower){
            maxTextArea.SetActive(true);
        }
        else {
            maxTextArea.SetActive(false);
        }
    }

    /// <summary>
    /// Helpers
    /// </summary>

    public float GetPower()
    {
        return currentPower;
    }


}
