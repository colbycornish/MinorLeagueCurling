using System;
using UnityEngine;
// using System.Collections.Generic;
// using Animancer;
// using UnityEngine.AI;
// using Animancer.Units;
// using static Animancer.Validate;


namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class SweepingParameters
    {


        [SerializeField]
        private float _SweepStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)
        public float SweepStrength 
        {
            get => _SweepStrength;
            set => _SweepStrength = value;
        }

        [SerializeField]
        private float _SweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
        public float SweepBoostAmount
        {
            get => _SweepBoostAmount;
            set => _SweepBoostAmount = value;
        }

        [SerializeField]
        private float _SweepDecayRate = 2f;
        public float SweepDecayRate
        {
            get => _SweepDecayRate;
            set => _SweepDecayRate = value;
        }

        [SerializeField]
        private float _SweepBoostFactor = 0f; // ** NEW SWEEPER CODE **
        public float SweepBoostFactor 
        {
            get => _SweepBoostFactor;
            set => _SweepBoostFactor = value;
        }

        [SerializeField]
        private float _CurlAmount = 0f;
        public float CurlAmount
        {
            get => _CurlAmount;
            set => _CurlAmount = value;
        }

        [SerializeField]
        private bool _IsSweepingLeft = false;
        public bool IsSweepingLeft
        {
            get => _IsSweepingLeft;
            set => _IsSweepingLeft = value;
        }

        [SerializeField]
        private bool _IsSweepingRight = false;
        public bool IsSweepingRight 
        {
            get => _IsSweepingRight;
            set => _IsSweepingRight = value;
        }

        // public float ForwardSpeed { get; set; }
        // public float DesiredForwardSpeed { get; set; }
        // public float VerticalSpeed { get; set; }


        // [Header("Launch Settings")]
        // public float sweepStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

        // [Header("Sweeper Settings")]
        // public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
        // public float sweepDecayRate = 2f;
        // public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl
        // private float sweepBoostFactor = 0f; // ** NEW SWEEPER CODE **

        // // Sweeper State ** NEW SWEEPER CODE **
        // private bool isSweepingLeft = false; // ** NEW SWEEPER CODE **
        // private bool isSweepingRight = false; // ** NEW SWEEPER CODE **       
    }
}