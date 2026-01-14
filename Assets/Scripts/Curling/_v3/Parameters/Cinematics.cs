using System;
using UnityEngine;
using UnityEngine.Playables;

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
        
        [Header("Course Cinematics")]
        public PlayableDirector courseFullTimeline;
        public PlayableDirector targetZoneTimeline;
        public PlayableDirector teamHomeIntroTimeline;
        public PlayableDirector teamAwayIntroTimeline;

        [Header("Announcer Cinematics")]
        public PlayableDirector announcerTimeline;
        
    }
}