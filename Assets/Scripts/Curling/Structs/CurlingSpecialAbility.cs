using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public struct CurlingSpecialAbility
{
    [Header("Basic Data")]
    public string title;
    public string description;


    [Header("Stats")]
    public CurlingSpecialAbilityEffect effect;

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

    [Header("Ability Category")]
    public string abilityTargetCategory;

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

    [Header("Ability Sub Category")]
    public string abilityTargetSubCategory;

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

    [Header("Ability Target Field")]
    public string abilityTargetField;

    public enum TargetField
    {
        availablity, // locked / unlocked
        equipment
    }
    
    

    public void BuildRandomStats()
    {

    }
  
}


