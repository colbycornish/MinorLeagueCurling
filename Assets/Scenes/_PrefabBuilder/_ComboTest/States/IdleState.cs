// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using Animancer;

namespace MLC.AnimancerTest
{
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Characters - Idle State")]
    // [AnimancerHelpUrl(typeof(IdleState))]
    public class IdleState : CharacterState
    {
        /************************************************************************************************************************/

        [SerializeField] private TransitionAsset _Animation;

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            Character.Animancer.Play(_Animation);
        }

        /************************************************************************************************************************/
    }
}
