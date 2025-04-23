using UnityEngine;
using UnityEngine.UI;

public class PowerMeterUI : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform meterBackground;

    public float speed = 300f;
    private bool goingRight = true;
    private bool isActive = false;

    private float currentPower = 0f;
    private bool powerSelected = false;

    void Update()
    {
        if (!isActive || powerSelected) return;

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
            currentPower = 0.25f;
        else if ((x >= -150f && x < -75f) || (x > 75f && x <= 150f))
            currentPower = 0.6f;
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