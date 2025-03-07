using UnityEngine;
using CurlingGameDataNamespace;

namespace CurlingGameDataNamespace {


    public class CurlingMatchManager : MonoBehaviour
    {
        // Start() and Update() methods deleted - we don't need them right now

        public static CurlingMatchManager Instance;
        public static CurlingGameManager GameInstance;

        public int currentGameNumber = 0;
        public int maxGameNumber = 3;


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

        void SetMatchSettings (
            int curGameNum,
            int maxGameNum
        ) {
            currentGameNumber = curGameNum;
            maxGameNumber = maxGameNum;
        }


        void SetTeams () {

        }

        void LogGameResult() {

        }

        /// <summary>
        /// Instantiate and End a Match
        /// </summary>

        void EndMatch () {
            // show results of the match (new scene)
            // storeAndUpdatePlayerGameDatat();
            // resetMatchData();

        }

        void ResetMatch () {

        }


        /// <summary>
        /// Return Game Results to State
        /// </summary>
    }
}