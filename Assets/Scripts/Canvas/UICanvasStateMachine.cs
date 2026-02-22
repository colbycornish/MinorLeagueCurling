using System.Collections.Generic;
using Animancer.FSM;
using UnityEngine;

namespace UICanvasManager.v3
{
    public class UICanvasStateMachine : StateMachine<ICanvasState>
    {
        private ICanvasState currentState;
        private List<ICanvasState> stateHistory = new List<ICanvasState>();

        public void ChangeState(ICanvasState newState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = newState;
            stateHistory.Add(currentState);
            
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
                stateHistory.RemoveAt(stateHistory.Count - 1);
                ChangeState(previousState);
            }
        }

        // void Update()
        // {
        //     if (currentState != null)
        //     {
        //         currentState.OnUpdate();
        //     }
        // }
    }
}