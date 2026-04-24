using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Playables;
using Unity.VisualScripting;

namespace CurlingManagersV3
{
    public class CurlingCameraController : MonoBehaviour
    {
        [Header("Cameras")]
        public Camera stoneCamera => CurlingManager._instance.Parameters.Cinematics.stoneCamera;
        public Camera targetZoneCamera => CurlingManager._instance.Parameters.Cinematics.targetZoneCamera;
        public Camera throwerCamera => CurlingManager._instance.Parameters.Cinematics.throwerCamera;
        public Camera courseCamera => CurlingManager._instance.Parameters.Cinematics.courseCamera;
        public Camera announcersCamera => CurlingManager._instance.Parameters.Cinematics.announcersCamera;

        [Header("Cinemachine Cameras")]
        // Stone & Target Zone
        public CinemachineCamera ccFollowStoneCamera => CurlingManager._instance.Parameters.Cinematics.ccFollowStoneCamera;
        public CinemachineCamera ccOrbitTargetZoneCamera => CurlingManager._instance.Parameters.Cinematics.ccOrbitTargetZoneCamera;
        public CinemachineCamera ccOrbitStoneCamera => CurlingManager._instance.Parameters.Cinematics.ccOrbitStoneCamera;
        
        // Team Area
        public CinemachineCamera ccStaticStoneBenchCamera => CurlingManager._instance.Parameters.Cinematics.ccStaticStoneBenchCamera;
        public CinemachineCamera ccDollyInHomeTeamCamera => CurlingManager._instance.Parameters.Cinematics.ccDollyInHomeTeamCamera;
        public CinemachineCamera ccDollyInAwayTeamCamera => CurlingManager._instance.Parameters.Cinematics.ccDollyInAwayTeamCamera;
        
        /************************************************************************************************************************/

        public void Setup(
            Camera stoneCamera,
            CinemachineCamera ccStoneCamera,
            Camera targetZoneCamera,
            Camera throwerCamera,
            Camera courseCamera,
            Camera announcersCamera
        ){
            CurlingManager cm = CurlingManager._instance;
            if (stoneCamera != null){
                cm.Parameters.Cinematics.stoneCamera = stoneCamera;
                // this.stoneCamera = stoneCamera;
            }
            if (ccStoneCamera != null){
                cm.Parameters.Cinematics.ccFollowStoneCamera = ccStoneCamera;
            }
            if (targetZoneCamera != null){
                // this.targetZoneCamera = targetZoneCamera;
                cm.Parameters.Cinematics.targetZoneCamera = targetZoneCamera;
            }
            if (throwerCamera != null){
                // this.throwerCamera = throwerCamera;
                cm.Parameters.Cinematics.throwerCamera = throwerCamera;
            }
            if (courseCamera != null){
                cm.Parameters.Cinematics.courseCamera = courseCamera;
                // this.courseCamera = courseCamera;
            }
            if (announcersCamera != null){
                cm.Parameters.Cinematics.announcersCamera = announcersCamera;
                // this.announcersCamera = announcersCamera;
            }
        }


        /************************************************************************************************************************/
        // Follow Stone Camera
        /************************************************************************************************************************/
        public void SwitchToFollowStoneCamera(){
            
            if (ccFollowStoneCamera != null){
                Debug.Log("🎥 Enabling ccFollowStoneCamera and setting follow target");
                UpdateFollowStoneCameraTarget();
                ccFollowStoneCamera.enabled = true;
                ccFollowStoneCamera.Prioritize();
            }            
        }

        public void UpdateFollowStoneCameraTarget(){
            Transform followTarget = CurlingManager._instance.Parameters.Stones.currentStone != null
                ? CurlingManager._instance.Parameters.Stones.currentStone.transform
                : CurlingManager._instance.Parameters.Course.course.launchPoint;

            ccFollowStoneCamera.Follow = followTarget;
        }

        /************************************************************************************************************************/
        // Orbit Stone Camera
        /************************************************************************************************************************/
        public void SwitchToOrbitStoneCamera(){
            
            if (ccOrbitStoneCamera != null){
                Debug.Log("🎥 Enabling ccOrbitStoneCamera and setting follow target");
                UpdateOrbitStoneCameraTarget();
                ccOrbitStoneCamera.enabled = true;
                ccOrbitStoneCamera.Prioritize();
            }            
        }

        public void UpdateOrbitStoneCameraTarget(){
            Transform followTarget = CurlingManager._instance.Parameters.Stones.currentStone != null
                ? CurlingManager._instance.Parameters.Stones.currentStone.transform
                : CurlingManager._instance.Parameters.Course.course.launchPoint;

            ccOrbitStoneCamera.Follow = followTarget;
        }

        /************************************************************************************************************************/
        // Announcer Camera
        /************************************************************************************************************************/

        public void SwitchToAnnouncerCamera(){
            // stoneCamera.GetComponent<CinemachineCamera>().Follow = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone.transform;
        }

        /************************************************************************************************************************/
        // Target Zone Camera
        /************************************************************************************************************************/

        public void SwitchToTargetZoneCamera(){
            if (ccOrbitTargetZoneCamera != null)
            {
                ccOrbitTargetZoneCamera.gameObject.SetActive(true);
                Debug.Log("🎥 Switching to Target Zone Camera");
                UpdateFollowTargetZoneCameraTarget();
                ccOrbitTargetZoneCamera.enabled = true;
                ccOrbitTargetZoneCamera.Prioritize();
            }
        }

        private void UpdateFollowTargetZoneCameraTarget(){
                ccOrbitTargetZoneCamera.Follow = CurlingManager._instance.Parameters.Course.targetZone.transform;
        }

        /************************************************************************************************************************/
        // Follow Static Stone Bench Camera
        /************************************************************************************************************************/

        public void SwitchToStoneBenchCamera(){
            if (ccStaticStoneBenchCamera != null)
            {
                Debug.Log("🎥 Switching to Stone Bench Camera");
                UpdateStaticStoneBenchCameraTarget();
                
                ccStaticStoneBenchCamera.enabled = true;
                ccStaticStoneBenchCamera.Prioritize();
            }
        }

        private void UpdateStaticStoneBenchCameraTarget(){
            Transform followTarget = CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
                ? CurlingManager._instance.Parameters.Course.stoneBenchTeamHome
                : CurlingManager._instance.Parameters.Course.stoneBenchTeamAway;

            ccStaticStoneBenchCamera.Follow = followTarget;
        }

        /************************************************************************************************************************/
        // Dolly In Team Camera
        /************************************************************************************************************************/
        public void SwitchToDollyInCurrentTeamCamera()
        {
            if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home)
            {
                SwitchToDollyHomeTeamCamera();
            } else if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Away)
            {
                SwitchToDollyAwayTeamCamera();
            }
        }

        public void SwitchToDollyAwayTeamCamera(){
            if (ccDollyInAwayTeamCamera != null)
            {
                Debug.Log("🎥 Switching to Dolly Away Team Zone Camera");
                ccOrbitTargetZoneCamera.gameObject.SetActive(false);
                ccOrbitTargetZoneCamera.enabled = false;
                UpdateDollyInAwayTeamCameraTarget();
                // ccDollyInAwayTeamCamera.
                 
                ccDollyInAwayTeamCamera.enabled = true;
                ccDollyInAwayTeamCamera.gameObject.SetActive(false);
                ccDollyInAwayTeamCamera.gameObject.SetActive(true);
                ccDollyInAwayTeamCamera.Prioritize();
                
                // ccDollyInAwayTeamCamera.Priority += 1; 
                
            }
        }

        public void SwitchToDollyHomeTeamCamera(){
            if (ccDollyInHomeTeamCamera != null)
            {
                Debug.Log("🎥 Switching to Dolly Home Team Camera");
                ccOrbitTargetZoneCamera.gameObject.SetActive(false);
                ccOrbitTargetZoneCamera.enabled = false;
                UpdateDollyInHomeTeamCameraTarget();
                ccDollyInHomeTeamCamera.enabled = true;
                ccDollyInHomeTeamCamera.gameObject.SetActive(false);
                ccDollyInHomeTeamCamera.gameObject.SetActive(true);
                ccDollyInHomeTeamCamera.Prioritize();
                
                // ccDollyInHomeTeamCamera.Priority += 1; 
            }
        }

        private void UpdateDollyInAwayTeamCameraTarget(){
                ccOrbitTargetZoneCamera.Follow = CurlingManager._instance.Parameters.Teams.teamAway.thrower.transform;
        }

        private void UpdateDollyInHomeTeamCameraTarget(){
                ccDollyInHomeTeamCamera.Follow = CurlingManager._instance.Parameters.Teams.teamHome.thrower.transform;
        }

        /************************************************************************************************************************/
        // Follow Stone Camera
        /************************************************************************************************************************/

        public void SwitchToTeamReactionCamera(){
            // stoneCamera.GetComponent<CinemachineCamera>().Follow = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone.transform;
        }

        /************************************************************************************************************************/

        public void PlayCourseIntroTimeline()
        {
            // timeline = GetComponent<PlayableDirector>();
            if (CurlingManager._instance.Parameters.Course.course.courseFullTimeline != null){
                PlayableDirector director = CurlingManager._instance.Parameters.Course.course.courseFullTimeline;
                director.Play();
                if (stoneCamera != null){
                    stoneCamera.enabled = false;
                }
                if (ccFollowStoneCamera != null){
                    ccFollowStoneCamera.enabled = false;
                }

                director.stopped += OnTimelineFinished;
                return;
            }
            else
            {
                Debug.LogWarning("No Course Intro Timeline Found!");
            }
            
        }

        /************************************************************************************************************************/

        void OnTimelineFinished(PlayableDirector aDirector)
        {
            aDirector.Pause();
            aDirector.Stop();
            Debug.Log("Timeline has finished!");
            SwitchToFollowStoneCamera();
            // Place the code you want to run after the timeline here
        }

        /************************************************************************************************************************/

        public void Reset(){
            CurlingManager cm = CurlingManager._instance;
            cm.Parameters.Cinematics.stoneCamera = null;
            cm.Parameters.Cinematics.ccFollowStoneCamera = null;
            cm.Parameters.Cinematics.targetZoneCamera = null;
            cm.Parameters.Cinematics.throwerCamera = null;
            cm.Parameters.Cinematics.courseCamera = null;
            cm.Parameters.Cinematics.announcersCamera = null;


            // this.stoneCamera = null;
            // this.targetZoneCamera = null;
            // this.throwerCamera = null;
            // this.courseCamera = null;
            // this.announcersCamera = null;
        }


    }
}