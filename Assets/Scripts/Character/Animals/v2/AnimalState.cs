// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;


namespace AnimalNPC
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Characters - Character State")]
    // [AnimancerHelpUrl(typeof(CharacterState))]
    public abstract class AnimalState : StateBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField]
        private Animal _Animal;
        public Animal Animal => _Animal;

        /************************************************************************************************************************/

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            gameObject.GetComponentInParentOrChildren(ref _Animal);
        }
#endif

        /************************************************************************************************************************/
        // Explained in the Interruptions sample.
        /************************************************************************************************************************/

        public virtual AnimalStatePriority Priority => AnimalStatePriority.Low;

        public virtual bool CanInterruptSelf => false;

        public override bool CanExitState
        {
            get
            {
                // There are several different ways of accessing the state change details:
                // CharacterState nextState = StateChange<CharacterState>.NextState;
                // CharacterState nextState = this.GetNextState();
                AnimalState nextState = _Animal.StateMachine.NextState;
                if (nextState == this)
                    return CanInterruptSelf;
                else if (Priority == AnimalStatePriority.Low)
                    return true;
                else
                    return nextState.Priority > Priority;
            }
        }

        /************************************************************************************************************************/

        public virtual bool FullMovementControl => true;

        public virtual Vector3 RootMotion => _Animal.Animancer.Animator.deltaPosition;

        /************************************************************************************************************************/


    }
}
