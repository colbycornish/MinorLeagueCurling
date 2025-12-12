// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{

    [Serializable]
    public class CharacterParametersStatus
    {
        
        [SerializeField]
        private bool _IsPatrolling;
        public ref bool IsPatrolling => ref _IsPatrolling;

        [SerializeField]
        private bool _IsWandering;
        public ref bool IsWandering => ref _IsWandering;

        [SerializeField]
        private bool _IsFollowing;
        public ref bool IsFollowing => ref _IsFollowing;

        /************************************************************************************************************************/

        [SerializeField]
        private bool _IsDead = false;
        public ref bool IsDead => ref _IsDead;

        /* HUNGER / THIRST / SLEEP ***********************************************************************************************************************/

        [SerializeField]
        private float _HungerLevel = 0.0f;
        public ref float HungerLevel => ref _HungerLevel;

        [SerializeField]
        private float _ThirstynessLevel = 0.0f;
        public ref float ThirstynessLevel => ref _ThirstynessLevel;

        [SerializeField]
        private float _SleepinessLevel = 0f;
        public ref float SleepinessLevel => ref _SleepinessLevel;

        [SerializeField]
        private float _WantsToPatrolLevel = 0f;
        public ref float WantsToPatrolLevel => ref _WantsToPatrolLevel;

        [SerializeField]
        private float _WantsToWanderLevel = 0f;
        public ref float WantsToWanderLevel => ref _WantsToPatrolLevel;

    }
}
