// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using Animancer;

namespace MLC.AnimancerTest
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Characters - Action State")]
    // [AnimancerHelpUrl(typeof(ActionState))]
    public class ActionState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            AnimancerState state = Character.Animancer.Play(_Animation);
            state.Events(this).OnEnd ??= Character.StateMachine.ForceSetDefaultState;
        }

        /************************************************************************************************************************/
        // Explained in the Interruptions sample.
        /************************************************************************************************************************/

        public override CharacterStatePriority Priority => CharacterStatePriority.Medium;

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/
    }
}
