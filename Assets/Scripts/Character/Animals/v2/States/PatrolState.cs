// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;

namespace AnimalNPC
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Animal Brains - Patrol State")]
    // [AnimancerHelpUrl(typeof(MoveState))]
    public class PatrolState : AnimalState
    {
        /************************************************************************************************************************/

        [Header("Movement Settings")]
        [SerializeField] private TransitionAsset _Animation;
        [SerializeField] private StringAsset _SpeedParameter;
        [SerializeField] private float _WalkParameterValue = 0.5f;
        [SerializeField] private float _RunParameterValue = 1;
        [SerializeField, Seconds] private float _ParameterSmoothTime = 0.15f;
        [SerializeField, DegreesPerSecond] private float _TurnSpeed = 360;

        private SmoothedFloatParameter _Speed;

        [Header("Patrol Settings")]
        [SerializeField] private AnimalState _MoveState;
        [SerializeField] private List<Transform> _patrolPoints;
        [SerializeField] private Transform _currentDestination;
        [SerializeField] private Transform _nextDestination;
        // [SerializeField] private bool _isAtCurrentDestination = false;
        [SerializeField] private float _stoppingDistance = 2f; // How close the agent needs to be to a waypoint


        [Header("Waiting State")]
        [SerializeField] private float waitTimer;
        [SerializeField] public bool isWaiting = false;
        [SerializeField] public float waitTimeAtWaypoint = 2f; // Time to wait at each waypoint
        
        public override AnimalStatePriority Priority => AnimalStatePriority.Low;

        public override bool CanInterruptSelf => true;
        // private SmoothedFloatParameter _Speed;

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
            Debug.Log("Entering Patrol State.");
            Animal.Animancer.Play(_Animation);
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            if (Animal.StateMachine.CurrentState == this)
            {
                
                if (isWaiting)
                {
                    if (waitTimer == waitTimeAtWaypoint)
                    {
                        Debug.Log("Waiting at patrol point...");
                    }

                    waitTimer -= Time.deltaTime;
                    
                    // Animal.StateMachine.TrySetState(Animal.StateMachine.DefaultState);
                    
                    if (waitTimer <= 0)
                    {
                        Debug.Log("Finished waiting, resuming patrol.");
                        isWaiting = false;   
                    }
                }
                else
                {
                    
                    UpdateDestination();
                    UpdateSpeed();
                    UpdateTurning();
                }
            }
        }

        /************************************************************************************************************************/

        private void UpdateSpeed()
        {
            _Speed.TargetValue = isWaiting 
                ? 0f
                : Animal.Parameters.WantsToRun
                    ? _RunParameterValue
                    : _WalkParameterValue;
        }

        /************************************************************************************************************************/
        void UpdateDestination()
        {
            if (_currentDestination == null && !isWaiting)
            {
                SetNextDestination();

            }
            else if (Animal.NavAgent.remainingDistance <= _stoppingDistance && !Animal.NavAgent.pathPending)
            {
                ClearDestinationAndWait();

                Debug.Log("Trying to set default state...");
                Animal.StateMachine.TrySetDefaultState();
                // Animal.StateMachine.TrySetState(Animal.StateMachine.DefaultState);
            }
            else if (_currentDestination != null)
            {
                // Debug.Log("Moving towards patrol destination.");
                Animal.Parameters.DistanceFromDestination = Animal.NavAgent.remainingDistance;
            }
        }


        void SetNextDestination()
        {
            Debug.Log("Setting next patrol destination.");
            /// randomly select a location from the list
            int randomIndex = UnityEngine.Random.Range(0, _patrolPoints.Count);
            Transform chosenPatrolPoint = _patrolPoints[randomIndex];

            // set the current destination in Animal Parameters
            _currentDestination = chosenPatrolPoint;
            Animal.Parameters.CurrentDestination = chosenPatrolPoint;

            // tell the nav agent to go to that location
            Animal.NavAgent.SetDestination(chosenPatrolPoint.position);
            Animal.Parameters.DistanceFromDestination = 100f;

            // reset the flag
            // _isAtCurrentDestination = false;
        }

        void ClearDestinationAndWait()
        {
            // clear destination
            Debug.Log("Clearing Destination.");
            isWaiting = true;
            waitTimer = waitTimeAtWaypoint;

            // Animal.Parameters.DistanceFromDestination = 0f;
            // Animal.Parameters.CurrentDestination = null;
            Animal.NavAgent.ResetPath();
            // _isAtCurrentDestination = true;
            _currentDestination = null;
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



        // void Update()
        // {
        //     if (isDead)
        //     {
        //         return;
        //     }
        //     if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
        //     {
        //         if (!isWaiting)
        //         {
        //             isWaiting = true;
        //             waitTimer = waitTimeAtWaypoint;
        //             Shake();

        //         }
        //         else
        //         {
        //             waitTimer -= Time.deltaTime;
        //             if (waitTimer <= 0)
        //             {
        //                 isWaiting = false;
        //                 SetNextDestination();
        //             }
        //         }
        //     }
        //     else
        //     {
        //         Move();
        //     }
        // }

        // void SetNextDestination()
        // {
        //     int randomIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
        //     Transform chosenPatrolPoint = patrolPoints[randomIndex];
        //     agent.SetDestination(chosenPatrolPoint.position);
        // }