// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using CurlingUI.v3;
using System;
using System.Collections.Generic;
// using PixelCrushers.DialogueSystem;

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
        public Action<CurlingBroomSO> _OnSelectBroomForLeftSweeper;
        public Action<CurlingBroomSO> _OnSelectBroomForRightSweeper;
        public Action<CurlingBroomSO> _OnSelectStone;
        public Action<CurlingBroomSO> _OnRemoveStone;

        public Action<CurlingBroomSO> _OnSelectCharacterThrower;
        public Action<CurlingBroomSO> _OnSelectCharacterLeftSweeper;
        public Action<CurlingBroomSO> _OnSelectCharacterRightSweeper;   

        [Header("Temp Data")]
        private CharacterSO selectedCharacterForThrower = null;
        private CharacterSO selectedCharacterForLeftSweeper = null;
        private CharacterSO selectedCharacterForRightSweeper = null;

        private CurlingBroomSO selectedBroomForLeftSweeper = null;
        private CurlingBroomSO selectedBroomForRightSweeper = null;  
        private List<CurlingStoneSO> selectedStonesForThrower = new List<CurlingStoneSO>();             

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

        public override void OnUpdate()
        {
            // Handle input or logic while in this state
        }

        /************************************************************************************************************************/

        public void UpdateDisplays()
        {
            teamMemberThrowerItem.UpdateCharacterData(selectedCharacterForThrower);
            teamMemberLeftSweeperItem.UpdateCharacterData(selectedCharacterForLeftSweeper);
            teamMemberRightSweeperItem.UpdateCharacterData(selectedCharacterForRightSweeper);
            //
            teamMemberThrowerItem.UpdateStonesData(selectedStonesForThrower);
            //
            teamMemberLeftSweeperItem.UpdateBroomData(selectedBroomForLeftSweeper);
            teamMemberRightSweeperItem.UpdateBroomData(selectedBroomForRightSweeper);
            // Handle input or logic while in this state
        }

        /************************************************************************************************************************/

        public void TeamThrowerSelected()
        {
            teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        public void TeamLeftSweeperSelected()
        {
            teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        public void TeamRightSweeperSelected()
        {
            teamMemberThrowerItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberLeftSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            teamMemberRightSweeperItem.gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
        }

        /************************************************************************************************************************/

        // Character Modals/Dialogues
        public void OpenThrowerEditCharacterModal()
        {
            TeamCharacterSelectionModalState characterSelectionModalController = teamCharacterSelectionModal.GetComponent<TeamCharacterSelectionModalState>();
            characterSelectionModalController._OnSelectCharacter = OnSelectCharacterForThrower;
            UICanvasManager.OpenTeamCharacterSelectionModal();
            TeamThrowerSelected();
        }

        public void OpenLeftSweeperEditCharacterModal()
        {
            TeamCharacterSelectionModalState characterSelectionModalController = teamCharacterSelectionModal.GetComponent<TeamCharacterSelectionModalState>();
            characterSelectionModalController._OnSelectCharacter = OnSelectCharacterForLeftSweeper;
            UICanvasManager.OpenTeamCharacterSelectionModal();
            TeamLeftSweeperSelected();
        }

        public void OpenRightSweeperEditCharacterModal()
        {
            TeamCharacterSelectionModalState characterSelectionModalController = teamCharacterSelectionModal.GetComponent<TeamCharacterSelectionModalState>();
            characterSelectionModalController._OnSelectCharacter = OnSelectCharacterForRightSweeper;
            UICanvasManager.OpenTeamCharacterSelectionModal();
            TeamRightSweeperSelected();
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
            selectedCharacterForThrower = characterData;
            UpdateDisplays();
        }

        public void OnSelectCharacterForLeftSweeper(CharacterSO characterData){
            Debug.Log($"Selected Character for Team Member: {characterData.Id}");
            selectedCharacterForLeftSweeper = characterData;
            UpdateDisplays();
        }

        public void OnSelectCharacterForRightSweeper(CharacterSO characterData){
            Debug.Log($"Selected Character for Team Member: {characterData.Id}");
            selectedCharacterForRightSweeper = characterData;
            UpdateDisplays();
        }

        /************************************************************************************************************************/

        public void OnSelectBroomForLeftSweeper(CurlingBroomSO broom){
            Debug.Log($"[Canvas State] Selected Broom for Left Sweeper: {broom.Name}");
            _OnSelectBroomForLeftSweeper?.Invoke(broom);
            selectedBroomForLeftSweeper = broom;
            UpdateDisplays();
        }

        public void OnSelectBroomForRightSweeper(CurlingBroomSO broom){
            Debug.Log($"[Canvas State] Selected Broom for Right Sweeper: {broom.Name}");
            _OnSelectBroomForRightSweeper.Invoke(broom);
            selectedBroomForRightSweeper = broom;
            UpdateDisplays();
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
            selectedCharacterForThrower = null;
            selectedCharacterForLeftSweeper = null;
            selectedCharacterForRightSweeper = null;

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