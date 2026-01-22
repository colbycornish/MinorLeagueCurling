// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;
using System.Collections.Generic;
using CharacterNPC.v2;
using TMPro;

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

        // [SerializeField] private GameObject _CurlingStone;
        [SerializeField] private UnityEvent _OnStart;
        [SerializeField] private UnityEvent _OnEnd; 

        public override JobStatePriority Priority => JobStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => true;

        /************************************************************************************************************************/

        public override bool CanEnterState => Character.Parameters.Status.IsCurling == true;

        /************************************************************************************************************************/

        public override JobStateType JobType => 
            JobStateType.Curl;

        /************************************************************************************************************************/
        bool isSweeping;

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
                // UpdateDestination();
                if (Character.Parameters.Curling.TempTargetObject != null && Character.Parameters.Curling.IsOnIce)
                {
                    if (Character.Parameters.Curling.CurlingTeamPosition == CurlingPlayerPosition.SweeperLeft ||
                        Character.Parameters.Curling.CurlingTeamPosition == CurlingPlayerPosition.SweeperRight){
                        UpdateFormation();
                    }
                }
                // if (Character.Parameters.Curling.ActiveStone != null && Character.Parameters.Curling.IsOnIce)
                // {
                //     UpdateFormation();
                // }
                
            }
        }

        
        

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

        

        // void Update()
        // {
        //     UpdateFormation();
        //     UpdateSweepInput();
        // }

        void UpdateFormation()
        {
            GameObject stone = Character.Parameters.Curling.TempTargetObject;
            Vector3 stoneForward = stone.transform.forward;
            Vector3 stoneRight   = Vector3.Cross(Vector3.up, stoneForward);

            // for (int i = 0; i < sweepers.Count; i++)
            // {
            float side = (Character.Parameters.Curling.CurlingTeamPosition == CurlingPlayerPosition.SweeperLeft) 
                ? -1f 
                : 1f;

            Vector3 targetPos =
                stone.transform.position +
                stoneForward * Character.Parameters.Curling.forwardDistance +
                stoneRight * Character.Parameters.Curling.lateralSpacing * side;

            // Character.Parameters.Curling.TargetPosition.position = targetPos;
            Character.NavAgent.SetDestination(targetPos);
            Character.Parameters.Movement.DistanceFromDestination = Character.NavAgent.remainingDistance;
            Character.Parameters.Movement.CurrentDestination = Character.Parameters.Curling.TempTargetObject.transform;

            if (Vector3.Distance(Character.transform.position, targetPos) > 1.1f
            // Mathf.Sqrt(
            //     (Character.Parameters.Curling.forwardDistance * Character.Parameters.Curling.forwardDistance) + 
            //     (Character.Parameters.Curling.lateralSpacing * Character.Parameters.Curling.lateralSpacing)) + 2f
            )
            {
                Character.Parameters.Movement.WantsToRun = true;
            } else
            {
                Character.Parameters.Movement.WantsToRun = false;
            }

            // SetTarget(
            //     targetPos,
            //     stoneForward,
            //     Character.Parameters.Curling.followSmoothing
            // );

        }
        

        // void UpdateSweepInput()
        // {
        //     bool sweepInput = Input.GetButton("Sweep");

        //     if (sweepInput != isSweeping)
        //     {
        //         isSweeping = sweepInput;
        //         foreach (var s in sweepers)
        //             s.SetSweeping(isSweeping);

        //         stone.SetSweepActive(isSweeping);
        //     }
        // }

    }
        
}
