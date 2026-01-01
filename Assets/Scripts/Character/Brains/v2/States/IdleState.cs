// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using Animancer;

/************************************************************************************************************************/
/*

BASIC STATE
(will be included by default on all characters)

  This Animation state represents when:
  - The NPC is idling. 
  - The NPC is changing postures.
  - The NPC is thinking.
  - The NPC is not moving.
  
  Extensions:
  - Should have multiple varieties of idling collections (looking)

*/
/************************************************************************************************************************/


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
        [SerializeField] private TransitionAsset _AnimationSweeperIdle;
        // [SerializeField] private TransitionAsset _AnimationLayingDown;


        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;


        public override bool CanExitState => true;
            // => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

        /************************************************************************************************************************/

        public override ActionType StateActionType => ActionType.Idle;
        public override bool FullMovementControl => false;

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            Character.Parameters.Movement.IsIdle = false;
        }

        protected virtual void OnEnable()
        {
            Character.Parameters.Movement.IsBaseFromIdleState = true;
            Character.Parameters.Movement.IsBaseFromMoveState = false;
            Character.Parameters.Jobs.CurrentAction = StateActionType;
            PlayBase();
        }

        protected virtual void Update()
        {
            if (Character.Parameters.Movement.IsBaseFromIdleState == false || 
                Character.Parameters.Movement.IsBaseFromMoveState == true
            )
            {
                Character.Parameters.Movement.IsBaseFromIdleState = true;
                Character.Parameters.Movement.IsBaseFromMoveState = false;
            }
        }


        private void PlayBase()
        {
            
            // If they are holding a broom two handed...
            if (Character.Equipment.Weapon != null && 
                Character.Equipment.Weapon.gameObject.GetComponent<CurlingBroom>() != null)
            {
                Character.AnimationManager.PlayBase(
                    transition: _AnimationSweeperIdle, 
                    canPlayActionFullBody: false
                );
                return;
            }

            // Else adjust their basic posture
            switch (Character.Parameters.Posture.DesiredPosture)
            {
                // Sitting
                case CharacterParametersPosture.CharacterPostureState.Sitting: 
                    PlaySitting();
                    break;
                // Crouching
                case CharacterParametersPosture.CharacterPostureState.Crouching: 
                    PlayCrouching();
                    break;
                // LayingDown
                case CharacterParametersPosture.CharacterPostureState.LayingDown: 
                    PlayLayingDown();
                    break;
                // Standing
                case CharacterParametersPosture.CharacterPostureState.Standing: 
                default:
                    PlayStanding();
                    break;
            }
        }

        private void PlayStanding()
        {
            Character.AnimationManager.PlayBase(
                transition: _AnimationStanding, 
                canPlayActionFullBody: false
            );
            Character.Parameters.Posture.CurrentPosture = 
                CharacterParametersPosture.CharacterPostureState.Standing;
        }

        private void PlayCrouching()
        {
            Character.AnimationManager.PlayBase(
                transition: _AnimationCrouching, 
                canPlayActionFullBody: false
            );
            Character.Parameters.Posture.CurrentPosture = 
                CharacterParametersPosture.CharacterPostureState.Crouching;
        }

        private void PlaySitting()
        {
            Character.AnimationManager.PlayBase(
                transition: _AnimationSitting, 
                canPlayActionFullBody: false
            );
            Character.Parameters.Posture.CurrentPosture = 
                CharacterParametersPosture.CharacterPostureState.Sitting;
        }

        private void PlayLayingDown()
        {
            Character.AnimationManager.PlayBase(
                transition: _AnimationSitting, 
                canPlayActionFullBody: false
            );
            Character.Parameters.Posture.CurrentPosture = 
                CharacterParametersPosture.CharacterPostureState.LayingDown;
        }

        /************************************************************************************************************************/
    }
}
