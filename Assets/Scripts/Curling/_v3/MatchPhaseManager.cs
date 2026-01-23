using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// </summary>


namespace CurlingManagersV3
{

    public class MatchPhaseManager : MonoBehaviour
    {
 
        public static CurlingManagersV3.MatchPhaseManager _instance { get; private set; }
        public CurlingMatchPhase currentPhase { get; private set; }
        public event Action<CurlingMatchPhase> OnPhaseChanged;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        public void SetPhase(CurlingMatchPhase newPhase)
        {
            // Debug.Log($"[MatchPhase] {currentPhase} → {newPhase}");
            if (currentPhase == newPhase) return;

            currentPhase = newPhase;
            // Debug.Log($"[MatchPhase] {currentPhase} → {newPhase}");
            OnPhaseChanged?.Invoke(newPhase);
        }

        public void TestNextPhase(){
            SetPhase(currentPhase + 1);
        }
    }
}