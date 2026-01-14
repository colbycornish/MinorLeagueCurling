using System;
using UnityEngine;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class Canvas
    {
        [Header("Power Meter")]
        public PowerMeterController powerMeterController;

        [Header("Scorebug")]
        public ScorebugController scoreBug;

        [Header("Sweeper Bars")]
        public GameObject leftSweeperExhaustionBar;
        public GameObject rightSweeperExhaustionBar;
        
        [SerializeField] 
        public SweeperExhaustionBarV2 leftSweeperExhaustionBarController;
        
        [SerializeField] 
        public SweeperExhaustionBarV2 rightSweeperExhaustionBarController;
        
    }
}