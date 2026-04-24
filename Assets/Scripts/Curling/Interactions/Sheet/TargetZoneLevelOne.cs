

using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


public class TargetZoneLevelOne : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            Debug.Log("Level One Collision");
            other.gameObject.GetComponent<CurlingStone>().Parameters.Status.IsInScoringZone = true;
            other.gameObject.GetComponent<CurlingStone>().Parameters.Status.IsInScoringZoneLevelOne = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            Debug.Log("Level Two Collision");
            other.gameObject.GetComponent<CurlingStone>().Parameters.Status.IsInScoringZoneLevelOne = false;
        }
    }
}