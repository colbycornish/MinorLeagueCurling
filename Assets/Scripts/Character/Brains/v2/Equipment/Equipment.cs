// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{
    /// <summary>Manages the items equipped by a <see cref="Character"/>.</summary>
    /// 
    /// <remarks>
    /// <strong>Sample:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/samples/fsm/weapons">
    /// Weapons</see>
    /// </remarks>
    /// 
    /// https://kybernetik.com.au/animancer/api/Animancer.Samples.StateMachines/Equipment
    /// 
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Equipment")]
    // [AnimancerHelpUrl(typeof(Equipment))]
    public class Equipment : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Transform _WeaponHolder;
        [SerializeField] private Transform _LeftHandContainer;
        [SerializeField] private Transform _RightHandContainer;
        [SerializeField] private Weapon _Weapon;

        /************************************************************************************************************************/

        public Weapon Weapon
        {
            get => _Weapon;
            set
            {
                DetachWeapon();
                _Weapon = value;
                AttachWeapon();
            }
        }

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            AttachWeapon();
        }

        /************************************************************************************************************************/

        private void AttachWeapon()
        {
            if (_Weapon == null)
                return;
            else if (
                _Weapon.gameObject.GetComponent<CurlingBroom>() != null
            )
            {
                Debug.Log("Attaching Broom");
                AttachBroom();
            } else {
                Transform transform = _Weapon.transform;
                transform.parent = _WeaponHolder;
                transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                transform.localScale = _Weapon.gameObject.transform.localScale; //Vector3.one * 

                _Weapon.gameObject.SetActive(true);
            }
        }

        private void AttachBroom()
        {
            CurlingBroom broom = _Weapon.gameObject.GetComponent<CurlingBroom>();
            Transform leftHandHold = broom.topHandHold.transform;
            Transform rightHandHold = broom.bottomHandHold.transform;
            
            Transform transform = _Weapon.transform;

            transform.parent = _LeftHandContainer;

            // Attach the broom to the palm location, at zero-zero.
            // (Broom zero zero is the head, so the hand will be holding the head for now)
            transform.SetLocalPositionAndRotation(
                Vector3.zero, 
                Quaternion.identity
            );

            transform.localScale = _Weapon.gameObject.transform.localScale; //Vector3.one * 
            
            // Offset the localized transform with an update to get the broom in the right position
            var offsetPosition = new Vector3(
                0.215f, // 0.322f, 
                -0.675f, // -0.852f, 
                0.23f // 0.294f
            );
            
            // Update the localized rotation so the broom head is pointed down
            var offsetRotation = Quaternion.Euler(-187.387f, -235.184f, 207.279f);
            
            // Apply that transform
            transform.SetLocalPositionAndRotation(
                offsetPosition, 
                offsetRotation
            );
            transform.localScale = _Weapon.gameObject.transform.localScale; //Vector3.one * 

            _Weapon.gameObject.SetActive(true);
        }

        private void AttachToLeftHand()
        {
            
        }

        private void AttachToRightHand()
        {
            
        }

        /************************************************************************************************************************/

        private void DetachWeapon()
        {
            if (_Weapon == null)
                return;

            _Weapon.transform.parent = transform;
            _Weapon.gameObject.SetActive(false);
        }

        /************************************************************************************************************************/
    }
}
