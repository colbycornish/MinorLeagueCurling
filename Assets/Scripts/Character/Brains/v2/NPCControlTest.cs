// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using Animancer.Units;
using Animancer;
using System;
using UnityEngine;
using Animancer.Samples;
using Unity.Entities.UniversalDelegates;
using static Animancer.Validate;
using Unity.VisualScripting;


namespace CharacterNPC.v2
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "NPC Control Test - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class NPCControlTest : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Character _CharacterChef;
        [SerializeField] private Character _CharacterPirate;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            // UpdateEquip();
            if (Input.GetKeyDown(KeyCode.T))
            {
                UpdatePosture();
            }
            
            // UpdateActions();

        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            
            
        }

        /************************************************************************************************************************/

        private void UpdateActions()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     _CharacterChef.Parameters.
            // }
            
            
        }


        private void UpdatePosture()
        {
            if (_CharacterChef.Parameters.Posture.CurrentPosture ==
                CharacterParametersPosture.CharacterPostureState.Standing)
            {
                _CharacterChef.Parameters.Posture.DesiredPosture =
                    CharacterParametersPosture.CharacterPostureState.Crouching;
            }
            else if (_CharacterChef.Parameters.Posture.CurrentPosture ==
                CharacterParametersPosture.CharacterPostureState.Crouching)
            {
                _CharacterChef.Parameters.Posture.DesiredPosture =
                    CharacterParametersPosture.CharacterPostureState.Standing;
            }
            
            


        }
    }
}


