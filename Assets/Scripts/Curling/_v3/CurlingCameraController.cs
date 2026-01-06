using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class CurlingCameraController : MonoBehaviour
    {
        [Header("Aim Settings")]
        public Camera stoneCamera;
        public Camera targetZoneCamera;
        public Camera throwerCamera;
        public Camera courseCamera;
        public Camera announcersCamera;
        

        public void Setup(
            Camera stoneCamera,
            Camera targetZoneCamera,
            Camera throwerCamera,
            Camera courseCamera,
            Camera announcersCamera
        ){
            if (stoneCamera != null){
                this.stoneCamera = stoneCamera;
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
            stoneCamera.GetComponent<CinemachineCamera>().Follow = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone.transform;
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