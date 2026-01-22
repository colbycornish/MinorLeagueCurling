// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;
using Animancer.FSM;
using UnityEngine.AI;
using Animancer.Units;
using static Animancer.Validate;

namespace CurlingStones
{

    [Serializable]
    public class CurlingStoneMovement
    {
    
        [Header("Speed & Direction")]
        [SerializeField]
        private Vector3 _MovementDirection;
        public Vector3 MovementDirection
        {
            get => _MovementDirection;
            set => _MovementDirection = Vector3.ClampMagnitude(value, 1);
        }

        public float ForwardSpeed { get; set; }
        public float DesiredForwardSpeed { get; set; }
        public float LinearVelocity { get; set; }
        public float AngularVelocity { get; set; }

        
        [SerializeField]
        private float _DistanceFromTarget = 1000f;
        public ref float DistanceFromTarget => ref _DistanceFromTarget;

        [Header("Spin & Rotation")]
        // public float SpinSpeedInitial { get; set; }
        public float spinSpeedInitial = 0f;
        public float spinSpeedCurrent = 0f;
        public float spinSpeedTarget = 0f;
        public float spinSpeedStep = 0.25f;
        public float spinSpeedDecay = 0.05f;
        public float spinSpeedTargetMax = 1f;
        public float maxTorque = 1f;
        // /// <summary>
        // /// Todo: remove?
        // /// </summary>
        [HideInInspector] public float curlAmountCurrent = 0f;
        [HideInInspector] public float curlAmountInitial = 0f;
        [HideInInspector] public float spinAmountInitial = 0f;
        [HideInInspector] public float spinAmountCurrent = 0f;

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        // private float _WalkSpeed = 3.5f;
        // public float WalkSpeed => _WalkSpeed;

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        // private float _RunSpeed = 7f;
        // public float RunSpeed => _RunSpeed;

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        // private float _SprintSpeed = 12f;
        // public float SprintSpeed => _SprintSpeed;

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        // private float _Acceleration = 8f;
        // public float Acceleration => _Acceleration;

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        // private float _Decceleration = 8f;
        // public float Decceleration => _Decceleration;

        // [Header("Stats")]
        // public float distanceFromTarget = 1000f;

        // [Header("Spin")]
        // /// <summary>
        // /// spin can go in either the left (negative) or right (positive) direction
        // /// </summary>
        
        
        
        // [Header("Speed")]
        // [HideInInspector] public float speedCurrent = 0f;

        // public Vector3 movementDirection;

       
    }
}
