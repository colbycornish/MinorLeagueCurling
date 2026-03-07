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
    public class TeamCharacterSelectionModalState : ICanvasModalState
    {

        
        /************************************************************************************************************************/

        public ListOfCurlingCharacterItems listOfCharacterItems;
        [SerializeField] public Action<CharacterSO> _OnSelectCharacter;

        /************************************************************************************************************************/
        public void Start()
        {
            listOfCharacterItems._OnSelectCharacter = SelectCharacter;
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

        public void SelectCharacter(CharacterSO characterData)
        {
            Debug.Log($"[Canvas Modal State] Selected Character for Left Sweeper: {characterData.Name}");
            // selectedCharacter = characterData;
            // selectedCharacterName.text = characterData.Name;
            // selectedCharacterDescription.text = characterData.Description;
            // selectedBroomThumbnail.sprite = broom.Thumbnail;
            _OnSelectCharacter?.Invoke(characterData);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasModalType StateCanvasModalType => CanvasModalType.TeamCharacterSelection;

    }
}