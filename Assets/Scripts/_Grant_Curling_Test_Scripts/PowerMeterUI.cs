using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Governs the UI power meter used to launch a curling stone.
/// Much like a madden kickoff, the user will select the power level, 
/// which can then be referenced elsewhere. 
/// </summary>


public class PowerMeterUI : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform meterBackground;

    public float speed = 300f;
    private bool goingRight = true;
    public bool isActive = false;

    private float currentPower = 0f;
    private bool powerSelected = false;

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
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

        if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            ResetMeter();
        }
        if (currentPhase == CurlingMatchPhase.CurlingPowerMeterPhase)
        {
            Activate();
        }
    }

    void Update()
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
        if (currentPhase == CurlingMatchPhase.CurlingPowerMeterPhase)
        {
            if (!isActive || powerSelected) return;
            MoveCursor();
        }
    }

    public void MoveCursor()
    {
        float maxX = meterBackground.rect.width / 2f;
        float minX = -maxX;
        float move = speed * Time.deltaTime * (goingRight ? 1 : -1);

        cursor.anchoredPosition += new Vector2(move, 0);

        if (cursor.anchoredPosition.x > maxX)
        {
            cursor.anchoredPosition = new Vector2(maxX, cursor.anchoredPosition.y);
            goingRight = false;
        }
        else if (cursor.anchoredPosition.x < minX)
        {
            cursor.anchoredPosition = new Vector2(minX, cursor.anchoredPosition.y);
            goingRight = true;
        }
    }

    public void Activate()
    {
        isActive = true;
        goingRight = true;
        powerSelected = false;
        cursor.anchoredPosition = Vector2.zero;
    }

    public void SelectPower()
    {
        if (!isActive || powerSelected) return;

        float x = cursor.anchoredPosition.x;

        if (x < -150f || x > 150f)
            currentPower = 0.6f;
        else if ((x >= -150f && x < -75f) || (x > 75f && x <= 150f))
            currentPower = 0.8f;
        else
            currentPower = 1f;

        powerSelected = true;
        isActive = false;
    }



    // 🔧 Added methods to match what other scripts are expecting

    public bool IsPowerSelected()
    {
        return powerSelected;
    }

    public float GetPower()
    {
        return currentPower;
    }

    public void ResetMeter()
    {
        isActive = false;
        powerSelected = false;
        currentPower = 0f;
        cursor.anchoredPosition = Vector2.zero;
    }
}