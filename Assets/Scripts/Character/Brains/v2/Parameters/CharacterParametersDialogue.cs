// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;
using System.Collections.Generic;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersDialogue
    {
        
        public enum CharacterDialogueState
        {
            None,
            Listening, 
            Shouting,
            Talking
        }

        [SerializeField]
        private CharacterDialogueState _CurrentState;
        public ref CharacterDialogueState CurrentState => ref _CurrentState;

        [SerializeField]
        private CharacterDialogueState _DesiredState;
        public ref CharacterDialogueState DesiredState => ref _DesiredState;

        /************************************************************************************************************************/

        // Talking
        [SerializeField]
        private bool _IsTalking = false;
        public ref bool IsTalking => ref _IsTalking;

        [SerializeField]
        private bool _IsListening = false;
        public ref bool IsListening => ref _IsListening;

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

        /************************************************************************************************************************/

        [SerializeField]
        private bool _IsEngagedInDialogueWithPlayer = false;
        public ref bool IsEngagedInDialogueWithPlayer => ref _IsEngagedInDialogueWithPlayer;
        
        [SerializeField]
        private bool _IsEngagedInDialogueWithOtherCharacters = false;
        public ref bool IsEngagedInDialogueWithOtherCharacters => ref _IsEngagedInDialogueWithOtherCharacters;

        /************************************************************************************************************************/

        [SerializeField]
        private List<Character> _ListOfCharactersEngagedInConversationWith;
        public ref List<Character> ListOfCharactersEngagedInConversationWith => ref _ListOfCharactersEngagedInConversationWith;

    }
}
