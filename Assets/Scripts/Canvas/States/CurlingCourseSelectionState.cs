using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CurlingUI.v3;

namespace UICanvasManager.v3
{
    public class CurlingCourseSelectionState : ICanvasState
    {
        // public GameObject mainMenuCanvas;
        [Header("Course Selection UI Elements")]
        public Image selectedCourseBkgImage;
        public TextMeshProUGUI selectedCourseText;
        public CurlingCourseSO selectedCourse;

        public ListOfCurlingCourseItems listOfCoursesController;

        public void Start()
        {
            listOfCoursesController._OnSelectCourse = OnSelectCourse;
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

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingCourseSelection;

        /************************************************************************************************************************/

        /// Useful functions
        public void OnSelectCourse(CurlingCourseSO course)
        {
            selectedCourse = course;
            UpdateSelectedCourseDisplay();
        }

        public bool IsCourseSelected()
        {
            return selectedCourse != null;
        }

        /************************************************************************************************************************/

        public void UpdateSelectedCourseDisplay()
        {
            selectedCourseBkgImage.sprite = selectedCourse.Thumbnail;
            selectedCourseText.text = selectedCourse.Title;
        }

    }
}