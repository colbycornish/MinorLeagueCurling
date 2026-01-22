// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{

    [Serializable]
    public class AnimalParametersStatus
    {
    
        [SerializeField]
        private bool _IsDead;
        public ref bool IsDead => ref _IsDead;
        
    }
}
