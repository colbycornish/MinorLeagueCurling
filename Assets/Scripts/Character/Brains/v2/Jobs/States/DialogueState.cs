using UnityEngine;
using Animancer;
using CharacterNPC.v2;
using System.Collections.Generic;
/************************************************************************************************************************/
/*

BASIC STATE
(will be included by default on all characters)

  This Job state represents when:
  - the Player Character (PC) is engaged in some kind of dialogue with this NPC, 
  - the PC is engaged in a dialogue with a group that this NPC is a part of.
  
  Extentions:
  - NPC is engaged in an NPC to NPC dialgue (background talking). For simplicity,
  this may need to be a seperate state, since it would involve different entry, exit, 
  and ongoing parameters

/************************************************************************************************************************/


namespace CharacterNPCJobs
{

    public class DialogueState : JobState
    {

        [SerializeField] private UnityEvent _OnStart; // See the Read Me.
        [SerializeField] private UnityEvent _OnEnd; // See the Read Me.

        public override JobStatePriority Priority => JobStatePriority.High;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => true; //_CurlingStone != null; 

        /************************************************************************************************************************/

        public override JobStateType JobType => JobStateType.Talk;

        /************************************************************************************************************************/

        private void UpdateAvailableActions()
        {
            // List<ActionType> newAvailableActions =
            Character.Parameters.Jobs.AvailableActions = new List<ActionType>
            {
                ActionType.Idle,
                ActionType.Talk
            };
            // Character.Parameters.Jobs.AvailableActions = newAvailableActions;
        }

        /************************************************************************************************************************/
        

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            // Debug.Log("Dialogue Job OnDisable");
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            // Debug.Log("Dialogue Job OnEnable");
            Character.Parameters.Jobs.CurrentJob = JobStateType.Talk;
            
        }

        protected virtual void Update()
        {
            if (Character.JobStateMachine.CurrentState == this)
            {
                // UpdateDestination();
            }
        }


        

        
        
    }
        
}
