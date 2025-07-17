using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/*
Team Structure
    - String: Team Name
    - GameObject: Thrower
    - GameObject: Sweeper Left
    - GameObject: Sweeper Right

    - List<GameObject>: Stones
         - Stone Structure
            - GameObject: Stone Type
            - string: StoneTypeId
            - GameObject: Attachment Type
            - bool: hasAttachment
            - string: AttachmentTypeId
            - bool hasAdditionalAnimations
            - bool hasAdditionalControls
            - CONTROLS
            - ANIMATIONS
            - IN_GAME_METRICS
                - bool hasBeenThrown
                - bool canBeThrown
                - bool isInPlay
                - Vector3: currentPosition
                - Vector3: currentRotation
*/



public struct CurlingTeamData
{
    //Variable declaration
    public string teamName;
    public string teamId;
    // public Image avatarImage;
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

