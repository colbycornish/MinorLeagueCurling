// // Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

// #pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

// using UnityEngine;
// using Animancer;

// namespace CharacterNPC
// {
//     [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Idle State")]
//     // [AnimancerHelpUrl(typeof(IdleState))]
//     public class IdleState : CharacterState
//     {
//         /************************************************************************************************************************/

//         [SerializeField] private TransitionAsset _Animation;
//         [SerializeField] private TransitionAsset _AnimationStanding;
//         [SerializeField] private TransitionAsset _AnimationSitting;
//         [SerializeField] private TransitionAsset _AnimationCrouching;
//         // [SerializeField] private TransitionAsset _AnimationLayingDown;


//         public override CharacterStatePriority Priority => CharacterStatePriority.Low;

//         /************************************************************************************************************************/

//         public override bool CanInterruptSelf => true;

//         /************************************************************************************************************************/

//         public override bool CanEnterState => true; //Character.Movement.IsGrounded;


//         public override bool CanExitState => true;
//             // => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

//         /************************************************************************************************************************/

//         public override bool FullMovementControl => false;

//         /************************************************************************************************************************/

//         protected virtual void OnEnable()
//         {
//             // Character.Animancer.Play(_Animation);
//             Character.AnimationManager.PlayBase(
//                 transition: _AnimationStanding, 
//                 canPlayActionFullBody: true
//             );

//             // if (Character.Parameters != null){
//             //     if (Character.Parameters.IsSitting == true)
//             //     {
//             //         // Character.Animancer.Play(_AnimationSitting);
//             //         Character.AnimationManager.PlayBase(
//             //             transition: _AnimationSitting, 
//             //             canPlayActionFullBody: false
//             //         );
//             //     }
//             //     else if (Character.Parameters.IsCrouching == true)
//             //     {
//             //         // Character.Animancer.Play(_AnimationCrouching);
//             //         Character.AnimationManager.PlayBase(
//             //             transition: _AnimationCrouching, 
//             //             canPlayActionFullBody: false
//             //         );
//             //     }
//             //     // else if (Character.Parameters.IsLayingDown == true)
//             //     // {
//             //     //     // Character.Animancer.Play(_AnimationLayingDown);
//             //     //     Character.AnimationManager.PlayBase(
//             //     //         transition: _AnimationLayingDown, 
//             //     //         canPlayActionFullBody: false
//             //     //     );
//             //     // }
//             //     else
//             //     {
//             //         // Character.Animancer.Play(_AnimationStanding);
//             //         Character.AnimationManager.PlayBase(
//             //             transition: _AnimationStanding, 
//             //             canPlayActionFullBody: true
//             //         );
//             //     }
                
//             // } else
//             // {
//             //     // Character.Animancer.Play(_AnimationStanding);
//             //     Character.AnimationManager.PlayBase(
//             //         transition: _AnimationStanding, 
//             //         canPlayActionFullBody: false
//             //     );
//             // }
//         }

//         /************************************************************************************************************************/
//     }
// }
