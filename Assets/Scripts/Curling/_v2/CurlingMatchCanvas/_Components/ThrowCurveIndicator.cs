using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Governs the UI power meter used to launch a curling stone.
/// Much like a madden kickoff, the user will select the power level, 
/// which can then be referenced elsewhere. 
/// </summary>


public class ThrowCurveIndicator : MonoBehaviour
{
    public GameObject SpinContainer;
    public GameObject ArrowLeft;
    public GameObject ArrowLeft2;
    public GameObject ArrowRight;
    public GameObject ArrowRight2;

    public float rotationSpeed = 300f;
    public float rotationDirection = 1f;
    public bool isActive = true;


    public void Hide()
    {
        SpinContainer.SetActive(false);
        isActive = false;
    }
    public void Show()
    {
        SpinContainer.SetActive(true);
        isActive = true;
    }

    public void Update()
    {
        if (isActive == true)
        {
            // Spin();
        }
    }

    public void SetRotationDirection(float direction)
    {
        rotationDirection = direction;
    }

    
    


}