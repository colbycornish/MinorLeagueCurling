// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{

    [Serializable]
    public class AnimalParameters
    {
        /************************************************************************************************************************/

        [SerializeField]
        private Vector3 _MovementDirection;
        public Vector3 MovementDirection
        {
            get => _MovementDirection;
            set => _MovementDirection = Vector3.ClampMagnitude(value, 1);
        }


        [SerializeField]
        private Transform _CurrentDestination;
        public Transform CurrentDestination
        {
            get => _CurrentDestination;
            set => _CurrentDestination = value;
        }

        [SerializeField]
        private float _DistanceFromDestination = 0f;
        public ref float DistanceFromDestination => ref _DistanceFromDestination;

        /************************************************************************************************************************/

        [SerializeField]
        private bool _IsPatrolling;
        public ref bool IsPatrolling => ref _IsPatrolling;

        [SerializeField]
        private bool _IsWandering;
        public ref bool IsWandering => ref _IsWandering;

        [SerializeField]
        private bool _WantsToRun;
        public ref bool WantsToRun => ref _WantsToRun;

        /************************************************************************************************************************/

        [SerializeField]
        private float _HungerLevel = 0.8f;
        public ref float HungerLevel => ref _HungerLevel;

        /************************************************************************************************************************/

        [SerializeField]
        private float _SleepinessLevel = 0f;
        public ref float SleepinessLevel => ref _SleepinessLevel;

        /************************************************************************************************************************/

        [SerializeField]
        private bool _IsDead = false;
        public ref bool IsDead => ref _IsDead;

        
    }
}
