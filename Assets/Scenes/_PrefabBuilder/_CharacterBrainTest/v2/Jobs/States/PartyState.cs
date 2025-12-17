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
(will NOT be included by default on all characters)

  This Job state represents when:
  - This NPC is engaged in Cooking activities. 
  
  Extensions:
  - Needing to fetch more ingrediants
  - Needing to serve/deliver a meal
  - 

/************************************************************************************************************************/


namespace CharacterNPCJobs
{
    // [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Drink State")]
    public class PartyState : JobState
    {
        /************************************************************************************************************************/

        // [SerializeField] private GameObject _CurlingStone;// = new List<Transform>();

        [SerializeField] private UnityEvent _OnStart; // See the Read Me.
        [SerializeField] private UnityEvent _OnEnd; // See the Read Me.

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => Character.Parameters.Jobs.AvailableJobStates.Contains(
            JobStateType.Party
        ); 

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Party;

        /************************************************************************************************************************/


        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
            Debug.Log("Cooking OnDisable");
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Debug.Log("Cooking OnEnable");
            Character.Parameters.Jobs.CurrentJob = JobStateType.Party;
        }

        protected virtual void Update()
        {
            if (Character.JobStateMachine.CurrentState == this)
            {
                Character.Parameters.Status.HungerLevel += Time.deltaTime * 0.01f;
                Character.Parameters.Status.HungerLevel = Mathf.Clamp01(
                    Character.Parameters.Status.HungerLevel
                );

                Character.Parameters.Status.ThirstynessLevel += Time.deltaTime * 0.005f;
                Character.Parameters.Status.ThirstynessLevel = Mathf.Clamp01(
                    Character.Parameters.Status.ThirstynessLevel
                );
            }

            // if (Character.StateMachine.CurrentState != _Eat)
            // {
                
            // }
            // Update sleepiness level over time
            // if (_Animal.StateMachine.CurrentState != _Sleep)
            // {
                
            // }
        }
    }
        
}
