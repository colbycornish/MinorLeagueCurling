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
using CharacterNPCJobs;


namespace CharacterNPC.v2
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class CharacterBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Character _Character;

        [Header("Basic States")]
        [SerializeField] private CharacterState _Idle;
        [SerializeField] private CharacterState _Talk;
        [SerializeField] private CharacterState _Pose;
        [SerializeField] private CharacterState _Wave;
        [SerializeField] private CharacterState _Drink;
        [SerializeField] private CharacterState _Eat;
        [SerializeField] private CharacterState _Move;
        [SerializeField] private EquipState _Equip;
        [SerializeField] private CharacterState _Sweep;

        [Header("Extra States")]
        [SerializeField] private CharacterState _Cook;
        [SerializeField] private CharacterState _Dance;
        

        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;

        [SerializeField] private Weapon[] _Weapons;

        private StateMachine<CharacterState>.InputBuffer _InputBuffer;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            _InputBuffer = new(_Character.StateMachine);
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            UpdateEquip();
            if (Input.GetKeyDown(KeyCode.T))
            {
                UpdatePosture();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _Character.Parameters.Movement.WantsToRun = !_Character.Parameters.Movement.WantsToRun;
            }

            if (_Character.Parameters.Posture.CurrentPosture != 
                    CharacterParametersPosture.CharacterPostureState.Sitting &&
                (_Character.StateMachine.CurrentState.FullMovementControl == false || 
                _Character.StateMachine.CurrentState == _Move)
            )
            {
                UpdateMovement();    
            }
            
            UpdateActions();
            UpdateActionsManually();

        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            // if (_Character.StateMachine.CurrentState != _Idle){
            //     Debug.Log("->> Enter Idle State");
            //     _Character.StateMachine.TrySetState(_Idle);
            // }
            _Character.Movement.UpdateMovementDirection();
            
            if (_Character.Parameters.Movement.CurrentDestination == null && 
                _Character.Parameters.Movement.MovementDirection == Vector3.zero && 
                _Character.StateMachine.CurrentState != _Idle &&
                _Character.StateMachine.CurrentState != _Idle != _Character.StateMachine.DefaultState
            )
            {
                
                if (_Character.Parameters.Movement.IsBaseFromIdleState == false){
                    Debug.Log("->> Enter Idle State");
                    _Character.StateMachine.TrySetState(_Idle);
                }
            } else if (
                _Character.Parameters.Movement.MovementDirection != Vector3.zero &&
                _Character.Parameters.Movement.IsStopped == false &&
                _Character.StateMachine.CurrentState != _Move
            )
            {
                if (_Character.Parameters.Movement.IsBaseFromMoveState == false){
                    Debug.Log("->> Enter Movement State");
                    _Character.StateMachine.TrySetState(_Move);
                }
                _Character.Movement.UpdateDistanceFromDestination();
                _Character.Movement.UpdateSpeed();
                // // _Character.Movement.UpdateDirectionalMovement(); 
                _Character.Movement.UpdateTurning();
            }
            
            // _Character.Movement.UpdateDistanceFromDestination();
            // _Character.Movement.UpdateSpeed();
            
            // // _Character.Movement.UpdateDirectionalMovement(); 
            // _Character.Movement.UpdateTurning();

            // if (_Character.Parameters.Movement.MovementDirection == Vector3.zero || 
            //     _Character.Parameters.Movement.IsMoving == false
            // )
            // {
            //     _Character.StateMachine.TrySetState(_Idle);
            //     return;
            // } else
            // {

            //     _Character.StateMachine.TrySetState(_Move);
            // }
        }

        /************************************************************************************************************************/

        private void UpdateActions()
        {
            /// COOK
            if (_Character.JobStateMachine.CurrentState.JobType == JobStateType.Cook)
            {
                if (_Character.StateMachine.CurrentState != _Cook){
                    Debug.Log("-> Update Action: Cook state");
                    _Character.StateMachine.TrySetState(_Cook);
                }
            }

            /// DANCE
            else if (_Character.JobStateMachine.CurrentState.JobType == JobStateType.Dance)
            {
                if (_Character.StateMachine.CurrentState != _Dance){
                    Debug.Log("-> Update Action: Dance state");
                    _Character.StateMachine.TrySetState(_Dance);
                }
            }

            /// PARTY STATE
            else if (_Character.JobStateMachine.CurrentState.JobType == JobStateType.Party)
            {
                if (_Character.Parameters.Status.HungerLevel > 0.8 && 
                    _Character.StateMachine.CurrentState != _Eat &&
                    _Character.StateMachine.CurrentState.CanExitState
                )
                {
                    Debug.Log("-> Update Action: Eat state");
                    _Character.StateMachine.TrySetState(_Eat);
                }

                if (_Character.Parameters.Status.ThirstynessLevel > 0.8 && 
                    _Character.StateMachine.CurrentState != _Drink &&
                    _Character.StateMachine.CurrentState.CanExitState
                )
                {
                    Debug.Log("-> Update Action: Drink state");
                    _Character.StateMachine.TrySetState(_Drink);
                }
                
            }
            else
            {

                // if (_Character.StateMachine.CurrentState != _Idle){
                //     Debug.Log("-> Update Action: Idle state");
                //     _Character.StateMachine.TrySetState(_Idle);   
                // }
            }

        }

        private void UpdateActionsManually()
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
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                _InputBuffer.Buffer(_Sweep, _AttackInputTimeOut);
            }
            // Character Wants To Idle && is not Idling
            
            
            _InputBuffer.Update();
        }

        /************************************************************************************************************************/

        private void UpdateEquip()
        {
            if (SampleInput.RightMouseDown)
            {
                int equippedWeaponIndex = Array.IndexOf(_Weapons, _Character.Equipment.Weapon);

                equippedWeaponIndex++;
                if (equippedWeaponIndex >= _Weapons.Length)
                    equippedWeaponIndex = 0;

                _Equip.NextWeapon = _Weapons[equippedWeaponIndex];
                _InputBuffer.Buffer(_Equip, _AttackInputTimeOut);
            }
        }


        /************************************************************************************************************************/

        private void UpdatePosture()
        {
            // if (_Character.Parameters.Posture.CurrentPosture ==
            //     CharacterParametersPosture.CharacterPostureState.Standing)
            // {
            //     _Character.Parameters.Posture.DesiredPosture =
            //         CharacterParametersPosture.CharacterPostureState.Crouching;
            // }
            // else if (_Character.Parameters.Posture.CurrentPosture ==
            //     CharacterParametersPosture.CharacterPostureState.Crouching)
            // {
            //     _Character.Parameters.Posture.DesiredPosture =
            //         CharacterParametersPosture.CharacterPostureState.Sitting;

            // } else if (_Character.Parameters.Posture.CurrentPosture ==
            //     CharacterParametersPosture.CharacterPostureState.Sitting)
            // {
            //     _Character.Parameters.Posture.DesiredPosture =
            //         CharacterParametersPosture.CharacterPostureState.LayingDown;

            // } else if (_Character.Parameters.Posture.CurrentPosture ==
            //     CharacterParametersPosture.CharacterPostureState.LayingDown)
            // {
            //     _Character.Parameters.Posture.DesiredPosture =
            //         CharacterParametersPosture.CharacterPostureState.Standing;
            // }

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
            


        }


        
    }
}


