using System;
using UnityEngine;
using System.Collections.Generic;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class Course
    {

        // [SerializeField]
        // private Transform _throwerStartLocation;
        // public ref Transform throwerStartLocation => ref _throwerStartLocation;
        // / - Locations
        [Header("Course")]
        public CurlingCourseData course;      

        [Header("Placement Locations")]
        public Transform throwerStartLocation;
        public Transform sweeperLStartLocation;
        public Transform sweeperRStartLocation;
        public List<Transform> idleLocationsTeamHome;
        public List<Transform> idleLocationsTeamAway;

        [Header("Locations")]
        public List<Transform> stonesSpawnLocationsTeamHome = new List<Transform>();
        public List<Transform> stonesSpawnLocationsTeamAway = new List<Transform>();

        [Header("Target")]
        public GameObject targetZone;

        [Header("Launch Point")]
        public Transform launchPoint;

        [Header("Directional Pivot")]
        public GameObject directionPivotObject;
        public Transform directionPivot; // assign this in Inspector  

        
        
    }
}

