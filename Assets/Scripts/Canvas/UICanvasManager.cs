using System.Collections.Generic;
using UnityEngine;


namespace UICanvasManager.v3
{
    [DefaultExecutionOrder(-10000)]// Initialize the StateMachine before anything uses it.
    public class UICanvasManager : MonoBehaviour
    {
        // [SerializeField]
        // private UICanvasStateMachine _StateMachine;
        // public UICanvasStateMachine StateMachine => _StateMachine;

        [SerializeField]
        private UICanvasStateMachine _StateMachine;
        public UICanvasStateMachine StateMachine => _StateMachine;

        // [SerializeField] private List<ICanvasState> _ListOfMainCanvasStates;

        // [SerializeField]
        // private CanvasParameters _Parameters;
        // public CanvasParameters Parameters => _Parameters;

        #if UNITY_EDITOR
        protected void OnValidate()
        {
            // gameObject.GetComponentInParentOrChildren(ref _Animancer);
            // gameObject.GetComponentInParentOrChildren(ref _AnimationManager);
            // gameObject.GetComponentInParentOrChildren(ref _Equipment);
            // gameObject.GetComponentInParentOrChildren(ref _Movement);
        }
        #endif

        // protected virtual void Awake()
        // {
            // _StateMachine.InitializeAfterDeserialize();
        // }


    }
}