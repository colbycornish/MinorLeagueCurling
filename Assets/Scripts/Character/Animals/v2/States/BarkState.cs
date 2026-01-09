// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Animal Brains - Bark State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class BarkState : AnimalState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;

        public override AnimalStatePriority Priority => AnimalStatePriority.Low;

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Animal.Animancer.Play(_Animation);
        }

        /************************************************************************************************************************/
    }
}
