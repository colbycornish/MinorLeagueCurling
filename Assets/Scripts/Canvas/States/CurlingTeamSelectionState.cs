using UnityEngine;
using CurlingUI.v3;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace UICanvasManager.v3
{
    public class CurlingTeamSelectionState : ICanvasState
    {

        /************************************************************************************************************************/    
        [Header("UI Elements")]
        // References to UI elements in the canvas
        public CurlingTeamMemberItem teamMemberThrowerItem;
        public CurlingTeamMemberItem teamMemberLeftSweeperItem;
        public CurlingTeamMemberItem teamMemberRightSweeperItem;

        public GameObject teamCharacterSelectionModal;
        public GameObject teamStonesSelectionModal;
        public GameObject teamBroomLeftSweeperSelectionModal;
        public GameObject teamBroomRightSweeperSelectionModal;

        [Header("Actions/Callbacks")]     
        public Action<CurlingBroomSO> _OnSelectBroomForLeftSweeper => CurlingPreGameSetupManagerV2._instance.OnSelectBroomForLeftSweeper;
        public Action<CurlingBroomSO> _OnSelectBroomForRightSweeper => CurlingPreGameSetupManagerV2._instance.OnSelectBroomForRightSweeper;
        public Action<CurlingStoneSO, int> _OnSelectStone => CurlingPreGameSetupManagerV2._instance.OnSelectStone;
        public Action<CurlingStoneSO, int> _OnRemoveStone => CurlingPreGameSetupManagerV2._instance.OnRemoveStone;

        public Action<CharacterSO> _OnSelectCharacterThrower => CurlingPreGameSetupManagerV2._instance.OnSelectCharacterForThrower;
        public Action<CharacterSO> _OnSelectCharacterLeftSweeper => CurlingPreGameSetupManagerV2._instance.OnSelectCharacterForLeftSweeper;
        public Action<CharacterSO> _OnSelectCharacterRightSweeper => CurlingPreGameSetupManagerV2._instance.OnSelectCharacterForRightSweeper;

        [Header("Temp Data")]
        public CharacterSO selectedCharacterForThrower => CurlingPreGameSetupManagerV2._instance.selectedThrower;
        public CharacterSO selectedCharacterForLeftSweeper => CurlingPreGameSetupManagerV2._instance.selectedLeftSweeper;
        public CharacterSO selectedCharacterForRightSweeper => CurlingPreGameSetupManagerV2._instance.selectedRightSweeper;

        public CurlingBroomSO selectedBroomForLeftSweeper = null;
        public CurlingBroomSO selectedBroomForRightSweeper = null;  
        public List<CurlingStoneSO> selectedStonesForThrower = new List<CurlingStoneSO>();

        [Header("Events")]             
        public UnityEvent _OnCharacterSelectionChanged;

        /************************************************************************************************************************/

        #if UNITY_EDITOR
        override protected void OnValidate()
        {
            // Auto load an attach the dialogues used by this state;
            ICanvasModalState[] listedModalStates = GetComponentsInChildren<ICanvasModalState>(true);
            foreach (var state in listedModalStates)
            {
                if (state.StateCanvasModalType == CanvasModalType.TeamStoneSelection)
                    teamStonesSelectionModal = state.gameObject;
                else if (state.StateCanvasModalType == CanvasModalType.TeamBroomLeftSweeperSelection)
                    teamBroomLeftSweeperSelectionModal = state.gameObject;
                else if (state.StateCanvasModalType == CanvasModalType.TeamBroomRightSweeperSelection)
                    teamBroomRightSweeperSelectionModal = state.gameObject;
                else if (state.StateCanvasModalType == CanvasModalType.TeamCharacterSelection)
                    teamCharacterSelectionModal = state.gameObject;
            }

            // Auto load and attach the team member items used by this state;
            CurlingTeamMemberItem[] listedTeamMemberItems = GetComponentsInChildren<CurlingTeamMemberItem>(true);
            foreach (var teamMemberItem in listedTeamMemberItems)
            {
                if (teamMemberItem.isThrower)
                    teamMemberThrowerItem = teamMemberItem;
                else if (teamMemberItem.isLeftSweeper)
                    teamMemberLeftSweeperItem = teamMemberItem;
                else if (teamMemberItem.isRightSweeper)
                    teamMemberRightSweeperItem = teamMemberItem;
            }

            TeamBroomLeftSweeperSelectionModalState leftSweeperBroomModalState = teamBroomLeftSweeperSelectionModal.GetComponent<TeamBroomLeftSweeperSelectionModalState>();
            leftSweeperBroomModalState._OnSelectBroom = OnSelectBroomForLeftSweeper;            
            // gameObject.GetComponentInParentOrChildren(ref _UICanvasManager);
        }
        #endif

        /************************************************************************************************************************/

        public override void OnEnter()
        {
            gameObject.SetActive(true); 
            TeamLeftSweeperSelected();

            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        /************************************************************************************************************************/

        public override void OnExit()
        {
            gameObject.SetActive(false); // Hide the canvas
            
            // Remove listeners
        }

        /************************************************************************************************************************/
        public void UpdateDisplays()
        {
            UpdateCharacterDisplays();
            UpdateEquipmentDisplays();
        }


        public void UpdateCharacterDisplays()
        {
            teamMemberThrowerItem.UpdateCharacterData(selectedCharacterForThrower);
            teamMemberLeftSweeperItem.UpdateCharacterData(selectedCharacterForLeftSweeper);
            teamMemberRightSweeperItem.UpdateCharacterData(selectedCharacterForRightSweeper);
        }

        public void UpdateEquipmentDisplays()
        {
            teamMemberThrowerItem.UpdateStonesData(selectedStonesForThrower);
            teamMemberLeftSweeperItem.UpdateBroomData(selectedBroomForLeftSweeper);
            teamMemberRightSweeperItem.UpdateBroomData(selectedBroomForRightSweeper);
        }

        /************************************************************************************************************************/

        public void TeamThrowerSelected()
        {
            teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;

            if (UICanvasManager.ModalStateMachine._currentState != null && 
                UICanvasManager.ModalStateMachine._currentState.StateCanvasModalType == CanvasModalType.TeamCharacterSelection)
            {
                Debug.Log("Thrower Item Selected and Character Selection Modal is Open - Opening Thrower Edit Character Modal");
                OpenThrowerEditCharacterModal();
            }
        }
        public void TeamLeftSweeperSelected()
        {
            teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;

            if (UICanvasManager.ModalStateMachine._currentState != null && 
                UICanvasManager.ModalStateMachine._currentState.StateCanvasModalType == CanvasModalType.TeamCharacterSelection)
            {
                Debug.Log("Left Sweeper Item Selected and Character Selection Modal is Open - Opening Left Sweeper Edit Character Modal");
                OpenLeftSweeperEditCharacterModal();
            }
        }
        public void TeamRightSweeperSelected()
        {
            teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = true;

            if (UICanvasManager.ModalStateMachine._currentState != null && 
                UICanvasManager.ModalStateMachine._currentState.StateCanvasModalType == CanvasModalType.TeamCharacterSelection)
            {
                Debug.Log("Right Sweeper Item Selected and Character Selection Modal is Open - Opening Right Sweeper Edit Character Modal");
                OpenRightSweeperEditCharacterModal();
            }
        }

        /************************************************************************************************************************/

        // Character Modals/Dialogues
        public void OpenThrowerEditCharacterModal()
        {
            Debug.Log("Opening Thrower Character Selection Modal");
            TeamCharacterSelectionModalState characterSelectionModalController = teamCharacterSelectionModal.GetComponent<TeamCharacterSelectionModalState>();
            characterSelectionModalController._OnSelectCharacter = OnSelectCharacterForThrower;
            UICanvasManager.OpenTeamCharacterSelectionModal();
            if (teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn == false)
            {
                TeamThrowerSelected();
            }
        }

        public void OpenLeftSweeperEditCharacterModal()
        {
            Debug.Log("Opening Left Sweeper Character Selection Modal");
            TeamCharacterSelectionModalState characterSelectionModalController = teamCharacterSelectionModal.GetComponent<TeamCharacterSelectionModalState>();
            characterSelectionModalController._OnSelectCharacter = OnSelectCharacterForLeftSweeper;
            UICanvasManager.OpenTeamCharacterSelectionModal();
            if (teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn == false)
            {
                TeamLeftSweeperSelected();
            }
            
        }

        public void OpenRightSweeperEditCharacterModal()
        {
            Debug.Log("Opening Right Sweeper Character Selection Modal");
            TeamCharacterSelectionModalState characterSelectionModalController = teamCharacterSelectionModal.GetComponent<TeamCharacterSelectionModalState>();
            characterSelectionModalController._OnSelectCharacter = OnSelectCharacterForRightSweeper;
            UICanvasManager.OpenTeamCharacterSelectionModal();
            
            if (teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn == false)
            {
                TeamRightSweeperSelected();
            }
        }

        // Equipment Modals/Dialogues
        public void OpenThrowerEditEquipmentModal()
        {
            UICanvasManager.OpenTeamStoneSelectionModal();
            TeamThrowerSelected();
        }

        public void OpenLeftSweeperEditEquipmentModal()
        {
            UICanvasManager.OpenTeamBroomLeftSweeperSelectionModal();
            TeamLeftSweeperSelected();
        }

        public void OpenRightSweeperEditEquipmentModal()
        {
            UICanvasManager.OpenTeamBroomRightSweeperSelectionModal();
            TeamRightSweeperSelected();
        }

        /************************************************************************************************************************/
        
        public void OnSelectCharacterForThrower(CharacterSO characterData){
            Debug.Log($"Selected Character for Team Member: {characterData.Id}");
            _OnSelectCharacterThrower?.Invoke(characterData);
            // selectedCharacterForThrower = characterData;
            UpdateDisplays();
            
        }

        public void OnSelectCharacterForLeftSweeper(CharacterSO characterData){
            Debug.Log($"Selected Character for Team Member: {characterData.Id}");
            _OnSelectCharacterLeftSweeper?.Invoke(characterData);
            // selectedCharacterForLeftSweeper = characterData;
            UpdateDisplays();
            
        }

        public void OnSelectCharacterForRightSweeper(CharacterSO characterData){
            Debug.Log($"Selected Character for Team Member: {characterData.Id}");
            _OnSelectCharacterRightSweeper?.Invoke(characterData);
            // selectedCharacterForRightSweeper = characterData;
            UpdateDisplays();
            
        }

        /************************************************************************************************************************/

        public void OnSelectBroomForLeftSweeper(CurlingBroomSO broom){
            Debug.Log($"[Canvas State] Selected Broom for Left Sweeper: {broom.Name}");
            _OnSelectBroomForLeftSweeper?.Invoke(broom);
            selectedBroomForLeftSweeper = broom;
            UpdateEquipmentDisplays();
        }

        public void OnSelectBroomForRightSweeper(CurlingBroomSO broom){
            Debug.Log($"[Canvas State] Selected Broom for Right Sweeper: {broom.Name}");
            _OnSelectBroomForRightSweeper.Invoke(broom);
            selectedBroomForRightSweeper = broom;
            UpdateEquipmentDisplays();
        }

        /************************************************************************************************************************/

        public void OnSelectStoneForThrower(
            string characterId
        ){
            // Debug.Log($"Selected Character for Team Member: {characterId}");
        }

        public void OnRemoveStoneForThrower(
            string characterId
        ){
            
            // Debug.Log($"Removed Character for Team Member: {characterId}");
        }

        /************************************************************************************************************************/
        public void Reset()
        {
            CurlingPreGameSetupManagerV2._instance.selectedThrower = null;
            CurlingPreGameSetupManagerV2._instance.selectedLeftSweeper = null;
            CurlingPreGameSetupManagerV2._instance.selectedRightSweeper = null;
            // selectedCharacterForThrower = null;
            // selectedCharacterForLeftSweeper = null;
            // selectedCharacterForRightSweeper = null;

            selectedBroomForLeftSweeper = null;
            selectedBroomForRightSweeper = null;
            selectedStonesForThrower = new List<CurlingStoneSO>();
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingTeamSelection;

    } 
}