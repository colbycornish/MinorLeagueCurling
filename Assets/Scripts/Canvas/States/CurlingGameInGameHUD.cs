using CurlingUI.v3;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using CurlingManagersV3;

namespace UICanvasManager.v3
{
    public class CurlingGameInGameHUD : ICanvasState
    {
        public CurlingUI.v3.Scorebug Scorebug;
        public CurlingUI.v3.SweeperExhaustionBar RightSweeperBar;
        public CurlingUI.v3.SweeperExhaustionBar LeftSweeperBar;
        public CurlingUI.v3.PowerMeter PowerMeter;
        
    
        public override void OnEnter()
        {

            CurlingManager._instance.OnMatchPhaseStateChanged += OnCurlingMatchPhaseStateChanged;
            gameObject.SetActive(true); // Show the canvas
            
        }

        private void OnCurlingMatchPhaseStateChanged(CurlingMatchPhase newPhase){
            if (newPhase == CurlingMatchPhase.AimingAndPowerPhase)
            {
                ShowScorebug();
                ShowAimingAndPowerSelection();
                ShowSweeperExhaustionDisplay();
            }
            else if (newPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
            {
                ShowScorebug();
                HideAimingAndPowerSelection();
                ShowSweeperExhaustionDisplay();
            }
            else if (newPhase == CurlingMatchPhase.CurlingNoSweepZone)
            {
                ShowScorebug();
                HideAimingAndPowerSelection();
                HideSweeperExhaustionDisplay();
            }
        }

        public override void OnExit()
        {
            CurlingManager._instance.OnMatchPhaseStateChanged -= OnCurlingMatchPhaseStateChanged;
            gameObject.SetActive(false); // Hide the canvas
            // Remove listeners
        }

        /************************************************************************************************************************/

        public void ShowScorebug(){
            DisplayScorebug(show: true);
        }

        public void HideScorebug(){
            DisplayScorebug(show: false);
        }

        private void DisplayScorebug(bool show = false){
            if (Scorebug != null && Scorebug.gameObject.activeInHierarchy != show) Scorebug.gameObject.SetActive(show);
        }

        /************************************************************************************************************************/

        public void ShowAimingAndPowerSelection(){
            DisplayAimingAndPowerSelection(show: true);
        }

        public void HideAimingAndPowerSelection(){
            DisplayAimingAndPowerSelection(show: false);
        }

        private void DisplayAimingAndPowerSelection(bool show = false){
            if (Scorebug.gameObject.activeInHierarchy != show) Scorebug.gameObject.SetActive(show);
            if (PowerMeter.gameObject.activeInHierarchy != show) PowerMeter.gameObject.SetActive(show);

        }

        /************************************************************************************************************************/

        public void ShowSweeperExhaustionDisplay(){
            DisplaySweeperExhaustionDisplay(show: true);
        }

        public void HideSweeperExhaustionDisplay(){
            DisplaySweeperExhaustionDisplay(show: false);
        }

        private void DisplaySweeperExhaustionDisplay(bool show = false){
            LeftSweeperBar.controlsLeftSweeper = true;
            if (LeftSweeperBar.gameObject.activeInHierarchy != show) LeftSweeperBar.gameObject.SetActive(show);
            
            if (RightSweeperBar.gameObject.activeInHierarchy != show) RightSweeperBar.gameObject.SetActive(show);
            RightSweeperBar.controlsRightSweeper = true;

            if (show){
                LeftSweeperBar.InitializeDisplay();
                RightSweeperBar.InitializeDisplay();
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGameInGameHUD;

    }
}