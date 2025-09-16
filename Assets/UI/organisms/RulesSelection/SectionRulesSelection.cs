using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SectionRulesSelection : MonoBehaviour
{
    // public Sprite icon;
    // [SerializeField] public CharacterSelectionDisplayArea characterSelectionDisplayArea;
    // [SerializeField] public SelectedDisplayArea selectedDisplayArea;
    [SerializeField] public ListOfCourseItems listOfCourses;

    

    void Start()
    {
        // StartCoroutine(selectedDisplayArea.leftSweeperItem.Expand());
        // selectedDisplayArea.UpdateSelectionDisplays();
        // listOfCourses.Init(OnSelect: OnSelectCourse);
        // LoadData();
    }

    //
    public void Update()
    {
        // Switch item selection right
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     listOfCourses.HighlightNext();
        //     UpdateUI();
        // }
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     listOfCourses.HighlightPrev();
        //     UpdateUI();
        // }
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
        // listOfCharacterSquareItems.UpdateHighlightedDisplay(
        //     higlighedIds: CurlingPreGameSetupManager._instance.GetSelectedCharacterIds()
        // );

        // string characterId = listOfCharacterSquareItems.GetSelectedChildCharacterId();
        // Character c = CurlingPreGameSetupManager._instance.GetCharacterById(
        //     characterId: characterId
        // );

        // switch (currentTeamMemberSelectionState)
        // {
        //     case TeamMemberSelectionState.LeftSweeper:
        //         characterSelectionDisplayArea.leftSweeperItem.UpdateInfo(character: c);
        //         break;
        //     case TeamMemberSelectionState.Thrower:
        //         characterSelectionDisplayArea.throwerItem.UpdateInfo(character: c);
        //         break;
        //     case TeamMemberSelectionState.RightSweeper:
        //         characterSelectionDisplayArea.rightSweeperItem.UpdateInfo(character: c);
        //         break;
        //     default:
        //         Debug.Log("Unknown selection");
        //         break;
        // }
        
        // GetCharacterById();

    }




    ///
    /// Selection Functions
    /// 
    public void OnUpdateRules()
    {
    //     CurlingPreGameSetupManager._instance.OnSelectCourse(
    //         courseId: courseId
    //     );
    }



    ///
    /// State
    /// 


}
