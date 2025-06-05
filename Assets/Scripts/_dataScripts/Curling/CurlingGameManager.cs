using UnityEngine;
using CurlingGameDataNamespace;

namespace CurlingGameDataNamespace {
    public class CurlingGameManager : MonoBehaviour
    {
        public CurlingMatchCanvasController curlingMatchCanvasController;
        public StoneThrowController stoneThrowController;
        // Start() and Update() methods deleted - we don't need them right now

        public static CurlingGameManager Instance;
        /// <summary>
        /// defines markers for the current turn
        /// </summary>
        /// 
        
        private string currentTurnTeamName = "";
        private string currentTurnPhase = "";
        private string startingTeamName = "";
        
        private int turnCount = 0;
        private int roundCount = 1;
        private int maxRounds = 10;
        


        // public ARRAY Teams; 

        // public TeamHome teamHome;
        // public TeamAway teamAway;

        /// <summary>
        /// Array of current stone placements (logged after throw)
        /// </summary>

        // private void Awake()
        // {
        //     if (Instance != null)
        //     {
        //         Destroy(gameObject);
        //         return;
        //     }
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }

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