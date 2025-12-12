// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Idle State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class IdleState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;
        [SerializeField] private TransitionAsset _AnimationStanding;
        [SerializeField] private TransitionAsset _AnimationSitting;
        [SerializeField] private TransitionAsset _AnimationCrouching;
        // [SerializeField] private TransitionAsset _AnimationLayingDown;


        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;


        public override bool CanExitState => true;
            // => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

        /************************************************************************************************************************/

        public override bool FullMovementControl => false;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Debug.Log("CB - IdleState OnEnable");
            switch (Character.Parameters.Posture.DesiredPosture)
            {
                // Sitting
                case CharacterParametersPosture.CharacterPostureState.Sitting: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationSitting, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.Sitting;
                    break;
                // Crouching
                case CharacterParametersPosture.CharacterPostureState.Crouching: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationCrouching, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.Crouching;
                    break;
                // LayingDown
                case CharacterParametersPosture.CharacterPostureState.LayingDown: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationSitting, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.LayingDown;
                    break;
                // Standing
                case CharacterParametersPosture.CharacterPostureState.Standing: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationStanding, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.Standing;
                    break;
                default:
                    break;
            }
                


            

            // if (Character.Parameters.Posture.DesiredPosture == )
            // {
                
            // }
            
            // if (Character.Parameters.Posture.IsSitting == true)
            // {
            //     Character.AnimationManager.PlayBase(
            //         transition: _AnimationSitting, 
            //         canPlayActionFullBody: false
            //     );
            //     Character.Parameters.Posture.IsSitting = true;
            // }
            // else if (Character.Parameters.Posture.IsCrouching == true)
            // {
            //     Character.AnimationManager.PlayBase(
            //         transition: _AnimationCrouching, 
            //         canPlayActionFullBody: false
            //     );
                
            //     Character.Parameters.Posture.IsStanding = true;
            //     Character.Parameters.Posture.IsCrouching = false;
            //     Character.Parameters.Posture.IsSitting = false;
            //     Character.Parameters.Posture.IsLayingDown = false;
            // }
            // // else if (Character.Parameters.IsLayingDown == true)
            // // {
            // //     // Character.Animancer.Play(_AnimationLayingDown);
            // //     Character.AnimationManager.PlayBase(
            // //         transition: _AnimationLayingDown, 
            // //         canPlayActionFullBody: false
            // //     );
            // // }
            // else
            // {
            //     // Character.Animancer.Play(_AnimationStanding);
            //     Character.AnimationManager.PlayBase(
            //         transition: _AnimationStanding, 
            //         canPlayActionFullBody: true
            //     );
            //     Character.Parameters.Posture.IsStanding = true;
            //     Character.Parameters.Posture.IsCrouching = false;
            //     Character.Parameters.Posture.IsSitting = false;
            //     Character.Parameters.Posture.IsLayingDown = false;
            // }
        }

        /************************************************************************************************************************/
    }
}
