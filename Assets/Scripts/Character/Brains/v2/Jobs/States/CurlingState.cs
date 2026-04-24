using UnityEngine;
using Animancer;
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

        public override JobStateType JobType => JobStateType.Curl;

        /************************************************************************************************************************/

        private void UpdateAvailableActions()
        {
            Character.Parameters.Jobs.AvailableActions = new List<ActionType>
            {
                ActionType.Idle,
                ActionType.CurlSweep,
                ActionType.Pose
            };
        }

        /************************************************************************************************************************/

        bool isSweeping;

        protected virtual void OnDisable()
        {
            // Debug.Log("CurlingState OnDisable");
            Character.Parameters.Movement.useDirectionalMovementAnimations = false;
            Character.NavAgent.acceleration = Character.Parameters.Movement.Acceleration;
            Character.Parameters.Movement.overrideDesiredSpeed = false;
            Character.Parameters.Movement.useDirectionalMovementAnimations = false;
            _OnEnd.Invoke();
            
            
        }

        protected virtual void OnEnable()
        {
            // Debug.Log("CurlingState OnEnable");
            _OnStart.Invoke();
            Character.Parameters.Jobs.CurrentJob = JobStateType.Curl;
            Character.Parameters.Movement.useDirectionalMovementAnimations = true;
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
            }
        }
        
        /// <summary>
        /// TODO: Impliment this, and remove from CharacterMovement.cs
        /// </summary>
        // void UpdateCharacterSpeed()
        // {
        //     Vector3 v = Character.Parameters.Curling.ActiveStone.rb.linearVelocity;
        //     // Debug.Log($"[Sweeper Movement] Stone Velocity: {v.normalized}");
        //     Debug.Log($"[Sweeper Movement] Stone Magnetude: {v.magnitude}");

        //     Character.Parameters.Movement.overrideDesiredSpeed = true;
        //     Character.Parameters.Movement.DesiredForwardSpeedOverride = v.magnitude * 1.4f;
        //     Character.Parameters.Movement.DesiredForwardSpeed = v.magnitude * 1.4f;
        //     Character.Parameters.Movement.ForwardSpeed = v.magnitude * 1.4f;
        //     //
        //     Character.NavAgent.acceleration = v.magnitude * 1.4f;
        //     Character.NavAgent.speed = Character.Parameters.Movement.ForwardSpeed;
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


        
    }
        
}
