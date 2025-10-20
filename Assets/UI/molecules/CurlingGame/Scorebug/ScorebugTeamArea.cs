

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorebugTeamArea : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    [SerializeField] private TextMeshProUGUI textName;
    public ScorebugTeamStoneAvailability stoneAvailabilityArea;
    public GameObject turnIndicator;


    [Header("Display Helpers")]
    private bool isCurrentTurn = false; 

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start(){
    
    }

    public void Init(){

    }

    public void UpdateTeamName(string text){
        if (textName != null)
        {
            textName.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        }
    }

    public void SetTotalNumberOfStones(int num){
        stoneAvailabilityArea.SetTotalNumberOfStones(num: num);   
    }

    public void UpdateStoneAvailability(
        int numAvailableStones
    ){
        stoneAvailabilityArea.UpdateStoneAvailability(
            numAvailableStones: numAvailableStones
        );
    }


    public void UpdateCurrentTurn(
        bool isCurrentTurn
    ){
        this.isCurrentTurn = isCurrentTurn;
        if (isCurrentTurn){
            turnIndicator.SetActive(true);
        }
        else {
            turnIndicator.SetActive(false);
        }
    }
}

