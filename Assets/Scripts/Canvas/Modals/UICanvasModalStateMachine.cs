using System.Collections.Generic;
using Animancer.FSM;
using UnityEngine;

namespace UICanvasManager.v3
{
    public class UICanvasModalStateMachine : StateMachine<ICanvasModalState>
    {
        public ICanvasModalState _currentState;
        private List<ICanvasModalState> stateHistory = new List<ICanvasModalState>();

        public void OnAwake()
        {
            this.SetAllowNullStates(true);
        }

        public void ChangeState(
            ICanvasModalState nextState,
            bool addToHistory = true
        )
        {
            Debug.Log($"Changing state to: {nextState?.name ?? "null"}");
            if (_currentState != null)
            {
                _currentState.OnExit();
                if (addToHistory)
                {
                    stateHistory.Add(_currentState);
                }
                
            }

            _currentState = nextState;
            
            if (_currentState != null)
            {
                _currentState.OnEnter();
            }
        }

        public void GoBack()
        {
            if (stateHistory.Count > 0)
            {
                ICanvasModalState previousState = stateHistory[stateHistory.Count - 1];
                Debug.Log($"Going back to previous state. Current history count: {previousState?.name ?? "null"}, History count: {stateHistory.Count}");
                stateHistory.RemoveAt(stateHistory.Count - 1);
                ChangeState(
                    nextState: previousState,
                    addToHistory: false
                );
            }
        }

        public void ClearHistory()
        {
            stateHistory.Clear();
        }

        public void CloseAllUIModals()
        {
            _currentState.OnExit();
            _currentState = null;
            ClearHistory();
        }

        
    }
}