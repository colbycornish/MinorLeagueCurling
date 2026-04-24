using UnityEngine;
using Animancer;

/************************************************************************************************************************/
/*

BASIC STATE
(will be included by default on all characters)

This Job state represents when:
- This NPC is tasked with following something (and needs to have the position of that
thing updated).

Extensions:
- Allowing multiple action states to occur while following?

/************************************************************************************************************************/


namespace CharacterNPCJobs
{

    public class FollowState : JobState
    {
        /************************************************************************************************************************/

        [SerializeField] private Transform _FollowTarget;// = new List<Transform>();

        // [SerializeField] private float _stoppingDistance = 5f;
        

        [SerializeField] private UnityEvent _OnStart; // See the Read Me.
        [SerializeField] private UnityEvent _OnEnd; // See the Read Me.

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; 
        //Character.Parameters.FollowTarget != null; 

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Wander;

        /************************************************************************************************************************/


        /************************************************************************************************************************/
        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            // Debug.Log("PatrolState OnDisable - Setting first patrol location");
            Character.Parameters.Status.IsFollowing = false;
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            // Debug.Log("PatrolState OnEnable - Setting first patrol location");
            Character.Parameters.Status.IsFollowing = true;
            Character.Parameters.Jobs.CurrentJob = JobStateType.Follow;
            SetNextPatrolLocation();
        }

        protected virtual void Update()
        {
            if (Character.JobStateMachine.CurrentState == this)
            {
                UpdateDestination();
            }
        }

        
        private void UpdateDistanceFromDestination()
        {
            // Character.Parameters.CurrentDestination = _PatrolPoints[_CurrentPatrolIndex];
        }

        private void UpdateDestination()
        {
            // Transform _currentDestination = Character.Parameters.CurrentDestination;
            
            // if (_currentDestination == null)
            // {
            //     SetNextPatrolLocation();
            // }
            // else if (Character.NavAgent.remainingDistance <= _stoppingDistance && !Character.NavAgent.pathPending)
            // {
            //     Character.JobStateMachine.TrySetDefaultState();
            // }
            // else if (_currentDestination != null)
            // {
            //     Character.Parameters.DistanceFromDestination = Character.NavAgent.remainingDistance;
            // }
        }

        private void SetNextPatrolLocation()
        {
            
        }
    }
        
}
