
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

[RequireComponent(typeof(CurlingObstacle))]
public class PropaneTankInteraction : MonoBehaviour
{
    // private bool stoneInRange = false;
    // private bool isFlameActive = true;
    private CurlingObstacle obstacle;

    private void Awake()
    {
        if (obstacle == null)
        {
            obstacle = GetComponent<CurlingObstacle>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            // this.stoneInRange = true;
            obstacle.ActivateFx();
            // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingNoSweepZone);
        }

    }

    // private void OnTriggerExit(Collider other)
    // {

    //     if (other.CompareTag("stone"))
    //     {
    //         this.stoneInRange = false;
    //     }

    // }

    // private void Update()
    // {
    //     // if (isFlameActive)
    //     // {

    //     // }
    // }
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