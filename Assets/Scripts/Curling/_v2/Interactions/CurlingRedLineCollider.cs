using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


public class CurlingRedLineCollider : MonoBehaviour
{

    // public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;
    private bool stoneInRange = false;
    // public SceneDatabase sceneDatabase;
    // public GameObject promptUI;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            stoneInRange = true;
            // stone.isInScoringZone = true;

            if (CurlingMatchPhaseManager.Instance != null){
                CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingNoSweepZone);
            }
            if (CurlingManagersV3.MatchPhaseManager._instance != null){
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingManagersV3.CurlingMatchPhase.CurlingNoSweepZone);
            }

            CurlingStone stone = other.gameObject.GetComponent<CurlingStone>();
            if (stone != null){
                stone.isInScoringZone = true;
            }
            
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("stone"))
        {
            stoneInRange = false;
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (stoneInRange)
        {

        }
        if (playerInRange)
        {
            
        }
    }
    


}