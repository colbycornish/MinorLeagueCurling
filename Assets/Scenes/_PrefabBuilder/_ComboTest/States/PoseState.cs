// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace MLC.AnimancerTest
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Characters - Pose State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class PoseState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;

        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Character.Animancer.Play(_Animation);
        }

        /************************************************************************************************************************/
    }
}
