using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Manages the curling stones for both teams in a curling game.
/// This class handles:
//  - Initial setup of the stones (building the variables and elements to be used by other functions)
//  - Spawning of stones various locations (launch location, team display areas)
//  - Preparing the next stone for play (switching teams)
/// </summary>

public class CurlingStoneManagerV2 : MonoBehaviour
{
    // base skin used for each team
    public GameObject stonePrefab_TeamHome;
    public GameObject stonePrefab_TeamAway;

    public List<CurlingStone> stonesTeamHome = new List<CurlingStone>();
    public List<CurlingStone> stonesTeamAway = new List<CurlingStone>();
    public List<Transform> stonesSpawnLocationsTeamHome = new List<Transform>();
    public List<Transform> stonesSpawnLocationsTeamAway = new List<Transform>();

    public Transform launchPoint;

    // This is one of the most important bits, and will be referenced by 
    // the stone throw controller, sweeping controller, etc.
    public CurlingStone currentStone;

    private int stonesSpawned = 0;
    private int stonesThrown = 0;
    private int stonesInPlay = 0;


    /// <summary>
    /// Prepare for Next Turn
    /// </summary>
    // TODO: Adjust to look for a selection from the canvas. 
    // These functions would then be a fallback state for if the selecting player
    // times out before selecting a stone.

    public bool IsCurrentStoneMoving()
    {
        
        CurlingStone cs = CurlingGameManagerV2.Instance.stoneManager.currentStone;
        Rigidbody rb = cs.rb;
        float threshold = 0.3f;

        Debug.Log($"[Stone Velocity] {rb.linearVelocity.magnitude}");
        if (rb.linearVelocity == Vector3.zero)
        {
            Debug.Log("Velocity is zero (direct comparison)");
            return false;
            // Do something when velocity is zero
        }

        // Method 2: Checking the magnitude
        if (rb.linearVelocity.magnitude < threshold)
        {
            Debug.Log("Velocity is near zero (magnitude)");
            return false;
            // Do something when velocity is near zero
        }

        // Method 3: Checking the square of the magnitude (slightly faster than magnitude)
        if (rb.linearVelocity.sqrMagnitude < threshold * threshold)
        {
            Debug.Log("Velocity is near zero (squared magnitude)");
            return false;
            // Do something when velocity is near zero
        }

        // Method 4: Using IsSleeping() (for more reliable check if object is at rest)
        if (rb.IsSleeping())
        {
            Debug.Log("Rigidbody is sleeping (at rest)");
            return false;
            // Do something when the rigidbody is sleeping
        }

        return true;
    }

    public bool IsCurrentStoneMovingForward()
    {
        CurlingStone cs = CurlingGameManagerV2.Instance.stoneManager.currentStone;
        Rigidbody rb = cs.rb;
        Vector3 forwardVelocity = Vector3.Project(rb.linearVelocity, transform.forward);
        float threshold = 0.3f;

        // Check if the forward velocity is close to zero
        if (Mathf.Approximately(forwardVelocity.magnitude, 0f) || forwardVelocity.magnitude < threshold)
        {
            // Object is not moving forward (or very slowly)
            Debug.Log("Object is not moving forward");
            return false;
        }

        return true;
    }


    // Needs to account for the initial state, with a null starting stone.
    public void PrepareNextStone()
    {
        // stonesInPlay++;
        // stonesThrown++;
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
        stonesInPlay++;
        stonesThrown++;
    }


    // Returns the next stone to be thrown based on the current team and stone index.
    private CurlingStone GetNextStone()
    {

        if (stonesThrown < stonesSpawned)
        {
            int currentTeam = stonesThrown % 2; // 0 for Team A, 1 for Team B
            int stoneIndex = stonesThrown / 2; // Each team has 5 stones

            if (currentTeam == 0 && stoneIndex < stonesTeamHome.Count)
            {
                Debug.Log("Current Stone Retrieved -> Team A");
                return stonesTeamHome[stoneIndex];
            }
            else if (currentTeam == 1 && stoneIndex < stonesTeamAway.Count)
            {
                Debug.Log("Current Stone Retrieved -> Team B");
                return stonesTeamAway[stoneIndex];
            }
        }
        return null;
    }
    


    /// <summary>
    /// Setup the initial values
    /// </summary>

    public void Setup(
        CurlingCourseData courseData,
        CurlingTeam teamHome,
        CurlingTeam teamAway
    )
    {
        SetLaunchPoint(courseData.launchPoint);
        SetStoneSpawnLocations(courseData.stonesSpawnLocationsTeamHome, 0);
        SetStoneSpawnLocations(courseData.stonesSpawnLocationsTeamAway, 1);
        SetDefaultTeamStones(
            teamHome.defaultStone,
            teamAway.defaultStone
        );
    }

    public void SetLaunchPoint(Transform location)
    {
        launchPoint = location;
    }

    public void SetStoneSpawnLocations(
        List<Transform> stoneSpawnLocations,
        int teamId_i
    )
    {
        // if (teamId_i == null) { Debug.log("No Team Id!"); return; }
        if (teamId_i == 0){ stonesSpawnLocationsTeamHome = stoneSpawnLocations;  }
        else { stonesSpawnLocationsTeamAway = stoneSpawnLocations; }   
    }

    public void SetDefaultTeamStones(
        GameObject homeTeamDefaultStone,
        GameObject awayTeamDefaultStone
    )
    {
        stonePrefab_TeamHome = homeTeamDefaultStone;
        stonePrefab_TeamAway = awayTeamDefaultStone;
    }
    // This method will takes the exiting stone prefab, and instantiate 5 stones for each team at 
    // the specified spawn locations in the scene. 
    public void SetupStones()
    {
        ClearExistingStones();
        stonesSpawned = 0;
        for (int i = 0; i < stonesSpawnLocationsTeamHome.Count; i++)
        {
            // find the spawn points for each team's stones
            Transform spawnPointA = stonesSpawnLocationsTeamHome[i];
            Transform spawnPointB = stonesSpawnLocationsTeamAway[i];

            SetupStone(stonePrefab_TeamHome, spawnPointA, 0, i);
            SetupStone(stonePrefab_TeamAway, spawnPointB, 1, i);

            stonesSpawned += 2;
        }
    }

    /// Setup a single stone on the ice
    public void SetupStone(
        GameObject stonePrefab,
        Transform location,
        int teamId,
        int stoneIndex
    )
    {
        // Instantiate Team A stones
        GameObject stoneInstance = Instantiate(stonePrefab, location.position, Quaternion.identity);
        CurlingStone curlingStone = stoneInstance.GetComponent<CurlingStone>();

        // toDo: change id to a string
        curlingStone.teamId_i = teamId; // Team A
        curlingStone.stoneIndex = stoneIndex;
        curlingStone.rb = stoneInstance.GetComponent<Rigidbody>();
        curlingStone.visual = stoneInstance;

        if (teamId == 0){ stonesTeamHome.Add(curlingStone);  }
        else { stonesTeamAway.Add(curlingStone); }

    }

    /// <summary>
    /// Reset various values
    /// </summary>
    
    /// clear the stones
    public void ClearExistingStones()
    {
        foreach (CurlingStone stone in FindObjectsByType<CurlingStone>(FindObjectsSortMode.None))
        {
            Destroy(stone.gameObject);
        }
    }

    public void Reset()
    {
        stonePrefab_TeamHome = null;
        stonePrefab_TeamAway = null;
        stonesTeamHome.Clear();
        stonesTeamAway.Clear();
        stonesSpawnLocationsTeamHome.Clear();
        stonesSpawnLocationsTeamAway.Clear();
        ClearExistingStones();
    }


    
}







            // // Instantiate Team A stones
            // GameObject stoneA = Instantiate(stonePrefab_TeamA, spawnPointA.position, Quaternion.identity);
            // CurlingStone curlingStoneA = stoneA.GetComponent<CurlingStone>();

            // // toDo: change id to a string
            // curlingStoneA.teamId = 0; // Team A
            // curlingStoneA.stoneIndex = i;
            // curlingStoneA.rb = stoneA.GetComponent<Rigidbody>();
            // curlingStoneA.visual = stoneA;
            // stonesTeamA.Add(curlingStoneA);
            

            // // Instantiate Team B stones
            // GameObject stoneB = Instantiate(stonePrefab_TeamB, spawnPointB.position, Quaternion.identity);
            // CurlingStone curlingStoneB = stoneB.GetComponent<CurlingStone>();

            // // toDo: change id to a string
            // curlingStoneB.teamId = 1; // Team B
            // curlingStoneB.stoneIndex = i;
            // curlingStoneB.rb = stoneB.GetComponent<Rigidbody>();
            // curlingStoneB.visual = stoneB; // Assuming the visual is the same as the stone GameObject
            // stonesTeamB.Add(curlingStoneB);

            