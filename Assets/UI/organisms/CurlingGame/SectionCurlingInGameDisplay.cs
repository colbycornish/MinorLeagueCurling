using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SectionCurlingInGameDisplay : MonoBehaviour
{
    [Header("ScoreBug")]
    [SerializeField] public ScorebugController scoreBug;

    [Header("Sweeper Bars")]
    [SerializeField] public SweeperExhaustionBarV2 leftSweeperExhaustionBar;
    [SerializeField] public SweeperExhaustionBarV2 rightSweeperExhaustionBar;
    

    void Start()
    {
        LoadData();
        leftSweeperExhaustionBar.InitializeDisplay();
    }

    public void Update()
    {
        // Switch item selection right
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftSweeperExhaustionBar.IncreaseExhaustionLevel();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            rightSweeperExhaustionBar.IncreaseExhaustionLevel();
            
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
