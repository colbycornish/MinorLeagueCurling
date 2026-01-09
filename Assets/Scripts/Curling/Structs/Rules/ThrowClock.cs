using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// throw clock
/// </summary>
namespace Curling.Rules {
    public struct ThrowClock
    {

        public bool enabled;
        public int timeAllowedInSeconds;

        public void SetTimerLength(int timeLength = 10){
            timeAllowedInSeconds = timeLength;
        }

        public void EnableThrowClock(){
            enabled = true;
            // if (timeAllowedInSeconds == null){
            timeAllowedInSeconds = 30;
            // }
        }

        public void DisableThrowClock(){
            enabled = false;
        }
    }
}
