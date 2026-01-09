using UnityEngine;

public class MainMenuState : ICanvasState
{
    private GameObject mainMenuCanvas;
    private UICanvasStateMachine uiStateMachine; // Reference to the controller

    public MainMenuState(GameObject canvas, UICanvasStateMachine stateMachine)
    {
        mainMenuCanvas = canvas;
        uiStateMachine = stateMachine;
    }

    public void OnEnter()
    {
        mainMenuCanvas.SetActive(true); // Show the canvas
        // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
    }

    public void OnUpdate()
    {
        // Handle input or logic while in this state
    }

    public void OnExit()
    {
        mainMenuCanvas.SetActive(false); // Hide the canvas
        // Remove listeners
    }
}