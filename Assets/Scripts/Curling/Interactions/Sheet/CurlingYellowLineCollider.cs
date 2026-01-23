using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


public class CurlingYellowLineCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            Debug.Log("Yellow Line Collision");
            if (CurlingManagersV3.MatchPhaseManager._instance != null){
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
            }
        }

    }
}