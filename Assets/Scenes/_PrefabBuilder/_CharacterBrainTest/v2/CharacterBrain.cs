// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using Animancer.Units;
using Animancer;
using System;
using UnityEngine;
using Animancer.Samples;
using Unity.Entities.UniversalDelegates;
using static Animancer.Validate;
using Unity.VisualScripting;


namespace CharacterNPC.v2
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class CharacterBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Character _Character;
        [SerializeField] private CharacterState _Idle;
        [SerializeField] private CharacterState _Talk;
        [SerializeField] private CharacterState _Pose;
        [SerializeField] private CharacterState _Wave;
        [SerializeField] private CharacterState _Drink;
        [SerializeField] private CharacterState _Eat;
        [SerializeField] private CharacterState _Move;
        [SerializeField] private CharacterState _Equip;
        [SerializeField] private CharacterState _Sweep;

        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;

        private StateMachine<CharacterState>.InputBuffer _InputBuffer;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            _InputBuffer = new(_Character.StateMachine);
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                UpdatePosture();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _Character.Parameters.Movement.WantsToRun = !_Character.Parameters.Movement.WantsToRun;
            }
            if (_Character.StateMachine.CurrentState.FullMovementControl != true || 
                _Character.StateMachine.CurrentState == _Move)
            {
                UpdateMovement();    
            }
            UpdateActions();

            
            
        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            _Character.Movement.UpdateMovementDirection();
            _Character.Movement.UpdateDistanceFromDestination();
            _Character.Movement.UpdateSpeed();
            // _Character.Movement.UpdateDirectionalMovement();
            _Character.Movement.UpdateTurning();
            if (_Character.Parameters.Movement.MovementDirection == Vector3.zero || 
                _Character.Parameters.Movement.IsMoving == false
            )
            {
                _Character.StateMachine.TrySetState(_Idle);
                return;
            } else
            {
                _Character.StateMachine.TrySetState(_Move);
            }
        }

        /************************************************************************************************************************/

        private void UpdateActions()
        {
            // Jump gets priority for better platforming.
            // Character Wants To Pose && is not posing
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _InputBuffer.Buffer(_Idle, _AttackInputTimeOut);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _InputBuffer.Buffer(_Pose, _AttackInputTimeOut);
            }
            // Character Wants To Talk && is not Talking
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _InputBuffer.Buffer(_Talk, _AttackInputTimeOut);
            }
            // Character Wants To Wave && is not Waving
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _InputBuffer.Buffer(_Wave, _AttackInputTimeOut);
            }
            // Character Wants To Eat && is not Eating
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _InputBuffer.Buffer(_Eat, _AttackInputTimeOut);
            }
            // Character Wants To Drink && is not Drinking
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                _InputBuffer.Buffer(_Drink, _AttackInputTimeOut);
            }
            // Character Wants To Idle && is not Idling
            
            
            _InputBuffer.Update();
        }

        private void UpdatePosture()
        {


            if (_Character.Parameters.Posture.CurrentPosture ==
                CharacterParametersPosture.CharacterPostureState.Standing)
            {
                _Character.Parameters.Posture.DesiredPosture =
                    CharacterParametersPosture.CharacterPostureState.Crouching;
            }
            else if (_Character.Parameters.Posture.CurrentPosture ==
                CharacterParametersPosture.CharacterPostureState.Crouching)
            {
                _Character.Parameters.Posture.DesiredPosture =
                    CharacterParametersPosture.CharacterPostureState.Sitting;

            } else if (_Character.Parameters.Posture.CurrentPosture ==
                CharacterParametersPosture.CharacterPostureState.Sitting)
            {
                _Character.Parameters.Posture.DesiredPosture =
                    CharacterParametersPosture.CharacterPostureState.LayingDown;

            } else if (_Character.Parameters.Posture.CurrentPosture ==
                CharacterParametersPosture.CharacterPostureState.LayingDown)
            {
                _Character.Parameters.Posture.DesiredPosture =
                    CharacterParametersPosture.CharacterPostureState.Standing;
            }

            if (_Character.Parameters.Posture.DesiredPosture !=
                _Character.Parameters.Posture.CurrentPosture)
            {
                if(_Character.StateMachine.CurrentState == _Idle)
                {
                    _Character.StateMachine.TryResetState(_Idle);
                }
                else if(_Character.StateMachine.CurrentState == _Move)
                {
                    _Character.StateMachine.TryResetState(_Move);
                }
            }
            // if (_Character.Parameters.Posture.WantsToStand == true && 
            //     _Character.Parameters.Posture.IsStanding == false
            // )
            // {
                
            // }
            // if (_Character.Parameters.Posture.WantsToSit == true && 
            //     _Character.Parameters.Posture.IsSitting == false
            // )
            // {
                
            // }
            // if (_Character.Parameters.Posture.WantsToCrouch == true && 
            //     _Character.Parameters.Posture.IsCrouching == false
            // )
            // {
                
            // }
            // if (_Character.Parameters.Posture.WantsToLayDown == true && 
            //     _Character.Parameters.Posture.IsLayingDown == false
            // )
            // {
                
            // }
          
            
            // if (_Character.Parameters.Posture.IsStanding == true)
            // {
            //     _Character.Parameters.Posture.IsStanding = false;
            //     _Character.Parameters.Posture.IsCrouching = true;
            // }
            // else if (_Character.Parameters.Posture.IsCrouching == true)
            // {
            //     _Character.Parameters.Posture.IsCrouching = false;
            //     _Character.Parameters.Posture.IsSitting = true;
            // }
            // else if (_Character.Parameters.Posture.IsSitting == true)
            // {
            //     _Character.Parameters.Posture.IsSitting = false;
            //     _Character.Parameters.Posture.IsStanding = true;
            // } else
            // {
            //     _Character.Parameters.Posture.IsStanding = true;
            //     _Character.Parameters.Posture.IsCrouching = false;
            //     _Character.Parameters.Posture.IsSitting = false;
            // }

            // if(_Character.StateMachine.CurrentState == _Idle)
            // {
            //     _Character.StateMachine.TryResetState(_Idle);
            // }
            // else if(_Character.StateMachine.CurrentState == _Move)
            // {
            //     _Character.StateMachine.TryResetState(_Move);
            // }


        }
    }
}


