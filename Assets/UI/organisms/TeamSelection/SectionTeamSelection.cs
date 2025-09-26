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
    [SerializeField] public GameObject listOfBroomItems;
    [SerializeField] public GameObject listOfStoneItems;

    public enum TeamMemberSelectionState
    {
        LeftSweeper,
        Thrower,
        RightSweeper
    }

    public enum TeamSelectionState
    {
        TeamMembers,
        Equipment
    }

    public enum TeamEquipmentSelectionState
    {
        Brooms,
        Stones
    }

    public TeamSelectionState currentTeamSelectionState = TeamSelectionState.TeamMembers;
    public TeamMemberSelectionState currentTeamMemberSelectionState = TeamMemberSelectionState.LeftSweeper;
    public TeamEquipmentSelectionState currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;
    public int stoneSelectionIndex = 0;
    public int maxStonesAllowed = 5;
    // Time in seconds to complete shrinkage


    void Start()
    {
        selectedDisplayArea.leftSweeperItem.SetSelected(status: true);
        selectedDisplayArea.UpdateSelectionDisplays();
        listOfCharacterSquareItems.Init(OnSelect: OnSelectCharacter);
        UpdateSelectedAreaDisplayItems();
        LoadData();
    }

    public void LoadData()
    {
        listOfCharacterSquareItems.ClearList();
        listOfCharacterSquareItems.BuildList(
            CurlingPreGameSetupManager._instance.listOfCharacters.ToArray()
        );

        // listOfBroomItems.BuildList(
        //     CurlingPreGameSetupManager._instance.listOfBrooms.ToArray()
        // );

        // listOfStoneItems.BuildList(
        //     CurlingPreGameSetupManager._instance.listOfStones.ToArray()
        // );
    }

    // private void OnEnable()
    // {
    //     if (CanvasManager._instance.canvasDisplayAreaController == null) return;
    //     CanvasManager._instance.canvasDisplayAreaController.OnCharactersLoaded += LoadData;
    // }

    //
    public void Update()
    {
        // change the team member selection from LS to T to RS
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (currentTeamMemberSelectionState)
            {
                case TeamMemberSelectionState.Thrower:
                    EditLeftSweeper();
                    break;
                case TeamMemberSelectionState.RightSweeper:
                    EditThrower();
                    break;
                default:
                    break;
            }
            UpdateUI();
        }

        // change the team member selection from RS to T to LS
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            switch (currentTeamMemberSelectionState)
            {
                case TeamMemberSelectionState.LeftSweeper:
                    EditThrower();
                    break;
                case TeamMemberSelectionState.Thrower:
                    EditRightSweeper();
                    break;
                default:
                    break;
            }
            UpdateUI();
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



        // Switch item selection right
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (currentTeamMemberSelectionState)
            {
                case TeamMemberSelectionState.LeftSweeper:
                    EditLeftSweeperCharacter();
                    break;
                case TeamMemberSelectionState.RightSweeper:
                    EditRightSweeperCharacter();
                    break;
                case TeamMemberSelectionState.Thrower:
                    if (currentTeamSelectionState == TeamSelectionState.Equipment && stoneSelectionIndex > 0) {
                        stoneSelectionIndex -= 1;
                        stoneSelectionIndex = Mathf.Clamp(stoneSelectionIndex, 0, maxStonesAllowed - 1);
                        UpdateSelectedAreaDisplayItems();
                    }
                    else {
                        stoneSelectionIndex = -1;
                        EditThrowerCharacter();
                    }
                    break;
                default:
                    break;
            }
            
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (currentTeamMemberSelectionState)
            {
                case TeamMemberSelectionState.LeftSweeper:
                    EditLeftSweeperEquipment();
                    break;
                case TeamMemberSelectionState.RightSweeper:
                    EditRightSweeperEquipment();
                    break;
                case TeamMemberSelectionState.Thrower:
                    if (currentTeamSelectionState == TeamSelectionState.TeamMembers){
                        stoneSelectionIndex = 0;
                        EditThrowerEquipment();
                    }
                    else {
                        stoneSelectionIndex += 1;
                        stoneSelectionIndex = Mathf.Clamp(stoneSelectionIndex, 0, maxStonesAllowed - 1);
                        UpdateSelectedAreaDisplayItems();
                    }
                    break;
                default:
                    break;
            }
            UpdateUI();
        }
    }

    

    public void UpdateUI()
    {
        // update highlighted lists
        listOfCharacterSquareItems.UpdateHighlightedDisplay(
            higlighedIds: CurlingPreGameSetupManager._instance.GetSelectedCharacterIds()
        );

        // update selection info displays
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
    }




    public void EditLeftSweeper(){
        currentTeamMemberSelectionState = TeamMemberSelectionState.LeftSweeper;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;
        UpdateSelectedAreaDisplayItems();
    }


    public void EditLeftSweeperCharacter(){
        currentTeamSelectionState = TeamSelectionState.TeamMembers;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;
        EditLeftSweeper();
    }

    public void EditLeftSweeperEquipment(){
        currentTeamSelectionState = TeamSelectionState.Equipment;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;
        EditLeftSweeper();
    }

    /// right sweeper
    public void EditRightSweeper(){
        currentTeamMemberSelectionState = TeamMemberSelectionState.RightSweeper;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;
        UpdateSelectedAreaDisplayItems();
    }

    public void EditRightSweeperCharacter(){
        currentTeamSelectionState = TeamSelectionState.TeamMembers;
        EditRightSweeper();
    }

    public void EditRightSweeperEquipment(){
        currentTeamSelectionState = TeamSelectionState.Equipment;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Brooms;
        EditRightSweeper();
        
    }

    /// thrower
    public void EditThrower(){
        currentTeamMemberSelectionState = TeamMemberSelectionState.Thrower;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Stones;
        UpdateSelectedAreaDisplayItems();
    }

    public void EditThrowerCharacter(){
        currentTeamSelectionState = TeamSelectionState.TeamMembers;
        EditThrower();
        
    }

    public void EditThrowerEquipment(){
        currentTeamSelectionState = TeamSelectionState.Equipment;
        currentTeamEquipmentSelectionState = TeamEquipmentSelectionState.Stones;
        EditThrower();
    }


    public void UpdateSelectedAreaDisplayItems(){
        // team member cards
        bool isLeftSweeperHighlighted = currentTeamMemberSelectionState == TeamMemberSelectionState.LeftSweeper;
        bool isRightSweeperHighlighted = currentTeamMemberSelectionState == TeamMemberSelectionState.RightSweeper;
        bool isThrowerHighlighted = currentTeamMemberSelectionState == TeamMemberSelectionState.Thrower;
        bool isLeftSweeperEquipmentHighlighted = (
            currentTeamMemberSelectionState == TeamMemberSelectionState.LeftSweeper &&
            currentTeamSelectionState == TeamSelectionState.Equipment
        );
        bool isRightSweeperEquipmentHighlighted = (
            currentTeamMemberSelectionState == TeamMemberSelectionState.RightSweeper &&
            currentTeamSelectionState == TeamSelectionState.Equipment
        );
        bool isThrowerEquipmentHighlighted = (
            currentTeamMemberSelectionState == TeamMemberSelectionState.Thrower &&
            currentTeamSelectionState == TeamSelectionState.Equipment
        );
        
        selectedDisplayArea.leftSweeperItem.SetHighlighted(isLeftSweeperHighlighted);
        selectedDisplayArea.throwerItem.SetHighlighted(isThrowerHighlighted);
        selectedDisplayArea.rightSweeperItem.SetHighlighted(isRightSweeperHighlighted);

        selectedDisplayArea.leftSweeperItem.SetSelected(isLeftSweeperHighlighted);
        selectedDisplayArea.throwerItem.SetSelected(isThrowerHighlighted);
        selectedDisplayArea.rightSweeperItem.SetSelected(isRightSweeperHighlighted);


        // team member card equipment
        selectedDisplayArea.leftSweeperItem.equipment.SetSelected(
            status: isLeftSweeperEquipmentHighlighted
        );
        selectedDisplayArea.throwerItem.equipment.SetSelected(
            status: isThrowerEquipmentHighlighted,
            selectedIndex: stoneSelectionIndex
        );
        selectedDisplayArea.rightSweeperItem.equipment.SetSelected(
            status: isRightSweeperEquipmentHighlighted
        );


        selectedDisplayArea.leftSweeperItem.equipment.SetHighlighted(
            status: isLeftSweeperEquipmentHighlighted
        );
        selectedDisplayArea.throwerItem.equipment.SetHighlighted(
            status: isThrowerEquipmentHighlighted,
            highlightedIndex: stoneSelectionIndex
        );
        selectedDisplayArea.rightSweeperItem.equipment.SetHighlighted(
            status: isRightSweeperEquipmentHighlighted
        );

        ///
        if (currentTeamSelectionState == TeamSelectionState.TeamMembers){
            ShowListOfCharacters();
        }
        if (currentTeamSelectionState == TeamSelectionState.Equipment){
            if (isLeftSweeperEquipmentHighlighted || isRightSweeperEquipmentHighlighted){
                ShowListOfBrooms();
            }
            else {
                ShowListOfStones();
            }
        }

    }


    ///
    /// Equipement
    ///  
    public void ShowListOfBrooms(){
        listOfCharacterSquareItems.gameObject.SetActive(false);
        listOfBroomItems.SetActive(true);
        listOfStoneItems.SetActive(false);
    }

    public void ShowListOfCharacters(){
        listOfCharacterSquareItems.gameObject.SetActive(true);
        listOfBroomItems.SetActive(false);
        listOfStoneItems.SetActive(false);
    }
    
    public void ShowListOfStones(){
        listOfCharacterSquareItems.gameObject.SetActive(false);
        listOfBroomItems.SetActive(false);
        listOfStoneItems.SetActive(true);
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

        Debug.Log($"Stone selected: {stoneId} | Stone Index: {stoneSelectionIndex}");
    }

}
