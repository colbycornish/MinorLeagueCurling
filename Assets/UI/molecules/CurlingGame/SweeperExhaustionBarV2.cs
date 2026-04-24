

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.Tools;

public class SweeperExhaustionBarV2 : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public GameObject warningIcon;
    public MMProgressBar progressBar;
    public GameObject characterProfile;

    [Header("Settings")]
    public bool controlsLeftSweeper = false;
    public bool controlsRightSweeper = false;

    // [Header("Current Levels")]
    // private float currentValue = 0.0f;

    [Header("Settings/Stats")]
    private float exhaustionLevel = 0.0f; // The current exhaustion level
    private float exhaustionWarningThreshold = 0.85f; // The threshold at which the Sweeper will start to show warning signs

    // these variables can be adjusted on a per-character basis.
    private float maxExhaustionValue = 1f; // The maximum exhaustion level
    private float minExhaustionValue = 0.0f;  // The minimum exhaustion level
    private float exhaustionRate = 0.06f; // The rate at which the exhaustion level increases
    private float recoveryRate = 0.04f; // The rate at which the exhaustion level decreases (mathed by time)
    private float exhaustionThreshold = 0.85f; // The threshold at which the Sweeper becomes exhausted
    
    [Header("Display Helpers")]
    // private bool isExhausted = false; // The current state of the Sweeper
    private bool isWarningVisible = false; // The current state of the Sweeper
    private bool isFrozen = false; // The current state of the Sweeper

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start(){
        ErrorCheck();
        InitializeDisplay();
        warningIcon.SetActive(false);
        // SetWarning(false);
    }

    public void FixedUpdate(){
        UpdateOnFixedTick();
        
    }

    public void UpdateOnFixedTick(){
        DecreaseExhaustionLevel();
        if (isFrozen == true && !ShouldBeFrozen()){
            UnFreeze();
        }
        if (isFrozen == false && ShouldBeFrozen()){
            Freeze();
        }

        if (isWarningVisible == true && !ShouldBeWarned()){
            UnWarn();
        }
        if (isWarningVisible == false && ShouldBeWarned()){
            Warn();
        }
    }



    /// <summary>
    /// Sets or resets variables and meters. 
    /// </summary>
    public void SetExhaustionLevelsFromCharacter(
        float exhaustionLevel, 
        float maxExhaustionLevel,// = 1.0f, 
        float minExhaustionLevel,// = 0.0f,
        float exhaustionRate,// = 0.06f,
        float recoveryRate,// = 0.04f,
        float exhaustionThreshold// = 0.85f,
    ){
        this.exhaustionLevel = exhaustionLevel;
        this.maxExhaustionValue = maxExhaustionLevel;
        this.minExhaustionValue = minExhaustionLevel;
        this.exhaustionRate = exhaustionRate;
        this.recoveryRate = recoveryRate;
        this.exhaustionThreshold = exhaustionThreshold;

        // Set the exhaustion warning threshold
        // The warning threshold is set to 75% of the range between the min and max exhaustion levels
        // This means that the Sweeper will start to show warning signs when they are at 75% of their maximum exhaustion level
        float exhaustionRange = maxExhaustionValue - minExhaustionValue;
        this.exhaustionWarningThreshold = (exhaustionRange * 0.75f) + minExhaustionValue;
    }


    public void InitializeDisplay(){
        progressBar.SetBar01(
            newPercent: 0.0f
        );
    }

    /// <summary>
    /// High level functions to change the exhaustion level of the Sweeper
    /// </summary>

    public void IncreaseExhaustionLevel(){
        // float newExhaustionValue = exhaustionRate + currentValue;
        // if (newExhaustionValue < maxExhaustionValue){
        //     currentValue = newExhaustionValue;
        // }
        // else {
        //     currentValue = maxExhaustionValue;
        // }
        // float newValue = currentValue + 0.1f;
        // newValue = Mathf.Clamp(newValue, 0f, 1f);
        // progressBar.UpdateBar01(newValue);
        // currentValue = newValue;
        if (!isFrozen){
            if (progressBar.BarTarget > 0.99){
                Freeze();
            }
            progressBar.ChangeCustomPercent(
                percentage: exhaustionRate
            );
        }

        // progressBar.UpdateBar01(
        //     normalizedValue: newExhaustionValue/100
        // );

        // // Show warning signs
        // if (exhaustionLevel >= exhaustionWarningThreshold){
        //     SetWarning(true);
        // }
    }


    public void DecreaseExhaustionLevel(){
        // float newExhaustionValue = currentValue - recoveryRate;
        // if (newExhaustionValue > minExhaustionValue){
        //     currentValue = newExhaustionValue;
        // }
        // else {
        //     currentValue = minExhaustionValue;
        // }

        progressBar.ChangeCustomPercent(
            percentage: -0.001f
        );

        // float newValue = currentValue - 0.001f;
        // newValue = Mathf.Clamp(newValue, 0f, 1f);
        // progressBar.UpdateBar01(newValue);
        // currentValue = newValue;

        // progressBar.UpdateBar01(
        //     normalizedValue: newExhaustionValue/100
        // );

        // Hide warning signs
        // This could be a visual effect, sound effect, or some other indication that the Sweeper is recovering
        // if (exhaustionLevel < exhaustionWarningThreshold){
        //     SetWarning(false);
        // }
    }

    public void UpdateBar(){
        // Update the Sweeper Exhaustion Bar
        // This will be called when the player is in the Sweeping phase
    }


   

    /// <summary>
    /// Warn and Freeze
    /// </summary>
    public bool ShouldBeWarned(){
        // if (isWarningVisible == true){
            if (progressBar.BarTarget > exhaustionWarningThreshold){
                return true;
            };
        // }
        return false;
    }

    public void Warn(){
        isWarningVisible = true;
        warningIcon.SetActive(true);
    }

    public void UnWarn(){
        isWarningVisible = false;
        warningIcon.SetActive(false);
    }

    public void SetWarning(bool wl){
        // if (isWarningVisible != wl){
        //     isWarningVisible = wl;
        //     warningIcon.SetActive(wl);
        // }
    }


    public bool ShouldBeFrozen(){
        if (isFrozen == true){
            if (progressBar.BarTarget > exhaustionWarningThreshold){
                return true;
            };
        }
        return false;
    }

    public void Freeze(){
        progressBar.LerpDecreasingDelayedBar = true;
        isFrozen = true;
    }

    public void UnFreeze(){
        progressBar.LerpDecreasingDelayedBar = false;
        isFrozen = false;
        progressBar.ChangeCustomPercent(
            percentage: 0.0f
        );
    }


    /// <summary>
    /// Transitional helpers for showing and hiding the Sweeper Exhaustion Bar
    /// </summary>
    public void TransitionIn(){
        // Transition in the Sweeper Exhaustion Bar
        // This will be called when the player is in the Sweeping phase
    }
    public void TransitionOut(){
        // Transition out the Sweeper Exhaustion Bar
        // This will be called when the player is in the Sweeping phase
    }



    /// <summary>
    /// Error Checking and Debugging
    /// </summary>
    public void Reset()
    {
        InitializeDisplay();
    }

    public void ErrorCheck()
    {
        if (controlsLeftSweeper == false && controlsRightSweeper == false)
        {
            Debug.Log("Error: No Sweeper Controls Assigned");
        }
        else if (controlsLeftSweeper == true && controlsRightSweeper == true)
        {
            Debug.Log("Error: Both Sweeper Controls Assigned to One button");
        }
        else
        {
            Debug.Log("Sweeper Bar Staus: ✅");
        }

        if (progressBar == null)
        {
            Debug.Log("Error: Sweeper Bar Display is not assigned");
        }
    }

}



    // private void OnEnable()
    // {
    //     if (CurlingMatchPhaseManager.Instance == null) return;
    //     CurlingMatchPhaseManager.Instance.OnPhaseChanged += HandlePhase;
    // }

    // private void OnDisable()
    // {
    //     if (CurlingMatchPhaseManager.Instance == null) return;
    //     CurlingMatchPhaseManager.Instance.OnPhaseChanged -= HandlePhase;
    // }

    // public void HandlePhase(CurlingMatchPhase phase)
    // {
    //     CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

    //     if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
    //     {
    //         Reset();
    //     }
    // }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void Update()
    // {
    //     CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
    //     if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
    //     {
            
    //         if (Input.GetKeyDown(rightSweepKey) & controlsRightSweeper)
    //         {
    //             Debug.Log("Exhaustion Bar: Sweep Right");
    //             IncreaseExhaustionLevel();
    //         }
    //         if (Input.GetKeyDown(leftSweepKey) & controlsLeftSweeper)
    //         {
    //             Debug.Log("Exhaustion Bar: Sweep Left");
    //             IncreaseExhaustionLevel();
    //         }
    //         // DecreaseExhaustionLevel();
    //     }
    //     else
    //     {
    //         // DecreaseExhaustionLevel();
    //     }
    // }