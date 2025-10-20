using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SectionGameSave : MonoBehaviour
{
    [SerializeField] public string helloString;
    // [SerializeField] public ListOfGameSaveSlots listOfGameSaves;

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

    ///
    /// Selection Functions
    /// 
    public void OnSelectGameSaveSlot(string gameSaveId)
    {
        // CurlingPreGameSetupManager._instance.OnSelectCourse(
        //     courseId: courseId
        // );
        UpdateUI();
    }

}
