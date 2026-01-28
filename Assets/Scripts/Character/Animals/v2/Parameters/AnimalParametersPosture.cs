// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{

    [Serializable]
    public class AnimalParametersPosture
    {
    
        public enum AnimalPostureState
        {
            Standing, 
            Sitting,
            LayingDown
        }

        [SerializeField]
        private AnimalPostureState _CurrentPosture;
        public ref AnimalPostureState CurrentPosture => ref _CurrentPosture;

        [SerializeField]
        private AnimalPostureState _DesiredPosture;
        public ref AnimalPostureState DesiredPosture => ref _DesiredPosture;

        // Other
        [SerializeField]
        private bool _IsHoldingPose = false;
        public ref bool IsHoldingPose => ref _IsHoldingPose;
       
    }
}
