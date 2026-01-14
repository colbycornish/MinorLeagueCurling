using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class SectionCurlingGameTeamStoneSelection : MonoBehaviour
{
    [SerializeField] public string helloString;
    [SerializeField] public ListOfStoneSquareItems listOfCurlingStones;

    void Start()
    {
        LoadData();
    }

    public void Update()
    {
        // Switch item selection right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // listOfCourses.HighlightNext();
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // listOfCourses.HighlightPrev();
            UpdateUI();
        }
    }

    public void LoadData()
    {
        // listOfCourses.ClearList();
        // listOfCourses.BuildList(
        //     CurlingPreGameSetupManager._instance.listOfCourses.ToArray()
        // );
    }

    public void UpdateUI()
    {
        // listOfCourses.UpdateSelectedDisplay(
            // selectedIds: CurlingPreGameSetupManager._instance.GetSelectedCourseIds()
        // );
    }

    public void DisplayActiveListOfStones(){
        CurlingManagersV3.CurlingManager cm = CurlingManagersV3.CurlingManager._instance;
        if(cm == null) return;

        List<CurlingStone> stones = cm.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home 
            ? cm.Parameters.Stones.stonesTeamHome
            : cm.Parameters.Stones.stonesTeamAway;
        
        
        if(stones == null) return;

        listOfCurlingStones.ClearList();
        // listOfCurlingStones.BuildList(
        //     stones.ConvertAll(s => s.id).ToArray()
        // );
        listOfCurlingStones.UpdateUI();
    }

    ///
    /// Selection Functions
    /// 
    public void OnSelectStone(string stoneId)
    {
        // CurlingPreGameSetupManager._instance.OnSelectCourse(
            // courseId: courseId
        // );
        UpdateUI();
    }

}
