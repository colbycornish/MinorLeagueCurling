using UnityEngine;
using CurlingGameDataNamespace;


namespace CurlingGameDataNamespace {
    public class CurlingMatchManager : MonoBehaviour
    {
        // Start() and Update() methods deleted - we don't need them right now

        public static CurlingMatchManager Instance;
        public static CurlingGameManager GameInstance;

        public CurlingMatchData matchData = new CurlingMatchData();
    
        public int currentGameNumber = 1;
        public int maxGameNumber = 1;
        public string team1;
        public string team2;

        public bool hasMatchStarted = false;
        public bool hasMatchEnded = false;

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

        // private void Start()
        // {
        //     InitiateDemo();
        // }

        void SetMatchSettings (
            int curGameNum,
            int maxGameNum
        ) {
            currentGameNumber = curGameNum;
            maxGameNumber = maxGameNum;
        }


        void StartMatch() {
            SetTeams();
            GameInstance.StartGame();
            hasMatchStarted = true;
        }

        void SetTeams () {
            
        }

        void LogGameResult() {

        }

        /// <summary>
        /// Instantiate and End a Match
        /// </summary>

       

        void ShouldEndMatch() {
            if (currentGameNumber == maxGameNumber && GameInstance.getHasGameEnded())
            {
                EndMatch();
                return;
            }
            // check if the match should end
            // if so, call EndMatch()
        }

        void EndMatch() {
            Instance.matchData.hasMatchEnded = true;


        }

        void ResetMatch () {

        }

        public void InitiateDemo()
        {
            initiateDemoMatch();
            initiateDemoTeams();
            initiateDemoGame();
            // GameInstance = CurlingGameManager.Instance;
            // GameInstance.SetMatchData(matchData);
            // GameInstance.StartGame();
        }

        public void initiateDemoMatch(){
            matchData.SetMatchData(
                matchId: "demo_match_001",
                matchName: "Demo Match",
                matchType: "Friendly"
            );
            matchData.matchSettings.SetSettings(
                gamesPerMatch: 1,
                roundsPerGame: 5,
                turnsPerTeamPerRound: 1,
                matchType: "Friendly"
            );
            matchData.hasMatchStarted = false;
            matchData.hasMatchEnded = false;
            matchData.hasMatchPaused = false;
        }

        public void initiateDemoTeams(){
            CurlingTeamData team1 = new CurlingTeamData();
            team1.SetTeamInfo("Blue Team", "team_red");

            CurlingTeamData team2 = new CurlingTeamData();
            team2.SetTeamInfo("Red Team", "team_blue");

            matchData.AddTeam(team1);
            matchData.AddTeam(team2);

            // Set the teams in the game instance
            

        }

        public void initiateDemoGame(){
            CurlingGameData game = new CurlingGameData();
            game.SetGameData(
                gameId: "demo_game_001",
                hasStarted: false,
                hasEnded: false,
                isPaused: false,
                roundCurrent: 1,
                turnCurrent: 0
            );
            game.gameSettings.SetSettings(
                roundsPerGame: matchData.matchSettings.roundsPerGame,
                turnsPerTeamPerRound: matchData.matchSettings.turnsPerTeamPerRound,
                maxStonesPerTeam: matchData.matchSettings.maxStonesPerTeam,
                isPracticeMode: matchData.matchSettings.isPracticeMode,
                isTimedMode: matchData.matchSettings.isTimedMode,
                timeLimit: matchData.matchSettings.timeLimit 
            );
            matchData.AddGame(game);
            matchData.currentGame = game;
        }

    }
}