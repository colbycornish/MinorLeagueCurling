// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using Animancer.Units;
using Animancer;
using System;
using UnityEngine;
using Animancer.Samples;
using Unity.Entities.UniversalDelegates;


namespace AnimalNPC
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Animal - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class AnimalBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Animal _Animal;
        [SerializeField] private AnimalState _Move;
        [SerializeField] private AnimalState _Idle;
        [SerializeField] private AnimalState _Patrol;
        [SerializeField] private AnimalState _Eat;
        [SerializeField] private AnimalState _Sleep;
        [SerializeField] private AnimalState _Dead;
        [SerializeField] private AnimalState _Attack;
        [SerializeField, Seconds] private float _InputTimeOut = 0.5f;
        private StateMachine<AnimalState>.InputBuffer _InputBuffer;

        [Header("Waiting State")]
        [SerializeField] private float waitTimer;
        [SerializeField] public bool isWaiting = false;
        [SerializeField] public float waitTimeAtWaypoint = 5f; // Time to wait at each waypoint

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            _InputBuffer = new StateMachine<AnimalState>.InputBuffer(_Animal.StateMachine);
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            // if is dead, go to dead state
            if (_Animal.Parameters.IsDead)
            {
                _InputBuffer.Buffer(_Dead, _InputTimeOut);
                _Animal.StateMachine.TrySetState(_Dead);
                // Debug.Log("Animal is dead, switching to Dead State");
                return;
            }

            UpdateParameters();
            _InputBuffer.Update();
            // If the animal is idle, decide what to do next
            if (_Animal.StateMachine.CurrentState == _Animal.StateMachine.DefaultState)
            {
                // continue to wait around
                if (isWaiting == true)
                {
                    waitTimer -= Time.deltaTime;
                    if (waitTimer <= 0)
                    {
                        isWaiting = false;
                        // Debug.Log("Animal finished waiting.");
                        return; // Still waiting, do not decide next action yet
                    }
                }
                else
                {
                    DecideWhatToDoNext();
                }

                // DecideWhatToDoNext();
                return;
            }
            else
            {

                switch (_Animal.StateMachine.CurrentState)
                {
                    case AnimalState state when state == _Eat:
                        if (_Animal.Parameters.HungerLevel <= 0.2f)
                        {
                            ReturnToIdleState();
                        }
                        return;
                    case AnimalState state when state == _Sleep:
                        if (_Animal.Parameters.SleepinessLevel <= 0.2f)
                        {
                            ReturnToIdleState();
                        }
                        return;
                    case AnimalState state when state == _Patrol:
                        if (_Animal.Parameters.DistanceFromDestination <= 2f && _Animal.Parameters.CurrentDestination != null)
                        {
                            ReturnToIdleState();
                        }
                        return;
                    // Already in one of these states, do nothing
                    default:
                        return;
                }
                

            }
            

            // UpdateMovement();
            // UpdateEquip();
            // UpdateAction();
            // UpdateParameters();
            // _InputBuffer.Update();
        }







        
        private void ReturnToIdleState()
        {
            isWaiting = true;
            waitTimer = waitTimeAtWaypoint;

            _Animal.StateMachine.TrySetState(_Animal.StateMachine.DefaultState);
            _Animal.NavAgent.ResetPath();
            _Animal.Parameters.CurrentDestination = null;
            _Animal.Parameters.DistanceFromDestination = 0f;
        }

        /************************************************************************************************************************/

        private void UpdateMovement()// This method is identical to the one in MovingCharacterBrain.
        {

            Vector2 input = SampleInput.WASD;
            if (input != Vector2.zero)
            {
                // Convert the input to 3D in the XZ plane.
                Vector3 movementDirection = new Vector3(input.x, 0, input.y);

                // Apply the camera's rotation and set the parameter.
                _Animal.Parameters.MovementDirection = movementDirection;

                // Enter the locomotion state if we aren't already in it.
                _Animal.StateMachine.TrySetState(_Move);
            }
            else
            {
                _Animal.Parameters.MovementDirection = Vector3.zero;
                _Animal.StateMachine.TrySetDefaultState();
            }

            // Indicate whether the character wants to run or not.
            _Animal.Parameters.WantsToRun = SampleInput.LeftShiftHold;
        }

        /************************************************************************************************************************/

        private void UpdateAction()
        {
            if (SampleInput.LeftMouseDown)
            {
                if (_Animal.StateMachine.CurrentState != _Eat)
                {
                    _InputBuffer.Buffer(_Eat, _InputTimeOut);
                    _Animal.StateMachine.TrySetState(_Eat);
                    return;
                }
                else if (_Animal.StateMachine.CurrentState != _Dead)
                {
                    _InputBuffer.Buffer(_Dead, _InputTimeOut);
                    _Animal.StateMachine.TrySetState(_Dead);
                    return;
                }

            }
        }
        

        private void UpdateParameters()
        {
            // Update hunger level over time
            if (_Animal.StateMachine.CurrentState != _Eat)
            {
                _Animal.Parameters.HungerLevel += Time.deltaTime * 0.01f;
                _Animal.Parameters.HungerLevel = Mathf.Clamp01(_Animal.Parameters.HungerLevel);
            }
            // Update sleepiness level over time
            if (_Animal.StateMachine.CurrentState != _Sleep)
            {
                _Animal.Parameters.SleepinessLevel += Time.deltaTime * 0.005f;
                _Animal.Parameters.SleepinessLevel = Mathf.Clamp01(_Animal.Parameters.SleepinessLevel);
            }
        }

        /************************************************************************************************************************/


        private void DecideWhatToDoNext()
        {
            // Debug.Log("Animal is deciding what to do next...");
            if (_Animal.Parameters.HungerLevel >= 0.8f)
            {
                _InputBuffer.Buffer(_Eat, _InputTimeOut);
                _Animal.StateMachine.TrySetState(_Eat);
                // Debug.Log("Animal is hungry, switching to Eat State");
                return;
            }
            else if (_Animal.Parameters.SleepinessLevel >= 0.8f)
            {
                _InputBuffer.Buffer(_Sleep, _InputTimeOut);
                _Animal.StateMachine.TrySetState(_Sleep);
                // Debug.Log("Animal is sleepy, switching to Sleep State");
                return;
            }
            else if (
                // _Animal.Parameters.DistanceFromDestination > 10f ||
                _Animal.Parameters.CurrentDestination == null
            )
            {
                // Debug.Log("SEND THE ANIMAL ON PATROL!");
                // ReturnToIdleState();
                _InputBuffer.Buffer(_Patrol, _InputTimeOut);
                _Animal.StateMachine.TrySetState(_Patrol);
                //     Debug.Log("Animal is far from destination, switching to Patrol State");
                return;
            }
            else
            {

                ReturnToIdleState();
                // _InputBuffer.Buffer(_Patrol, _InputTimeOut);
                // _Animal.StateMachine.TrySetState(_Patrol);
                // Debug.Log("Animal is patrolling, switching to Patrol State");
            }

            return;
        }
    }
}
