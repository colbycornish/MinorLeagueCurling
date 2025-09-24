

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorebugTeamStoneAvailability : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public GameObject stoneAvailabilityItemPrefab;
    
    [Header("Settings")]
    public int numberOfStones = 5;

    // [Header("Display Helpers")]
    // private bool isWarningVisible = false; // The current state of the Sweeper
    // private bool isFrozen = false; // The current state of the Sweeper

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start(){
        
    }

    public void SetNumberOfStones(int num){
        numberOfStones = num;
    }


    public void BuildList(){
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < numberOfStones; i++){
            GameObject listItem = Instantiate(stoneAvailabilityItemPrefab, this.transform);
            listItem.transform.SetParent(this.transform);
            ScorebugTeamStoneAvailabilityItem controller = listItem.GetComponent<ScorebugTeamStoneAvailabilityItem>();
            controller.SetAvailability(true);
        }
    }

    public void UpdateStoneAvailability(
        int numAvailableStones
    ){

        int i = 0;

        foreach (Transform child in this.transform)
        {
            if (i <= numAvailableStones){
                ScorebugTeamStoneAvailabilityItem controller = child.GetComponent<ScorebugTeamStoneAvailabilityItem>();
                controller.SetAvailability(false);
            }
            i++;
            
        }

    }

}

