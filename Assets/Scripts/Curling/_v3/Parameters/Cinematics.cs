using System;
using UnityEngine;
using UnityEngine.Playables;
using Unity.Cinemachine;

namespace CurlingManagersV3.Parameters
{
    [Serializable]
    public class Cinematics
    {
        [Header("Cameras")]
        public Camera stoneCamera;
        public Camera targetZoneCamera;
        public Camera throwerCamera;
        public Camera courseCamera;
        public Camera announcersCamera;

        [Header("Cinemachine Cameras")]
        // Stone & Target Zone
        public CinemachineCamera ccFollowStoneCamera;
        public CinemachineCamera ccOrbitTargetZoneCamera;
        public CinemachineCamera ccOrbitStoneCamera;
        // Team Area
        public CinemachineCamera ccStaticStoneBenchCamera;
        public CinemachineCamera ccDollyInHomeTeamCamera;
        public CinemachineCamera ccDollyInAwayTeamCamera;
        // Announcers
        public CinemachineCamera ccDollyAnnouncerCamera;
        public CinemachineCamera ccProfileBroomyAnnouncerCamera;
        public CinemachineCamera ccProfileMickAnnouncerCamera;
        
        
        [Header("Course Cinematics")]
        public PlayableDirector courseFullTimeline;
        public PlayableDirector targetZoneTimeline;
        public PlayableDirector teamHomeIntroTimeline;
        public PlayableDirector teamAwayIntroTimeline;

        [Header("Announcer Cinematics")]
        public PlayableDirector announcerTimeline;
        
    }
}