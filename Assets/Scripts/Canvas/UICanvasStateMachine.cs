using System.Collections.Generic;
using Animancer.FSM;
using UnityEngine;

namespace UICanvasManager.v3
{
    public class UICanvasStateMachine : StateMachine<ICanvasState>
    {
        public ICanvasState currentState;
        private List<ICanvasState> stateHistory = new List<ICanvasState>();

        public void ChangeState(
            ICanvasState nextState,
            bool addToHistory = true
        )
        {
            Debug.Log($"Changing state to: {nextState?.name ?? "null"}");
            if (currentState != null)
            {
                currentState.OnExit();
                if (addToHistory)
                {
                    stateHistory.Add(currentState);
                }
                
            }

            currentState = nextState;
            
            if (currentState != null)
            {
                currentState.OnEnter();
            }
        }

        public void GoBack()
        {
            if (stateHistory.Count > 0)
            {
                ICanvasState previousState = stateHistory[stateHistory.Count - 1];
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

        public void CloseAllUI()
        {
            currentState.OnExit();
            currentState = null;
            ClearHistory();
        }

        
    }
}