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
  - The NPC has something to clean.
  
  Extensions:
  - The dirty location they are in should subsequently be labelled as clean.
  - The dirty location should be marked as "cleaning in progress"

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    /// <summary>A <see cref="CharacterState"/> which plays a series of "attack" animations.</summary>
    /// 
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Cleaning State")]
    public class CleaningState : CharacterState
    {
        /************************************************************************************************************************/


        // [SerializeField] private UnityEvent _SetWeaponOwner;// See the Read Me.
        
        [SerializeField] private ClipTransition[] _Animations;

        private int _CurrentAnimationIndex = int.MaxValue;
        private ClipTransition _CurrentAnimation;

        public override CharacterStatePriority Priority => CharacterStatePriority.Low;

        public override bool CanInterruptSelf => true;

        [SerializeField] private UnityEvent _OnStart;// See the Read Me.
        [SerializeField] private UnityEvent _OnEnd;// See the Read Me.

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            // _SetWeaponOwner.Invoke();
        }

        /************************************************************************************************************************/

        public override bool CanEnterState => true; //Character.Movement.IsGrounded;

        /************************************************************************************************************************/

        // Use the End Event time to determine when this state is alowed to exit.

        // We cannot simply have this method return false and set the End Event to call Character.CheckMotionState
        // because it uses TrySetState (instead of ForceSetState) which would be prevented if this returned false.

        // And we cannot have this method return true because that would allow other actions like jumping in the
        // middle of an attack.

        public override bool CanExitState
            => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.NormalizedEndTime;

        /************************************************************************************************************************/

        public override ActionType StateActionType => ActionType.Clean;
        public override bool FullMovementControl => false;

        /************************************************************************************************************************/

        /// <summary>
        /// Start at the beginning of the sequence by default, but if the previous pose hasn't faded out yet then
        /// perform the next pose instead.
        /// </summary>
        protected virtual void OnEnable()
        {
            Character.Parameters.Jobs.CurrentAction = StateActionType;
            _CurrentAnimation = SelectAnimationToPlay();
            Character.AnimationManager.PlayAction(_CurrentAnimation);
            // Character.Parameters.Movement.ForwardSpeed = 0;
            _OnStart.Invoke();
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

        protected virtual void OnDisable()
        {
            _OnEnd.Invoke();
        }

        /************************************************************************************************************************/

        // protected virtual void FixedUpdate()
        // {
        //     if (Character.CheckMotionState())
        //         return;

        //     // Character.Movement.TurnTowards(Character.Parameters.MovementDirection, _TurnSpeed);
        // }

        /************************************************************************************************************************/
        


        

    }
}



