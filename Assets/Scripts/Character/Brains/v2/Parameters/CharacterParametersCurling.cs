// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersCurling
    {
        
        [SerializeField]
        private CurlingPlayerPosition _CurlingTeamPosition;
        public ref CurlingPlayerPosition CurlingTeamPosition => ref _CurlingTeamPosition;

        [SerializeField]
        private bool _IsOnIce;
        public ref bool IsOnIce => ref _IsOnIce;

        [SerializeField]
        private CurlingStone _ActiveStone;
        public ref CurlingStone ActiveStone => ref _ActiveStone;

        [SerializeField]
        private GameObject _TempTargetObject;
        public ref GameObject TempTargetObject => ref _TempTargetObject;

        [SerializeField]
        private Transform _TargetPosition;
        public ref Transform TargetPosition => ref _TargetPosition;

        [Header("Formation")]
        [SerializeField] public float forwardDistance = 2.5f;
        [SerializeField] public float lateralSpacing = 3.6f;

        [Header("Behavior")]
        [SerializeField] float followSmoothing = 8f;

        [SerializeField]
        private bool _IsAbleToSweep = false;
        public ref bool IsAbleToSweep => ref _IsAbleToSweep;
    }
}
