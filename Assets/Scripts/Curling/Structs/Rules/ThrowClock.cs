using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// throw clock
/// </summary>
namespace Curling.Rules {

    [Serializable]
    public class ThrowClock
    {

        
        public bool enabled;
        public int timeAllowedInSeconds;

        // public void SetTimerLength(int timeLength = 10){
        //     timeAllowedInSeconds = timeLength;
        // }

        // public void EnableThrowClock(){
        //     enabled = true;
        //     // if (timeAllowedInSeconds == null){
        //     timeAllowedInSeconds = 30;
        //     // }
        // }

        // public void DisableThrowClock(){
        //     enabled = false;
        // }
    }
}
