using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PauseController : UIController
{
    private void Update()
    {
        MainState currentState = MainManager._instance.currentState;

        switch (currentState)
        {
            case MainState.Paused:
                // make things visible
                Show();
                break;
            default:
                Hide();
                // hide this canvas
                break;
        }
    }
}