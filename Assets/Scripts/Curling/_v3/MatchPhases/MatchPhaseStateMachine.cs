using System.Collections.Generic;
using Animancer.FSM;
using UnityEngine;
using System;

namespace CurlingManagersV3
{
    public class MatchPhaseStateMachine : StateMachine<IMatchPhaseState>
    {
        public IMatchPhaseState currentState;
        private List<IMatchPhaseState> stateHistory = new List<IMatchPhaseState>();
        public event Action<CurlingMatchPhase> OnPhaseChanged;

        public void ChangeState(
            IMatchPhaseState nextState,
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
                OnPhaseChanged?.Invoke(currentState.StateMatchPhaseType);
            }


        }

        public void GoBack()
        {
            if (stateHistory.Count > 0)
            {
                IMatchPhaseState previousState = stateHistory[stateHistory.Count - 1];
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


        
    }
}