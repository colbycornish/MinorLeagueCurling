using UnityEngine;

public class UICanvasStateMachine : MonoBehaviour
{
    private ICanvasState currentState;

    public void ChangeState(ICanvasState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}