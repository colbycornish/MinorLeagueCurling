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

    public class MailmanState : JobState
    {
        /************************************************************************************************************************/

        [SerializeField] private List<Transform> _PatrolPoints;// = new List<Transform>();
        private int _CurrentPatrolIndex = 0;

        [SerializeField] private float _stoppingDistance = 5f;
        

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
                JobStateType.Mailman
            ); 

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Mailman;

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            Debug.Log("PatrolState OnDisable");
            Character.Parameters.Status.IsPatrolling = false;
            Character.NavAgent.ResetPath();
            Character.NavAgent.isStopped = true;
            Character.Parameters.Movement.CurrentDestination = null;
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Debug.Log("PatrolState OnEnable");
            Character.Parameters.Jobs.CurrentJob = JobStateType.Mailman;
            Character.NavAgent.isStopped = false;
            Character.Parameters.Status.IsPatrolling = true;
            SetNextPatrolLocation();
        }

        protected virtual void Update()
        {
            if (Character.JobStateMachine.CurrentState == this)
            {
                UpdateDestination();
            }
            /*
                if at the destination, equip the mail item, then unequip the mail item




            */
        }

        
        // private void UpdateDistanceFromDestination()
        // {
        //     Character.Parameters.CurrentDestination = _PatrolPoints[_CurrentPatrolIndex];
        // }

        private void UpdateDestination()
        {
            Transform _currentDestination = Character.Parameters.Movement.CurrentDestination;
            
            if (_currentDestination == null)
            {
                SetNextPatrolLocation();
            }
            else if (Character.NavAgent.remainingDistance <= _stoppingDistance && !Character.NavAgent.pathPending)
            {
                Debug.Log("PatrolState Reached Destination - Should be going idle");
                Character.JobStateMachine.TrySetDefaultState();
            }
            else if (_currentDestination != null)
            {
                Character.Parameters.Movement.DistanceFromDestination = Character.NavAgent.remainingDistance;
            }
        }

        private void SetNextPatrolLocation()
        {
            _CurrentPatrolIndex++;
            if (_CurrentPatrolIndex >= _PatrolPoints.Count)
                _CurrentPatrolIndex = 0;

            Character.Parameters.Movement.CurrentDestination = _PatrolPoints[_CurrentPatrolIndex];

            // tell the nav agent to go to that location
            Character.NavAgent.SetDestination(_PatrolPoints[_CurrentPatrolIndex].position);
            Character.Parameters.Movement.DistanceFromDestination = Character.NavAgent.remainingDistance;
        }
    }
        
}
