using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    [Serializable]
    public class Aiming : MonoBehaviour
    {
        [Header("Aim Settings")]
        [SerializeField]
        public float rotationSpeed = 100f;

        /// <summary>
        /// Setup / Reset
        /// </summary>
        public void Enable()
        {
            if (CurlingManager._instance.Parameters.Course.directionPivotObject == null) return;
            CurlingManager._instance.Parameters.Course.directionPivotObject.SetActive(true);
            ThrowDirectionIndicator tdi = CurlingManager._instance.Parameters.Course.directionPivotObject.GetComponent<ThrowDirectionIndicator>();
        }

        public void Disable()
        {
            if (CurlingManager._instance.Parameters.Course.directionPivotObject == null) return;
            CurlingManager._instance.Parameters.Course.directionPivotObject.SetActive(true);
            ThrowDirectionIndicator tdi = CurlingManager._instance.Parameters.Course.directionPivotObject.GetComponent<ThrowDirectionIndicator>();
            tdi.DeactivateSpin();
        }

        public void Reset()
        {

            if (
                CurlingManager._instance.Parameters.Course.directionPivot != null &&
                CurlingManager._instance.Parameters.Course.directionPivotObject != null
            )
            {
                CurlingManager._instance.Parameters.Course.directionPivotObject.transform.rotation =  Quaternion.identity;//Quaternion.Euler(0, 0, 0);
                CurlingManager._instance.Parameters.Course.directionPivot.rotation = Quaternion.identity; //Quaternion.Euler(0, 0, 0);
            }

            CurlingManager._instance.Parameters.Aiming.CurlAmountInitial = 0f;
        }

        /// <summary>
        /// Curl Amount Adjustments (Spin Amount): This governs how much initial spin is applied to the stone
        /// </summary>


        public void IncreaseRightCurlAmount(){
            CurlingManager._instance.Parameters.Aiming.CurlAmountInitial = 
                Mathf.Clamp(
                    CurlingManager._instance.Parameters.Aiming.CurlAmountInitial + 1f, 
                    -1f, 
                    1f
                ); 

            ApplyIntialCurlAmount(
                stone: CurlingManager._instance.Parameters.Stones.currentStone, //CurlingManager._instance.stoneManager.currentStone,
                amount: CurlingManager._instance.Parameters.Aiming.CurlAmountInitial //curlAmountInitial
            );
        }

        public void IncreaseLeftCurlAmount(){
            CurlingManager._instance.Parameters.Aiming.CurlAmountInitial = 
                Mathf.Clamp(
                    CurlingManager._instance.Parameters.Aiming.CurlAmountInitial - 1f, 
                    -1f, 
                    1f
                ); 

            ApplyIntialCurlAmount(
                stone: CurlingManager._instance.Parameters.Stones.currentStone, //CurlingManager._instance.stoneManager.currentStone,
                amount: CurlingManager._instance.Parameters.Aiming.CurlAmountInitial //curlAmountInitial
            );
        }

        public void SetCurlAmountToZero(){
            CurlingManager._instance.Parameters.Aiming.CurlAmountInitial = 0f;
        }

        /// <summary>
        /// Launch Direction: Change the direction in which the stone is initially thrown
        /// </summary>

        public void ChangeDirection(float input = 0f)
        {
            // Get the rotation direction from the input
            // A/D or Left/Right arrows
            if (input != 0)
            {
                float rotationAmount = 
                    input * 
                    rotationSpeed * 
                    Time.deltaTime;

                bool canRotate = CanUpdateDirection(rotationAmount);
                if (canRotate)
                {
                    CurlingManager._instance.Parameters.Course.directionPivot.Rotate(0f, rotationAmount, 0f);
                }
            }
        }

        public bool CanUpdateDirection(float rotationAmount)
        {
            float currentY = CurlingManager._instance.Parameters.Course.directionPivot.eulerAngles.y;
            float nextY = currentY + rotationAmount;

            // Rotation is 360 degrees. The arrow starts at zero. 
            // We're allowing for 30 degrees to the left, and 30 degrees to the right.
            float leftLimit = 360f - CurlingManager._instance.Parameters.Aiming.DirectionalLimit;
            float rightLimit = 0 + CurlingManager._instance.Parameters.Aiming.DirectionalLimit;

            // Test that the next position is within the range
            // [leftBound....359, 360/0, 1....rightBound]
            return (nextY > leftLimit || nextY < rightLimit);
        }

        /// <summary>
        /// Curl amount (Spin Amount): This governs how much initial spin is applied to the stone
        /// </summary>

        public void ApplyIntialCurlAmount(
            CurlingStone stone,
            float amount
        )
        {

            CurlingStone currentStone = stone;
            if (currentStone == null){
                currentStone = CurlingManager._instance.Parameters.Stones.currentStone;
                //CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone;
            }
            if (currentStone == null || currentStone.rb == null) return;


            CurlingManager._instance.Parameters.Aiming.CurlAmountInitial = amount; // TODO: phase out
            currentStone.curlAmountInitial = amount; // TODO: Phase out
            currentStone.spinSpeedInitial = amount;
            currentStone.spinAmountInitial = amount;

            ThrowDirectionIndicator tdi = CurlingManager._instance.Parameters.Course.directionPivotObject.GetComponent<ThrowDirectionIndicator>();
            tdi.SetCurlAmount(amount);
        }
    }
}



//         [SerializeField]
//         private Character _Character;
//         public Character Character => _Character;

//         /************************************************************************************************************************/

//         public StateMachine<CharacterState> OwnerStateMachine => _Character.StateMachine;

//         /************************************************************************************************************************/

// #if UNITY_EDITOR
//         protected override void OnValidate()
//         {
//             base.OnValidate();
//             gameObject.GetComponentInParentOrChildren(ref _Character);
//         }
// #endif


// public void Setup(CurlingCourseData courseData)
        // {
        //     CurlingManager._instance.Parameters.Course.directionPivotObject = courseData.directionalPivot;
        //     CurlingManager._instance.Parameters.Course.directionPivot = courseData.directionalPivot.transform;
        // }