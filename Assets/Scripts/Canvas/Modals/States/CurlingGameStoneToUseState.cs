using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UICanvasManager.v3
{
    public class CurlingGameStoneToUseState : ICanvasModalState
    {

        public GameObject listOfStoneItems;
        [SerializeField] public Action<CurlingStoneSO> _OnSelectStone;

        /************************************************************************************************************************/

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

        public void SelectStone(CurlingStoneSO stone)
        {
            Debug.Log($"[Canvas Modal State] Selected Stone for Left Sweeper: {stone.Name}");
            // selectedStone = stone;
            // selectedStoneName.text = stone.Name;
            // selectedStoneDescription.text = stone.Description;
            // selectedStoneThumbnail.sprite = stone.Thumbnail;
            _OnSelectStone?.Invoke(stone);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasModalType StateCanvasModalType => CanvasModalType.StoneToUseSelection;

    }
}