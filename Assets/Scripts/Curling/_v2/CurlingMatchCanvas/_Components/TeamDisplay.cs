

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamDisplay : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public bool controlsTeamOne = false;
    public bool controlsTeamTwo = false;

    public GameObject teamNameText;
    public GameObject parentStoneDisplay;

    public void Start(){
        errorCheck();
    }

    /// <summary>
    /// Error Checking and Debugging
    /// </summary>

    public void UpdateTeamName(string teamName){
        if (teamName != null) {
            teamNameText.GetComponent<TMPro.TextMeshProUGUI>().text = teamName; 
        } 
    }

    public void UpdateStoneDisplay(){
        
    }


    public void errorCheck(){
        
        
    }



}
