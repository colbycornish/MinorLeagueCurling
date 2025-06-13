using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages the curling stones for both teams in a curling game.
/// This class handles:
//  - Initial setup of the stones 
//  - Spawning of stones various locations
//  - Preparing the next stone for play
/// </summary>

public class CurlingStoneManagerV2 : MonoBehaviour
{
    public GameObject stonePrefab_TeamA;
    public GameObject stonePrefab_TeamB;

    public List<CurlingStone> stonesTeamA = new List<CurlingStone>();
    public List<CurlingStone> stonesTeamB = new List<CurlingStone>();
    public List<Transform> stonesSpawnLocationsTeamA = new List<Transform>();
    public List<Transform> stonesSpawnLocationsTeamB = new List<Transform>();

    public Transform launchPoint;
    public CurlingStone currentStone;

    private int stonesSpawned = 0;
    private int stonesThrown = 0;
    private int stonesInPlay = 0;

    public void SetupStones()
    {
        ClearExistingStones();
        // This method will takes the exiting stone prefab, and instantiate 5 stones for each team at 
        // the specified spawn locations in the scene. 

        for (int i = 0; i < stonesSpawnLocationsTeamA.Count; i++)
        {
            // find the spawn points for each team's stones
            Transform spawnPointA = stonesSpawnLocationsTeamA[i];
            Transform spawnPointB = stonesSpawnLocationsTeamB[i];

            // Instantiate Team A stones
            GameObject stoneA = Instantiate(stonePrefab_TeamA, spawnPointA.position, Quaternion.identity);
            CurlingStone curlingStoneA = stoneA.GetComponent<CurlingStone>();
            curlingStoneA.teamId = 0; // Team A
            curlingStoneA.stoneIndex = i;
            curlingStoneA.rb = stoneA.GetComponent<Rigidbody>();
            curlingStoneA.visual = stoneA;
            stonesTeamA.Add(curlingStoneA);

            // Instantiate Team B stones
            GameObject stoneB = Instantiate(stonePrefab_TeamB, spawnPointB.position, Quaternion.identity);
            CurlingStone curlingStoneB = stoneB.GetComponent<CurlingStone>();
            curlingStoneB.teamId = 1; // Team B
            curlingStoneB.stoneIndex = i;
            curlingStoneB.rb = stoneB.GetComponent<Rigidbody>();
            curlingStoneB.visual = stoneB; // Assuming the visual is the same as the stone GameObject
            stonesTeamB.Add(curlingStoneB);
        }

        stonesSpawned = 10;
    }

    public void PrepareNextStone()
    {
        stonesInPlay++;
        // This method is called when a stone has been thrown, and prepares the next stone for the current team.
        if (stonesThrown < stonesSpawned)
        {
            CurlingStone nextStone = GetNextStone();
            if (nextStone != null)
            {
                nextStone.isThrown = false;
                nextStone.isInPlay = true;
                nextStone.transform.position = launchPoint.position; // Reset position to launch point
                nextStone.rb.linearVelocity = Vector3.zero; // Reset velocity
                nextStone.rb.angularVelocity = Vector3.zero; // Reset angular velocity
                currentStone = nextStone;
            }
        }
    }



    private CurlingStone GetNextStone()
    {
        // This method returns the next stone to be thrown based on the current team and stone index.
        if (stonesThrown < stonesSpawned)
        {
            int currentTeam = stonesThrown % 2; // 0 for Team A, 1 for Team B
            int stoneIndex = stonesThrown / 2; // Each team has 5 stones

            if (currentTeam == 0 && stoneIndex < stonesTeamA.Count)
            {
                return stonesTeamA[stoneIndex];
            }
            else if (currentTeam == 1 && stoneIndex < stonesTeamB.Count)
            {
                return stonesTeamB[stoneIndex];
            }
        }
        return null;
    }


    public void ClearExistingStones()
    {
        foreach (CurlingStone stone in FindObjectsByType<CurlingStone>(FindObjectsSortMode.None))
        {
            Destroy(stone.gameObject);
        }
    }
}
