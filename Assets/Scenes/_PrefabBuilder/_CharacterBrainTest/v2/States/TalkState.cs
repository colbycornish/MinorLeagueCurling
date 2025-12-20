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
  - The NPC is talking.
  
  Extensions:
  - Should they be listening here too?

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Talk State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class TalkState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;


        [SerializeField] private UnityEvent _OnStart;// See the Read Me.
        [SerializeField] private UnityEvent _OnEnd;// See the Read Me.

        // private int _CurrentAnimationIndex = int.MaxValue;
        // private ClipTransition _CurrentAnimation;

        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        /************************************************************************************************************************/

        public override bool CanInterruptSelf => true;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Character.Parameters.Jobs.CurrentAction = StateActionType;
            Character.AnimationManager.PlayAction(_Animation);
        }

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        public override bool CanExitState => true;
            // => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

        /************************************************************************************************************************/

        public override ActionType StateActionType => ActionType.Talk;
        public override bool FullMovementControl => false;

        /************************************************************************************************************************/



        

    }
}
