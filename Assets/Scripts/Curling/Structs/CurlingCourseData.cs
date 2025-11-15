using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CurlingObjects;

public class CurlingCourseData : MonoBehaviour
{
    [Header("Base Data")]
    public string id = "";
    public string title = "";
    public string description = "";
    public Texture thumbnail;

    // Main Locations
    [Header("In-Play Locations")]
    public Transform launchPoint;
    public GameObject targetZone;
    public GameObject directionalPivot;

    [Header("Player Locations")]
    public Transform throwerStartLocation;
    public Transform sweeperLStartLocation;
    public Transform sweeperRStartLocation;
    public List<Transform> idleLocationsTeamHome;
    public List<Transform> idleLocationsTeamAway;

    [Header("Stone Locations")]
    public List<Transform> stonesSpawnLocationsTeamHome = new List<Transform>();
    public List<Transform> stonesSpawnLocationsTeamAway = new List<Transform>();

    [Header("Objects to hide")]
    public GameObject objectsToHideIfCurling;

    [Header("Announcer Booth")]
    public AnnouncerBooth announcerBooth;

    [Header("Post Game Spawn")]
    public Transform afterGameSpawnLocation;

    private void Awake()
    {

    }

    public void ResetStone()
    {

    }


}