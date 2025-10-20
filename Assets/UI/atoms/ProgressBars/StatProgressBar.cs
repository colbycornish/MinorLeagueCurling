using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class StatProgressBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject TopBarFill;
    [SerializeField] private GameObject BottomBarFill;

    [Header("Settings")]
    [SerializeField] private bool useSecondFill = false;
    // [SerializeField] private bool shouldUpdate = false;

    [Header("Stat Values")]
    [SerializeField] private int minValue = 0;
    [SerializeField] private int maxValue = 100;
    [SerializeField] private int currentValue = 0;

    [SerializeField] private int targetValue = 50;

    [Header("Transform Values")]
    [SerializeField] private float rectangleWidth = 200.0f;
    [SerializeField] private float bottomBarTargetWidth = 100.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (currentValue != targetValue)
        {
            Refresh();
        }
    }

    private void Init()
    {
        rectangleWidth = transform.GetComponent<RectTransform>().rect.width;
        TopBarFill.SetActive(useSecondFill);
    }

    public void UpdateValues(
        int statValue,
        int minValue = 0,
        int maxValue = 100
    )
    {
        int sv = statValue;
        // if (targetValue == 25)
        // {
        //     sv = 50;
        // }
        // else if (targetValue == 50)
        // {
        //     sv = 75;
        // }
        // else // targetValue = 75
        // {
        //     sv = 25; //targetValue = 75;
        // }
        float multiplier = (float)sv / (float)maxValue;
        
        this.bottomBarTargetWidth = rectangleWidth * multiplier;
        this.targetValue = sv;
        this.minValue = minValue;
        this.maxValue = maxValue;

        // if (shouldUpdate == true)
        // {
            UpdateUI();
        // }
    }

    public void Refresh()
    {
        StartCoroutine(UpdateTopBarFillSize());
    }
    
    public IEnumerator UpdateTopBarFillSize()
    {
        float timer = 0f;
        float duration = 0.4f;
        float bottomBarTargetRight = bottomBarTargetWidth;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            RectTransform rt = BottomBarFill.GetComponent<RectTransform>();
            float left   =  rt.offsetMin.x;
            float right  = rt.offsetMax.x; // this value is inherantly negative for some reason
            float top    = -rt.offsetMax.y;
            float bottom =  rt.offsetMin.y;

            float newRight = Mathf.Lerp(right, -bottomBarTargetRight, timer / duration);
            rt.offsetMax = new Vector2(newRight, 0);

            currentValue = Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, timer / duration));
            yield return null; // Wait for the next frame
        }
        // Ensure the width is exactly the targetWidth at the end
        BottomBarFill.GetComponent<RectTransform>().offsetMax = new Vector2(-bottomBarTargetRight, 0);
        currentValue = targetValue;

    }

    // Update is called once per frame

}
