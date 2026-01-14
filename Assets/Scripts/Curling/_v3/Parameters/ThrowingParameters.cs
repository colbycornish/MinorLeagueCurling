using System;
using UnityEngine;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class ThrowingParameters
    {
        
        [SerializeField]
        private float _LaunchForce = 100f; // Base launch force (tweak as needed; adjust for distance--may want to bring force down if we shorten the distance)
        public ref float LaunchForce => ref _LaunchForce;

        [SerializeField]
        private float _SpinStrength = 5f; // Tweak for how much spin affects trajectory (side force applied during slide)
        public ref float SpinStrength => ref _SpinStrength;

        [SerializeField]
        private float _LaunchPower = 0f; 
        public ref float LaunchPower => ref _LaunchPower;

        // public bool isActive = false;
        // public bool isReady = false;
        
    }
}

