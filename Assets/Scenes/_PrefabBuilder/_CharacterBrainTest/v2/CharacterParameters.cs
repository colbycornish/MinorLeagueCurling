// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParameters
    {

        [SerializeField]
        private CharacterParametersDialogue _Dialogue;
        public CharacterParametersDialogue Dialogue => _Dialogue;

        [SerializeField]
        private CharacterParametersMovement _Movement;
        public CharacterParametersMovement Movement => _Movement;

        [SerializeField]
        private CharacterParametersPosture _Posture;
        public CharacterParametersPosture Posture => _Posture;

        [SerializeField]
        private CharacterParametersStatus _Status;
        public CharacterParametersStatus Status => _Status; 

        [SerializeField]
        private CharacterParametersJobs _Jobs;
        public CharacterParametersJobs Jobs => _Jobs;

        [SerializeField]
        private CharacterParametersSurroundings _Surroundings;
        public CharacterParametersSurroundings Surroundings => _Surroundings;  

        /* MOVEMENT ***********************************************************************************************************************/

        
    }
}



// /* TALKING ***********************************************************************************************************************/

// [SerializeField]
// private bool _IsTalking = false;
// public ref bool IsTalking => ref _IsTalking;

// [SerializeField]
// private bool _IsShouting = false;
// public ref bool IsShouting => ref _IsShouting;