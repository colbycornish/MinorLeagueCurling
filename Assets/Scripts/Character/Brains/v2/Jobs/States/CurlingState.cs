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

BASIC STATE
(will be included by default on all characters)

  This Job state represents when:
  - This NPC is engaged in Curling activities (as part of a team). 
  - This should subsequently lock the NPC out of other activities, 
  and take priority over any other task.
  
  
  Extensions:

/************************************************************************************************************************/


namespace CharacterNPCJobs
{
    // [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Drink State")]
    public class CurlingState : JobState
    {
        /************************************************************************************************************************/

        [SerializeField] private GameObject _CurlingStone;// = new List<Transform>();
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

        public override bool CanEnterState => _CurlingStone != null; 

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Curl;

        /************************************************************************************************************************/


        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            Debug.Log("CurlingState OnDisable");
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Debug.Log("CurlingState OnEnable");
            Character.Parameters.Jobs.CurrentJob = JobStateType.Curl;
        }

        protected virtual void Update()
        {
            if (Character.JobStateMachine.CurrentState == this)
            {
                UpdateDestination();
            }
        }

        
        // private void UpdateDistanceFromDestination()
        // {
        //     Character.Parameters.CurrentDestination = _PatrolPoints[_CurrentPatrolIndex];
        // }

        private void UpdateDestination()
        {
            // Transform _currentDestination = Character.Parameters.Movement.CurrentDestination;
            
            // if (_currentDestination == null)
            // {
            //     SetNextPatrolLocation();
            // }
            // else if (Character.NavAgent.remainingDistance <= _stoppingDistance && !Character.NavAgent.pathPending)
            // {
            //     Debug.Log("PatrolState Reached Destination - Should be going idle");
            //     Character.JobStateMachine.TrySetDefaultState();
            // }
            // else if (_currentDestination != null)
            // {
            //     Character.Parameters.Movement.DistanceFromDestination = Character.NavAgent.remainingDistance;
            // }
        }

        private void SetNextPatrolLocation()
        {
            // _CurrentPatrolIndex++;
            // if (_CurrentPatrolIndex >= _PatrolPoints.Count)
            //     _CurrentPatrolIndex = 0;

            // Character.Parameters.Movement.CurrentDestination = _PatrolPoints[_CurrentPatrolIndex];

            // // tell the nav agent to go to that location
            // Character.NavAgent.SetDestination(_PatrolPoints[_CurrentPatrolIndex].position);
            // Character.Parameters.Movement.DistanceFromDestination = Character.NavAgent.remainingDistance;
        }
    }
        
}
