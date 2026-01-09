// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Animal Brains - Sleep State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class SleepState : AnimalState
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

        protected virtual void Update()
        {
            if (Animal.StateMachine.CurrentState == this)
            {
                Animal.Parameters.SleepinessLevel -= Time.deltaTime * 0.03f;
                Animal.Parameters.SleepinessLevel = Mathf.Clamp01(Animal.Parameters.SleepinessLevel);
                // CanInterruptSelf = ;
                if (Animal.Parameters.SleepinessLevel <= 0.2f)
                {
                    Animal.StateMachine.TrySetState(Animal.StateMachine.DefaultState);
                }
            }
        }
    }
}
