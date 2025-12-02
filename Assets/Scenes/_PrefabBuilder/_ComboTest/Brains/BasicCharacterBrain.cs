// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;
using Animancer;
using Animancer.Samples;


namespace MLC.AnimancerTest
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Characters - Basic Character Brain")]
    // [AnimancerHelpUrl(typeof(BasicCharacterBrain))]
    public class BasicCharacterBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Character _Character;
        [SerializeField] private CharacterState _Move;
        [SerializeField] private CharacterState _Action;

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            UpdateMovement();
            UpdateAction();
        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            float forward = SampleInput.WASD.y;
            if (forward > 0)
            {
                _Character.StateMachine.TrySetState(_Move);
            }
            else
            {
                _Character.StateMachine.TrySetDefaultState();
            }
        }

        /************************************************************************************************************************/

        private void UpdateAction()
        {
            if (SampleInput.LeftMouseUp)
                _Character.StateMachine.TryResetState(_Action);
        }

        /************************************************************************************************************************/
    }
}
