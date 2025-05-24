using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SweeperStats {
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public bool isSweeping;
    public string name;

    // public float power;
    // public float speed;
    // public float stamina;

    // Exhaustion Levels
    public bool isExhausted; // The current state of the Sweeper
    public float maxExhaustionLevel; // The maximum exhaustion level
    public float minExhaustionLevel;  // The minimum exhaustion level
    public float exhaustionRate; // The rate at which the exhaustion level increases
    public float recoveryRate; // The rate at which the exhaustion level decreases (mathed by time)
    public float coolDownThreshold;
    //Constructor (not necessary, but helpful)
    public SweeperStats(
        bool isSweeping = false, 
        string name = "default name",
        bool isExhausted = false,
        float maxExhaustionLevel = 100.0f,
        float minExhaustionLevel = 0.0f,
        float exhaustionRate = 6.0f,
        float recoveryRate = 0.05f,
        float coolDownThreshold = 50.0f
    ) {
        this.isSweeping = isSweeping;
        this.name = name;
        this.isExhausted = isExhausted;
        this.maxExhaustionLevel = maxExhaustionLevel;
        this.minExhaustionLevel = minExhaustionLevel;
        this.exhaustionRate = exhaustionRate;
        this.recoveryRate = recoveryRate;
        this.coolDownThreshold = coolDownThreshold;
    }
}