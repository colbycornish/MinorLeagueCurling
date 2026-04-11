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

        // [SerializeField]
        public bool IsUnused => !_IsThrown;

        /************************************************************************************************************************/

        [SerializeField]
        private bool _IsScoringStone;
        public ref bool IsScoringStone => ref _IsScoringStone;
        
        [SerializeField]
        private bool _IsInScoringZone;
        public ref bool IsInScoringZone => ref _IsInScoringZone;

        [SerializeField]
        private bool _IsInScoringZoneBullseye;
        public ref bool IsInScoringZoneBullseye => ref _IsInScoringZoneBullseye;
        
        [SerializeField]
        private bool _IsInScoringZoneLevelOne;
        public ref bool IsInScoringZoneLevelOne => ref _IsInScoringZoneLevelOne;

        [SerializeField]
        private bool _IsInScoringZoneLevelTwo;
        public ref bool IsInScoringZoneLevelTwo => ref _IsInScoringZoneLevelTwo;

        [SerializeField]
        private bool _IsInBlockingZone;
        public ref bool IsInBlockingZone => ref _IsInBlockingZone;

        [SerializeField]
        private bool _IsOutOfBounds;
        public ref bool IsOutOfBounds => ref _IsOutOfBounds;

        /************************************************************************************************************************/

        [SerializeField]
        private int _NumObstaclesHitByStone = 0;
        public ref int NumObstaclesHitByStone => ref _NumObstaclesHitByStone;

    }
}
