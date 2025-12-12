// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;

namespace CharacterNPCJobs
{

    public class IdleState : JobState
    {
        
        [SerializeField] private UnityEvent _OnStart;// See the Read Me.
        [SerializeField] private UnityEvent _OnEnd;// See the Read Me.

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; 

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            Debug.Log("IdleJob OnDisable");
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Debug.Log("IdleJob OnEnable");
        }
    }
        
}
