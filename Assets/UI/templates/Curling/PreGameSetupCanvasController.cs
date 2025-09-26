using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPreGameSetupCanvasController : MonoBehaviour
{
    [Header("Sections")]
    [SerializeField] public GameObject sectionCourseSelection;
    [SerializeField] public GameObject sectionRulesSelection;
    [SerializeField] public GameObject sectionTeamSelection;
    [SerializeField] public GameObject sectionReadyScreen;

    



    public void DemoSelectCourse(){
        CurlingPreGameSetupManager._instance.OnSelectCourse(courseId: "");
    }

    public void DemoSelectRules(){
        CurlingRules rules = new CurlingRules();
        rules.gameMode.SetGameMode(isPractice: true);
        rules.opponent.SetOpponentType(isLocal: true);
        rules.difficulty.SetDifficulty(isEasy: true);
        rules.scoring.SetScoringMode(isClassic: true);
        rules.obstacles.SetFrequency(isLow: true);
        rules.throwClock.DisableThrowClock();

        CurlingPreGameSetupManager._instance.OnSelectRules(
            rules: rules
        );
        
    }

    public void DemoSelectTeam(){
        // characters
        CurlingPreGameSetupManager._instance.OnSelectThrower(
            characterId: ""
        );
        CurlingPreGameSetupManager._instance.OnSelectRightSweeper(
            characterId: ""
        );
        CurlingPreGameSetupManager._instance.OnSelectLeftSweeper(
            characterId: ""
        );

        // brooms
        CurlingPreGameSetupManager._instance.OnSelectBroom(
            broomId: "",
            isLeft: true, 
            isRight: false
        );
        CurlingPreGameSetupManager._instance.OnSelectBroom(
            broomId: "",
            isLeft: false, 
            isRight: true
        );

        // stones
        CurlingPreGameSetupManager._instance.OnSelectStone(
            stoneId: "", 
            stoneIndex: 1
        );
        CurlingPreGameSetupManager._instance.OnSelectStone(
            stoneId: "", 
            stoneIndex: 2
        );
        CurlingPreGameSetupManager._instance.OnSelectStone(
            stoneId: "", 
            stoneIndex: 3
        );
        CurlingPreGameSetupManager._instance.OnSelectStone(
            stoneId: "", 
            stoneIndex: 4
        );
        CurlingPreGameSetupManager._instance.OnSelectStone(
            stoneId: "", 
            stoneIndex: 5
        );
        
    }

    public void DemoSelectReadyToPlay(){
        
    }
    
}
