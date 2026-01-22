// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;
using Animancer.FSM;
using UnityEngine.AI;
using Animancer.Units;
using static Animancer.Validate;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersMovement
    {
    
        [SerializeField]
        private Vector3 _MovementDirection;
        public Vector3 MovementDirection
        {
            get => _MovementDirection;
            set => _MovementDirection = Vector3.ClampMagnitude(value, 1);
        }

        public float ForwardSpeed { get; set; }
        public float DesiredForwardSpeed { get; set; }
        public float DesiredForwardSpeedOverride { get; set; }
        public float VerticalSpeed { get; set; }

        [SerializeField] 
        public bool overrideDesiredSpeed = false;

        [SerializeField] 
        public bool useDirectionalMovementAnimations = false;


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

        [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        private float _WalkSpeed = 3.5f;
        public float WalkSpeed => _WalkSpeed;

        [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        private float _RunSpeed = 7f;
        public float RunSpeed => _RunSpeed;

        [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        private float _SprintSpeed = 12f;
        public float SprintSpeed => _SprintSpeed;

        [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        private float _Acceleration = 8f;
        public float Acceleration => _Acceleration;

        [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        private float _Decceleration = 8f;
        public float Decceleration => _Decceleration;

        

        [SerializeField]
        private bool _WantsToRun;
        public ref bool WantsToRun => ref _WantsToRun;

        [SerializeField]
        private bool _IsStopped = false;
        public ref bool IsStopped => ref _IsStopped;

        [SerializeField]
        private bool _IsMoving = false;
        public ref bool IsMoving => ref _IsMoving;

        [SerializeField]
        private bool _IsIdle = false;
        public ref bool IsIdle => ref _IsIdle;

        [SerializeField]
        private bool _IsBaseFromIdleState = true;
        public ref bool IsBaseFromIdleState => ref _IsBaseFromIdleState;

        [SerializeField]
        private bool _IsBaseFromMoveState = false;
        public ref bool IsBaseFromMoveState => ref _IsBaseFromMoveState;
       
    }
}
