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


namespace CharacterNPC
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Character - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class CharacterBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Character _Character;
        [SerializeField] private CharacterState _Idle;
        [SerializeField] private CharacterState _Patrol;
        [SerializeField] private CharacterState _Pose;
        [SerializeField] private CharacterState _Talk;
        [SerializeField] private CharacterState _Attack;
        [SerializeField] private CharacterState _Airborne;
        [SerializeField] private CharacterState _Move;

        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;

        private StateMachine<CharacterState>.InputBuffer _InputBuffer;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            _InputBuffer = new(_Character.StateMachine);
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            if (_Character.StateMachine.CurrentState.FullMovementControl != true)
            {
                UpdateMovement();
            }
            UpdateActions();
            
        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            
            Vector2 input = SampleInput.WASD;
            if (input == Vector2.zero)
            {
                
                _Character.Parameters.MovementDirection = Vector3.zero;
                return;
            }
            // Debug.Log("Movement Input Detected");
            // Convert the input to 3D in the XZ plane.
            Vector3 movementDirection = new Vector3(input.x, 0, input.y);
            _Character.Parameters.MovementDirection = movementDirection;
            // Apply the camera's rotation and set the parameter.
            // Transform camera = Camera.main.transform;
            // movementDirection = camera.TransformDirection(movementDirection);
            // _Character.Parameters.MovementDirection = movementDirection;
        }

        /************************************************************************************************************************/

        private void UpdateActions()
        {
            // Jump gets priority for better platforming.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _InputBuffer.Buffer(_Pose, _AttackInputTimeOut);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _InputBuffer.Buffer(_Talk, _AttackInputTimeOut);
            }

            if (SampleInput.LeftMouseDown)
            {
                _InputBuffer.Buffer(_Attack, _AttackInputTimeOut);
            }

            _InputBuffer.Update();
        }
    }
}


