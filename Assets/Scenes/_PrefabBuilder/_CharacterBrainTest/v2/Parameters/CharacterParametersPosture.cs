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

        // Other
        [SerializeField]
        private bool _IsHoldingPose = false;
        public ref bool IsHoldingPose => ref _IsHoldingPose;
    }
}
