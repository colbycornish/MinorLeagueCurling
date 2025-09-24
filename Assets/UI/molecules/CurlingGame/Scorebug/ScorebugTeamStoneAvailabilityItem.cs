

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorebugTeamStoneAvailabilityItem : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public GameObject availableColor;
    public GameObject usedColor;

    [Header("Settings")]
    public bool isAvailable = true;
    public bool isUsed = false;

    public void Start(){
        SetAvailable();
    }

    public void SetAvailability(bool available){
        isAvailable = available;
        isUsed = !available;
        if (available){
            SetAvailable();
        }
        else {
            SetUsed();
        }
    }

    public void SetAvailable(){
        isAvailable = true;
        isUsed = false;
        availableColor.SetActive(true);
        usedColor.SetActive(false);
    }

    public void SetUsed(){
        isAvailable = false;
        isUsed = true;
        availableColor.SetActive(false);
        usedColor.SetActive(true);
    }

}

