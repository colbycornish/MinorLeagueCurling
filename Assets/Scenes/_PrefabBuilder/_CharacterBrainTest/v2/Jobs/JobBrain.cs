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
using CharacterNPC.v2;

namespace CharacterNPCJobs
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Jobs - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class JobBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private CharacterNPC.v2.Character _Character;
        [Header("Standard Jobs")]
        [SerializeField] private JobState _Curling;
        [SerializeField] private JobState _Cooking;
        [SerializeField] private JobState _Dialogue;
        [SerializeField] private JobState _Follow;
        [SerializeField] private JobState _Idle;
        [SerializeField] private JobState _Patrol;
        [SerializeField] private JobState _Wander;

        [Header("Extra Jobs")]
        [SerializeField] private JobState _Dance;

        [SerializeField] private JobState _Party;

        [Header("Settings")]
        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;

        private StateMachine<JobState>.InputBuffer _InputBuffer;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            Debug.Log("JobBrain Active");
            _InputBuffer = new(_Character.JobStateMachine);
            // _Character.JobStateMachine.TrySetDefaultState();
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            // UpdateMovement();
            // UpdateActions();
            UpdateJob();
        }

        /************************************************************************************************************************/

        private void UpdateJob()
        {
            /************************************************************/
            // First, let's handle the items that are immediate iterrupts.
            // These usually invole direct player interactions that should take
            // priority over anything that the NPC might otherwise be doing. 
            
            /************************************************************/
            // Character is talking to the player
            if (_Character.Parameters.Surroundings.IsEngagedInDialogueWithPlayer && 
                _Character.JobStateMachine.CurrentState != _Dialogue
            )
            {
                _Character.JobStateMachine.TrySetState(_Dialogue);
            }

            // Get a players attention
            // if (_Character.Parameters.Surroundings.IsPlayerInView && 
            //     _Character.Parameters.Surroundings.IsPlayerInRange && 
            //     _Character.JobStateMachine.CurrentState != _GetPlayersAttention
            // )
            // {
            //     _Character.JobStateMachine.TrySetState(_GetPlayersAttention);
            // }

            /************************************************************/
            // Second, let's see if we can exit our current state. If we can,
            // let's see if there's something of greater value that we should
            // move on to.
            // 
            // Else we should retain the same job, since there's really 
            // no reason to continue.
            //
            /************************************************************/

            else if (_Character.JobStateMachine.CurrentState.CanExitState == true)
            {
                UpdateMostDesiredJob();
                if (_Character.Parameters.Jobs.CurrentJob != _Character.Parameters.Jobs.DesiredJob)
                {
                    ChangeJobs();
                }
            
                
            }

            /************************************************************/
            // Lastly, let's examine our base job states, and see if there's
            // something to fallback on.
            /************************************************************/
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("JobBrain - Trying to set Patrol State");
                // _InputBuffer.Buffer(_Patrol, _AttackInputTimeOut);
                _Character.JobStateMachine.TrySetState(_Patrol);
            }

            // Patrol
            // if (_Character.Parameters.Status.WantsToPatrolLevel > 50 || 
            //     _Character.Parameters.Jobs.DesiredJob == 
            //         JobStateType.Patrol)
            // {
            //     Debug.Log("JobBrain - Trying to set Patrol State");
            //     _Character.JobStateMachine.TrySetState(_Patrol);
            // }

            // Wander
            if (_Character.Parameters.Status.WantsToWanderLevel > 50 ||
                _Character.Parameters.Jobs.DesiredJob == 
                    JobStateType.Wander)
            {
                Debug.Log("JobBrain - Trying to set Wander State");
                _Character.JobStateMachine.TrySetState(_Patrol);
            }


            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("JobBrain - Trying to set Idle State");
                // _InputBuffer.Buffer(_Patrol, _AttackInputTimeOut);
                _Character.JobStateMachine.TrySetState(_Idle);
            }
        }

        /************************************************************************************************************************/

        private void ChangeJobs()
        {
            
            switch (_Character.Parameters.Jobs.DesiredJob) {
                case JobStateType.Cook:
                    _Character.JobStateMachine.TrySetState(_Cooking);
                    break;
                case JobStateType.Dance:
                    _Character.JobStateMachine.TrySetState(_Dance);
                    break;
                case JobStateType.Party:
                    _Character.JobStateMachine.TrySetState(_Party);
                    break;
                case JobStateType.Patrol:
                    _Character.JobStateMachine.TrySetState(_Patrol);
                    break;
                case JobStateType.Follow:
                    _Character.JobStateMachine.TrySetState(_Follow);
                    break;
                case JobStateType.Talk:
                    _Character.JobStateMachine.TrySetState(_Dialogue);
                    break;
                case JobStateType.Wander:
                    _Character.JobStateMachine.TrySetState(_Wander);
                    break;

                case JobStateType.Clean:
                    // _Character.JobStateMachine.TrySetState(_Eating);
                    break;
                case JobStateType.Drink:
                    break;
                case JobStateType.Eat:
                    // _Character.JobStateMachine.TrySetState(_Dialogue);
                    break;
                case JobStateType.Idle:
                default:
                    _Character.JobStateMachine.TrySetState(_Idle);
                    break;
            }
        }

        private void UpdateMostDesiredJob()
        {

            foreach (JobStateType jobType in _Character.Parameters.Jobs.AvailableJobStates)
            {
                switch (jobType) {
                    case JobStateType.Cook:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Cook;
                        break;
                    case JobStateType.Drink:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Drink;
                        break;
                    case JobStateType.Clean:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Clean;
                        break;
                    case JobStateType.Eat:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Eat;
                        break;
                    case JobStateType.Party:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Party;
                        break;
                    case JobStateType.Dance:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Dance;
                        break;
                    case JobStateType.Patrol:
                        _Character.Parameters.Jobs.DesiredJob = JobStateType.Patrol;
                        break;
                    
                    // case JobStateType.Patrol:
                    //     _Character.Parameters.Jobs.DesiredJob = JobStateType.Patrol;
                    //     break;
                    // case JobStateType.Follow:
                    //     _Character.Parameters.Jobs.DesiredJob = JobStateType.Follow;
                    //     break;
                    // case JobStateType.Talk:
                    //     _Character.Parameters.Jobs.DesiredJob = JobStateType.Talk;
                    //     break;
                    // case JobStateType.Wander:
                    //     _Character.Parameters.Jobs.DesiredJob = JobStateType.Wander;
                    //     break;
                    // case JobStateType.Idle:
                    //     _Character.Parameters.Jobs.DesiredJob = JobStateType.Idle;
                    //     break;
                    default:
                        // _Character.Parameters.Jobs.DesiredJob = JobStateType.Idle;
                        break;
                }
            }
            
        }
    }
}


