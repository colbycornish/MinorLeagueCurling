// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;
using Unity.Entities.UniversalDelegates;
using CharacterNPCJobs;

/************************************************************************************************************************/
/*

BASIC STATE
(will be included by default on all characters)

  This Animation state represents when:
  - The NPC is moving. 
  - The NPC is changing postures.
  
  Extensions:
  - 

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Move State")]
    // [AnimancerHelpUrl(typeof(MoveState))]
    public class MoveState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;
        [SerializeField] private TransitionAsset _AnimationWalking;
        [SerializeField] private TransitionAsset _AnimationCrouching;

        // [SerializeField] private StringAsset _SpeedParameter;

        [SerializeField] private TransitionAssetBase _DirectionalMovementTransition;


        /************************************************************************************************************************/

        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;


        public override bool CanExitState => true;
            // => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

        /************************************************************************************************************************/

        // TODO: The player can perform actions while moving. This should not override the 
        // existing action state.
        // public override ActionType StateActionType => ActionType.None;
        public override bool FullMovementControl => true;

        /************************************************************************************************************************/

        // protected virtual void Awake()
        // {
        //     _Speed = new SmoothedFloatParameter(
        //         Character.Animancer,
        //         _SpeedParameter,
        //         _ParameterSmoothTime);
        // }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            PlayBase();
            Character.Parameters.Movement.IsBaseFromIdleState = false;
            Character.Parameters.Movement.IsBaseFromMoveState = true;
        }

        /************************************************************************************************************************/
        public void PlayBase()
        {
            if (Character.Parameters.Movement.useDirectionalMovementAnimations)
            {
                Character.AnimationManager.PlayBase(
                    transition: _DirectionalMovementTransition, 
                    canPlayActionFullBody: false
                );
                return;
            }


            switch (Character.Parameters.Posture.DesiredPosture)
            {

                // Crouching
                case CharacterParametersPosture.CharacterPostureState.Crouching: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationCrouching, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.Crouching;
                    break;

                // Standing
                case CharacterParametersPosture.CharacterPostureState.Standing: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationWalking, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.Standing;
                    break;

                // Sitting
                case CharacterParametersPosture.CharacterPostureState.Sitting: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationWalking, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.Sitting;
                    break;
                
                // LayingDown
                case CharacterParametersPosture.CharacterPostureState.LayingDown: 
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationWalking, 
                        canPlayActionFullBody: false
                    );
                    Character.Parameters.Posture.CurrentPosture = 
                        CharacterParametersPosture.CharacterPostureState.LayingDown;
                    break;
                
                default:
                    break;
            }
        }


        protected virtual void Update()
        {
            if (!Character.Parameters.Movement.useDirectionalMovementAnimations)
            {   
                Character.Movement.UpdateDistanceFromDestination();
                Character.Movement.UpdateMovementDirection();
                Character.Movement.UpdateSpeed();
                Character.Movement.UpdateTurning();
            } else
            {
                Character.Movement.UpdateDistanceFromDestination();
                Character.Movement.UpdateMovementDirection();
                Character.Movement.UpdateSpeed();
                Character.Movement.UpdateDirectionalMovement();
            }
        }

        /************************************************************************************************************************/
        public void UseDirectionalMovement(){
            // Calculate the movement direction.
            // Vector3 movementDirection = GetMovementDirection();

            // The movement direction is in world space,
            // so we need to convert it to the character's local space
            // to be appropriate for their current rotation.
            // Vector3 localDirection = transform.InverseTransformDirection(
                // movementDirection
            // );

            // Then set the target value for the parameters to move towards:
            // - Parameter X towards Direction X (right/left).
            // - Parameter Y towards Direction Z (forwards/backwards).
            // - Ignore Direction Y because the Mixer is only 2D.
            // _SmoothedParameters.TargetValue = new Vector2(localDirection.x, localDirection.z);

        }
    }
}
