// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Animal Brains - Move State")]
    // [AnimancerHelpUrl(typeof(MoveState))]
    public class MoveState : AnimalState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;
        [SerializeField] private StringAsset _SpeedParameter;
        [SerializeField] private float _WalkParameterValue = 0.5f;
        [SerializeField] private float _RunParameterValue = 1;
        [SerializeField, Seconds] private float _ParameterSmoothTime = 0.15f;
        [SerializeField, DegreesPerSecond] private float _TurnSpeed = 360;

        private SmoothedFloatParameter _Speed;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            _Speed = new SmoothedFloatParameter(
                Animal.Animancer,
                _SpeedParameter,
                _ParameterSmoothTime);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Animal.Animancer.Play(_Animation);
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
            _Speed.TargetValue = Animal.Parameters.WantsToRun
                ? _RunParameterValue
                : _WalkParameterValue;
        }

        /************************************************************************************************************************/

        private void UpdateTurning()
        {
            // Don't turn if we aren't trying to move.
            Vector3 movement = Animal.Parameters.MovementDirection;
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
            Transform transform = Animal.Animancer.transform;
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = Mathf.MoveTowardsAngle(eulerAngles.y, targetAngle, turnDelta);
            transform.eulerAngles = eulerAngles;
        }

        

        /************************************************************************************************************************/
    }
}
