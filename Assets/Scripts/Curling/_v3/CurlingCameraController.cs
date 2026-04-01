using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Playables;
using Unity.VisualScripting;

namespace CurlingManagersV3
{
    public class CurlingCameraController : MonoBehaviour
    {
        [Header("Aim Settings")]
        public Camera stoneCamera;
        public CinemachineCamera ccStoneCamera; 
        public Camera targetZoneCamera;
        public Camera throwerCamera;
        public Camera courseCamera;
        public Camera announcersCamera;
        

        public void Setup(
            Camera stoneCamera,
            CinemachineCamera ccStoneCamera,
            Camera targetZoneCamera,
            Camera throwerCamera,
            Camera courseCamera,
            Camera announcersCamera
        ){
            if (stoneCamera != null){
                this.stoneCamera = stoneCamera;
            }
            if (ccStoneCamera != null){
                this.ccStoneCamera = ccStoneCamera;
            }
            if (targetZoneCamera != null){
                this.targetZoneCamera = targetZoneCamera;
            }
            if (throwerCamera != null){
                this.throwerCamera = throwerCamera;
            }
            if (courseCamera != null){
                this.courseCamera = courseCamera;
            }
            if (announcersCamera != null){
                this.announcersCamera = announcersCamera;
            }
        }

        public void SwitchToStoneCamera(){
            if (stoneCamera != null){
                stoneCamera.enabled = true;
            }
            if (ccStoneCamera != null){
                ccStoneCamera.enabled = true;
            }
            
            Debug.Log("Switching Target for Stone Camera");
            Transform stoneCameraTarget = CurlingManager._instance.Parameters.Stones.currentStone.transform;
            if (CurlingManager._instance.Parameters.Stones.currentStone == null || stoneCameraTarget == null)
            {   
                stoneCameraTarget = CurlingManager._instance.Parameters.Course.course.launchPoint;
            }

            if (stoneCamera != null){
                stoneCamera.GetComponent<CinemachineCamera>().Follow = stoneCameraTarget;
            }
            if (ccStoneCamera != null) 
            {
                ccStoneCamera.Follow = stoneCameraTarget;
            }
                
            
        }

        public void SwitchToAnnouncerCamera(){
            // stoneCamera.GetComponent<CinemachineCamera>().Follow = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone.transform;
        }

        public void SwitchToTargetZoneCamera(){
            // stoneCamera.GetComponent<CinemachineCamera>().Follow = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone.transform;
        }

        public void SwitchToTeamReactionCamera(){
            // stoneCamera.GetComponent<CinemachineCamera>().Follow = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone.transform;
        }

        public void PlayCourseIntroTimeline()
        {
            // timeline = GetComponent<PlayableDirector>();
            if (CurlingManager._instance.Parameters.Course.course.courseFullTimeline != null){
                PlayableDirector director = CurlingManager._instance.Parameters.Course.course.courseFullTimeline;
                director.Play();
                if (stoneCamera != null){
                    stoneCamera.enabled = false;
                }
                if (ccStoneCamera != null){
                    ccStoneCamera.enabled = false;
                }

                director.stopped += OnTimelineFinished;
                return;
            }
            else
            {
                Debug.LogWarning("No Course Intro Timeline Found!");
            }
            
        }

        void OnTimelineFinished(PlayableDirector aDirector)
        {
            aDirector.Pause();
            aDirector.Stop();
            Debug.Log("Timeline has finished!");
            SwitchToStoneCamera();
            // Place the code you want to run after the timeline here
        }
        // 

        public void Reset(){
            this.stoneCamera = null;
            this.targetZoneCamera = null;
            this.throwerCamera = null;
            this.courseCamera = null;
            this.announcersCamera = null;
        }


    }
}