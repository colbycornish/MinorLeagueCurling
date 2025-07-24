
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


public class PropaneTankInteraction : MonoBehaviour
{

    // public KeyCode interactKey = KeyCode.E;
    private bool stoneInRange = false;
    private bool isFlameActive = true;
    // public SceneDatabase sceneDatabase;
    // public GameObject promptUI;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            stoneInRange = true;
            // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingNoSweepZone);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("stone"))
        {
            stoneInRange = false;
        }

    }

    private void Update()
    {
        if (isFlameActive)
        {

        }
    }
}


public class PropaneTankObstacle : MonoBehaviour
{

    // public KeyCode interactKey = KeyCode.E;
    public Rigidbody rb;
    private bool isFlameActive = true;

    private void Update()
    {
        if (isFlameActive)
        {

        }
    }

    private void Move()
    {
        
    }
}