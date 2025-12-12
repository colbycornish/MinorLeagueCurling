// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using CharacterNPC.v2;


namespace CharacterNPCJobs
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Jobs - Job State")]
    // [AnimancerHelpUrl(typeof(CharacterState))]
    public abstract class JobState : StateBehaviour, IOwnedState<JobState>
    {

        [SerializeField]
        private CharacterNPC.v2.Character _Character;
        public CharacterNPC.v2.Character Character => _Character;

        /************************************************************************************************************************/

        public StateMachine<JobState> OwnerStateMachine => _Character.JobStateMachine;

        /************************************************************************************************************************/

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.GetComponentInParentOrChildren(ref _Character);
        }
#endif

        /************************************************************************************************************************/
        // Explained in the Interruptions sample.
        /************************************************************************************************************************/

        public virtual JobStatePriority Priority => JobStatePriority.Low;

        public virtual bool CanInterruptSelf => false;

        // public virtual bool JobIsComplete => false;

        // public virtual bool CanInterruptJob => false;

        public override bool CanExitState //=> true;
        {
            get
            {
                // There are several different ways of accessing the state change details:
                // JobState nextState = StateChange<JobState>.NextState;
                // JobState nextState = this.GetNextState();
                JobState nextState = _Character.JobStateMachine.NextState;
                if (nextState == this)
                    return CanInterruptSelf;
                else if (Priority == JobStatePriority.Low)
                    return true;
                else
                    return nextState.Priority > Priority;
            }
        }

        // public virtual bool FullMovementControl => true;
    }
}
