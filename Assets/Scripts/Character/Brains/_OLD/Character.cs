// // Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

// #pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

// using Animancer.FSM;
// using UnityEngine;
// using Animancer;
// using UnityEngine.AI;

// namespace CharacterNPC
// {

//     [AddComponentMenu(Strings.SamplesMenuPrefix + "CharacterNPC - Character")]
//     // [AnimancerHelpUrl(typeof(Character))]
//     [DefaultExecutionOrder(-10000)]// Initialize the StateMachine before anything uses it.
//     public class Character : MonoBehaviour
//     {
//         /************************************************************************************************************************/
//         // Used in the Characters sample.
//         /************************************************************************************************************************/

//         [SerializeField]
//         private AnimancerComponent _Animancer;
//         public AnimancerComponent Animancer => _Animancer;

//         [SerializeField]
//         private LayeredAnimationManager _AnimationManager;
//         public LayeredAnimationManager AnimationManager => _AnimationManager;

//         [SerializeField]
//         private CharacterMovement _Movement;
//         public CharacterMovement Movement => _Movement;

//         [SerializeField]
//         private NavMeshAgent _NavAgent;
//         public NavMeshAgent NavAgent => _NavAgent;
        

//         /************************************************************************************************************************/

//         // [SerializeField]
//         // private StateMachine<CharacterState>.WithDefault _StateMachine;
//         // public StateMachine<CharacterState>.WithDefault StateMachine => _StateMachine;

//         [SerializeField]
//         private CharacterState.StateMachine _StateMachine;
//         public CharacterState.StateMachine StateMachine => _StateMachine;

//         protected virtual void Awake()
//         {
//             _StateMachine.InitializeAfterDeserialize();
//         }

//         /************************************************************************************************************************/
//         // Used in the Interruptions sample.
//         /************************************************************************************************************************/

//         // [SerializeField]
//         // private HealthPool _Health;
//         // public HealthPool Health => _Health;

//         /************************************************************************************************************************/
//         // Used in the Brains sample.
//         /************************************************************************************************************************/

//         [SerializeField]
//         private CharacterParameters _Parameters;
//         public CharacterParameters Parameters => _Parameters;

//         /************************************************************************************************************************/
//         // Used in the Weapons sample.
//         /************************************************************************************************************************/

//         // [SerializeField]
//         // private Equipment _Equipment;
//         // public Equipment Equipment => _Equipment;

//         /************************************************************************************************************************/

//         /// <summary>
//         /// Check if this <see cref="Character"/> should enter the Idle, Locomotion, or Airborne state depending on
//         /// whether it is grounded and the movement input from the <see cref="Brain"/>.
//         /// </summary>
//         /// <remarks>
//         /// We could add some null checks to this method to support characters that don't have all the standard states,
//         /// such as a character that can't move or a flying character that never lands.
//         /// </remarks>
//         public bool CheckMotionState()
//         {
//             CharacterState state;
//             if (Movement.IsGrounded)
//             {
//                 // if there is no movement occuring in any direction...
//                 state = Parameters.MovementDirection == Vector3.zero && Parameters.ForwardSpeed < 0.1f
//                     // the character in the default state
//                     ? StateMachine.DefaultState
//                     // else the character is in the lovomotion state
//                     : StateMachine.Locomotion;
//             }
//             else
//             {
//                 // if the character is not grounded, then they are airbourne (while jumping or falling)
//                 state = StateMachine.Airborne;
//             }

//             return
//                 // if the determined state is not already the current state...
//                 state != StateMachine.CurrentState &&
//                 // and the current state can't reset to the determined state...
//                 StateMachine.TryResetState(state);
//                 // then return true to indicate a state change is currently occuring,
//                 // and cannot be interrupted until it is complete.
//                 // else return false to indicate no state change is occuring.
//         }
//     }
// }
