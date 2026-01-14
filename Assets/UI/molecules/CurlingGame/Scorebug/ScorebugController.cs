

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CurlingManagersV3;

public class ScorebugController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public GameObject teamAreaHome;
    public GameObject teamAreaAway;
    public GameObject scoreArea;
    public GameObject timer;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start(){
        InitializeDisplay();
    }

    public void OnEnable()
    {
        Debug.Log("Scorebug Enabled");
        UpdateScore(
            homeTeamScore: CurlingManager._instance.Parameters.CurrentGameScore.teamHomeScore,
            awayTeamScore: CurlingManager._instance.Parameters.CurrentGameScore.teamAwayScore
        );

        UpdateTurn(
            isHomeTeamTurn: CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home,
            isAwayTeamTurn: CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Away
        );

        UpdateStoneAvailability(
            numHomeTeamStonesAvailable: CurlingManager._instance.Parameters.Stones.stonesTeamHome.FindAll(s => s.isInPlay == false).Count,
            numAwayTeamStonesAvailable: CurlingManager._instance.Parameters.Stones.stonesTeamAway.FindAll(s => s.isInPlay == false).Count
        );
    }

    ///
    /// Initiate
    /// 
    public void Init(){

    }

    public void InitializeDisplay(){
       
    }
    ///
    /// Update
    /// 

    public void UpdateTeamInfo(
        string homeTeamName,
        string awayTeamName
    ){
        ScorebugTeamArea teamAreaHomeController = teamAreaHome.GetComponent<ScorebugTeamArea>();
        teamAreaHomeController.UpdateTeamName(
            text: "Blue Broom Brushers"
        );

        ScorebugTeamArea teamAreaAwayController = teamAreaAway.GetComponent<ScorebugTeamArea>();
        teamAreaAwayController.UpdateTeamName(
            text: "Purple Stone Throwers"
        );
    }

    public void UpdateTurn(
        bool isHomeTeamTurn, 
        bool isAwayTeamTurn = false
    ){
        ScorebugTeamArea teamAreaHomeController = teamAreaHome.GetComponent<ScorebugTeamArea>();
        ScorebugTeamArea teamAreaAwayController = teamAreaAway.GetComponent<ScorebugTeamArea>();

        
        if (isHomeTeamTurn == true){
            teamAreaHomeController.UpdateCurrentTurn(
                isCurrentTurn: isHomeTeamTurn
            );
            teamAreaAwayController.UpdateCurrentTurn(
                isCurrentTurn: false
            );
        } else {
            teamAreaHomeController.UpdateCurrentTurn(
                isCurrentTurn: false
            );
            teamAreaAwayController.UpdateCurrentTurn(
                isCurrentTurn: true
            );
        }
    }

    public void SetTotalNumberOfStonesPerTeam(
        int totalNumberOfStonesPerTeam
    ){
        ScorebugTeamArea teamAreaHomeController = teamAreaHome.GetComponent<ScorebugTeamArea>();
        ScorebugTeamArea teamAreaAwayController = teamAreaAway.GetComponent<ScorebugTeamArea>();

        teamAreaHomeController.SetTotalNumberOfStones(num: totalNumberOfStonesPerTeam);
        teamAreaAwayController.SetTotalNumberOfStones(num: totalNumberOfStonesPerTeam);
    }

    public void UpdateStoneAvailability(
        int numHomeTeamStonesAvailable,
        int numAwayTeamStonesAvailable
    ){
        ScorebugTeamArea teamAreaHomeController = teamAreaHome.GetComponent<ScorebugTeamArea>();
        ScorebugTeamArea teamAreaAwayController = teamAreaAway.GetComponent<ScorebugTeamArea>();

        teamAreaHomeController.UpdateStoneAvailability(
            numAvailableStones: numHomeTeamStonesAvailable
        );
        teamAreaAwayController.UpdateStoneAvailability(
            numAvailableStones: numAwayTeamStonesAvailable
        );

    }
    
    public void UpdateScore(
        int homeTeamScore,
        int awayTeamScore
    ){
        ScorebugScoreArea scoreController = scoreArea.GetComponent<ScorebugScoreArea>();
        scoreController.UpdateScore(
            homeTeamScore: homeTeamScore,
            awayTeamScore: awayTeamScore
        );
    }

    public void UpdateTimer(){

    }

    public void EnableTimer(){
        timer.SetActive(true);
    }

    public void DisableTimer(){
        timer.SetActive(false);
    }

    ///
    /// Reset
    /// 
    public void ResetStoneAvailability(){
        ScorebugTeamArea teamAreaHomeController = teamAreaHome.GetComponent<ScorebugTeamArea>();
        ScorebugTeamArea teamAreaAwayController = teamAreaAway.GetComponent<ScorebugTeamArea>();
        teamAreaHomeController.UpdateStoneAvailability(
            numAvailableStones: 5
        );
        teamAreaAwayController.UpdateStoneAvailability(
            numAvailableStones: 5
        );
    }

    public void ResetScore(){
        ScorebugScoreArea scoreController = scoreArea.GetComponent<ScorebugScoreArea>();
        scoreController.UpdateScore(
            homeTeamScore: 0,
            awayTeamScore: 0
        );
    }




}

