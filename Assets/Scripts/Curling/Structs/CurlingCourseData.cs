using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CurlingObjects;
using UnityEngine.Playables;
using Unity.Cinemachine;

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
    public Transform stoneBenchTeamHome;
    public Transform stoneBenchTeamAway;

    [Header("Objects to hide")]
    public GameObject objectsToHideIfCurling;
    public GameObject objectsToShowIfCurling;

    [Header("Announcer Booth")]
    public AnnouncerBooth announcerBooth;

    [Header("Post Game Spawn")]
    public Transform afterGameSpawnLocation;

    [Header("Cameras")]
    public CinemachineCamera ccDollyInTeamHome;
    public CinemachineCamera ccDollyInTeamAway;
    public CinemachineCamera ccOverheadCourseCamera;
    // public CinemachineCamera ccOverheadCourseCamera;


    [Header("Cinematics")]
    public PlayableDirector courseFullTimeline;
    public PlayableDirector targetZoneTimeline;
    public PlayableDirector teamHomeIntroTimeline;
    public PlayableDirector teamAwayIntroTimeline;

    private void Awake()
    {

    }

    public void ResetStone()
    {

    }


}