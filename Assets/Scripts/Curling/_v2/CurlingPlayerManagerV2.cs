using System.Collections.Generic;
using UnityEngine;
using System;




public class CurlingPlayerManagerV2 : MonoBehaviour
{
    // public int totalStonesPerEnd = 10;
    // public List<CurlingTeamData> teams = new List<CurlingTeamData>();
    public List<CurlingTeam> teams = new List<CurlingTeam>();
    public event Action<List<CurlingTeam>> OnTeamDataChanged;
    public CurlingTeam currentTeam;

    private int currentStoneIndex = 0;
    private int currentTeamIndex = 0;

    public Transform throwerStartLocation;
    public Transform sweeperLStartLocation;
    public Transform sweeperRStartLocation;
    public List<Transform> idleLocationsTeamHome;
    public List<Transform> idleLocationsTeamAway;
    private GameObject teamHomeThrower;
    private GameObject teamHomeSweeperL;
    private GameObject teamHomeSweeperR;
    private GameObject teamAwayThrower;
    private GameObject teamAwaySweeperL;
    private GameObject teamAwaySweeperR;


    /// <summary>
    /// Changing Turns
    /// </summary>
    public void PreparePlayers()
    {
        currentStoneIndex = 0;
        currentTeamIndex = 0;
    }

    public void NextPlayer()
    {
        currentStoneIndex++;
        currentTeamIndex = 1 - currentTeamIndex;
    }

    public int GetCurrentTeamIndex() => currentTeamIndex;
    public CurlingTeam GetCurrentTeam() => teams[currentTeamIndex];

    /// <summary>
    /// Setup / Reset: Ingesting and setting up all the initial data
    /// </summary>
    public void Setup(
        CurlingCourseData curlingCourse,
        CurlingTeam teamHome,
        CurlingTeam teamAway
    )
    {
        SetLocations(curlingCourse);
        SetPlayers(teamHome, teamAway, curlingCourse);
        SetTeams(teamHome, teamAway);
    }

    // assigns the teams.
    public void SetTeams(CurlingTeam teamHome, CurlingTeam teamAway)
    {
        teams.Clear();
        teams.Add(teamHome);
        teams.Add(teamAway);
        Debug.Log($"Teams set: {teamHome.teamName} vs {teamAway.teamName}");
        OnTeamDataChanged?.Invoke(teams);
    }

    public void SetLocations(CurlingCourseData curlingCourse)
    {
        idleLocationsTeamHome = curlingCourse.idleLocationsTeamHome;
        idleLocationsTeamAway = curlingCourse.idleLocationsTeamAway;
        throwerStartLocation = curlingCourse.throwerStartLocation;
        sweeperLStartLocation = curlingCourse.sweeperLStartLocation;
        sweeperRStartLocation = curlingCourse.sweeperRStartLocation;
    }
    
    /// <summary>
    /// Updating character positions 
    /// - Moving players into  playing locations or to the bench
    /// - Activing the movement controls for active players
    /// - Deactivating movement controls for non-active players
    /// </summary>


    public void RepositionCharacters()
    {
        /// if turn == Home
        /// - Move Away players to bench
        /// - deactivate Away players
        /// - Move Home players into position
        /// - activate Home players
        /// 
        /// if turn == Away
        /// - Move Home players to bench
        /// - deactivate Home players
        /// - Move Away players into position
        /// - activate Away players
    }


    // TODO: Establish the Curling Player Data structure first.
    // We cannot just bring in one of the NPC as a base (since their nav agent and 
    // action controls conflict with the basis here)
    public void SetPlayers(
        CurlingTeam teamHome, 
        CurlingTeam teamAway, 
        CurlingCourseData curlingCourse
    )
    {
        teamHomeThrower = Instantiate(teamHome.thrower, idleLocationsTeamHome[0].position, Quaternion.identity);
        teamHome.thrower = teamHomeThrower;

        teamHomeSweeperL = Instantiate(teamHome.sweeperLeft, idleLocationsTeamHome[1].position, Quaternion.identity);
        teamHome.sweeperLeft = teamHomeSweeperL;

        teamHomeSweeperR = Instantiate(teamHome.sweeperRight, idleLocationsTeamHome[2].position, Quaternion.identity);
        teamHome.sweeperRight = teamHomeSweeperR;
        /// away team
        teamAwayThrower = Instantiate(teamAway.thrower, idleLocationsTeamAway[0].position, Quaternion.identity);
        teamAway.thrower = teamAwayThrower;

        teamAwaySweeperL = Instantiate(teamAway.sweeperLeft, idleLocationsTeamAway[1].position, Quaternion.identity);
        teamAway.sweeperLeft = teamAwaySweeperL;

        teamAwaySweeperR = Instantiate(teamAway.sweeperRight, idleLocationsTeamAway[2].position, Quaternion.identity);
        teamAway.sweeperRight = teamAwaySweeperR;
    }


    /// <summary>
    /// Helper Functions
    /// </summary>
    /// 

    // Ready?
    public bool IsReady()
    {
        return teams.Count == 2;
    }

    // Reset
    public void Reset()
    {
        teams.Clear();
        idleLocationsTeamHome.Clear();
        idleLocationsTeamAway.Clear();
        throwerStartLocation = null;
        sweeperLStartLocation = null;
        sweeperRStartLocation = null;
        currentStoneIndex = 0;
        currentTeamIndex = 0;
    }
}