using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingTeam : MonoBehaviour
{
    public CurlingTeamData data;

    [Header("Basic Data")]
    public string teamName; // change to name
    public string teamId; // change to id
    // public Image avatarImage;
    // public bool isControlledByAi;

    [Header("Team Members")]
    public GameObject thrower;
    public GameObject sweeperLeft;
    public GameObject sweeperRight;

    [Header("Stones")]
    public GameObject defaultStone;
    public List<GameObject> stones;

    public void PopulateListOfStones()
    {
        
    }

    public void SetData()
    {
        data.teamName = teamName;
        data.teamId = teamId;
        // data.avatarImage = avatarImage;

        data.defaultStone = defaultStone;
        data.stones = stones;

        CurlingPlayer cp_thrower = new CurlingPlayer
        {
            name = "Thrower A",
            characterId = "char_a",
            isThrower = true,
            isSweeper = false,
            character = thrower,
            // rb = ??,
            visual = thrower
        };

        CurlingPlayer cp_sweeperLeft = new CurlingPlayer
        {
            name = "Thrower A",
            characterId = "char_a",
            isThrower = false,
            isSweeper = true,
            isSweeperLeft = true,
            isSweeperRight = false,
            character = sweeperLeft,
            // rb = ??,
            visual = sweeperLeft,
        };

        CurlingPlayer cp_sweeperRight = new CurlingPlayer
        {
            name = "Thrower A",
            characterId = "char_a",
            isThrower = false,
            isSweeper = true,
            isSweeperLeft = true,
            isSweeperRight = true,
            character = sweeperRight,
            // rb = ??,
            visual = sweeperRight
        };


        data.thrower = cp_thrower;
        data.sweeperLeft = cp_sweeperLeft;
        data.sweeperRight = cp_sweeperRight;
    }    
}