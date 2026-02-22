// using Animancer.FSM;
// using Animancer;
// using UnityEngine;
using Animancer.FSM;
using UnityEngine;
using Animancer;

namespace UICanvasManager.v3
{
    // public interface ICanvasState
    public abstract class ICanvasState : StateBehaviour //, IOwnedState<ICanvasState>
    {

        [SerializeField]
        private UICanvasManager _UICanvasManager;
        public UICanvasManager UICanvasManager => _UICanvasManager;

        // public UICanvasStateMachine<ICanvasState> OwnerStateMachine => _UICanvasManager.StateMachine;
        // [SerializeField]
        // private UICanvasManager _CanvasManager;
        // public UICanvasManager CanvasManager => _CanvasManager;

        /************************************************************************************************************************/

        #if UNITY_EDITOR
        override protected void OnValidate()
        {
            gameObject.GetComponentInParentOrChildren(ref _UICanvasManager);
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
        public virtual CanvasType CurrentCanvasType => CanvasType.None;
        
    }
}




// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

// #pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.




// namespace CharacterNPC.v2
// {

//     [AddComponentMenu(Strings.SamplesMenuPrefix + "Characters - Character State")]
//     // [AnimancerHelpUrl(typeof(CharacterState))]
//     public abstract class CharacterState : StateBehaviour, IOwnedState<CharacterState>
//     {

//         // [System.Serializable]
//         // public class StateMachine : StateMachine<CharacterState>.WithDefault
//         // {
//         //     /************************************************************************************************************************/

//         //     [SerializeField]
//         //     private CharacterState _Locomotion;
//         //     public CharacterState Locomotion => _Locomotion;

//         //     // [SerializeField]
//         //     // private CharacterState _Airborne;
//         //     // public CharacterState Airborne => _Airborne;

//         //     /************************************************************************************************************************/
//         // }
//         /************************************************************************************************************************/

//         [SerializeField]
//         private Character _Character;
//         public Character Character => _Character;

//         /************************************************************************************************************************/

//         public StateMachine<CharacterState> OwnerStateMachine => _Character.StateMachine;

//         /************************************************************************************************************************/

// #if UNITY_EDITOR
//         protected override void OnValidate()
//         {
//             base.OnValidate();
//             gameObject.GetComponentInParentOrChildren(ref _Character);
//         }
// #endif

//         /************************************************************************************************************************/
//         // Explained in the Interruptions sample.
//         /************************************************************************************************************************/

//         public virtual CharacterStatePriority Priority => CharacterStatePriority.Low;

//         public virtual bool CanInterruptSelf => false;

//         public override bool CanExitState
//         {
//             get
//             {
//                 // There are several different ways of accessing the state change details:
//                 // CharacterState nextState = StateChange<CharacterState>.NextState;
//                 // CharacterState nextState = this.GetNextState();
//                 CharacterState nextState = _Character.StateMachine.NextState;
//                 if (nextState == this)
//                     return CanInterruptSelf;
//                 else if (Priority == CharacterStatePriority.Low)
//                     return true;
//                 else
//                     return nextState.Priority > Priority;
//             }
//         }

 
//         /// <summary>
//         /// Jumping enters the <see cref="AirborneState"/>, but <see cref="CharacterController.isGrounded"/> doesn't
//         /// become false until after the first update, so we want to make sure the <see cref="Character"/> won't stick
//         /// to the ground during that update.
//         /// </summary>
//         // public virtual bool StickToGround => true;

//         /// <summary>
//         /// Some states (such as <see cref="AirborneState"/>) will want to apply their own source of root motion, but
//         /// most will just use the root motion from the animations.
//         /// </summary>
//         public virtual Vector3 RootMotion => _Character.Animancer.Animator.deltaPosition;

//         /// <summary>
//         /// Indicates whether the root motion applied each frame while this state is active should be constrained to
//         /// only move in the specified <see cref="CharacterBrain.Movement"/>. Otherwise the root motion can
//         /// move the <see cref="Character"/> in any direction. Default is true.
//         /// </summary>
//         public virtual bool FullMovementControl => true;
        
//         /// <summary>
//         /// Used to help the NPC Job Brain tell the NPC Animation brain what to do. 
//         /// </summary>
//         public virtual ActionType StateActionType => 
//             ActionType.None;
//     }
// }
