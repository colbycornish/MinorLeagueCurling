// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using Animancer;

namespace MLC.AnimancerTest
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Interruptions - Flinch State")]
    // [AnimancerHelpUrl(typeof(FlinchState))]
    public class FlinchState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            Character.Health.OnHitReceived += () => Character.StateMachine.TryResetState(this);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            AnimancerState state = Character.Animancer.Play(_Animation);
            state.Events(this).OnEnd ??= Character.StateMachine.ForceSetDefaultState;
        }

        /************************************************************************************************************************/

        public override CharacterStatePriority Priority => CharacterStatePriority.High;

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/
    }
}
