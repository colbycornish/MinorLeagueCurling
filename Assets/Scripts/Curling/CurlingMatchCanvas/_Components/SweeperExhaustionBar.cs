

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SweeperExhaustionBar : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public bool controlsLeftSweeper = false;
    public bool controlsRightSweeper = false;

    public GameObject warningIcon;
    // TODO: This uses the preferred height element to set a height, but this isn't really
    // sustainable for different screen sizes. 
    public GameObject displayBar;
    private LayoutElement displayBarLayoutElement;
    private float displaybarMaxHeight = 100.0f;
    private float displaybarMinHeight = 0.0f;
    private float displaybarCurHeight = 100.0f;
    private float displaybarIncreaseRate = 6.0f;
    private float displaybarDecreaseRate = 0.05f; // (mathed by time)
    
    // private bool isSweeping = false; // The current state of the Sweeper
    private float exhaustionLevel = 0.0f; // The current exhaustion level
    private float exhaustionWarningThreshold = 75.0f; // The threshold at which the Sweeper will start to show warning signs

    // these variables can be adjusted on a per-character basis.
    // private bool isExhausted = false; // The current state of the Sweeper
    private float maxExhaustionLevel = 100.0f; // The maximum exhaustion level
    private float minExhaustionLevel = 0.0f;  // The minimum exhaustion level
    private float exhaustionRate = 6.0f; // The rate at which the exhaustion level increases
    private float recoveryRate = 0.05f; // The rate at which the exhaustion level decreases (mathed by time)
    private float exhaustionThreshold = 50.0f; // The threshold at which the Sweeper becomes exhausted
    public KeyCode rightSweepKey = KeyCode.L;
    public KeyCode leftSweepKey = KeyCode.K;

    // Display Helpers
    private bool isWarningVisible = false; // The current state of the Sweeper

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start(){
        errorCheck();
        initializeDisplay();
        setWarning(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(rightSweepKey) & controlsRightSweeper){
            increaseExhaustionLevel();
        }
        if (Input.GetKeyDown(leftSweepKey) & controlsLeftSweeper){
            increaseExhaustionLevel();
        }

        decreaseExhaustionLevel();
        
    }

    /// <summary>
    /// Sets or resets variables and meters. 
    /// </summary>
    public void setExhaustionLevelsFromCharacter(
        int exhaustionLevel, 
        int maxExhaustionLevel, 
        int minExhaustionLevel,
        int exhaustionRate,
        int recoveryRate,
        int exhaustionThreshold
    ){
        this.exhaustionLevel = exhaustionLevel;
        this.maxExhaustionLevel = maxExhaustionLevel;
        this.minExhaustionLevel = minExhaustionLevel;
        this.exhaustionRate = exhaustionRate;
        this.recoveryRate = recoveryRate;
        this.exhaustionThreshold = exhaustionThreshold;

        // Set the exhaustion warning threshold
        // The warning threshold is set to 75% of the range between the min and max exhaustion levels
        // This means that the Sweeper will start to show warning signs when they are at 75% of their maximum exhaustion level
        int exhaustionRange = maxExhaustionLevel - minExhaustionLevel;
        this.exhaustionWarningThreshold = (exhaustionRange * 0.75f) + minExhaustionLevel;
    }

    public void reset(){

    }

    public void initializeDisplay(){
        displayBarLayoutElement = displayBar.GetComponentsInChildren<LayoutElement>()[0];
        displaybarMaxHeight = displayBarLayoutElement.preferredHeight;
        displaybarCurHeight = 0.0f;
        displayBarLayoutElement.preferredHeight = 0.0f;
        displaybarIncreaseRate = (displaybarMaxHeight / 100.0f) * exhaustionRate;
        displaybarDecreaseRate = (displaybarMaxHeight / 100.0f) * recoveryRate;
    }

    /// <summary>
    /// High level functions to change the exhaustion level of the Sweeper
    /// This includes helper functions that manipulate the display
    /// </summary>

    public void increaseExhaustionLevel(){
        // Increase the exhaustion level
        if (exhaustionLevel < maxExhaustionLevel){
            exhaustionLevel += exhaustionRate;
            increaseExhaustionDisplay();
            if (exhaustionLevel > maxExhaustionLevel){
                exhaustionLevel = maxExhaustionLevel;
            }
            
        }
        else{
            exhaustionLevel = maxExhaustionLevel;
        }

        // Show warning signs
        if (exhaustionLevel >= exhaustionWarningThreshold){
            setWarning(true);
        }
    }

    // TODO: This uses the preferred height element to set a height, but this isn't really
    // sustainable for different screen sizes. 
    private void increaseExhaustionDisplay(){
        // Increase the height of the Sweeper Exhaustion Bar
        displayBarLayoutElement.preferredHeight = displaybarCurHeight + displaybarIncreaseRate;
        if (displayBarLayoutElement.preferredHeight > displaybarMaxHeight){
            displayBarLayoutElement.preferredHeight = displaybarMaxHeight;
        }
        displaybarCurHeight = displayBarLayoutElement.preferredHeight;

    }

    public void decreaseExhaustionLevel(){
        // Debug.Log("decreasing Exhaustion!");
        // Decrease the exhaustion level
        if (exhaustionLevel > minExhaustionLevel){
            exhaustionLevel -= recoveryRate;
            decreaseExhaustionDisplay();
            if (exhaustionLevel < 0.0f){
                exhaustionLevel = 0.0f;
            }
        }
        else{
            exhaustionLevel = minExhaustionLevel;
        }
        // Hide warning signs
        // This could be a visual effect, sound effect, or some other indication that the Sweeper is recovering
        if (exhaustionLevel < exhaustionWarningThreshold){
            setWarning(false);
        }
    }

    private void decreaseExhaustionDisplay(){
        // Decrease the height of the Sweeper Exhaustion Bar
        displayBarLayoutElement.preferredHeight = displaybarCurHeight - displaybarDecreaseRate;
        
        if (displayBarLayoutElement.preferredHeight < displaybarMinHeight){
            displayBarLayoutElement.preferredHeight = displaybarMinHeight;
        }
        displaybarCurHeight = displayBarLayoutElement.preferredHeight;

    }

    public void setWarning(bool wl){
        if (isWarningVisible != wl){
            isWarningVisible = wl;
            warningIcon.SetActive(wl);
        }
    }

    /// <summary>
    /// Transitional helpers for showing and hiding the Sweeper Exhaustion Bar
    /// </summary>
    public void transitionIn(){
        // Transition in the Sweeper Exhaustion Bar
        // This will be called when the player is in the Sweeping phase
    }
    public void transitionOut(){
        // Transition out the Sweeper Exhaustion Bar
        // This will be called when the player is in the Sweeping phase
    }
    public void updateBar(){
        // Update the Sweeper Exhaustion Bar
        // This will be called when the player is in the Sweeping phase
    }




    
    /// <summary>
    /// Error Checking and Debugging
    /// </summary>

    public void errorCheck(){
        if (controlsLeftSweeper == false && controlsRightSweeper == false){
            Debug.Log("Error: No Sweeper Controls Assigned");
        }
        else if (controlsLeftSweeper == true && controlsRightSweeper == true){
            Debug.Log("Error: Both Sweeper Controls Assigned to One button");
        }
        else{
            Debug.Log("Sweeper Bar Staus: ✅");
        }

        if (displayBar == null){
            Debug.Log("Error: Sweeper Bar Display is not assigned");
        }
        
    }

}
