using System;
using UnityEngine;
using CurlingUI.v3;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class Canvas
    {
        [Header("v2/Power Meter")]
        public CurlingUI.v3.PowerMeter powerMeterController;

        [Header("v2/Scorebug")]
        public CurlingUI.v3.Scorebug scoreBugController;

        [Header("v2/Sweeper Bars")]
        public CurlingUI.v3.SweeperExhaustionBar leftSweeperExhaustionBarController;
        public CurlingUI.v3.SweeperExhaustionBar rightSweeperExhaustionBarController;

        // [Header("OLD/Power Meter")]
        // public PowerMeterController powerMeterController;

        // [Header("OLD/Scorebug")]
        // public ScorebugController scoreBug;

        // [Header("OLD/Sweeper Bars")]
        // public GameObject leftSweeperExhaustionBar;
        // public GameObject rightSweeperExhaustionBar;
        
        // [SerializeField] 
        // public SweeperExhaustionBarV2 leftSweeperExhaustionBarController;
        
        // [SerializeField] 
        // public SweeperExhaustionBarV2 rightSweeperExhaustionBarController;
        
    }
}