using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public struct CurlingSpecialAbility
{
    [Header("Basic Data")]
    public string title;
    public string description;


    [Header("Stats")]
    public CurlingPlayerStats stats;

    // [Header("Model")]



    public void BuildRandomStats()
    {

    }
}


public struct CurlingSpecialAbilityEffect
{
    [Header("Basic Data")]
    public string title;
    public string description;

    [Header("Ability Target")]
    public string abilityTarget;
    
    public enum TargetPlayer
    {
        Player, // -> Stat
        Opponent // -> Stat
    }

    public enum TargetCategory
    {
        team, // -> Stat
        teamMember, // -> Stat
        // leftSweeper
        // rightSweeper
        // thrower
        equipment, // -> Lock/Unlock, Ability Control (jump)
        // leftSweeperBroom
        // rightSweeperBroom
        // allBrooms
        // stone
        // firstStone
        // lastStone
        obstacle, // -> Lock/Unlock, 
        inventory
    }

    public enum TargetSubCategory
    {
        // team, // -> Stat
        // teamMember, // -> Stat
        leftSweeper,
        rightSweeper,
        thrower,
        // equipment, // -> Lock/Unlock, Ability Control (jump)
        broom,
        stone,
        obstacle, // -> Lock/Unlock, 
        inventory
    }

    public enum TargetField
    {
        availablity, // locked / unlocked
        equipment
    }
    
    

    public void BuildRandomStats()
    {

    }
  
}


