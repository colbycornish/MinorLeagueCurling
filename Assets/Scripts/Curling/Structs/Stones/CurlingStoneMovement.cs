using System;
using UnityEngine;

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

        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        public float ForwardSpeed { get; set; }
        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        public float DesiredForwardSpeed { get; set; }
        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        public float LinearVelocity { get; set; }
        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        public float AngularVelocity { get; set; }

        
        [SerializeField]
        private float _DistanceFromTarget = 1000f;
        public ref float DistanceFromTarget => ref _DistanceFromTarget;

        /* SPIN & ROTATION ***********************************************************************************************************************/

        [Header("Spin & Rotation")]
        // [SerializeField, MetersPerSecond(Rule = Value.IsNotNegative)]
        [SerializeField]
        private float _SpinSpeedInitial = 0f;
        public float SpinSpeedInitial
        {
            get => _SpinSpeedInitial;
            set => _SpinSpeedInitial = value;
        }

        [SerializeField]
        private float _SpinSpeedCurrent = 0f;
        public float SpinSpeedCurrent
        {
            get => _SpinSpeedCurrent;
            set => _SpinSpeedCurrent = value;
        }

        [SerializeField]
        private float _SpinSpeedTarget = 0f;
        public float SpinSpeedTarget
        {
            get => _SpinSpeedTarget;
            set => _SpinSpeedTarget = value;
        }

        [SerializeField]
        private float _SpinSpeedStep = 0.25f;
        public float SpinSpeedStep => _SpinSpeedStep;

        [SerializeField]
        private float _SpinSpeedDecay = 0.05f;
        public float SpinSpeedDecay => _SpinSpeedDecay;

        [SerializeField]
        private float _SpinSpeedTargetMax = 1f;
        public float SpinSpeedTargetMax
        {
            get => _SpinSpeedTargetMax;
            set => _SpinSpeedTargetMax = value;
        }
        

        [SerializeField]
        private float _MaxTorque = 1f;
        public float MaxTorque => _MaxTorque;

        [SerializeField]
        [HideInInspector] private float _CurlAmountCurrent = 0f;
        [HideInInspector] public float CurlAmountCurrent
        {
            get => _CurlAmountCurrent;
            set => _CurlAmountCurrent = value;
        }

        [SerializeField]
        [HideInInspector] private float _CurlAmountInitial = 0f;
        [HideInInspector] public float CurlAmountInitial
        {
            get => _CurlAmountInitial;
            set => _CurlAmountInitial = value;
        }

        [SerializeField]
        [HideInInspector] private float _SpinAmountInitial = 0f;
        [HideInInspector] public float SpinAmountInitial
        {
            get => _SpinAmountInitial;
            set => _SpinAmountInitial = value;
        }

        [SerializeField]
        [HideInInspector] private float _SpinAmountCurrent = 0f;
        [HideInInspector] public float SpinAmountCurrent
        {
            get => _SpinAmountCurrent;
            set => _SpinAmountCurrent = value;
        }
    }
}
