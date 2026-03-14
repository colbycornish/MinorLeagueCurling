using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using CurlingUI.v3;

namespace UICanvasManager.v3
{
    public class TeamBroomRightSweeperSelectionModalState : ICanvasModalState
    {

        public CurlingBroomSO selectedBroom;
        public TextMeshProUGUI selectedBroomName;
        public TextMeshProUGUI selectedBroomDescription;
        public Image selectedBroomThumbnail;
        
        public ListOfCurlingBroomItems listOfBroomItems;
        [SerializeField] public Action<CurlingBroomSO> _OnSelectBroom;


        public void Start()
        {
            listOfBroomItems._OnSelectBroom = SelectBroom;
        }

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

        public void SelectBroom(CurlingBroomSO broom)
        {
            Debug.Log($"[Canvas Modal State] Selected Broom for Left Sweeper: {broom.Name}");
            selectedBroom = broom;
            selectedBroomName.text = broom.Name;
            selectedBroomDescription.text = broom.Description;
            selectedBroomThumbnail.sprite = broom.Thumbnail;
            _OnSelectBroom?.Invoke(broom);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasModalType StateCanvasModalType => CanvasModalType.TeamBroomRightSweeperSelection;

    }
}