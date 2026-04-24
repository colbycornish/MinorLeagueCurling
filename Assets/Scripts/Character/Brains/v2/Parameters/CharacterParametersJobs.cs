// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;
using System.Collections.Generic;
using CharacterNPCJobs;

namespace CharacterNPC.v2
{
    [Serializable]
    public class CharacterParametersJobs
    {

        [SerializeField]
        private JobStateType _CurrentJob;
        public ref JobStateType CurrentJob => ref _CurrentJob;

        [SerializeField]
        private JobStateType _DesiredJob;
        public ref JobStateType DesiredJob => ref _DesiredJob;

        /************************************************************************************************************************/

        [SerializeField]
        private List<JobStateType> _AvailableJobStates;
        public ref List<JobStateType> AvailableJobStates => ref _AvailableJobStates;

        /************************************************************************************************************************/

        [SerializeField]
        private ActionType _CurrentAction;
        public ref ActionType CurrentAction => ref _CurrentAction;

        [SerializeField]
        private ActionType _DesiredAction;
        public ref ActionType DesiredAction => ref _DesiredAction;

        [SerializeField]
        private List<ActionType> _AvailableActions;
        public ref List<ActionType> AvailableActions => ref _AvailableActions;

        /************************************************************************************************************************/

        [SerializeField]
        private Dictionary<JobStateType, JobStateSetting> _JobStateSettings;
        public ref Dictionary<JobStateType, JobStateSetting> JobStateSettings => ref _JobStateSettings;

        public class JobStateSetting {
             public float start; 
             public float increaseMultiplier;
             public float cooldownRate;
        };

    }
}
