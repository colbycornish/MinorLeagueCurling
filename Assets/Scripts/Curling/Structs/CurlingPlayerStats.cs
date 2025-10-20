using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public struct CurlingPlayerStats
{

    [Header("Exhaustion Stats")]
    public CurlingPlayerStat exhaustionRate;

    [Header("Cooldown Stats")]
    public CurlingPlayerStat cooldownRate; // How quickly the player can recover from exhaustion

    [Header("Strength Stats")]
    public CurlingPlayerStat strength;
    
    [Header("Stamina Stats")]
    public CurlingPlayerStat stamina;

    [Header("Speed Stats")]
    public CurlingPlayerStat speed;

    [Header("Sweeper State")]
    public bool isSweeping;
    public bool isExhausted;
    public bool hasAdditionalBenefits;

    // Sweeper State Functions
    public void SetIsSweepingState(bool isSweeping = false)
    {
        this.isSweeping = isSweeping; // Default sweeping state
    }

    public void SetIsExhausted(bool isExhausted = false)
    {
        this.isExhausted = isExhausted; // Default exhausted state
    }

    public void SetHasAdditionalBenefits(bool hasAdditionalBenefits = false)
    {
        this.hasAdditionalBenefits = hasAdditionalBenefits; // Default additional benefits state
    }
}


public struct CurlingPlayerStat
{
    public string title;
    public int baseValue;
    public int max;
    public int min;
    public int current;

    public void SetStat(
        int baseValue,
        int min = 0,
        int max = 100,
        string title = null
    )
    {
        this.baseValue = baseValue;
        this.max = max; // Default max value
        this.min = min; // Default min value
        this.current = baseValue; // Default current value
        this.title = title;
    }

    public void SetCurrent(int newCurrent)
    {
        if (newCurrent > max) this.current = max;
        else if (newCurrent < min) this.current = min;
        else this.current = newCurrent;
    }

    public void Reset()
    {
        this.current = baseValue; // Reset current value to base value
    }

}




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
