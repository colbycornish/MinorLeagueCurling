using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UICanvasManager.v3
{
    [DefaultExecutionOrder(-10000)]// Initialize the StateMachine before anything uses it.
    public class UICanvasManager : Singleton<UICanvasManager>
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

        [Header("Events")]
        [SerializeField] private UnityEvent _OnOpenModal; // See the Read Me.
        [SerializeField] private UnityEvent _OnCloseModal; // See the Read Me. 
        [SerializeField] private UnityEvent _OnOpenSection; // See the Read Me.
        [SerializeField] private UnityEvent _OnCloseSection; // See the Read Me.
        [SerializeField] private UnityEvent _CloseAllCanvases; // See the Read Me.

        [Header("Input Actions")]
        private InputAction openPauseMenuAction;
        private InputAction openMainMenuAction;
        private InputAction openCompanySplashAction;

        

        protected virtual void Awake()
        {
            _StateMachine.InitializeAfterDeserialize();
            openPauseMenuAction = InputSystem.actions.FindAction("Pause", true);
            openMainMenuAction = InputSystem.actions.FindAction("DevOpenMainMenu", true);
            openCompanySplashAction = InputSystem.actions.FindAction("DevOpenCompanySplashScreens", true);
        }

        /// <summary>
        /// Manual Triggering of canvas sections for testing purposes. Can be removed or replaced with a more robust input handling system later.
        /// </summary>
        protected virtual void OnEnable()
        {
            openPauseMenuAction.performed += OpenPauseMenuFromInputAction;
            openPauseMenuAction.Enable();

            openMainMenuAction.performed += OpenMainMenuFromInputAction;
            openMainMenuAction.Enable();

            openCompanySplashAction.performed += OpenCompanySplashScreensFromInputAction;
            openCompanySplashAction.Enable();
        }

        protected virtual void OnDisable()
        {
            openPauseMenuAction.performed -= OpenPauseMenuFromInputAction;
            openPauseMenuAction.Disable();

            openMainMenuAction.performed -= OpenMainMenuFromInputAction;
            openMainMenuAction.Disable();

            openCompanySplashAction.performed -= OpenCompanySplashScreensFromInputAction;
            openCompanySplashAction.Disable();
        }

        // public void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.M))
        //     {
        //         if (_StateMachine.CurrentState == null)
        //         {
        //             OpenSection(canvasType: CanvasType.MainMenu); // Attempt to open the pause menu even if the state machine is missing, to provide some feedback.
        //             return;
        //         }
                
        //     }

        //     if (Input.GetKeyDown(KeyCode.B))
        //     {
        //         if (_StateMachine.CurrentState == null)
        //         {
        //             OpenSection(canvasType: CanvasType.SplashScreenBurnoutGames); 
        //             return;
        //         }
                
        //     }

        //     if (Input.GetKeyDown(KeyCode.P))
        //     {
        //         if (_StateMachine.CurrentState == null)
        //         {
        //             OpenSection(canvasType: CanvasType.PauseMenu); // Attempt to open the pause menu even if the state machine is missing, to provide some feedback.
        //             return;
        //         }
                
        //     }
        // }

        
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

        /************************************************************************************************************************/

        /// <summary>
        /// reference functions for opening specific canvas sections, to be used by buttons and other UI elements. 
        /// These just call the more general OpenSection function with the appropriate CanvasType, 
        /// which allows for more flexibility in how the sections are opened while still providing easy-to-use 
        /// functions for common actions.
        /// </summary>
        public void OpenSplashMinorLeagueCurling() => OpenSection(canvasType: CanvasType.SplashScreenMinorLeagueCurling);
        public void OpenSplashBurnoutGames() => OpenSection(canvasType: CanvasType.SplashScreenBurnoutGames);
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
        public void OpenCurlingLockerRoomSection() => OpenSection(canvasType: CanvasType.CurlingLockerRoom);
        //
        public void OpenCurlingTeamSplash() => OpenSection(canvasType: CanvasType.CurlingGameSplashTeamDisplay);
        public void OpenCurlingInGameHUD() => OpenSection(canvasType: CanvasType.CurlingGameInGameHUD);
        // public void OpenCurlingRoundSplash() => OpenSection(canvasType: CanvasType.CurlingGameSplashTurnDisplay);
        // public void OpenCurlingGameSplashTurnDisplaySection() => OpenSection(canvasType: CanvasType.CurlingGameSplashTurnDisplay);
        public void OpenCurlingTurnSplash() => OpenSection(canvasType: CanvasType.CurlingGameSplashTurnDisplay);
        public void OpenCurlingScoringDisplayState() => OpenSection(canvasType: CanvasType.CurlingGameScoringDisplayState);
        public void OpenCurlingPostThrowResults() => OpenSection(canvasType: CanvasType.CurlingGamePostThrowResultDisplay);
        public void OpenCurlingFinalResults() => OpenSection(canvasType: CanvasType.CurlingGameFinalResultDisplay);
        public void OpenCurlingGameFinalResultDisplaySection() => OpenSection(canvasType: CanvasType.CurlingGameFinalResultDisplay);
        // 
        public void OpenLoadingFullScreenSection() => OpenSection(canvasType: CanvasType.LoadingFullScreen);
        
        /************************************************************************************************************************/

        /// <summary>
        /// General function for opening a dialogue section based on its CanvasModalType.
        /// </summary>
        /// 
        public void OpenTeamCharacterSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamCharacterSelection);
        public void OpenTeamStoneSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamStoneSelection);
        public void OpenTeamBroomLeftSweeperSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamBroomLeftSweeperSelection);
        public void OpenTeamBroomRightSweeperSelectionModal() => OpenModal(canvasModalType: CanvasModalType.TeamBroomRightSweeperSelection);
        public void OpenStoneToUseSelectionModal() => OpenModal(canvasModalType: CanvasModalType.StoneToUseSelection);
        
        /************************************************************************************************************************/

        public void CloseAllCanvases()
        {
            _CloseAllCanvases?.Invoke();
            _StateMachine.ChangeState(null);
            Time.timeScale = 1f;
        }
        
        public void CloseAllModals() => _ModalStateMachine.ChangeState(null);

        /************************************************************************************************************************/
        /// <summary>
        /// Open Menus from Input Actions
        /// </summary>
        
        private void OpenPauseMenuFromInputAction(InputAction.CallbackContext obj)
        {
            if (_StateMachine.CurrentState == null)
            {
                PauseGame();
                return;
            }
        }

        private void OpenMainMenuFromInputAction(InputAction.CallbackContext obj)
        {
            if (_StateMachine.CurrentState == null)
            {
                OpenSection(canvasType: CanvasType.MainMenu); 
                return;
            }
        }
        

        private void OpenCompanySplashScreensFromInputAction(InputAction.CallbackContext obj)
        {
            if (_StateMachine.CurrentState == null)
            {
                OpenSection(canvasType: CanvasType.SplashScreenBurnoutGames); 
                return;
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            OpenSection(canvasType: CanvasType.PauseMenu);
        }

        /************************************************************************************************************************/

        

        


    }
}