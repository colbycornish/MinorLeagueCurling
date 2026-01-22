// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CurlingStones
{

    [Serializable]
    public class CurlingStoneStatus
    {
        
        [SerializeField]
        private bool _IsThrown;
        public ref bool IsThrown => ref _IsThrown;

        [SerializeField]
        private bool _IsInPlay;
        public ref bool IsInPlay => ref _IsInPlay;

        [SerializeField]
        private bool _IsSliding;
        public ref bool IsSliding => ref _IsSliding;

        [SerializeField]
        private bool _IsInScoringZone;
        public ref bool IsInScoringZone => ref _IsInScoringZone;

        /************************************************************************************************************************/
    }
}
