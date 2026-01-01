// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

/************************************************************************************************************************/
/*

BASIC STATE
(will be included by default on all characters)

  This Animation state represents when:
  - The NPC is thirsty, and really wants to drink.
  
  Extensions:
  - Should have a drink equiped?

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Drink State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class DrinkState : CharacterState
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
            _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime &&
            Character.Parameters.Status.ThirstynessLevel < 0.2;
        // TODO: if character is no longer thirsty and has equipped a drink item,

        /************************************************************************************************************************/

        public override ActionType StateActionType => ActionType.Drink;
        public override bool FullMovementControl => false;

        /************************************************************************************************************************/

        public override bool CanEnterState => 
            Character.Parameters.Status.ThirstynessLevel > 0.8 && 
            Character.Parameters.Posture.CurrentPosture == CharacterParametersPosture.CharacterPostureState.Sitting;
        // TODO: if character has equipped a drink item, can enter drink state

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        protected virtual void OnEnable()
        {
            _OnStart.Invoke();
            Character.Parameters.Jobs.CurrentAction = StateActionType;
            PlayDrinkAnimation();
        }

        

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            UpdateParameterLevels();
        }

        protected virtual void UpdateParameterLevels()
        {
            if (Character.StateMachine.CurrentState == this)
            {
                Character.Parameters.Status.ThirstynessLevel -= Time.deltaTime * 0.09f;
                Character.Parameters.Status.ThirstynessLevel = Mathf.Clamp01(
                    Character.Parameters.Status.ThirstynessLevel
                );
            }
        }

        private void PlayDrinkAnimation()
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
