using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// obstacles
/// </summary>
namespace Curling.Rules {
    public struct Obstacles
    {
    

        public CurlingGameObstacleFrequency frequency;
        public bool enableObstaclePlacementByPlayer;
        public bool enableObstaclePlacementByCourse;
        public bool enableObstacles;

        public void SetFrequency(
            bool isNone = false,
            bool isLow = false,
            bool isMedium = false,
            bool isHigh = false
        ){

            if (isLow) this.frequency = CurlingGameObstacleFrequency.Low;
            else if (isMedium) this.frequency = CurlingGameObstacleFrequency.Medium;
            else if (isHigh) this.frequency = CurlingGameObstacleFrequency.High;
            else if (isNone) this.frequency = CurlingGameObstacleFrequency.None;
            else {
                this.frequency = CurlingGameObstacleFrequency.None;
            }
        }

        ///
        /// Enable or disable obstacles
        /// 
        public void EnableObstaclePlacementByPlayer(){
            enableObstaclePlacementByPlayer = true;
        }

        public void DisableObstaclePlacementByPlayer(){
            enableObstaclePlacementByPlayer = false;
        }

        public void EnableObstaclePlacementByCourse(){
            enableObstaclePlacementByCourse = true;
        }

        public void DisableObstaclePlacementByCourse(){
            enableObstaclePlacementByCourse = false;
        }

        public void EnableObstacles(){
            enableObstacles = true;
            // enableObstaclePlacementByCourse = true;
        }

        public void DisableObstacles(){
            enableObstaclePlacementByPlayer = false;
            enableObstaclePlacementByCourse = false;
            enableObstacles = false;
        }
    }
}