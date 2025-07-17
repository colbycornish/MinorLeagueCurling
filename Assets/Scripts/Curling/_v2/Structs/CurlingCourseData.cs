using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingCourseData : MonoBehaviour
{
    [Header("Base Data")]
    public string title = "";
    public string description = "";

    // Main Locations
    [Header("In-Play Locations")]
    public Transform launchPoint;
    public GameObject targetZone;

    [Header("Player Locations")]
    public Transform throwerStartLocation;
    public Transform sweeperLStartLocation;
    public Transform sweeperRStartLocation;
    public List<Transform> idleLocationsTeamHome;
    public List<Transform> idleLocationsTeamAway;

    // Stones
    // public List<CurlingStone> stonesTeamHome = new List<CurlingStone>();
    // public List<CurlingStone> stonesTeamAway = new List<CurlingStone>();
    [Header("Stone Locations")]
    public List<Transform> stonesSpawnLocationsTeamHome = new List<Transform>();
    public List<Transform> stonesSpawnLocationsTeamAway = new List<Transform>();
    
    [Header("Objects to hide")]
    public GameObject objectsToHideIfCurling;


    [Header("Post Game Spawn")]
    public Transform afterGameSpawnLocation;
    
    private void Awake()
    {

    }

    public void ResetStone()
    {
        
    }

    
}