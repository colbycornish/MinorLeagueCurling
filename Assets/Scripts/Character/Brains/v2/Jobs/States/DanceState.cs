using UnityEngine;
using Animancer;
using System.Collections.Generic;
using CharacterNPC.v2;

/************************************************************************************************************************/
/*

SPECIFIC STATE 
(will NOT be included by default on all characters)

  This Job state represents when:
  - This NPC is engaged in Dancing activities. 
  
  Extensions:
  - Factor in Stamina


/************************************************************************************************************************/


namespace CharacterNPCJobs
{
    // [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Drink State")]
    public class DanceState : JobState
    {
        /************************************************************************************************************************/

        [SerializeField] private UnityEvent _OnStart; // See the Read Me.
        [SerializeField] private UnityEvent _OnEnd; // See the Read Me.

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => Character.Parameters.Jobs.AvailableJobStates.Contains(
            JobStateType.Dance
        ); 

        /************************************************************************************************************************/

        public override JobStateType JobType => JobStateType.Dance;

        /************************************************************************************************************************/

        private void UpdateAvailableActions()
        {
            // List<ActionType> newAvailableActions =
            Character.Parameters.Jobs.AvailableActions = new List<ActionType>
            {
                ActionType.Idle,
                ActionType.Dance
            };
            // Character.Parameters.Jobs.AvailableActions = newAvailableActions;
        }

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            Debug.Log("Dance OnDisable");
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Debug.Log("Dance OnEnable");
            Character.Parameters.Jobs.CurrentJob = JobStateType.Dance;
            Character.Parameters.Jobs.DesiredAction = ActionType.Dance;
        }

        

        // protected virtual void Update()
        // {
        //     if (Character.JobStateMachine.CurrentState == this)
        //     {
        //         // UpdateDestination();
        //     }
        // }
    }
        
}
