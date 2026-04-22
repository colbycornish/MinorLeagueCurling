using UnityEngine;
using CharacterNPCJobs;
using System.Collections.Generic;
using CharacterNPC.v2;

namespace NPCGroupController
{
    public class NPCGCHospitalWaitingRoom : NPCGroupController
    {
        
        public void Awake()
        {
            SetUpCharacters();
        }

        // public void Start()
        // {
            
        // }

        public void SetUpCharacters()
        {
            foreach(CharacterNPC.v2.Character c in _listOfCharacters)
            {
                SetUpCharacter(CharacterController: c);
            }
        }

        public void SetUpCharacter(CharacterNPC.v2.Character CharacterController)
        {
            SetUpCharacterInHospitalWaitingRoom(CharacterController: CharacterController);
        }

        public void SetUpCharacterInHospitalWaitingRoom(CharacterNPC.v2.Character CharacterController)
        {
            
            CharacterController.Parameters.Posture.DesiredPosture = CharacterPostureState.Sitting;
            CharacterController.Parameters.Jobs.AvailableActions.Clear();
            CharacterController.Parameters.Jobs.AvailableActions.Add(ActionType.Idle);
            CharacterController.Parameters.Jobs.AvailableActions.Add(ActionType.IdleLookAround);
            CharacterController.Parameters.Jobs.AvailableActions.Add(ActionType.Vomit);
        }

        private void AddPatrolPoints(CharacterNPC.v2.Character CharacterController)
        {
            CharacterController.Parameters.Movement.PatrolPoints = _patrolLocations;
        }




    }
}
