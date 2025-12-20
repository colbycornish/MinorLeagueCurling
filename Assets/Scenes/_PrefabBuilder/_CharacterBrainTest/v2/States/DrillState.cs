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
  - The NPC wants to dance.
  - The dance moves should loop.
  - The characters stamina should deplete?
  
  Extensions:
  - Can be invoked after certain curling events (might take this from a specific to a basic state)
  - 

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Dance State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class DrillState : CharacterState
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

        public override ActionType StateActionType => ActionType.Drill;
        public override bool FullMovementControl => true;

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
            Character.Parameters.Jobs.CurrentAction = StateActionType;
            _OnStart.Invoke();
            // _CurrentAnimation = SelectAnimationToPlay();
            PlayDancingAnimation();
            

            // AnimancerState state = Character.Animancer.Layers[0].CurrentState;
            // state.Events(this).OnEnd ??= Character.StateMachine.ForceSetDefaultState;
            
        }

        protected virtual void Update()
        {
            if (_CurrentAnimation == null ||
                _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime + 0.1
            )
            {
                PlayDancingAnimation();
            }
        }

        protected virtual void PlayDancingAnimation()
        {
            _CurrentAnimation = SelectAnimationToPlay();
            Character.AnimationManager.PlayBase(
                transition: _CurrentAnimation, 
                canPlayActionFullBody: true
            );
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
