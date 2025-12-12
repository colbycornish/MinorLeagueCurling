// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;
using System.Linq.Expressions;

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

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
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
