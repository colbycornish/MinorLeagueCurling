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

    // public KeyCode interactKey = KeyCode.E;
    private bool stoneInRange = false;
    // public SceneDatabase sceneDatabase;
    // public GameObject promptUI;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            Debug.Log("Yellow Line Collision");
            stoneInRange = true;
            /// previous phase manager code, not needed once v3 is adopted
            // if (CurlingManagersV3.MatchPhaseManager._instance != null){
            //     CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingManagersV3.MatchPhaseManager.CurlingStoneSweepingPhase);
            // }
            if (CurlingManagersV3.MatchPhaseManager._instance != null){
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingManagersV3.CurlingMatchPhase.CurlingStoneSweepingPhase);
            }
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
        if (stoneInRange)
        {
            
        }
    }
    


}