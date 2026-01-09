using UnityEngine;

public interface ICanvasState
{
    void OnEnter(); // Called when entering the state
    void OnUpdate(); // Logic that runs per frame
    void OnExit();  // Called when exiting the state
    
}