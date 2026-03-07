using System.Collections.Generic;
using UnityEngine;


namespace UICanvasManager.v3
{
    [DefaultExecutionOrder(-10000)]// Initialize the StateMachine before anything uses it.
    public class UICanvasManager : MonoBehaviour
    {
        [SerializeField]
        private UICanvasStateMachine _StateMachine = new UICanvasStateMachine();
        public UICanvasStateMachine StateMachine => _StateMachine;

        [SerializeField]
        private UICanvasModalStateMachine _ModalStateMachine = new UICanvasModalStateMachine();
        public UICanvasModalStateMachine ModalStateMachine => _ModalStateMachine;

        [SerializeField]
        private List<ICanvasState> _ListOfCanvasStates = new List<ICanvasState>();
        public List<ICanvasState> ListOfCanvasStates => _ListOfCanvasStates;

        [SerializeField]
        private List<ICanvasModalState> _ListOfCanvasModalStates = new List<ICanvasModalState>();
        public List<ICanvasModalState> ListOfCanvasModalStates => _ListOfCanvasModalStates;

        private bool _ModalIsOpen => ModalStateMachine.CurrentState != null;
        public bool ModalIsOpen => _ModalIsOpen;

        

        protected virtual void Awake()
        {
            _StateMachine.InitializeAfterDeserialize();
        }

        
        #if UNITY_EDITOR
        protected void OnValidate()
        {
            ICanvasState[] listedStates = GetComponentsInChildren<ICanvasState>(true);
            _ListOfCanvasStates = new List<ICanvasState>(listedStates);

            ICanvasModalState[] listedModalStates = GetComponentsInChildren<ICanvasModalState>(true);
            _ListOfCanvasModalStates = new List<ICanvasModalState>(listedModalStates);
        }
        #endif

        public void GoBack() => _StateMachine.GoBack();

        

        /// <summary>
        /// General function for opening a canvas section based on its CanvasType.
        /// </summary>
        public void OpenSection(CanvasType canvasType)
        {
            foreach (ICanvasState state in _ListOfCanvasStates){
                if (state.StateCanvasType == canvasType)
                {
                    Debug.Log($"Opening canvas section: {canvasType}");
                    if (_StateMachine == null)
                    {
                        Debug.LogError("StateMachine reference is null in UICanvasManager.");
                        return;
                    }
                    _StateMachine.ChangeState(state);
                    return;
                }
            }
        }

        public void OpenModal(CanvasModalType canvasModalType)
        {
            foreach (ICanvasModalState state in _ListOfCanvasModalStates){
                if (state.StateCanvasModalType == canvasModalType)
                {
                    Debug.Log($"Opening canvas modal: {canvasModalType}");
                    if (_ModalStateMachine == null)
                    {
                        Debug.LogError("ModalStateMachine reference is null in UICanvasManager.");
                        return;
                    }
                    _ModalStateMachine.ChangeState(state);
                    return;
                }
            }
        }

        /// <summary>
        /// reference functions for opening specific canvas sections, to be used by buttons and other UI elements. 
        /// These just call the more general OpenSection function with the appropriate CanvasType, 
        /// which allows for more flexibility in how the sections are opened while still providing easy-to-use 
        /// functions for common actions.
        /// </summary>

        public void OpenMainMenu() => OpenSection(canvasType: CanvasType.MainMenu);
        /// 
        public void OpenControlsSection() => OpenSection(canvasType: CanvasType.Controls);
        //
        public void OpenExitGameSection() => OpenSection(canvasType: CanvasType.ExitGame);
        public void OpenLoadGameSection() => OpenSection(canvasType: CanvasType.LoadGame);
        public void OpenNewGameSection() => OpenSection(canvasType: CanvasType.NewGame);
        public void OpenSaveGameSection() => OpenSection(canvasType: CanvasType.SaveGame);
        //
        public void OpenGameSettingSection() => OpenSection(canvasType: CanvasType.GameSettings);
        //
        public void OpenCurlingCourseSelectionSection() => OpenSection(canvasType: CanvasType.CurlingCourseSelection);
        public void OpenCurlingTeamSelectionSection() => OpenSection(canvasType: CanvasType.CurlingTeamSelection);
        public void OpenCurlingRulesSection() => OpenSection(canvasType: CanvasType.CurlingRules);
        //
        public void OpenCurlingGameSplashTurnDisplaySection() => OpenSection(canvasType: CanvasType.CurlingGameSplashTurnDisplay);
        public void OpenCurlingGameFinalResultDisplaySection() => OpenSection(canvasType: CanvasType.CurlingGameFinalResultDisplay);

        public void CloseAllCanvases() => _StateMachine.ChangeState(null);
        public void CloseAllModals() => _ModalStateMachine.ChangeState(null);
        



        /// <summary>
        /// General function for opening a dialogue section based on its CanvasModalType.
        /// </summary>
        /// 
        public void OpenTeamCharacterSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamCharacterSelection);
        public void OpenTeamStoneSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamStoneSelection);
        public void OpenTeamBroomLeftSweeperSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamBroomLeftSweeperSelection);
        public void OpenTeamBroomRightSweeperSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamBroomRightSweeperSelection);
        public void OpenStoneToUseSelectionModal() => OpenModal(canvasModalType: CanvasModalType.StoneToUseSelection);
        

        /// <summary>
        /// Additional quick reference functions for opening specific sections, to be used by buttons and other UI elements.
        /// </summary>

        public void PauseGame()
        {
            OpenSection(canvasType: CanvasType.PauseMenu);
        }

        /// <summary>
        /// Manual Triggering of canvas sections for testing purposes. Can be removed or replaced with a more robust input handling system later.
        /// </summary>

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (_StateMachine.CurrentState == null)
                {
                    OpenSection(canvasType: CanvasType.MainMenu); // Attempt to open the pause menu even if the state machine is missing, to provide some feedback.
                    return;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (_StateMachine.CurrentState == null)
                {
                    OpenSection(canvasType: CanvasType.PauseMenu); // Attempt to open the pause menu even if the state machine is missing, to provide some feedback.
                    return;
                }
                
            }
        }

        


    }
}