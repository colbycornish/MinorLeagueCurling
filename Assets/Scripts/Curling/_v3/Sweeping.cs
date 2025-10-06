using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class Sweeping : MonoBehaviour
    {
        [Header("Launch Settings")]
        public float sweepStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

        [Header("Sweeper Settings")]
        public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
        public float sweepDecayRate = 2f;
        public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl
        private float sweepBoostFactor = 0f; // ** NEW SWEEPER CODE **

        // Sweeper State ** NEW SWEEPER CODE **
        private bool isSweepingLeft = false; // ** NEW SWEEPER CODE **
        private bool isSweepingRight = false; // ** NEW SWEEPER CODE **

        [Header("Input Keys")]
        public KeyCode resetKey = KeyCode.R;
        public KeyCode rightSweeperKey = KeyCode.RightShift; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
        public KeyCode leftSweeperKey = KeyCode.LeftShift; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **


        [Header("Sweeper Bars")]
        [SerializeField] public SweeperExhaustionBarV2 leftSweeperExhaustionBar;
        [SerializeField] public SweeperExhaustionBarV2 rightSweeperExhaustionBar;
        
        /// <summary>
        /// Setup
        /// </summary>
        public void Setup()
        {           
            GameObject lsEB = CurlingManagersV3.CurlingManager._instance.leftSweeperExhaustionBar;
            GameObject rsEB = CurlingManagersV3.CurlingManager._instance.rightSweeperExhaustionBar;

            leftSweeperExhaustionBar = lsEB.GetComponent<SweeperExhaustionBarV2>();
            rightSweeperExhaustionBar = rsEB.GetComponent<SweeperExhaustionBarV2>();
        }

        /// <summary>
        /// Listen for curling phase changes
        /// </summary>

       

        /// <summary>
        /// Update!
        /// </summary>
        void Update()
        {
            CurlingManagersV3.CurlingMatchPhase currentPhase = CurlingManagersV3.MatchPhaseManager._instance.currentPhase;
            if (currentPhase == CurlingManagersV3.CurlingMatchPhase.CurlingStoneSweepingPhase)
            {
                
                // Track sweeping input ** NEW SWEEPER CODE **
                // this does not work. It's unclear why. 
                // this.isSweepingLeft = Input.GetKey(leftSweeperKey); // ** NEW SWEEPER CODE **
                // this.isSweepingRight = Input.GetKey(rightSweeperKey); // ** NEW SWEEPER CODE **

                // if (Input.GetKey(leftSweeperKey)){
                //     Debug.Log("left press");
                // }
                // if (Input.GetKey(rightSweeperKey)){
                //     Debug.Log("right press");
                // }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isSweepingLeft = true;
                    leftSweeperExhaustionBar.IncreaseExhaustionLevel();
                }
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    isSweepingRight = true;
                    rightSweeperExhaustionBar.IncreaseExhaustionLevel();
                }
                if (Input.GetKeyUp(KeyCode.LeftShift)){
                    isSweepingLeft = false;
                }
                if (Input.GetKeyUp(KeyCode.RightShift)){
                    isSweepingRight = false;
                }
                return;
            }
        }

        void FixedUpdate()
        {
            
            CurlingStone currentStone = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone;
            if (currentStone == null || currentStone.rb == null) return;
            // Debug.Log("Sweeping Analysis: Fixed Update");

            CurlingManagersV3.CurlingMatchPhase currentPhase = CurlingManagersV3.MatchPhaseManager._instance.currentPhase;
            if (currentPhase != CurlingManagersV3.CurlingMatchPhase.CurlingStoneSweepingPhase)
            { 
                return;
            }

            // TODO: Move to the match flow
            if (
                currentStone != null && 
                currentStone.isSliding && 
                currentStone.rb != null
                // The following line used to be in immediately after != null,
                // and I think it was throwing things off: 
                // && Mathf.Abs(curlAmount) > 0.01f
            ) 
            {
                
                // todo: move to match flow
                if (
                    currentStone.rb != null && 
                    currentStone.rb.linearVelocity.sqrMagnitude < 0.01f && 
                    currentStone.rb.angularVelocity.sqrMagnitude < 0.01f
                )
                {
                    currentStone.isSliding = false;
                    // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
                }

                
                ApplySpinForceToStone(stone: currentStone);

                // ** Apply sweeping boost ** NEW SWEEPER CODE
                
                /// TODO: both of these are currently false
                bool sweeping = isSweepingLeft || isSweepingRight;
                if (sweeping)
                {
                    Debug.Log("p3 - ASITS");
                    ApplySweepingImpactToStone(stone: currentStone);
                }

                // Debug lines to track that sweeping is working correctly
                if (isSweepingLeft) Debug.Log("🧹 Sweeping LEFT (K key)");
                if (isSweepingRight) Debug.Log("🧹 Sweeping RIGHT (L key)");
            }
        }

        /// <summary>
        /// Apply Spin
        /// </summary>
        public void ApplySpinForceToStone(CurlingStone stone)
        {
            // Debug.Log("Applying Spin Force to Stone");
            CurlingStone currentStone = stone;
            if (currentStone == null){
                currentStone = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone;
            }
            if (currentStone == null || currentStone.rb == null) return;
            // Fire the stone
            currentStone.ApplySpinForceToStone(
                spinAmount: curlAmount, // curlAmount
                sweepStrength: sweepStrength
            );
            
            // 🧪 Debug: draw movement and curl direction
            // Debug.DrawRay(currentStone.position, forward * 2f, Color.green);  // forward
            // Debug.DrawRay(currentStone.position, side * 2f, Color.red);       // curl direction
            // Debug.Log("Drawing curl debug rays!"); // FLAG: THIS IS NOT TRIGGERING SO CLEARLY SOMETHING IS WRONG
        }

        /// <summary>
        /// Sweeping Impact
        /// </summary>

        public void ApplySweepingImpactToStone(CurlingStone stone)
        {
            Debug.Log("....Attempting to apply impact");
            CurlingStone currentStone = stone;
            if (currentStone == null){
                currentStone = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone;
            }
            if (currentStone == null || currentStone.rb == null) return;
            Debug.Log("Applying Sweeping Impace to Stone");
            
            Rigidbody rb = currentStone.rb; // currentStone
            Vector3 forward = rb.linearVelocity.normalized;
            Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
            sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor + Time.fixedDeltaTime * sweepDecayRate);

            currentStone.ApplySweepingImpactToStone(
                sweepStrength: sweepStrength,
                sweepBoostFactor: sweepBoostFactor,
                sweepBoostAmount: sweepBoostAmount,
                sweepDecayRate: sweepDecayRate,
                isSweepingLeft: isSweepingLeft,
                isSweepingRight: isSweepingRight
            );

            if (isSweepingLeft && isSweepingRight)
            {
                sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor - Time.fixedDeltaTime * sweepDecayRate);
            }
        }

        ///
        /// 
        /// 
        
        public void ResetSweeperExhaustionBars()
        {
            /// leftSweeperBar
            leftSweeperExhaustionBar.SetExhaustionLevelsFromCharacter(
                exhaustionLevel: 0.0f, 
                maxExhaustionLevel: 1.0f, 
                minExhaustionLevel: 0.0f,
                exhaustionRate: 0.06f,
                recoveryRate: 0.04f,
                exhaustionThreshold: 0.85f
            );
            leftSweeperExhaustionBar.InitializeDisplay();
            
            /// rightSweeperBar
            rightSweeperExhaustionBar.SetExhaustionLevelsFromCharacter(
                exhaustionLevel: 0.0f, 
                maxExhaustionLevel: 1.0f, 
                minExhaustionLevel: 0.0f,
                exhaustionRate: 0.06f,
                recoveryRate: 0.04f,
                exhaustionThreshold: 0.85f
            );
            rightSweeperExhaustionBar.InitializeDisplay();

            // /// Power
            // powerMeter.UpdateCurrentPower(
            //     current: 0.8f
            // );

            // /// scoreBug
            // scoreBug.UpdateTeamInfo(
            //     homeTeamName: "Blue Broom Brushers",
            //     awayTeamName: "Purple Stone Throwers"
            // );
            // scoreBug.SetTotalNumberOfStonesPerTeam(totalNumberOfStonesPerTeam: 5);
            // scoreBug.UpdateScore(
            //     homeTeamScore: 0,
            //     awayTeamScore: 0
            // );
            // scoreBug.UpdateStoneAvailability(
            //     numHomeTeamStonesAvailable: 5,
            //     numAwayTeamStonesAvailable: 5
            // );
        }
    
        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            curlAmount = 0f;
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
        //     if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        //     {
        //         Reset();
        //     }
        //     if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase){
        //         // hasLaunched = true;
        //         // isSliding = true;
        //     }
        // }