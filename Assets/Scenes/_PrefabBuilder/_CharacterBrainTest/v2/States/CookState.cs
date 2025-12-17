// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

/************************************************************************************************************************/
/*

SPECIFIC STATE
(will NOT be included by default on all characters)

  This Animation state represents when:
  - The NPC has something to cook.
  - The character should have all the materials needed inorder to cook.
  - The caracter should be at a location where a meal can be cooked.
  
  Extensions:
  - The meal they are in should have it's cooking progress updated.
  - 

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Cook State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class CookState : CharacterState
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

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;
        // TODO: if character has equipped a drink item, can enter drink state

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            PlayCookingAnimation();
            // _CurrentAnimation = SelectAnimationToPlay();
            // Character.AnimationManager.PlayAction(_CurrentAnimation);

            // AnimancerState state = Character.Animancer.Layers[0].CurrentState;
            // state.Events(this).OnEnd ??= Character.StateMachine.ForceSetDefaultState;
        }

        

        protected virtual void Update()
        {
            if (_CurrentAnimation == null ||
                _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime + 0.1
            )
            {
                PlayCookingAnimation();
            }
        }

        protected virtual void PlayCookingAnimation()
        {
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
