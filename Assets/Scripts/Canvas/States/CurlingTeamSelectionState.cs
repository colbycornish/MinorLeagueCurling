// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using CurlingUI.v3;
// using PixelCrushers.DialogueSystem;

namespace UICanvasManager.v3
{
    public class CurlingTeamSelectionState : ICanvasState
    {

        public CurlingTeamMemberItem teamMemberThrowerItem;
        public CurlingTeamMemberItem teamMemberLeftSweeperItem;
        public CurlingTeamMemberItem teamMemberRightSweeperItem;

        public GameObject teamCharacterSelectionModal;
        public GameObject teamStonesSelectionModal;
        public GameObject teamBroomLeftSweeperSelectionModal;
        public GameObject teamBroomRightSweeperSelectionModal;
        
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

        // Character Modals
        public void OpenThrowerEditCharacterModal()
        {
            UICanvasManager.OpenTeamCharacterSelectionModal();
            TeamThrowerSelected();
        }

        public void OpenLeftSweeperEditCharacterModal()
        {
            UICanvasManager.OpenTeamCharacterSelectionModal();
            TeamLeftSweeperSelected();
        }

        public void OpenRightSweeperEditCharacterModal()
        {
            UICanvasManager.OpenTeamCharacterSelectionModal();
            TeamRightSweeperSelected();
        }

        // Character Modals
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


        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingTeamSelection;

    }
}