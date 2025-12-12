// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Drink State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class ThrowStoneState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private ClipTransition[] _Animations;

        private int _CurrentAnimationIndex = int.MaxValue;
        private ClipTransition _CurrentAnimation;

        [SerializeField] private UnityEvent _OnStart;// See the Read Me.
        [SerializeField] private UnityEvent _OnEnd;// See the Read Me.

        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        public override bool CanExitState => 
            _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;
        // TODO: if character is no longer thirsty and has equipped a drink item,

        /************************************************************************************************************************/

        public override bool FullMovementControl => false;

        /************************************************************************************************************************/

        public override bool CanEnterState => Character.Parameters.Posture.IsSitting == true; //Character.Movement.IsGrounded;
        // TODO: if character has equipped a drink item, can enter drink state

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            _CurrentAnimation = SelectAnimationToPlay();
            Character.AnimationManager.PlayAction(_CurrentAnimation);
        }

        private ClipTransition SelectAnimationToPlay()
        {
            if (_CurrentAnimationIndex >= _Animations.Length - 1 ||
                _Animations[_CurrentAnimationIndex].State.Weight == 0)
            {
                _CurrentAnimationIndex = 0;
            }
            else
            {
                _CurrentAnimationIndex++;
            }

            return _Animations[_CurrentAnimationIndex];
        }

    }
}
