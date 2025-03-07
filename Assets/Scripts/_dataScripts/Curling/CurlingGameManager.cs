using UnityEngine;
using CurlingGameDataNamespace;

namespace CurlingGameDataNamespace {
    public class CurlingGameManager : MonoBehaviour
    {
        // Start() and Update() methods deleted - we don't need them right now

        public static CurlingGameManager Instance;
        /// <summary>
        /// defines markers for the current turn
        /// </summary>
        public string currentTurnTeamName = "";
        public string currentTurnPhase = "";
        
        public double turnCount = 0;
        public string startingTeamName = "";


        // public ARRAY Teams; 

        // public TeamHome teamHome;
        // public TeamAway teamAway;

        /// <summary>
        /// Array of current stone placements (logged after throw)
        /// </summary>

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void SetTeams () {

        }

        void SaveStonePositions () {

        }

        void AdvanceTurn () {

        }

        void EndGame() {

        }

        void Reset() {
            
        }
    }
}