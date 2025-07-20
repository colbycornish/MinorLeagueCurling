using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages the curling stones for both teams in a curling game.
/// This class handles:
//  - Initial setup of the stones (building the variables and elements to be used by other functions)
//  - Spawning of stones various locations (launch location, team display areas)
//  - Preparing the next stone for play (switching teams)
/// </summary>

public class CurlingDemoLauncher : MonoBehaviour
{

    public CurlingCourseData courseData;
    public CurlingTeam teamHome;
    public CurlingTeam teamAway;
    public CurlingTeamData teamDataA;
    public CurlingTeamData teamDataB;

    public bool demoHasBeenLoaded = false;
    public KeyCode beginKey = KeyCode.T;
    public KeyCode resetAllKey = KeyCode.R;

    // base skin used for each team
    // This method will takes the exiting stone prefab, and instantiate 5 stones for each team at 
    // the specified spawn locations in the scene. 
    public void Start()
    {

    }

    public void Update()
    {
        if (!demoHasBeenLoaded && Input.GetKeyDown(beginKey))
        {
            StartDemo();
        }
        if (demoHasBeenLoaded && Input.GetKeyDown(resetAllKey))
        {
            // StartDemo();
        }
    }

    public void ResetDemo()
    {
        demoHasBeenLoaded = false;
    }

    public void StartDemo()
    {
        PrepDemoTeamData();
        CurlingGameManagerV2.Instance.InitSetupFromDemo(
            courseData,
            teamHome,
            teamAway
        );
        demoHasBeenLoaded = true;
    }

    public void PrepDemoTeamData()
    {
        teamHome.SetData();
        teamAway.SetData();

        teamDataA = teamHome.data;
        teamDataB = teamAway.data;
    }

    public void PrepGameDataSettings()
    {
        teamHome.SetData();
        teamAway.SetData();

        teamDataA = teamHome.data;
        teamDataB = teamAway.data;
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

            