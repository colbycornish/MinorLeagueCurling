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
  - The NPC is hungry, and really wants to eat.
  
  Extensions:
  - Should have a meal equiped or be range of a meal they can eat?
  - That meal should "belong" to the NPC (i.e. no eating someone elses meal)

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Eat State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class EatState : CharacterState
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
            Character.Parameters.Status.HungerLevel < 0.4;

        /************************************************************************************************************************/

        public override bool FullMovementControl => false;

        /************************************************************************************************************************/

        public override bool CanEnterState => 
            Character.Parameters.Status.HungerLevel > 0.8 && 
            Character.Parameters.Posture.CurrentPosture == CharacterParametersPosture.CharacterPostureState.Sitting; 

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

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            UpdateParameterLevels();
        }

        protected virtual void UpdateParameterLevels()
        {
            if (Character.StateMachine.CurrentState == this)
            {
                Character.Parameters.Status.HungerLevel -= Time.deltaTime * 0.08f;
                Character.Parameters.Status.HungerLevel = Mathf.Clamp01(
                    Character.Parameters.Status.HungerLevel
                );
            }
        }



    }
}
