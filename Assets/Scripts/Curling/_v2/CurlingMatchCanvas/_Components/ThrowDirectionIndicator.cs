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
    // private bool isCurveActive = false;
    private bool isActive = false;



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
    //     if (phase == CurlingMatchPhase.CurlingAimControlsPhase)
    //     {
    //         // Set Active
    //         isActive = true;
    //     }
    //     if (phase == CurlingMatchPhase.CurlingStoneSweepingPhase)
    //     {
    //         // Set Active
    //         isActive = false;
    //     }
    // }
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
            Debug.Log("Setting Curl amount");
            SetCurlAmount(CurlingGameManagerV2.Instance.aimController.curlAmountInitial);
            Spin();
        }

    }


    public void SetCurlAmount(float amount)
    {
        ThrowCurveIndicator tci = curveIndicator.GetComponent<ThrowCurveIndicator>();
        if (amount == 0f)
        {
            tci.Hide();
            curveIndicator.SetActive(false);
        }
        if (amount < 0f)
        {
            tci.Show();
        }
        if (amount < 0f)
        {
            tci.Show();
        }
    }
    
    public void Spin()
    {
        // curveIndicator.transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed * rotationDirection, 0));
    }

}