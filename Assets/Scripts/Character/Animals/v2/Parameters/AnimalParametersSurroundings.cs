// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{

    [Serializable]
    public class AnimalParametersSurroundings
    {
        
        [SerializeField]
        private bool _IsPlayerInView = false;
        public ref bool IsPlayerInView => ref _IsPlayerInView;

        [SerializeField]
        private bool _IsPlayerInRange = false;
        public ref bool IsPlayerInRange => ref _IsPlayerInRange;

        [SerializeField]
        private bool _IsPlayerInRangeToInteractWith = false;
        public ref bool IsPlayerInRangeToInteractWith => ref _IsPlayerInRangeToInteractWith;
        
        [SerializeField]
        private bool _IsEngagedInDialogueWithPlayer = false;
        public ref bool IsEngagedInDialogueWithPlayer => ref _IsEngagedInDialogueWithPlayer;
    
    
    }
}
