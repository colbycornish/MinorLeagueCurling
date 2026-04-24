// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace AnimalNPC
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Animal Brains - Eating State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class EatState : AnimalState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;

        public override AnimalStatePriority Priority => AnimalStatePriority.Low;

        public override bool CanInterruptSelf => true;  //Animal.Parameters.HungerLevel >= 0.2f;

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
                Animal.Parameters.HungerLevel -= Time.deltaTime * 0.05f;
                Animal.Parameters.HungerLevel = Mathf.Clamp01(Animal.Parameters.HungerLevel);
                // CanInterruptSelf = ;
                if (Animal.Parameters.HungerLevel <= 0.2f)
                {
                    Animal.StateMachine.TrySetState(Animal.StateMachine.DefaultState);
                }
            }
        }
    }
}
