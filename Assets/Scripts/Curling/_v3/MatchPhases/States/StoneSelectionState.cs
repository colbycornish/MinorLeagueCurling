using UnityEngine;
using UICanvasManager.v3;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class StoneSelectionState : IMatchPhaseState
    {
        

        /************************************************************************************************************************/
        protected virtual void OnEnable()
        {
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllCanvases();
            UICanvasManager.v3.UICanvasManager.Instance.OpenStoneToUseSelectionModal();
            MainCurlingManager.cameraController.SwitchToStoneBenchCamera();
        }

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllModals();
            MainCurlingManager.Players.RepositionCharactersForCurling();
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.StoneSelection;

    }
}