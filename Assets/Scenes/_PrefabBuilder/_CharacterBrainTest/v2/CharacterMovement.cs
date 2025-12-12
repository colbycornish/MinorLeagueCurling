// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;
using Animancer.Units;
using static Animancer.Validate;
using Unity.VisualScripting;

namespace CharacterNPC.v2
{
    /// <summary>
    ///  placed in the same location as the Animancer Component
    /// </summary>
    /// <summary>The stats and logic for moving a <see cref="Character"/>.</summary>
    /// 
    /// <remarks>
    /// <strong>Sample:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/samples/animator-controllers/3d-game-kit">
    /// 3D Game Kit</see>
    /// </remarks>
    /// 
    /// https://kybernetik.com.au/animancer/api/Animancer.Samples.AnimatorControllers.GameKit/CharacterMovement
    /// 
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Character Movement")]
    [AnimancerHelpUrl(typeof(CharacterMovement))]
    public class CharacterMovement : MonoBehaviour
    {
        /************************************************************************************************************************/
// #if UNITY_PHYSICS_3D
        /************************************************************************************************************************/

        [SerializeField] private Character _Character;
        // [SerializeField] private CharacterController _CharacterController;
        // TODO: Transition this doc to use a nav agent instead of a character controller.
        [SerializeField]
        private NavMeshAgent _NavAgent;
        public NavMeshAgent NavAgent => _NavAgent;

        /************************************************************************************************************************/


        [Header("Locomotion Settings")]
        [SerializeField] private StringAsset _SpeedParameter;
        [SerializeField] private float _WalkParameterValue = 0.5f;
        [SerializeField] private float _RunParameterValue = 1;
        [SerializeField, Seconds] private float _ParameterSmoothTime = 0.15f;
        [SerializeField, DegreesPerSecond] private float _TurnSpeed = 120;

        private SmoothedFloatParameter _Speed;

        /************************************************************************************************************************/

        // [Header("Directional Movement Settings")]
        // [SerializeField] private StringAsset _DirectionalParameterX;
        // [SerializeField] private StringAsset _DirectionalParameterY;
        // [SerializeField, Seconds] private float _DirectionalParameterSmoothTime = 0.15f;
        // [SerializeField, Meters] private float _DirectionalStopProximity = 0.0001f;

        // private SmoothedVector2Parameter _DirectionalSmoothedParameters;

        /************************************************************************************************************************/

        [SerializeField] private bool _FullMovementControl = true;

        /************************************************************************************************************************/
        [Header("Nav Agent Settings")]
        [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        private float _WalkSpeed = 3.5f;
        public float WalkSpeed => _WalkSpeed;

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        // private float _MaxSpeed = 8;
        // public float MaxSpeed => _MaxSpeed;

        // [SerializeField, MetersPerSecondPerSecond(Rule = Value.IsNotNegative)]
        // private float _Acceleration = 20;
        // public float Acceleration => _Acceleration;

        // [SerializeField, MetersPerSecondPerSecond(Rule = Value.IsNotNegative)]
        // private float _Deceleration = 25;
        // public float Deceleration => _Deceleration;

        // [SerializeField, DegreesPerSecond(Rule = Value.IsNotNegative)]
        // private float _MinTurnSpeed = 400;
        // public float MinTurnSpeed => _MinTurnSpeed;

        // [SerializeField, DegreesPerSecond(Rule = Value.IsNotNegative)]
        // private float _MaxTurnSpeed = 1200;
        // public float MaxTurnSpeed => _MaxTurnSpeed;

        // [SerializeField, MetersPerSecondPerSecond(Rule = Value.IsNotNegative)]
        // private float _Gravity = 20;
        // public float Gravity => _Gravity;

        // [SerializeField, Multiplier(Rule = Value.IsNotNegative)]
        // private float _StickingGravityProportion = 0.3f;
        // public float StickingGravityProportion => _StickingGravityProportion;

        /************************************************************************************************************************/

        // public bool IsGrounded { get; private set; }
        // public Material GroundMaterial { get; private set; }

        /************************************************************************************************************************/
        // new
        protected virtual void Awake()
        {
            _Character.NavAgent.updatePosition = false;
            _Character.NavAgent.updateRotation = false;

            _Speed = new SmoothedFloatParameter(
                _Character.Animancer,
                _SpeedParameter,
                _ParameterSmoothTime);

            // Directional
            // _DirectionalSmoothedParameters = new SmoothedVector2Parameter(
            //     _Character.Animancer,
            //     _DirectionalParameterX,
            //     _DirectionalParameterY,
            //     _DirectionalParameterSmoothTime
            // );
        }

        /************************************************************************************************************************/
        // new
        public void UpdateSpeed()
        {
            // TODO: Need a parameter to set the speed to zero when not moving.
            // this includes setting the NavAgent speed to zero when no movement is allowed.
            
            // _Speed.TargetValue = _Character.NavAgent.velocity.magnitude < 0.1f
            //     ? 0f
            //     : 
            // if (_Character.StateMachine.CurrentState.FullMovementControl != true)
            // {
            // }
            _Character.Parameters.Movement.IsStopped = _Character.NavAgent.isStopped;
            
            if (_Character.Parameters.Movement.MovementDirection == Vector3.zero || 
                _Character.Parameters.Movement.CurrentDestination == null || 
                _Character.Parameters.Movement.DistanceFromDestination < 0.1f
            )
            {
                _Speed.TargetValue = 0f;
                _Character.Parameters.Movement.IsMoving = false;
                return;
            } else {
                _Character.Parameters.Movement.IsMoving = true;
                _Speed.TargetValue = _Character.Parameters.Movement.WantsToRun
                    ? _RunParameterValue
                    : _WalkParameterValue;
            }
            
            _Character.NavAgent.speed = _Speed.TargetValue * 7f; // Animation walk is 0.5
            _Character.Parameters.Movement.ForwardSpeed = _Speed.TargetValue * 7f;
            
        }
        
        // old
        public void UpdateSpeedControl()
        {
        //     Vector3 movement = _Character.Parameters.MovementDirection;

        //     _Character.Parameters.DesiredForwardSpeed = movement.magnitude * MaxSpeed;

        //     float deltaSpeed = movement != Vector3.zero ? Acceleration : Deceleration;
        //     _Character.Parameters.ForwardSpeed = Mathf.MoveTowards(
        //         _Character.Parameters.ForwardSpeed,
        //         _Character.Parameters.DesiredForwardSpeed,
        //         deltaSpeed * Time.deltaTime);
        }

        public void UpdateMovementDirection()
        {
            if (_Character.NavAgent.velocity.magnitude < 0.1f)
            {
                _Character.Parameters.Movement.MovementDirection = Vector3.zero;
                // navMeshAgent.speed = 0
                return; // Let the NavMeshAgent control movement.
            } else {
                // Convert the input to 3D in the XZ plane.
                Vector3 nextPositionDirection = _Character.NavAgent.nextPosition;
                Vector3 movementDirection = new Vector3(
                    nextPositionDirection.x - _Character.transform.position.x, 
                    0, 
                    nextPositionDirection.z - _Character.transform.position.z
                );
                _Character.Parameters.Movement.MovementDirection = movementDirection;
            }
        }

        public void UpdateDistanceFromDestination()
        {
            _Character.Parameters.Movement.DistanceFromDestination = _Character.NavAgent.remainingDistance;
        }

        // /************************************************************************************************************************/

        public void UpdateTurning()
        {
            // Don't turn if we aren't trying to move.
            Vector3 movement = _Character.Parameters.Movement.MovementDirection;
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
            Transform transform = _Character.Animancer.transform;
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = Mathf.MoveTowardsAngle(eulerAngles.y, targetAngle, turnDelta);
            transform.eulerAngles = eulerAngles;
        }

        /************************************************************************************************************************/

        

        // public float CurrentTurnSpeed
        //     => Mathf.Lerp(
        //         MaxTurnSpeed,
        //         MinTurnSpeed,
        //         _Character.Parameters.ForwardSpeed / _Character.Parameters.DesiredForwardSpeed);
                
        /************************************************************************************************************************/

        // public bool GetTurnAngles(Vector3 direction, out float currentAngle, out float targetAngle)
        // {
        //     if (direction == Vector3.zero)
        //     {
        //         currentAngle = float.NaN;
        //         targetAngle = float.NaN;
        //         return false;
        //     }

        //     currentAngle = transform.eulerAngles.y;
        //     targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //     return true;
        // }
        
        /************************************************************************************************************************/

        // public void TurnTowards(float currentAngle, float targetAngle, float speed)
        // {
        //     currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, speed * Time.deltaTime);

        //     transform.eulerAngles = new(0, currentAngle, 0);
        // }

        // public void TurnTowards(Vector3 direction, float speed)
        // {
        //     if (GetTurnAngles(direction, out float currentAngle, out float targetAngle))
        //         TurnTowards(currentAngle, targetAngle, speed);
        // }
        
        /************************************************************************************************************************/

        protected virtual void OnAnimatorMove()
        {
            Vector3 movement = GetRootMotion();
            // Debug.Log("Root Motion: " + movement);
            // CheckGround(ref movement);
            // UpdateGravity(ref movement);
            // _CharacterController.Move(movement);
            // _Character.Animancer.Animator.SetFloat("ForwardSpeed", Mathf.Abs(_Speed.TargetValue));
            // _Character.NavAgent.nextPosition = transform.position + _Character.Animancer.Animator.deltaPosition;
            _Character.gameObject.transform.position = _Character.NavAgent.nextPosition;
            // _Character.gameObject.
            // transform.rotation = _Character.NavAgent.transform.rotation * _Character.Animancer.Animator.deltaRotation;
            // IsGrounded = _CharacterController.isGrounded;

            // _Character.gameObject.transform.rotation *= _Character.Animancer.Animator.deltaRotation;
            // if (_Character.NavAgent != null && _Character.NavAgent.velocity.magnitude > 0.1f)
            // {
            //     // Calculate the direction to the immediate steering target
            //     // Use steeringTarget instead of destination for smoother immediate turns
            //     Vector3 direction = (_Character.NavAgent.steeringTarget - _Character.gameObject.transform.position).normalized;
                
            //     // Ensure the rotation is only on the Y-axis (for a typical character)
            //     direction.y = 0;

            //     // If there is a valid direction to look at
            //     if (direction != Vector3.zero)
            //     {
            //         // Create a rotation looking in that direction
            //         Quaternion lookRotation = Quaternion.LookRotation(direction);
            //         float rotationSpeed = 5f;
            //         // Smoothly rotate the agent towards the calculated rotation using Slerp
            //         _Character.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            //     }
            // }
        }

        /************************************************************************************************************************/

        private Vector3 GetRootMotion()
        {
            Vector3 rawMotion = _Character.StateMachine.CurrentState.RootMotion;

            if (!_FullMovementControl ||// If Full Movement Control is disabled in the Inspector.
                !_Character.StateMachine.CurrentState.FullMovementControl)// Or the current state doesn't want it.
                return rawMotion;// Return the raw Root Motion.

            // If the Brain is not trying to control movement,
            // let the animation do what it wants (it's probably Idle or transitioning to Idle anyway).
            Vector3 direction = _Character.Parameters.Movement.MovementDirection;
            direction.y = 0;
            if (direction == Vector3.zero)
                return rawMotion;

            // Otherwise calculate the Root Motion only in the specified direction.

            float magnitude = direction.magnitude;
            direction /= magnitude;

            Vector3 controlledMotion = direction * Vector3.Dot(direction, rawMotion);

            // Interpolate towards that based on the desired movement magnitude (i.e. control stick tilt).
            // 0 tilt = use only the raw motion (would have already returned above to skip these calculations).
            // 1 tilt = use only the controlled motion.
            // And values in between give proportional motion between those values.
            return Vector3.Lerp(rawMotion, controlledMotion, magnitude);
        }

        /************************************************************************************************************************/

        // private void CheckGround(ref Vector3 movement)
        // {
        //     if (!_CharacterController.isGrounded)
        //         return;

        //     const float GroundedRayDistance = 1f;

        //     Ray ray = new(
        //         transform.position + GroundedRayDistance * 0.5f * Vector3.up,
        //         -Vector3.up);

        //     if (Physics.Raycast(
        //         ray,
        //         out RaycastHit hit,
        //         GroundedRayDistance,
        //         Physics.AllLayers,
        //         QueryTriggerInteraction.Ignore))
        //     {
        //         // Rotate the movement to lie along the ground vector.
        //         movement = Vector3.ProjectOnPlane(movement, hit.normal);

        //         // Store the current walking surface so the correct audio is played.
        //         Renderer groundRenderer = hit.collider.GetComponentInChildren<Renderer>();
        //         GroundMaterial = groundRenderer ? groundRenderer.sharedMaterial : null;
        //     }
        //     else
        //     {
        //         GroundMaterial = null;
        //     }
        // }

        /************************************************************************************************************************/

        // private void UpdateGravity(ref Vector3 movement)
        // {
        //     if (_CharacterController.isGrounded && _Character.StateMachine.CurrentState.StickToGround)
        //         _Character.Parameters.VerticalSpeed = -Gravity * StickingGravityProportion;
        //     else
        //         _Character.Parameters.VerticalSpeed -= Gravity * Time.deltaTime;

        //     movement.y += _Character.Parameters.VerticalSpeed * Time.deltaTime;
        // }

        /************************************************************************************************************************/

        // Ignore these Animation Events because the attack animations will only start when we tell them to, so it
        // would be silly to use additional events for something we already directly caused. That sort of thing is only
        // necessary in Animator Controllers because they run their own logic to decide what they want to do.
        // private void MeleeAttackStart(int throwing = 0) { }
        // private void MeleeAttackEnd() { }

        /************************************************************************************************************************/
// #else
//         /************************************************************************************************************************/

//         protected virtual void Awake()
//         {
//             SampleModules.LogMissingPhysics3DModuleError(this);
//         }

//         /************************************************************************************************************************/

//         public bool IsGrounded => default;

//         public float CurrentTurnSpeed => default;

//         public void UpdateSpeedControl() { }

//         public bool GetTurnAngles(Vector3 direction, out float currentAngle, out float targetAngle)
//         {
//             currentAngle = default;
//             targetAngle = default;
//             return default;
//         }

//         public void TurnTowards(float currentAngle, float targetAngle, float speed) { }

//         public void TurnTowards(Vector3 direction, float speed) { }

//         /************************************************************************************************************************/
// #endif
        /************************************************************************************************************************/

    }
}



// public void UpdateDirectionalMovement()
//         {
//             //new Vector3(-1,0,0);//
//             Vector3 direction = _Character.Parameters.Movement.MovementDirection;
//             // Vector3 movementDirection = direction;

//             // float squaredDistance = direction.sqrMagnitude;
//             // if (squaredDistance <= _DirectionalStopProximity * _DirectionalStopProximity)
//             // {
//             //     movementDirection = Vector3.zero;
//             // }
//             // else
//             // {
//             //     // Otherwise normalize the direction so that we don't change speed based on distance.
//             //     // Calling direction.Normalize() would do the same thing, but would calculate the magnitude again.
//             //     movementDirection = direction / Mathf.Sqrt(squaredDistance);
//             // }
//             float dirX = direction.x == 0f
//                 ? 0f
//                 : direction.x > 0f 
//                     ? 1f
//                     : -1f;

//             float dirZ = direction.z == 0f
//                 ? 0f
//                 : direction.z > 0f 
//                     ? 1f
//                     : -1f;

//             // Vector3 localDirection = _Character.gameObject.transform.InverseTransformDirection(movementDirection);

//             // Then set the target value for the parameters to move towards:
//             // - Parameter X towards Direction X (right/left).
//             // - Parameter Y towards Direction Z (forwards/backwards).
//             // - Ignore Direction Y because the Mixer is only 2D.
            
//             _DirectionalSmoothedParameters.TargetValue = new Vector2(
//                 dirX, 
//                 dirZ
//             );
//         }