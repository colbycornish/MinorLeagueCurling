// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;
using System.Collections.Generic;
using CharacterNPC.v2;


/************************************************************************************************************************/
/*

SPECIFIC STATE
(will be included by default on all characters)

  This Job state represents when:
  - 

  Extensions:
  - 
*/
/************************************************************************************************************************/


namespace CharacterNPCJobs
{

    public class ForageState : JobState
    {
        /************************************************************************************************************************/

        [SerializeField] private List<Transform> _PatrolPoints;// = new List<Transform>();
        private int _CurrentPatrolIndex = 0;

        [SerializeField] private float _stoppingDistance = 5f;
        [SerializeField] private float _TimeToSpendForaging = 0.15f;
        [SerializeField] private float _TimeSpentForaging = 0.0f;
        

        [SerializeField] private UnityEvent _OnStart; // See the Read Me.
        [SerializeField] private UnityEvent _OnEnd; // See the Read Me.

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => 
            _PatrolPoints.Count > 0 && 
            Character.Parameters.Jobs.AvailableJobStates.Contains(
                JobStateType.Forage
            ); 

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Forage;

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            Debug.Log("PatrolState OnDisable");
            StopPatrolling();
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Debug.Log("PatrolState OnEnable");
            Character.Parameters.Jobs.CurrentJob = JobStateType.Forage;
            StartPatrolling();
        }

        protected virtual void Update()
        {

            if (
                Character.NavAgent.remainingDistance <= _stoppingDistance && 
                !Character.NavAgent.pathPending && 
                Character.StateMachine.CurrentState.StateActionType != ActionType.Forage
            )
            {
                Debug.Log("Stopping Patrol");
                // StopPatrolling();
                Character.Parameters.Status.IsPatrolling = false;
                Character.NavAgent.ResetPath();
                Character.NavAgent.isStopped = true;
                Character.Parameters.Movement.CurrentDestination = null;

                // StartForaging();
                Character.Parameters.Jobs.DesiredAction = ActionType.Forage;
                _TimeSpentForaging = 0.0f;

            } else if (Character.StateMachine.CurrentState.StateActionType == ActionType.Forage && 
                _TimeSpentForaging < _TimeToSpendForaging
            )
            {
                _TimeSpentForaging += Time.deltaTime * 0.01f;   
            }
            else if (_TimeSpentForaging > _TimeToSpendForaging && 
                Character.Parameters.Jobs.DesiredAction != ActionType.Idle && 
                Character.NavAgent.isStopped == true
            )
            {
                
                // StopForaging();
                Character.Parameters.Jobs.DesiredAction = ActionType.Idle;

                // StartPatrolling()
                SetNextPatrolLocation();
            } else if (Character.NavAgent.isStopped == true && 
                Character.Parameters.Jobs.CurrentAction == ActionType.Idle    
            )
            {
                Character.NavAgent.isStopped = false;
                Character.Parameters.Status.IsPatrolling = true;
                Debug.Log("Starting Patrol");
            }            
        }

        /// <summary>
        /// Forage
        /// </summary>
        
        private void StartForaging()
        {
            _TimeSpentForaging = 0f;
            Character.Parameters.Jobs.DesiredAction = ActionType.Forage;
            
        }

        private void StopForaging()
        {
            Character.Parameters.Jobs.DesiredAction = ActionType.Idle;
        }



        /// <summary>
        /// Patrolling
        /// </summary>

        private void StartPatrolling()
        {
            // Character.Parameters.Jobs.CurrentJob = JobStateType.Forage;
            Character.NavAgent.isStopped = false;
            Character.Parameters.Status.IsPatrolling = true;
            // Character.Parameters.Jobs.DesiredAction = ActionType.Idle;
            // UpdateDestination();
            SetNextPatrolLocation();
        }

        private void StopPatrolling()
        {
            Character.Parameters.Status.IsPatrolling = false;
            Character.NavAgent.ResetPath();
            Character.NavAgent.isStopped = true;
            Character.Parameters.Movement.CurrentDestination = null;
        }


        private void SetNextPatrolLocation()
        {
            _CurrentPatrolIndex++;
            if (_CurrentPatrolIndex >= _PatrolPoints.Count)
                _CurrentPatrolIndex = 0;

            Character.Parameters.Movement.CurrentDestination = _PatrolPoints[_CurrentPatrolIndex];

            // tell the nav agent to go to that location
            Character.NavAgent.SetDestination(_PatrolPoints[_CurrentPatrolIndex].position);
            Character.Parameters.Movement.DistanceFromDestination = 50; //Character.NavAgent.remainingDistance;
            
        }
    }
        
}
