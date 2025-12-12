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
using CharacterNPC.v2;

namespace CharacterNPCJobs
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "Jobs - Brain")]
    // [AnimancerHelpUrl(typeof(WeaponsCharacterBrain))]
    public class JobBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private CharacterNPC.v2.Character _Character;
        [SerializeField] private JobState _Curling;
        [SerializeField] private JobState _Dialogue;
        [SerializeField] private JobState _Follow;
        [SerializeField] private JobState _Idle;
        [SerializeField] private JobState _Patrol;
        [SerializeField] private JobState _Wander;

        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;

        private StateMachine<JobState>.InputBuffer _InputBuffer;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            Debug.Log("JobBrain Active");
            _InputBuffer = new(_Character.JobStateMachine);
            // _Character.JobStateMachine.TrySetDefaultState();
        }

        /************************************************************************************************************************/

        protected virtual void Update()
        {
            UpdateMovement();
            UpdateActions();
        }

        /************************************************************************************************************************/

        private void UpdateMovement()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
            
                Debug.Log("JobBrain - Trying to set Idle State");
                // _InputBuffer.Buffer(_Patrol, _AttackInputTimeOut);
                _Character.JobStateMachine.TrySetState(_Idle);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
            
                Debug.Log("JobBrain - Trying to set Patrol State");
                // _InputBuffer.Buffer(_Patrol, _AttackInputTimeOut);
                _Character.JobStateMachine.TrySetState(_Patrol);
            }
        }

        /************************************************************************************************************************/

        private void UpdateActions()
        {
            
        }
    }
}


