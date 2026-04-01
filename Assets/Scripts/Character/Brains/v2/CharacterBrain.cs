#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using Animancer.Units;
using Animancer;
using System;
using UnityEngine;
using Animancer.Samples;
using static Animancer.Validate;
using CharacterNPCJobs;
using System.Collections.Generic;


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
        
        
        [Header("Curling States")]
        [SerializeField] private CharacterState _Sweep;

        [Header("Movement States")]
        [SerializeField] private CharacterState _Move;

        [Header("Extra States")]
        [SerializeField] private CharacterState _Cook;
        [SerializeField] private CharacterState _Dance;
        [SerializeField] private CharacterState _Forage;
        [SerializeField] private CharacterState _DeliverMail;
        [SerializeField] private CharacterState _UseComputer;
        [SerializeField] private List<CharacterState> _ListOfCharacterStates;


        [Header("Equipment")]
        [SerializeField] private EquipState _Equip;
        [SerializeField] private Weapon[] _Weapons;

        [Header("Settings")]
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
            // Equip takes priority
            UpdateEquip();

            // Posture changes can affect the availability of 
            // any subsequent choices of movement or action
            UpdatePosture();

            // RUN COMMAND
            // needs to be moved and automated?
            if (Input.GetKeyDown(KeyCode.R))
            {
                _Character.Parameters.Movement.WantsToRun = !_Character.Parameters.Movement.WantsToRun;
            }

            // Updating movement
            if (_Character.Parameters.Posture.CurrentPosture != 
                    CharacterParametersPosture.CharacterPostureState.Sitting &&
                (_Character.StateMachine.CurrentState.FullMovementControl == false || 
                _Character.StateMachine.CurrentState == _Move)
            )
            {
                UpdateMovement();    
            }
            
            // Update the action a character is doing
            UpdateActions();

            // Manual Check here for testing
            UpdateActionsManually();

        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            // _Character.Movement.UpdateMovementDirection();
            _Character.Movement.UpdateMovementParameters();
            


            if (_Character.Parameters.Movement.CurrentDestination == null && 
                _Character.Parameters.Movement.MovementDirection == Vector3.zero && 
                _Character.StateMachine.CurrentState != _Idle &&
                _Character.StateMachine.CurrentState != _Character.StateMachine.DefaultState
            )
            {
                
                if (_Character.Parameters.Movement.IsBaseFromIdleState == false && 
                    _Character.StateMachine.CurrentState.CanExitState
                ){
                    // Debug.Log("->> Enter Idle State");
                    _Character.StateMachine.TrySetState(_Idle);
                    // _Character.StateMachine.TryResetState(_Idle);
                }
            } else if (
                _Character.Parameters.Movement.MovementDirection != Vector3.zero &&
                _Character.Parameters.Movement.IsStopped == false //&&
                // _Character.StateMachine.CurrentState != _Move
            )
            {
                if (_Character.Parameters.Movement.IsBaseFromMoveState == false){
                    // Debug.Log("->> Enter Movement State");
                    // _Character.StateMachine.TrySetState(_Move);
                    _Character.StateMachine.TryResetState(_Move);
                }
                _Character.Movement.UpdateMovementParameters();
                // _Character.Movement.UpdateDistanceFromDestination();
                // _Character.Movement.UpdateSpeed();
                // // _Character.Movement.UpdateDirectionalMovement(); 
                if (_Character.Parameters.Jobs.CurrentJob != JobStateType.Curl){
                    _Character.Movement.UpdateTurning();
                }
            }
        }

        /************************************************************************************************************************/

        private void UpdateEquip()
        {
            // TODO: Don't use this input setting :(
            // if (SampleInput.RightMouseDown)
            // {
            //     int equippedWeaponIndex = Array.IndexOf(_Weapons, _Character.Equipment.Weapon);

            //     equippedWeaponIndex++;
            //     if (equippedWeaponIndex >= _Weapons.Length)
            //         equippedWeaponIndex = 0;

            //     _Equip.NextWeapon = _Weapons[equippedWeaponIndex];
            //     _InputBuffer.Buffer(_Equip, _AttackInputTimeOut);
            // }
        }


        /************************************************************************************************************************/

        private void UpdatePosture()
        {
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

        /************************************************************************************************************************/

        private void UpdateActions()
        {
            /// NOTE: This may be incorrect, since some actions can be overridden
            /// only by other actions
            if (!_Character.StateMachine.CurrentState.CanExitState)
            {
                return;
            }

            // Action Override to Get Player Attention
            if (_Character.Parameters.Status.IsCurling)
            {
                UpdateActionsWhileCurling();
                return;
            }

            // Action Override to Get Player Attention
            if (_Character.Parameters.Surroundings.IsPlayerInRangeToInteractWith && 
                !_Character.Parameters.Surroundings.IsEngagedInDialogueWithPlayer)
            {
                UpdateActionsToGetPlayerAttention();
                return;
            }

            // Is talking to player
            if (_Character.Parameters.Surroundings.IsPlayerInRangeToInteractWith && 
                _Character.Parameters.Surroundings.IsEngagedInDialogueWithPlayer)
            {
                UpdateActionsToTalkToPlayer();
                return;
            }

            

            /// Job Based Action Trees
            switch (_Character.JobStateMachine.CurrentState.JobType)
            {
                case JobStateType.Cook:
                    UpdateActionsForCookJob();
                    break;
                case JobStateType.Dance:
                    UpdateActionsForDanceJob();
                    break;
                case JobStateType.Forage:
                    UpdateActionsForForageJob();
                    break;
                case JobStateType.Party:
                    UpdateActionsForPartyJob();
                    break;
                case JobStateType.Mailman:
                    UpdateActionsForMailmanJob();
                    break;
                // case JobStateType.Shopkeeper:
                    // UpdateActionsForMailmanJob();
                    // break;
                // case JobStateType.Hospital:
                    // UpdateActionsForMailmanJob();
                    // break;
                default:
                    break;
            }
        }

        private void UpdateActionsWhileCurling()
        {
            // if (_Character.StateMachine.CurrentState != _Sweep)
            // {
            //     Debug.Log("-> Update Action: Sweep state");
            //     _Character.StateMachine.TrySetState(_Sweep);
            // }
        }


        private void UpdateActionsToGetPlayerAttention()
        {
            if (_Character.StateMachine.CurrentState != _Wave)
            {
                // Debug.Log("-> Update Action: Wave state");
                _Character.StateMachine.TrySetState(_Wave);
            }
        }

        private void UpdateActionsToTalkToPlayer()
        {
            if (_Character.StateMachine.CurrentState != _Talk)
            {
                // Debug.Log("-> Update Action: Talk state");
                _Character.StateMachine.TrySetState(_Talk);
            }
        }

        private void UpdateActionsForCookJob()
        {
            if (_Character.StateMachine.CurrentState != _Cook){
                // Debug.Log("-> Update Action: Cook state");
                _Character.StateMachine.TrySetState(_Cook);
            }
        }

        private void UpdateActionsForDanceJob()
        {
            if (_Character.StateMachine.CurrentState != _Dance){
                // Debug.Log("-> Update Action: Dance state");
                _Character.StateMachine.TrySetState(_Dance);
            }
        }

        private void UpdateActionsForDeskJob()
        {
            if (_Character.StateMachine.CurrentState.StateActionType != ActionType.UseComputer){
                // Debug.Log("-> Update Action: Use Computer state");
                _Character.StateMachine.TrySetState(_UseComputer);
            }
        }

        private void UpdateActionsForForageJob()
        {
            if (_Character.StateMachine.CurrentState.StateActionType != ActionType.Forage &&
                _Character.Parameters.Jobs.DesiredAction == ActionType.Forage &&
                _Character.Parameters.Movement.CurrentDestination == null &&
                // _Character.Parameters.Surroundings.Location.Type == LocationType.Mailbox && 
                _Character.StateMachine.CurrentState.CanExitState
            )
            {
                // Debug.Log("-> Update Action: Forage state");
                _Character.StateMachine.TrySetState(_Forage);
            }
            else if (_Character.StateMachine.CurrentState.StateActionType == ActionType.Forage &&
                _Character.Parameters.Jobs.DesiredAction == ActionType.Idle &&
                _Character.StateMachine.CurrentState.StateActionType != ActionType.Idle
            )
            {
                // Debug.Log("-> Update Action: Idle state");
                _Character.StateMachine.TrySetState(_Idle);
            }
        }

        private void UpdateActionsForPartyJob()
        {
            
            if (_Character.Parameters.Status.HungerLevel > 0.8 && 
                _Character.StateMachine.CurrentState != _Eat &&
                _Character.StateMachine.CurrentState.CanExitState
            )
            {
                // Debug.Log("-> Update Action: Eat state");
                _Character.StateMachine.TrySetState(_Eat);
            }

            // if _Drink.CanEnterState
            if (_Character.Parameters.Status.ThirstynessLevel > 0.7 && 
                _Character.StateMachine.CurrentState != _Drink &&
                _Character.StateMachine.CurrentState.CanExitState
            )
            {
                // Debug.Log("-> Update Action: Drink state");
                _Character.StateMachine.TrySetState(_Drink);
            }

            if (_Character.Parameters.Status.WantsToTalkLevel > 0.6 && 
                _Character.StateMachine.CurrentState != _Talk &&
                _Character.StateMachine.CurrentState.CanExitState
            )
            {
                // Debug.Log("-> Update Action: Talk state");
                _Character.StateMachine.TrySetState(_Talk);
            }

        }

        private void UpdateActionsForMailmanJob()
        {
            if (_Character.StateMachine.CurrentState.StateActionType != ActionType.DeliverMail &&
            _Character.Parameters.Jobs.DesiredAction == ActionType.DeliverMail &&
                // _Character.Parameters.Surroundings.Location.Type == LocationType.Mailbox && 
                _Character.StateMachine.CurrentState.CanExitState
            )
            {
                // Debug.Log("-> Update Action: Drink state");
                _Character.StateMachine.TrySetState(_DeliverMail);
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

        
    }
}







//  private void UpdateMovement()
// {
    // if (_Character.StateMachine.CurrentState != _Idle){
    //     Debug.Log("->> Enter Idle State");
    //     _Character.StateMachine.TrySetState(_Idle);
    // }
    // _Character.Movement.UpdateMovementDirection();
    
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
// }


// if (_Character.Parameters.Movement.IsBaseFromIdleState && 
            //     _Character.Parameters.Movement.MovementDirection != Vector3.zero &&
            //     _Character.Parameters.Movement.IsStopped == false
            // )
            // {
            //         Debug.Log("->> Enter Movement State");
            //         _Character.StateMachine.TrySetState(_Move);
            // }
            // else if (
            //     _Character.Parameters.Movement.IsBaseFromMoveState
            // ){
            //     _Character.Movement.UpdateMovementParameters();
            //     _Character.Movement.UpdateTurning();

            //     if (_Character.Parameters.Movement.CurrentDestination == null && 
            //         _Character.Parameters.Movement.MovementDirection == Vector3.zero && 
            //         _Character.StateMachine.CurrentState != _Idle &&
            //         _Character.StateMachine.CurrentState != _Character.StateMachine.DefaultState
            //     )
            //     {
                    
            //         if (_Character.Parameters.Movement.IsBaseFromIdleState == false){
            //             Debug.Log("->> Enter Idle State");
            //             _Character.StateMachine.TrySetState(_Idle);
            //         }
            //     } 

            // }