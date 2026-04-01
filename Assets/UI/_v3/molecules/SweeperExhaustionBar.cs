using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.InputSystem;

namespace CurlingUI.v3 {
    public class SweeperExhaustionBar : MonoBehaviour
    {
        [Header("Game Objects")]
        public MMProgressBar ProgressBar;
        public GameObject CharacterAvatar;
        public GameObject WarningIndicator;

        [Header("Settings")]
        public bool controlsLeftSweeper = false;
        public bool controlsRightSweeper = false;


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
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void OnEnable()
        {
            // var actions = new DefaultInputActions();
            // InputSystem_Actions
            
            //.SweepLeft.performed += (context) => IncreaseExhaustionLevel();
            // InputSystem_Actions.CurlingActions.SweepRight.performed += (context) => IncreaseExhaustionLevel();
            // InputSystem.actions += OnAnyButtonPressed;
        }

        

        public void InitializeDisplay(){
            ProgressBar.SetBar01(
                newPercent: 0.0f
            );
        }

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
                if (ProgressBar.BarTarget > 0.99){
                    // Freeze();
                }
                ProgressBar.ChangeCustomPercent(
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

            ProgressBar.ChangeCustomPercent(
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

        public void SetExhaustionLevelsFromCharacterStats(
                float exhaustionLevel,
                float maxExhaustionLevel, 
                float minExhaustionLevel,
                float exhaustionRate,
                float recoveryRate,
                float exhaustionThreshold
        ){
            this.exhaustionLevel = exhaustionLevel;
            this.maxExhaustionValue = maxExhaustionLevel;
            this.minExhaustionValue = minExhaustionLevel;
            this.exhaustionRate = exhaustionRate;
            this.recoveryRate = recoveryRate;
            this.exhaustionThreshold = exhaustionThreshold;
        }


    }
}