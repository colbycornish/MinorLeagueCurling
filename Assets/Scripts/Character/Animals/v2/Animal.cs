// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using UnityEngine;
using Animancer;
using UnityEngine.AI;

namespace AnimalNPC
{

    [AddComponentMenu(Strings.SamplesMenuPrefix + "AnimalNPC - Animal")]
    // [AnimancerHelpUrl(typeof(Character))]
    [DefaultExecutionOrder(-10000)]// Initialize the StateMachine before anything uses it.
    public class Animal : MonoBehaviour
    {
        /************************************************************************************************************************/
        // Used in the Characters sample.
        /************************************************************************************************************************/

        [SerializeField]
        private AnimancerComponent _Animancer;
        public AnimancerComponent Animancer => _Animancer;

        [SerializeField]
        private LayeredAnimationManager _AnimationManager;
        public LayeredAnimationManager AnimationManager => _AnimationManager;

        [SerializeField]
        private NavMeshAgent _NavAgent;
        public NavMeshAgent NavAgent => _NavAgent;
        

        /************************************************************************************************************************/

        [SerializeField]
        private StateMachine<AnimalState>.WithDefault _StateMachine;
        public StateMachine<AnimalState>.WithDefault StateMachine => _StateMachine;

        protected virtual void Awake()
        {
            _StateMachine.InitializeAfterDeserialize();
        }

        /************************************************************************************************************************/
        // Used in the Interruptions sample.
        /************************************************************************************************************************/

        // [SerializeField]
        // private HealthPool _Health;
        // public HealthPool Health => _Health;

        /************************************************************************************************************************/
        // Used in the Brains sample.
        /************************************************************************************************************************/

        [SerializeField]
        private AnimalParameters _Parameters;
        public AnimalParameters Parameters => _Parameters;

        /************************************************************************************************************************/
        // Used in the Weapons sample.
        /************************************************************************************************************************/

        // [SerializeField]
        // private Equipment _Equipment;
        // public Equipment Equipment => _Equipment;

        /************************************************************************************************************************/
    }
}
