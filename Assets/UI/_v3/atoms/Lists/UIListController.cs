using UnityEngine;
using UnityEngine.Events;

namespace CurlingUI.v3 {
    public abstract class  IListController : MonoBehaviour
    {
        
        [Header("State")]
        [SerializeField] private bool isHighlighted;
        [SerializeField] private bool isDisabled;
        [SerializeField] private bool isPressed;
        [SerializeField] private bool isNormal;
        [SerializeField] private bool isSelected;

        [Header("Events")]
        [SerializeField] private UnityEvent _OnSelected; // See the Read Me.
        [SerializeField] private UnityEvent _OnPressed; // See the Read Me.
        [SerializeField] private UnityEvent _OnHighlighted; // See the Read Me.
        [SerializeField] private UnityEvent _OnNormal; // See the Read Me.
        [SerializeField] private UnityEvent _OnDisabled; // See the Read Me.

        [Header("Animations")]
        [SerializeField] private Animator animator;

        public void OnEnter(){} // Called when entering the state
        public void OnExit(){}  // Called when exiting the state
        // public void OnUpdate(){} // Logic that runs per frame

        public void OnSelectItem() { // Called when an item is selected
            _OnSelected.Invoke();
        }
        
    }
}


