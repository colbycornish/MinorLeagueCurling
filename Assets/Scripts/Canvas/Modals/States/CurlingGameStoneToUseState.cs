using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using CurlingUI.v3;

namespace UICanvasManager.v3
{
    public class CurlingGameStoneToUseState : ICanvasModalState
    {

        [Header("Component References")]
        public GameObject continueButton;
        public GameObject listOfStoneItems;

        public ListOfCurlingStoneItemsAvailableToThrow listOfStoneItemsScript;

        [Header("Data")]
        private bool stoneHasBeenSelected => CurlingManagersV3.CurlingManager._instance.Parameters.Stones.currentStone != null;
        private List<CurlingStone> listOfAvailableStones;
        public Action<CurlingStone> _OnSelectStone => CurlingManager._instance.stoneManager.UpdateCurrentStone;

        /************************************************************************************************************************/
        

        public void OnEnable()
        {
            CurlingManager cm = CurlingManagersV3.CurlingManager._instance;
            
            listOfAvailableStones = cm.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home 
                ? cm.Parameters.Stones.stonesTeamHome 
                : cm.Parameters.Stones.stonesTeamAway;

            listOfStoneItemsScript.listOfAvailableStones = listOfAvailableStones;
            listOfStoneItemsScript._OnSelectStone = OnSelectStone;
            listOfStoneItemsScript.Reset();            
            listOfStoneItemsScript.BuildList();
        }


        public override void OnEnter()
        {
            gameObject.SetActive(true); // Show the canvas
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            // Handle input or logic while in this state
        }

        public override void OnExit()
        {
            gameObject.SetActive(false); // Hide the canvas
            // Remove listeners
        }

        /************************************************************************************************************************/

        public void OnSelectStone(CurlingStone stone)
        {
            Debug.Log($"[Canvas Modal State] Selected Stone Thrower: {stone.name}");
            _OnSelectStone?.Invoke(stone);
        }

        public void OnContinueButtonPressed()
        {
            if (stoneHasBeenSelected)
            {
                CurlingManager cm = CurlingManagersV3.CurlingManager._instance;
                Debug.Log("FROM UI: CM Stone Selection Confirmed, invoking CM method to update current stone");
                cm.OnStoneSelectionConfirmed();
                Debug.Log("FROM UI: Continuing to Aiming and Power Phase");
                cm.ChangePhase(matchPhaseType: CurlingMatchPhase.AimingAndPowerPhase);
            }
            else
            {
                Debug.Log("Please select a stone before continuing.");
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasModalType StateCanvasModalType => CanvasModalType.StoneToUseSelection;

    }
}