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

    void Start()
    {
        OpenCourseSelection();
    }

    void OnEnable(){
        // CurlingPreGameSetupManager._instance.OnChangeState += OnChangeState;
        CanvasManager._instance.canvasDisplayAreaController.BuildAllDisplayAreas();
    }

    void OnDisable(){
        // CurlingPreGameSetupManager._instance.OnChangeState -= OnChangeState;
        CanvasManager._instance.canvasDisplayAreaController.CloseAllDisplayAreas();
    }

    
    public void OpenRulesSelection(){
        SetActiveSelection(openRulesSelection: true);
    }

    public void OpenCourseSelection(){
        SetActiveSelection(openCourseSelection: true);
    }

    public void OpenTeamSelection(){
        SetActiveSelection(openTeamSelection: true);
    }

    public void OpenReadyScreen(){
        SetActiveSelection(openReadyScreen: true);
    }

    public void LaunchCurlingGame(){
        Debug.Log("Launching Curling Game...");
    }

    /// activate/Deactivate Sections
    private void SetActiveSelection(
        bool openCourseSelection = false, 
        bool openRulesSelection = false, 
        bool openTeamSelection = false, 
        bool openReadyScreen = false
    ){
        sectionCourseSelection.SetActive(openCourseSelection);
        sectionRulesSelection.SetActive(openRulesSelection);
        sectionTeamSelection.SetActive(openTeamSelection);
        sectionReadyScreen.SetActive(openReadyScreen);
    }


    /// DEMO FUNCTIONS
    public void DemoSelectCourse(){
        CurlingPreGameSetupManager._instance.OnSelectCourse(courseId: "");
    }

    public void DemoSelectRules(){
        CurlingRules rules = new CurlingRules();
        // rules.gameMode.SetGameMode(isPractice: true);
        // rules.opponent.SetOpponentType(isLocal: true);
        // rules.difficulty.SetDifficulty(isEasy: true);
        // rules.scoring.SetScoringMode(isClassic: true);
        // rules.obstacles.SetFrequency(isLow: true);
        // rules.throwClock.DisableThrowClock();

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
