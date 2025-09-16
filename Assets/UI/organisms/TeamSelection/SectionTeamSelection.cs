using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SectionTeamSelection : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] public CharacterSelectionDisplayArea characterSelectionDisplayArea;
    [SerializeField] public SelectedDisplayArea selectedDisplayArea;
    [SerializeField] public ListOfCharacterSquareItems listOfCharacterSquareItems;

    public enum TeamSelectionState
    {
        TeamMembers,
        Equipment
    }

    public enum TeamMemberSelectionState
    {
        LeftSweeper,
        Thrower,
        RightSweeper
    }

    public enum TeamEquipmentSelectionState
    {
        Brooms,
        Stones
    }

    public TeamSelectionState currentTeamSelectionState = TeamSelectionState.TeamMembers;
    public TeamMemberSelectionState currentTeamMemberSelectionState = TeamMemberSelectionState.LeftSweeper;
    public TeamEquipmentSelectionState currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;

    // Time in seconds to complete shrinkage


    void Start()
    {
        StartCoroutine(selectedDisplayArea.leftSweeperItem.Expand());
        selectedDisplayArea.UpdateSelectionDisplays();
        listOfCharacterSquareItems.Init(OnSelect: OnSelectCharacter);
        LoadData();
    }

    //
    public void Update()
    {
        // change the team member selection from LS to T to RS
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (currentTeamMemberSelectionState)
            {
                case TeamMemberSelectionState.Thrower:
                    ChangeTeamMemberSelectionState(TeamMemberSelectionState.LeftSweeper);
                    break;
                case TeamMemberSelectionState.RightSweeper:
                    ChangeTeamMemberSelectionState(TeamMemberSelectionState.Thrower);
                    break;
                default:
                    break;
            }

        }
        // change the team member selection from RS to T to LS
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            switch (currentTeamMemberSelectionState)
            {
                case TeamMemberSelectionState.LeftSweeper:
                    ChangeTeamMemberSelectionState(TeamMemberSelectionState.Thrower);
                    break;
                case TeamMemberSelectionState.Thrower:
                    ChangeTeamMemberSelectionState(TeamMemberSelectionState.RightSweeper);
                    break;
                default:
                    break;
            }
        }
        // Switch item selection right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            listOfCharacterSquareItems.HighlightNext();
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            listOfCharacterSquareItems.HighlightPrev();
            UpdateUI();
        }
    }

    public void LoadData()
    {
        listOfCharacterSquareItems.ClearList();
        listOfCharacterSquareItems.BuildList(
            CurlingPreGameSetupManager._instance.listOfCharacters.ToArray()
        );
    }

    public void UpdateUI()
    {
        listOfCharacterSquareItems.UpdateHighlightedDisplay(
            higlighedIds: CurlingPreGameSetupManager._instance.GetSelectedCharacterIds()
        );

        string characterId = listOfCharacterSquareItems.GetSelectedChildCharacterId();
        Character c = CurlingPreGameSetupManager._instance.GetCharacterById(
            characterId: characterId
        );

        switch (currentTeamMemberSelectionState)
        {
            case TeamMemberSelectionState.LeftSweeper:
                characterSelectionDisplayArea.leftSweeperItem.UpdateInfo(character: c);
                break;
            case TeamMemberSelectionState.Thrower:
                characterSelectionDisplayArea.throwerItem.UpdateInfo(character: c);
                break;
            case TeamMemberSelectionState.RightSweeper:
                characterSelectionDisplayArea.rightSweeperItem.UpdateInfo(character: c);
                break;
            default:
                Debug.Log("Unknown selection");
                break;
        }
        
        // GetCharacterById();

    }




    ///
    /// Selection Functions
    /// 
    public void OnSelectCharacter(string characterId)
    {
        Debug.Log($"ST Selected character with ID: {characterId}");
        switch (currentTeamMemberSelectionState)
        {
            case TeamMemberSelectionState.LeftSweeper:
                CurlingPreGameSetupManager._instance.OnSelectLeftSweeper(characterId);
                break;
            case TeamMemberSelectionState.Thrower:
                CurlingPreGameSetupManager._instance.OnSelectThrower(characterId);
                break;
            case TeamMemberSelectionState.RightSweeper:
                CurlingPreGameSetupManager._instance.OnSelectRightSweeper(characterId);
                break;
            default:
                Debug.Log("Unknown selection");
                break;
        }
        selectedDisplayArea.UpdateSelectionDisplays();
        UpdateUI();
    }


    public void OnSelectBroom(string broomId)
    {
        Debug.Log($"ST Selected character with ID: {broomId} at index");
        switch (currentTeamMemberSelectionState)
        {
            case TeamMemberSelectionState.LeftSweeper:
                CurlingPreGameSetupManager._instance.OnSelectBroom(broomId: broomId, isLeft: true, isRight: false);
                break;
            case TeamMemberSelectionState.RightSweeper:
                CurlingPreGameSetupManager._instance.OnSelectBroom(broomId: broomId, isLeft: false, isRight: true);
                break;
            case TeamMemberSelectionState.Thrower:
                break;
            default:
                Debug.Log("Unknown selection");
                break;
        }
    }

    public void OnSelectStone(string stoneId)
    {
        Debug.Log($"Stone selected: {stoneId}");
        
    }


    ///
    /// State
    /// 

    public void ChangeTeamSelectionState(TeamSelectionState newState)
    {
        currentTeamSelectionState = newState;
        switch (currentTeamSelectionState)
        {
            case TeamSelectionState.TeamMembers:
                // Show character selection
                listOfCharacterSquareItems.gameObject.SetActive(true);
                break;
            case TeamSelectionState.Equipment:
                // Show equipment selection
                listOfCharacterSquareItems.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void ChangeTeamMemberSelectionState(TeamMemberSelectionState newState)
    {
        int currentIndex = (int)currentTeamMemberSelectionState;
        int newIndex = (int)newState;
        int modifyIndex = newIndex - currentIndex;

        currentTeamMemberSelectionState = newState;

        characterSelectionDisplayArea.ChangeSelection(modifyIndex);
        selectedDisplayArea.ChangeSelection(modifyIndex);
    }
    
    public void ChangeTeamEquipmentSelectionState(TeamEquipmentSelectionState newState)
    {
        currentTeamEquipmentSelectionState = newState;
        switch (currentTeamEquipmentSelectionState)
        {
            case TeamEquipmentSelectionState.Brooms:
                // Show broom selection
                break;
            case TeamEquipmentSelectionState.Stones:
                // Show stone selection
                break;
            default:
                break;
        }
    }
}
