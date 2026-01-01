// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.Units;
using UnityEngine;
using Animancer;

namespace CharacterNPC.v2
{
    /// <summary>Manages the interactions allowed by a <see cref="Character"/>.</summary>
    
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character NPC - Interaction Trigger")]
    
    public class NPCPlayerInteractionTrigger : MonoBehaviour
    {
        /************************************************************************************************************************/
        [SerializeField]
        private Character _Character;
        public Character Character => _Character;
        
        /************************************************************************************************************************/

#if UNITY_EDITOR
        protected void OnValidate()
        {
            // base.OnValidate();
            gameObject.GetComponentInParentOrChildren(ref _Character);
        }
#endif

        private void Update()
        {
            // if (playerInRange && Input.GetKeyDown(interactKey))
            // {
                // Debug.Log("Corkboard Interaction Triggered");
                // MenuManager._instance.ToggleCorkboardMenu();
                // Assuming you have a method to toggle the corkboard menu
            // }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                // playerInRange = true;
                // OpenNotification();

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // CloseNotification();
            }
        }

    }
}
