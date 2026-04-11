using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class StartGameIntroState : IMatchPhaseState
    {
        private InputAction skipCinematicsAction;
        private PlayableDirector director;

        void Awake()
        {
            skipCinematicsAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            
            skipCinematicsAction.performed += OnGoToNextPhase;
            skipCinematicsAction.Enable();
            MainCurlingManager.Players.RepositionCharactersForEpicTeamPose();
            OnEnter();

        }

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllCanvases();
            PlayCourseIntroTimeline();
        }

        /************************************************************************************************************************/
        protected virtual void DisableActions()
        {
            skipCinematicsAction.performed -= OnGoToNextPhase;
            skipCinematicsAction.Disable();
        }

        protected virtual void OnDisable()
        {
            DisableActions();
            director.stopped -= OnTimelineFinished;
        }

        public override void OnExit()
        {
            // director.stopped -= OnTimelineFinished;
            // MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.RoundSplash);
            // Remove listeners
        }

        /************************************************************************************************************************/


        public void PlayCourseIntroTimeline()
        {
            // Debug.Log("StartGameIntroState PlayCourseIntroTimeline: Attempting to play course intro timeline");
            // timeline = GetComponent<PlayableDirector>();
            if (CurlingManager._instance.Parameters.Course.course.courseFullTimeline != null){
                director = CurlingManager._instance.Parameters.Course.course.courseFullTimeline;
                director.Play();
                if (MainCurlingManager.cameraController.stoneCamera != null){
                    MainCurlingManager.cameraController.stoneCamera.enabled = false;
                }
                if (MainCurlingManager.cameraController.ccFollowStoneCamera != null){
                    MainCurlingManager.cameraController.ccFollowStoneCamera.enabled = false;
                }

                director.stopped += OnTimelineFinished;
                return;
            }
            else
            {
                Debug.LogWarning("No Course Intro Timeline Found!");
            }
            
        }

        void OnTimelineFinished(PlayableDirector aDirector)
        {
            aDirector.Stop();
            Debug.Log("Timeline has finished!");
            if (MainCurlingManager.StateMachine.CurrentState.StateMatchPhaseType == CurlingMatchPhase.StartGameIntro)
            {
                OnGoToNextPhase(new InputAction.CallbackContext());
                // MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.TurnSplash);
            }
            
        }

        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {
            Debug.Log("Moving to Next Phase: Round Splash");
            DisableActions();
            director.Stop();
            // MainCurlingManager.Players.RepositionCharactersForCurling();
            if (MainCurlingManager.StateMachine.CurrentState.StateMatchPhaseType == CurlingMatchPhase.StartGameIntro)
            {
                MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.TurnSplash);
            }
        }

        /************************************************************************************************************************/


        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.StartGameIntro;

    }
}