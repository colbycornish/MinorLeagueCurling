using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Governs the UI power meter used to launch a curling stone.
/// Much like a madden kickoff, the user will select the power level, 
/// which can then be referenced elsewhere. 
/// </summary>


public class ThrowDirectionIndicator : MonoBehaviour
{
    public GameObject arrowContainer;
    public Transform pivotPoint;
    public GameObject curveIndicator;
    public float rotationSpeed = 300f;
    public float rotationDirection = 1f;
    // private bool isCurveActive = false;
    private bool isActive = false;


    private void OnEnable()
    {
        SetCurlAmount(0);
        isActive = false;
    }

    private void OnDisable()
    {
        SetCurlAmount(0);
        isActive = false;
    }

    public void ActivateSpin()
    {
        isActive = true;
    }

    public void DeactivateSpin()
    {
        isActive = false;
    }

    void Update()
    {

        if (isActive == true)
        {
            // if (CurlingGameManagerV2.Instance != null){
            //     SetCurlAmount(
            //         CurlingGameManagerV2.Instance.aimController.curlAmountInitial
            //     );
            //     Spin();
            // }
            if (CurlingManagersV3.CurlingManager._instance != null){
                SetCurlAmount(
                    CurlingManagersV3.CurlingManager._instance.Parameters.Aiming.CurlAmountInitial
                );
                Spin();
            }
            // Debug.Log("Setting Curl amount");
            
        }

    }


    public void SetCurlAmount(float amount)
    {
        ThrowCurveIndicator tci = curveIndicator.GetComponent<ThrowCurveIndicator>();
        tci.SetRotationDirection(amount);
        rotationDirection = amount;
        if (amount == 0f)
        {
            tci.Hide();
            DeactivateSpin();
        }
        if (amount < 0f)
        {
            tci.Show();
            tci.CurlLeft();
            ActivateSpin();
            
        }
        if (amount > 0f)
        {
            tci.Show();
            tci.CurlRight();
            ActivateSpin();
        }
    }
    
    public void Spin()
    {
        curveIndicator.transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed * rotationDirection, 0));
    }

}