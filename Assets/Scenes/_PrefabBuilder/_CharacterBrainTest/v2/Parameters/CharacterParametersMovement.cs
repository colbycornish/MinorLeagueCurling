// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersMovement
    {
        [SerializeField]
        private bool _IsMoving = false;
        public ref bool IsMoving => ref _IsMoving;
        
        [SerializeField]
        private Vector3 _MovementDirection;
        public Vector3 MovementDirection
        {
            get => _MovementDirection;
            set => _MovementDirection = Vector3.ClampMagnitude(value, 1);
        }

        public float ForwardSpeed { get; set; }
        public float DesiredForwardSpeed { get; set; }
        public float VerticalSpeed { get; set; }


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

        [SerializeField]
        private bool _WantsToRun;
        public ref bool WantsToRun => ref _WantsToRun;

        [SerializeField]
        private bool _IsStopped = false;
        public ref bool IsStopped => ref _IsStopped;
       
    }
}
