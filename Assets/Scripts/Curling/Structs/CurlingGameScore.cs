using UnityEngine;
using System.Collections.Generic;


public struct CurlingGameScore {
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public int team01Score;
    public string team01Id;
    public int team02Score;
    public string team02Id;
    public bool isFinal;
    
    //Constructor (not necessary, but helpful)
    public void setScore(
        int team01Score = 0,
        int team02Score = 0,
        bool isFinal = false
    ) {
        this.team01Score = team01Score;
        this.team02Score = team02Score;
        this.isFinal = isFinal;
    }
    public void SetTeam01Score(int score) {
        this.team01Score = score;
    }
    public void SetTeam02Score(int score) {
        this.team02Score = score;
    }
    public void SetIsFinal(bool isFinal) {
        this.isFinal = isFinal;
    }
    public void ResetScore() {
        this.team01Score = 0;
        this.team02Score = 0;
        this.isFinal = false;
    }
}
