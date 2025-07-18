using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Governs the UI power meter used to launch a curling stone.
/// Much like a madden kickoff, the user will select the power level, 
/// which can then be referenced elsewhere. 
/// </summary>


public class ThrowDirectionIndicator : MonoBehaviour
{
    public GameObject ArrowContainer;
    public Transform pivotPoint;
    public GameObject CurveIndicator;
    private bool isCurveActive = false;

    private void OnEnable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged += HandlePhase;
    }

    private void OnDisable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged -= HandlePhase;
    }


    public void HandlePhase(CurlingMatchPhase phase)
    {
        if (phase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            // Set Active
        }
        else if (phase == CurlingMatchPhase.CurlingPowerMeterPhase)
        {
            // do nothing
        }
        else
        {
            // Disable
        }

    }


    public void SetCurlAmount(int amount)
    {
        
    }

}