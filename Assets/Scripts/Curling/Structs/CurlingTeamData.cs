using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public struct CurlingTeamData
{
    //Variable declaration
    public string teamName;
    public string teamId;
    public Image avatarImage;
    public CurlingPlayer thrower;
    public CurlingPlayer sweeperLeft;
    public CurlingPlayer sweeperRight;
    public GameObject defaultStone;
    public List<GameObject> stones; // List of stones for the team

    //Constructor (not necessary, but helpful)
    public void SetTeamInfo(
        string teamName = "Team A",
        string teamId = "team_a"
    )
    {
        this.teamName = teamName;
        this.teamId = teamId;
    }
}

