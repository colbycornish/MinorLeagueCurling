using System;
using UnityEngine;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class Status
    {
        [Header("Setup Settings")]
        public bool isReady = false;
        public bool isLoading = false;
        
        
    }
}