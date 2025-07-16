using UnityEngine;
using System.Collections.Generic;

/*
Match Structure
    - String: Match ID
    - String: Match Name
    - String: Match Type (e.g., "Friendly", "Tournament")
    - List<GameObject>: Teams
    - Match_Settings
        - Int: Max Games
        - Int: Max Rounds Per Game
        - Int: Max Turns Per Round
        - bool: isPracticeMode
        - bool: isTimedMode
        - float: timeLimit (if timed mode)    
    - Match Score
        - Int: Team 1 Total Score
        - Int: Team 2 Total Score
        - bool: isFinal
        - bool: hasStarted
        - bool: hasEnded
        - bool: hasPaused
        - List<GameObject>: Game Results
            - Game Result Structure
                - String: Game ID
                - Int: Team 1 Score
                - Int: Team 2 Score
                - bool: isFinal

*/

public struct CurlingMatchData {
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public string matchId;
    public string matchName;
    public string matchType;
    public bool hasMatchStarted;
    public bool hasMatchEnded;
    public bool hasMatchPaused {
        get { return hasMatchPaused; }
        set { hasMatchPaused = value; }
    }
    public int currentGameNumber;
    public List<CurlingGameData> games; // List of games in the match
    public CurlingGameData currentGame;
    public List<CurlingTeamData> teams; // List of teams in the match
    public CurlingMatchSettings matchSettings;
    

    public void SetMatchData(
        string matchId = "Match_001",
        string matchName = "Test Match",
        string matchType = "Friendly",
        List<CurlingTeamData> teams = null //,
        // CurlingMatchSettings matchSettings
    ) {
        this.matchId = matchId;
        this.matchName = matchName;
        this.matchType = matchType;
        // this.teams = teams ?? new List<CurlingTeamData>();
        // this.matchSettings.setSettings(
        //     gamesMax: 1,
        //     roundsPerGame: 5,
        //     turnsPerTeamPerRound: 1,
        //     matchType: "Friendly"
        // );
    }

    // Start, End, Pause, Resume Match
    public void StartMatch() {
        this.hasMatchStarted = true;
    }
    public void EndMatch() {
        this.hasMatchEnded = true;
    }
    public void PauseMatch() {
        this.hasMatchPaused = true;
    }
    public void ResumeMatch() {
        this.hasMatchPaused = false;
    }

    // Teams
    public void SetTeams(List<CurlingTeamData> teams) {
        this.teams = teams;
    }
    public void AddTeam(CurlingTeamData team) {
        if (teams == null) {
            teams = new List<CurlingTeamData>();
        }
        teams.Add(team);
    }
    public void RemoveTeam(CurlingTeamData team) {
        if (teams != null && teams.Contains(team)) {
            teams.Remove(team);
        }
    }
    public void ClearTeams() {
        if (teams != null) {
            teams.Clear();
        }
    }

    // Match Settings
    public void SetMatchSettings(CurlingMatchSettings settings) {
        this.matchSettings = settings;
    }
    public void SetMatchType(string matchType) {
        this.matchType = matchType;
    }
    public void SetMatchName(string matchName) {
        this.matchName = matchName;
    }
    public void SetMatchId(string matchId) {
        this.matchId = matchId;
    }   
    public void SetCurrentGameNumber(int currentGameNumber) {
        this.currentGameNumber = currentGameNumber;
    }

    public void AddGame(CurlingGameData game) {
        CurlingGameData newGame = new CurlingGameData();
        newGame.SetGameData(
            gameId: "test_game_001",
            hasStarted: false,
            hasEnded: false,
            isPaused: false,
            roundCurrent: 1,
            turnCurrent: 0
        );

        games.Add(game);
    }

    public void ResetMatch() {
        this.matchId = "";
        this.matchName = "";
        this.matchType = "Friendly";
        this.currentGameNumber = 1;
        if (teams != null) {
            teams.Clear();
        }
        this.matchSettings.SetSettings(
            gamesPerMatch: 1,
            roundsPerGame: 5,
            turnsPerTeamPerRound: 1,
            matchType: "Friendly"
        );
    }    
}

