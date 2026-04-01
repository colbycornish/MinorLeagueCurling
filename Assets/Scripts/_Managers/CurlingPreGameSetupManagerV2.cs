using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Curling.Rules;
using CurlingManagersV3;

public class CurlingPreGameSetupManagerV2 : MonoBehaviour
{
    public static CurlingPreGameSetupManagerV2 _instance;

    // public Sprite icon;
    [Header("Base Data (All)")]
    [SerializeField] public ListOfCharactersSO listOfCharacters;
    [SerializeField] public CurlingBroomsSO listOfBrooms;
    [SerializeField] public CurlingStonesSO listOfStones;
    [SerializeField] public CurlingCoursesSO listOfCourses;

    [Header("Course Selection")]
    [SerializeField] public CurlingCourseSO selectedCourse;

    [Header("Rules")]
    [SerializeField] public CurlingRules curlingRules;

    [Header("Team Selection")]
    [SerializeField] public CharacterSO selectedLeftSweeper;
    [SerializeField] public CharacterSO selectedRightSweeper;
    [SerializeField] public CharacterSO selectedThrower;

    [Header("Equipment Selection")]
    [SerializeField] public CurlingBroomSO selectedLeftSweeperBroom;
    [SerializeField] public CurlingBroomSO selectedRightSweeperBroom;
    [SerializeField] public List<GameObject> selectedStones;
    [SerializeField] public GameObject selectedStone1;
    [SerializeField] public GameObject selectedStone2;
    [SerializeField] public GameObject selectedStone3;
    [SerializeField] public GameObject selectedStone4;
    [SerializeField] public GameObject selectedStone5;

    [Header("Temp Demo Functionality")]
    [SerializeField] public CurlingTeam demoTeamHome;
    [SerializeField] public CurlingTeam demoTeamAway;
    [SerializeField] public CurlingCourseData demoCurlingCourseData;

    // [Header("Opponent")]
    // [SerializeField] public bool isOpponentAI;
    // [SerializeField] public bool isOpponentLocal;
    // [SerializeField] public bool isOpponentOnline;

    
    [Header("Completeness")]
    [SerializeField] public bool isReadyToPlay;

    // [Header("Selection Settings")]
    // rules
    // [SerializeField] public bool disableUserRuleSelection = false;
    // // course
    // [SerializeField] public bool disableUserCourseSelection = false;
    // // team
    // [SerializeField] public bool disableUserTeamSelection = false;
    // [SerializeField] public bool disableUserTeamThrowerSelection = false;
    // [SerializeField] public bool disableUserTeamLeftSweeperSelection = false;
    // [SerializeField] public bool disableUserTeamRightSweeperSelection = false;

    // // equipment
    // [SerializeField] public bool disableUserEquipmentSelection = false;


    // [Header("Settings")]
    // [SerializeField] public bool isTeamSelected;
    // [SerializeField] public bool isEquipmentSelected;

    public void Reset()
    {
        // Team
        selectedLeftSweeper = null;
        selectedRightSweeper = null;
        selectedThrower = null;

        // Equipment
        selectedLeftSweeperBroom = null;
        selectedRightSweeperBroom = null;
        selectedStone1 = null;
        selectedStone2 = null;
        selectedStone3 = null;
        selectedStone4 = null;
        selectedStone5 = null;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    public void Update()
    {

    }

    public void LoadData()
    {

    }


    public void StartDemoCurlingGame()
    {
        UICanvasManager.v3.UICanvasManager.Instance.CloseAllCanvases();
        if (selectedLeftSweeper != null){ demoTeamHome.sweeperLeft = selectedLeftSweeper.GameModel; }
        if (selectedRightSweeper != null){ demoTeamHome.sweeperRight = selectedRightSweeper.GameModel; }
        if (selectedThrower != null){ demoTeamHome.thrower = selectedThrower.GameModel; }

        CurlingManager._instance.Setup.SetupAll(
            rulesData: curlingRules,
            courseData: demoCurlingCourseData,
            teamHome: demoTeamHome,
            teamAway: demoTeamAway,

            gameData: new CurlingGameData(),
            exitSceneName: "MainMenu",
            exitSpawnId: "MainMenuSpawn"
        );
    }

    public void StartCurlingGame()
    {
        UICanvasManager.v3.UICanvasManager.Instance.CloseAllCanvases();
        CurlingManager._instance.ResetCurlingGame();
        CurlingManager._instance.Setup.SetupAll(
            rulesData: curlingRules,
            courseData: PrepCourseData(), // courseData,
            teamHome: PrepHomeTeam(),
            teamAway: PrepAwayTeam(),

            gameData: new CurlingGameData(),
            exitSceneName: "MainMenu",
            exitSpawnId: "MainMenuSpawn"
        );
    }

    // private CurlingCourse PrepCourse()
    // {
        

        // return team;
    // }
    private CurlingCourseData PrepCourseData()
    {

        CurlingCourseData courseData = new CurlingCourseData();
        // courseData.courseSO = selectedCourse;
        // courseData.obstacles = selectedObstacles;
        return courseData;
    }

    private CurlingTeam PrepHomeTeam()
    {
        CurlingTeam team = new CurlingTeam();
        team.teamName = "Purple People Eaters";
        team.thrower = selectedThrower.GameModel;
        team.sweeperLeft = selectedLeftSweeper.GameModel;
        team.sweeperRight = selectedRightSweeper.GameModel;

        return team;
    }

    private CurlingTeam PrepAwayTeam()
    {
        CurlingTeam team = new CurlingTeam();
        team.teamName = "Silver Stone Throwers";
        team.thrower = selectedThrower.GameModel;
        team.sweeperLeft = selectedLeftSweeper.GameModel;
        team.sweeperRight = selectedRightSweeper.GameModel;

        return team;
    }

    /************************************************************************************************************************/

    /// <summary>
    /// When an item is selected...
    /// </summary>

    public void OnSelectCharacterForLeftSweeper(CharacterSO lsweeper)
    {
        selectedLeftSweeper = lsweeper;
        // Ensure same character can't be selected for both sweepers   
        if (selectedRightSweeper != null && lsweeper.Id == selectedRightSweeper.Id){
            selectedRightSweeper = null; 
        } 
        // Ensure same character can't be selected for thrower and left sweeper
        if (selectedThrower != null && lsweeper.Id == selectedThrower.Id){
            selectedThrower = null; 
        }
    }

    public void OnSelectCharacterForRightSweeper(CharacterSO rsweeper)
    {
        selectedRightSweeper = rsweeper;
        // Ensure same character can't be selected for both sweepers   
        // Ensure same character can't be selected for thrower and left sweeper
        if (selectedLeftSweeper != null && rsweeper.Id == selectedLeftSweeper.Id){
            selectedLeftSweeper = null; 
        }
        // Ensure same character can't be selected for thrower and left sweeper
        if (selectedThrower != null && rsweeper.Id == selectedThrower.Id){
            selectedThrower = null; 
        }
    }

    public void OnSelectCharacterForThrower(CharacterSO thrower)
    {
        selectedThrower = thrower;
        // Ensure same character can't be selected for both sweepers   
        if (selectedRightSweeper != null && thrower.Id == selectedRightSweeper.Id){
            selectedRightSweeper = null; 
        } 
        // Ensure same character can't be selected for thrower and left sweeper
        if (selectedLeftSweeper != null && thrower.Id == selectedLeftSweeper.Id){
            selectedLeftSweeper = null; 
        }
    }

    /************************************************************************************************************************/

    

    /************************************************************************************************************************/

    public void OnSelectCourse(CurlingCourseSO course)
    {
        selectedCourse = course;
    }


    public void OnSelectBroomForLeftSweeper(CurlingBroomSO broom)
    {
        selectedLeftSweeperBroom = broom;
    }

    public void OnSelectBroomForRightSweeper(CurlingBroomSO broom)
    {
        selectedRightSweeperBroom = broom;
    }


    public void OnSelectStone(
        CurlingStoneSO stone,
        int stoneIndex
    )
    {
        // GameObject obj = listOfStones.Find(b => b.GetComponent<CurlingStone>().id == stoneId);
        // if (stoneIndex == 0){ selectedStone1 = obj; }
        // else if (stoneIndex == 1){ selectedStone2 = obj; }
        // else if (stoneIndex == 2){ selectedStone3 = obj; }
        // else if (stoneIndex == 3){ selectedStone4 = obj; }
        // else if (stoneIndex == 4){ selectedStone5 = obj; }
        

    }

    public void OnRemoveStone(
        CurlingStoneSO stone,
        int stoneIndex
    )
    {
        // GameObject obj = listOfStones.Find(b => b.GetComponent<CurlingStone>().id == stoneId);
        // if (stoneIndex == 0){ selectedStone1 = obj; }
        // else if (stoneIndex == 1){ selectedStone2 = obj; }
        // else if (stoneIndex == 2){ selectedStone3 = obj; }
        // else if (stoneIndex == 3){ selectedStone4 = obj; }
        // else if (stoneIndex == 4){ selectedStone5 = obj; }
        

    }


    /************************************************************************************************************************/

    public void OnSelectRules(
        CurlingRules rules
    )
    {
        this.curlingRules = rules;
    }

    

    /// <summary>
    /// Check for completeness
    /// </summary>
    
    public bool CheckIsReadyToPlay() => 
        IsTeamSelectionComplete() && 
        IsEquipmentSelectionComplete() && 
        IsCurlingRulesSet();

    public bool IsCourseSelected() => selectedCourse != null;

    public bool IsTeamSelectionComplete()
    {
        bool isLeftSweeperSelected = selectedLeftSweeper != null;
        bool isRightSweeperSelected = selectedRightSweeper != null;
        bool isThrowerSelected = selectedThrower != null;
        return isLeftSweeperSelected && isRightSweeperSelected && isThrowerSelected;
    }

    public bool IsEquipmentSelectionComplete()
    {
        bool isLeftSweeperBroomSelected = selectedLeftSweeperBroom != null;
        bool isRightSweeperBroomSelected = selectedRightSweeperBroom != null;
        bool isStonesSelected = selectedStone1 != null && selectedStone2 != null && selectedStone3 != null &&
               selectedStone4 != null && selectedStone5 != null;
        
        return isLeftSweeperBroomSelected && isRightSweeperBroomSelected && isStonesSelected;
               
    }

    public bool IsCurlingRulesSet()
    {
        bool isCurlingRulesCompleted = true;
        return isCurlingRulesCompleted;
    }

    



    /// <summary>
    /// Reset
    /// </summary>
    /// <returns></returns>

    public void OnResetAll()
    {
        OnResetEquipmentSelections();
        OnResetTeamMemberSelections();
        OnResetCourseSelections();
    }

    public void OnResetEquipmentSelections()
    {
        selectedLeftSweeperBroom = null;
        selectedRightSweeperBroom = null;
        selectedStone1 = null;
        selectedStone2 = null;
        selectedStone3 = null;
        selectedStone4 = null;
        selectedStone5 = null;
    }

    public void OnResetTeamMemberSelections()
    {
        selectedLeftSweeper = null;
        selectedRightSweeper = null;
        selectedThrower = null;
    }

    public void OnResetCourseSelections()
    {
        selectedCourse = null;
    }

    /// <summary>
    /// Helper
    /// </summary>
    /// <returns></returns>

    



}








