// using Animancer.FSM;
// using Animancer;
// using UnityEngine;
using Animancer.FSM;
using UnityEngine;
using Animancer;

namespace CurlingManagersV3
{
    public abstract class IMatchPhaseState : StateBehaviour //, IOwnedState<ICanvasState>
    {

        [SerializeField]
        private CurlingManager _MainCurlingManager;
        public CurlingManager MainCurlingManager => _MainCurlingManager;

        public MatchPhaseStateMachine OwnerStateMachine => _MainCurlingManager.StateMachine;
        // [SerializeField]
        // private UICanvasManager _CanvasManager;
        // public UICanvasManager CanvasManager => _CanvasManager;

        /************************************************************************************************************************/

        #if UNITY_EDITOR
        override protected void OnValidate()
        {
            gameObject.GetComponentInParentOrChildren(ref _MainCurlingManager);
        }
        #endif

        /************************************************************************************************************************/

        // public StateMachine<ICanvasState> OwnerStateMachine => _CanvasManager.StateMachine;
        
        public virtual void OnEnter(){} // Called when entering the state
        
        public virtual void OnExit(){}  // Called when exiting the state

        public virtual void OnUpdate(){} // Logic that runs per frame
         

        // public bool CanExitState
        // {
        //     get
        //     {
        //         // There are several different ways of accessing the state change details:
        //         // CharacterState nextState = StateChange<CharacterState>.NextState;
        //         // CharacterState nextState = this.GetNextState();
        //         CharacterState nextState = _Character.StateMachine.NextState;
        //         if (nextState == this)
        //             return CanInterruptSelf;
        //         else if (Priority == CharacterStatePriority.Low)
        //             return true;
        //         else
        //             return nextState.Priority > Priority;
        //     }
        // } 

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public virtual CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.None;
        
    }
}

 