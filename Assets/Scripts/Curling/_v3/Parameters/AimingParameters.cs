using System;
using UnityEngine;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class AimingParameters
    { 
        [SerializeField]
        private float _CurlStrength = 5f;
        public ref float CurlStrength => ref _CurlStrength;
        
        [SerializeField]
        private float _CurlAmountInitial = 0f;
        public ref float CurlAmountInitial => ref _CurlAmountInitial; // -1 = left curl, 0 = no curl, 1 = right curl
        
        [SerializeField]
        private float _RotationSpeed = 100f;
        public ref float RotationSpeed => ref _RotationSpeed;
        
        [SerializeField]
        private float _DirectionalLimit = 30f; // Number of degrees off from straight ahead that a player can aim
        public ref float DirectionalLimit => ref _DirectionalLimit;        
    }
}

