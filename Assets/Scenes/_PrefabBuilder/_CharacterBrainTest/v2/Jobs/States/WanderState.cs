// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;
using System.Linq.Expressions;
using CharacterNPC.v2;

/************************************************************************************************************************/
/*

BASIC STATE
(will be included by default on all characters)

  This Job state represents when:
  - The NPC is simply supposed to wander around a pre-designated zone.
  - Exact points are not apart of this, but an exact destination will be set.
  - Needs to stop at each point. 

  Extensions:
  - when at a specific point,
  they should be able to pause and reconsider what to do next (cooldown re-entry)
  - They should be able to do more actions?

*/
/************************************************************************************************************************/


namespace CharacterNPCJobs
{

    public class WanderState : JobState
    {
        
        // [SerializeField] private List<Transform> _PatrolPoints;// = new List<Transform>();
        private int _CurrentPatrolIndex = 0;

        [SerializeField] private float _stoppingDistance = 5f;
        [SerializeField] private float _wanderZone = 5f;
        

        [SerializeField] private UnityEvent _OnStart;
        [SerializeField] private UnityEvent _OnEnd; 

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; 

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Wander;

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Character.Parameters.Jobs.CurrentJob = JobStateType.Wander;
        }

        protected virtual void Update()
        {
            
        }

        private void UpdateDestination()
        {
            
        }

        private void SetNextLocation()
        {
            
        }
    }
        
}
