using UnityEngine;
using System.Collections.Generic;


public struct CurlingGameScore {
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public int teamHomeScore;
    public string teamHomeId;
    public int teamAwayScore;
    public string teamAwayId;
    public bool isFinal;
    
    //Constructor (not necessary, but helpful)
    public void SetScore(
        int teamHomeScore = 0,
        int teamAwayScore = 0,
        bool isFinal = false
    ) {
        this.teamHomeScore = teamHomeScore;
        this.teamAwayScore = teamAwayScore;
        this.isFinal = isFinal;
    }
    public void SetTeamHomeScore(int score) {
        this.teamHomeScore = score;
    }
    public void SetTeamAwayScore(int score) {
        this.teamAwayScore = score;
    }
    public void SetIsFinal(bool isFinal) {
        this.isFinal = isFinal;
    }
    public void ResetScore() {
        this.teamHomeScore = 0;
        this.teamAwayScore = 0;
        this.isFinal = false;
    }
}
