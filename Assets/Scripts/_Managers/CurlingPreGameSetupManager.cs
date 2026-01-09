using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Curling.Rules;

public class CurlingPreGameSetupManager : MonoBehaviour
{
    public static CurlingPreGameSetupManager _instance;

    // public Sprite icon;
    [Header("Character Data (All)")]
    [SerializeField] public List<GameObject> listOfCharacters;

    [Header("Equipment Data (All)")]
    [SerializeField] public List<GameObject> listOfBrooms;
    [SerializeField] public List<GameObject> listOfStones;

    [Header("Team Selection")]
    [SerializeField] public GameObject selectedLeftSweeper;
    [SerializeField] public GameObject selectedRightSweeper;
    [SerializeField] public GameObject selectedThrower;

    [Header("Equipment Selection")]
    [SerializeField] public GameObject selectedLeftSweeperBroom;
    [SerializeField] public GameObject selectedRightSweeperBroom;
    [SerializeField] public List<GameObject> selectedStones;
    [SerializeField] public GameObject selectedStone1;
    [SerializeField] public GameObject selectedStone2;
    [SerializeField] public GameObject selectedStone3;
    [SerializeField] public GameObject selectedStone4;
    [SerializeField] public GameObject selectedStone5;

    [Header("Opponent")]
    [SerializeField] public bool isOpponentAI;
    [SerializeField] public bool isOpponentLocal;
    [SerializeField] public bool isOpponentOnline;

    [Header("Rules")]
    [SerializeField] public CurlingRules curlingRules;

    [Header("Course Selection")]
    [SerializeField] public List<GameObject> listOfCourses;
    [SerializeField] public GameObject selectedCourse;

    [Header("Completeness")]
    [SerializeField] public bool isReadyToPlay;

    [Header("Selection Settings")]
    // rules
    [SerializeField] public bool disableUserRuleSelection = false;
    // course
    [SerializeField] public bool disableUserCourseSelection = false;
    // team
    [SerializeField] public bool disableUserTeamSelection = false;
    [SerializeField] public bool disableUserTeamThrowerSelection = false;
    [SerializeField] public bool disableUserTeamLeftSweeperSelection = false;
    [SerializeField] public bool disableUserTeamRightSweeperSelection = false;

    // equipment
    [SerializeField] public bool disableUserEquipmentSelection = false;


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

    /// <summary>
    /// When an item is selected...
    /// </summary>

    public void OnSelectLeftSweeper(string characterId)
    {
        Debug.Log($"Selected Left Sweeper: {characterId}");
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        selectedLeftSweeper = obj;

        CheckForCharacterSelectionErrors(
            characterId: characterId,
            checkThrower: true,
            checkRightSweeper: true
        );
    }

    public void OnSelectRightSweeper(string characterId)
    {
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        selectedRightSweeper = obj;

        CheckForCharacterSelectionErrors(
            characterId: characterId,
            checkThrower: true,
            checkLeftSweeper: true
        );
    }

    public void OnSelectThrower(string characterId)
    {
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        selectedThrower = obj;

        CheckForCharacterSelectionErrors(
            characterId: characterId,
            checkLeftSweeper: true,
            checkRightSweeper: true
        );
    }

    public void OnSelectCourse(string courseId)
    {
        Debug.Log("Course Selected: {courseId}");
        GameObject obj = listOfCourses.Find(c => c.GetComponent<CurlingCourseData>().id == courseId);
        selectedCourse = obj;
    }


    public void OnSelectBroom(
        string broomId, 
        bool isLeft = false, 
        bool isRight = false
    )
    {
        GameObject obj = listOfBrooms.Find(b => b.GetComponent<CurlingBroom>().id == broomId);
        if (isLeft){ selectedLeftSweeperBroom = obj; }
        else{ selectedRightSweeperBroom = obj; }
    }


    public void OnSelectStone(
        string stoneId, 
        int stoneIndex
    )
    {
        GameObject obj = listOfStones.Find(b => b.GetComponent<CurlingStone>().id == stoneId);
        if (stoneIndex == 0){ selectedStone1 = obj; }
        else if (stoneIndex == 1){ selectedStone2 = obj; }
        else if (stoneIndex == 2){ selectedStone3 = obj; }
        else if (stoneIndex == 3){ selectedStone4 = obj; }
        else if (stoneIndex == 4){ selectedStone5 = obj; }
        

    }

    public void OnSelectRules(
        CurlingRules rules
    )
    {
        this.curlingRules = rules;
    }

    public void CheckForCharacterSelectionErrors(
        string characterId,
        bool checkLeftSweeper = false,
        bool checkThrower = false,
        bool checkRightSweeper = false
    )
    {
        if (checkLeftSweeper &&
            selectedLeftSweeper != null &&
            characterId == selectedLeftSweeper?.GetComponent<Character>().id)
        {
            selectedLeftSweeper = null;
        }
        if (checkThrower &&
            selectedThrower != null &&
            characterId == selectedThrower?.GetComponent<Character>().id)
        {
            selectedThrower = null;
        }
        if (checkRightSweeper &&
            selectedRightSweeper != null &&
            characterId == selectedRightSweeper?.GetComponent<Character>().id)
        {
            selectedRightSweeper = null;
        }
    }

    /// <summary>
    /// Check for completeness
    /// </summary>
    public bool IsCourseSelected()
    {
        return selectedCourse != null;
    }

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

    public bool CheckIsReadyToPlay()
    {
        isReadyToPlay = IsTeamSelectionComplete() && IsEquipmentSelectionComplete() && IsCurlingRulesSet();
        return isReadyToPlay;
    }


    /// <summary>
    /// Reset
    /// </summary>
    /// <returns></returns>

    public void OnResetAllSelections()
    {
        OnResetEquipmentSelections();
        OnResetTeamMemberSelections();
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

    public Character GetCharacterById(string characterId)
    {
        GameObject obj = listOfCharacters.Find(c => c.GetComponent<Character>().id == characterId);
        return obj.GetComponent<Character>();
    }

    public List<string> GetSelectedCourseIds()
    {
        List<string> listOfIds = new List<string>();// = new Array();
        if (selectedCourse != null)
        {
            listOfIds.Add(selectedCourse.GetComponent<CurlingCourseData>().id);
            
        }
        return listOfIds;
    }

    public List<string> GetSelectedCharacterIds()
    {
        List<string> listOfIds = new List<string>();// = new Array();
        if (selectedLeftSweeper != null)
        {
            string lsId = selectedLeftSweeper.GetComponent<Character>().id;
            listOfIds.Add(lsId);
        }
        if (selectedThrower != null)
        {
            string tId = selectedThrower.GetComponent<Character>().id;
            listOfIds.Add(tId);
        }
        if (selectedRightSweeper != null)
        {
            string rsId = selectedRightSweeper.GetComponent<Character>().id;
            listOfIds.Add(rsId);
        }
        return listOfIds;
    }
    
    



}








