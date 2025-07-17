using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;



/*
- Sweeper Structure
    - GameObject: Sweeper Character
    - Int: Exhaustion Level
    - Int: Exhaustion Level Max
    - Int: Exhaustion Level Min
    - Int: Exhaustion Level Current
    - Int: Strength Level
    - Int: Strength Level Max
    - Int: Strength Level Min
    - Int: Strength Level Current
    - Int: Stamina Level
    - Int: Stamina Level Max
    - Int: Stamina Level Min
    - Int: Stamina Level Current
    - Int: Speed Level
    - Int: Speed Level Max
    - Int: Speed Level Min
    - Int: Speed Level Current
    - bool isSweeping
    - bool isExhausted
    - hasAdditionalBenefits
    - SWEEPERBENEFITS
    - ANIMATIONS
*/

public struct CurlingPlayerStats {

    [Header("Exhaustion Stats")]
    public int exhaustionRate;
    public int exhaustionLevelMax;
    public int exhaustionLevelMin;
    public int exhaustionLevelCurrent;

    [Header("Cooldown Stats")]
    public int cooldownRate; // How quickly the player can recover from exhaustion

    [Header("Strength Stats")]
    public int strengthLevelBase;
    public int strengthLevelMax;
    public int strengthLevelMin;
    public int strengthLevelCurrent;

    [Header("Stamina Stats")]
    public int staminaLevelBase;
    public int staminaLevelMax;
    public int staminaLevelMin;
    public int staminaLevelCurrent;
    
    [Header("Speed Stats")]
    public int speedLevelBase;
    public int speedLevelMax;
    public int speedLevelMin;
    public int speedLevelCurrent;

    [Header("Sweeper State")]
    public bool isSweeping;
    public bool isExhausted;
    public bool hasAdditionalBenefits;
    // Add any additional benefits or animations as needed
    // public List<string> additionalBenefits; // List of additional benefits
    // public List<string> animations; // List of animations

    // Exhaustion Functions
    public void SetExhaustionStats(
        int exhaustionRate = 1,
        int exhaustionLevelMin = 0,
        int exhaustionLevelMax = 100
        
    ) {
        this.exhaustionRate = exhaustionRate; // Default exhaustion rate
        this.exhaustionLevelMax = exhaustionLevelMax; // Default max exhaustion level
        this.exhaustionLevelMin = exhaustionLevelMin;
    }

    public void SetExhaustionLevelCurrent(
        int exhaustionLevelCurrent
    ) {
        this.exhaustionLevelCurrent = exhaustionLevelCurrent; // Default current exhaustion level
    }

    // Cooldown Functions
    public void SetCooldownRate(
        int cooldownRate = 1
    ) {
        this.cooldownRate = cooldownRate; // Default cooldown rate
    }

    // Strength Functions
    public void SetStrengthLevels(
        int strengthLevelBase,
        int strengthLevelMin = 0,
        int strengthLevelMax = 100
    ) {
        this.strengthLevelBase = strengthLevelBase;
        this.strengthLevelMax = strengthLevelMax; // Default max strength level
        this.strengthLevelMin = strengthLevelMin;
    }

    public void SetStrengthLevelCurrent(
        int strengthLevelCurrent
    ) {
        this.strengthLevelCurrent = strengthLevelCurrent; // Default current exhaustion level
    }

    // Stamina Functions
    public void SetStaminaLevels(
        int staminaLevelBase,
        int staminaLevelMin = 0,
        int staminaLevelMax = 100
    ) {
        this.staminaLevelBase = staminaLevelBase;
        this.staminaLevelMin = staminaLevelMin;
        this.staminaLevelMax = staminaLevelMax; // Default max stamina level
    }

    public void SetStaminaLevelCurrent(
        int staminaLevelCurrent
    ) {
        this.staminaLevelCurrent = staminaLevelCurrent; // Default current stamina level
    }

    // Speed Functions
    public void SetSpeedLevels(
        int speedLevelBase,
        int speedLevelMin = 0,
        int speedLevelMax = 100
        // int speedLevelCurrent
    ) {
        this.speedLevelBase = speedLevelBase;
        this.speedLevelMin = speedLevelMin;
        this.speedLevelMax = speedLevelMax; // Default max speed level
        // this.speedLevelCurrent = speedLevelCurrent ?? speedLevelBase;
    }

    // Sweeper State Functions
    public void SetIsSweepingState(
        bool isSweeping = false
    ) {
        this.isSweeping = isSweeping; // Default sweeping state
    }
    
    public void SetIsExhausted(
        bool isExhausted = false
    ) {
        this.isExhausted = isExhausted; // Default exhausted state
    }

    public void SetHasAdditionalBenefits(
        bool hasAdditionalBenefits = false
    ) {
        this.hasAdditionalBenefits = hasAdditionalBenefits; // Default additional benefits state
    }
}













// public struct CurlingThrower {
//     //Variable declaration
//     public string name;
//     public string characterId;
//     public GameObject characterBody;
//     public CurlingPlayerStats stats;
   
    
//     public void SetThrowerInfo(

//     ) {
        
//     }
// }

// public struct CurlingSweeper {
//     //Variable declaration
//     public string name;
//     public string characterId;
//     public GameObject characterBody;
//     public CurlingPlayerStats stats;
   
    
//     public void SetSweeperInfo(

//     ) {
        
//     }
// }

