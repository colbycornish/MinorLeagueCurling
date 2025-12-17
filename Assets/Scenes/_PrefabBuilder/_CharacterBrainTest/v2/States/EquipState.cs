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
  - The NPC needs to equip a specific item
  - Will need additional parameters to make this work.
  
  Extensions:
  - Parameter upgrades

*/
/************************************************************************************************************************/


namespace CharacterNPC.v2
{
    /// <summary>A <see cref="CharacterState"/> which managed the currently equipped <see cref="CurrentWeapon"/>.</summary>
    /// 
    /// <remarks>
    /// <strong>Sample:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/samples/fsm/weapons">
    /// Weapons</see>
    /// </remarks>
    /// 
    /// https://kybernetik.com.au/animancer/api/Animancer.Samples.StateMachines/EquipState
    /// 
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Equip State")]
    // [AnimancerHelpUrl(typeof(EquipState))]
    public class EquipState : CharacterState
    {
        /************************************************************************************************************************/

        public Weapon NextWeapon { get; set; }

        public Weapon CurrentWeapon
            => Character.Equipment.Weapon;

        public override CharacterStatePriority Priority
            => CharacterStatePriority.Medium;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            NextWeapon = CurrentWeapon;
        }

        /************************************************************************************************************************/

        public override bool CanEnterState
            => !enabled
            && NextWeapon != CurrentWeapon;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            if (CurrentWeapon.UnequipAnimation.IsValid())
            {
                AnimancerState state = Character.Animancer.Play(CurrentWeapon.UnequipAnimation);
                state.Events(this).OnEnd ??= OnUnequipEnd;
            }
            else
            {
                OnUnequipEnd();
            }
        }

        /************************************************************************************************************************/

        private void OnUnequipEnd()
        {
            Character.Equipment.Weapon = NextWeapon;

            if (CurrentWeapon.EquipAnimation.IsValid())
            {
                AnimancerState state = Character.Animancer.Play(CurrentWeapon.EquipAnimation);
                state.Events(this).OnEnd ??= Character.StateMachine.ForceSetDefaultState;
            }
            else
            {
                Character.StateMachine.ForceSetDefaultState();
            }
        }

        /************************************************************************************************************************/
    }
}
