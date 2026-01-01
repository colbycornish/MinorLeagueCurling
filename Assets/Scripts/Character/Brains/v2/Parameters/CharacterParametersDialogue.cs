// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersDialogue
    {
        
        // Talking
        [SerializeField]
        private bool _IsTalking = false;
        public ref bool IsTalking => ref _IsTalking;

        [SerializeField]
        private bool _WantsToTalk = false;
        public ref bool WantsToTalk => ref _WantsToTalk;

        // Shouting
        [SerializeField]
        private bool _IsShouting = false;
        public ref bool IsShouting => ref _IsShouting;

        [SerializeField]
        private bool _WantsToShout = false;
        public ref bool WantsToShout => ref _WantsToShout;

    }
}
