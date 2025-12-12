// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersPosture
    {
        
        public enum CharacterPostureState
        {
            Standing, 
            Sitting,
            Crouching,
            LayingDown
        }

        [SerializeField]
        private CharacterPostureState _CurrentPosture;
        public ref CharacterPostureState CurrentPosture => ref _CurrentPosture;

        [SerializeField]
        private CharacterPostureState _DesiredPosture;
        public ref CharacterPostureState DesiredPosture => ref _DesiredPosture;

        [SerializeField]
        private bool _IsStanding;
        public ref bool IsStanding => ref _IsStanding;

        [SerializeField]
        private bool _WantsToStand;
        public ref bool WantsToStand => ref _WantsToStand;

        // Crouching
        [SerializeField]
        private bool _IsCrouching = false;
        public ref bool IsCrouching => ref _IsCrouching;

        [SerializeField]
        private bool _WantsToCrouch;
        public ref bool WantsToCrouch => ref _WantsToCrouch;

        // Sitting
        [SerializeField]
        private bool _IsSitting = false;
        public ref bool IsSitting => ref _IsSitting;

        [SerializeField]
        private bool _WantsToSit = false;
        public ref bool WantsToSit => ref _WantsToSit;

        // Laying Down
        [SerializeField]
        private bool _IsLayingDown = false;
        public ref bool IsLayingDown => ref _IsLayingDown;

        [SerializeField]
        private bool _WantsToLayDown = false;
        public ref bool WantsToLayDown => ref _WantsToLayDown;

        // Other
        [SerializeField]
        private bool _IsHoldingPose = false;
        public ref bool IsHoldingPose => ref _IsHoldingPose;

        
    }
}
