// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

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
        [SerializeField] private StringAsset _SpeedParameter;
        [SerializeField] private float _WalkParameterValue = 0.5f;
        [SerializeField] private float _RunParameterValue = 1;
        [SerializeField, Seconds] private float _ParameterSmoothTime = 0.15f;
        [SerializeField, DegreesPerSecond] private float _TurnSpeed = 360;

        private SmoothedFloatParameter _Speed;

        /************************************************************************************************************************/

        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;


        public override bool CanExitState => true;
            // => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

        /************************************************************************************************************************/

        public override bool FullMovementControl => true;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            _Speed = new SmoothedFloatParameter(
                Character.Animancer,
                _SpeedParameter,
                _ParameterSmoothTime);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Character.Animancer.Play(_Animation);
            if (Character.Parameters != null){
                if (Character.Parameters.IsCrouching == true)
                {
                    // Character.Animancer.Play(_AnimationCrouching);
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationCrouching, 
                        canPlayActionFullBody: false
                    );
                }
                else
                {
                    // Character.Animancer.Play(_AnimationWalking);
                    Character.AnimationManager.PlayBase(
                        transition: _AnimationWalking, 
                        canPlayActionFullBody: false
                    );
                }
                
            } else
            {
                // Character.Animancer.Play(_Animation);
                Character.AnimationManager.PlayBase(
                    transition: _AnimationWalking, 
                    canPlayActionFullBody: false
                );
                // Character.Animancer.Play(_AnimationWalking);
            }
            
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            UpdateSpeed();
            UpdateTurning();
        }

        /************************************************************************************************************************/

        private void UpdateSpeed()
        {
            _Speed.TargetValue = Character.Parameters.WantsToRun
                ? _RunParameterValue
                : _WalkParameterValue;
        }

        /************************************************************************************************************************/

        private void UpdateTurning()
        {
            // Don't turn if we aren't trying to move.
            Vector3 movement = Character.Parameters.MovementDirection;
            if (movement == Vector3.zero)
                return;

            // Determine the angle we want to turn towards.
            // Without going into the maths behind it, Atan2 gives us the angle of a vector in radians.
            // So we just feed in the x and z values because we want an angle around the y axis,
            // then convert the result to degrees because Transform.eulerAngles uses degrees.
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;

            // Determine how far we can turn this frame (in degrees).
            float turnDelta = _TurnSpeed * Time.deltaTime;

            // Get the current rotation, move its y value towards the target, and apply it back to the Transform.
            Transform transform = Character.Animancer.transform;
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = Mathf.MoveTowardsAngle(eulerAngles.y, targetAngle, turnDelta);
            transform.eulerAngles = eulerAngles;
        }

        

        /************************************************************************************************************************/
    }
}
