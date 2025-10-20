using UnityEngine;
using System.Collections;

public class ShrinkUIWidth : MonoBehaviour
{
    public float targetWidth = 50f; // The final width to shrink to
    public float shrinkDuration = 2f; // Time in seconds to complete shrinkage

    private RectTransform rectTransform;
    private float initialWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialWidth = rectTransform.sizeDelta.x;
        StartCoroutine(ShrinkWidthOverTime());
    }

    IEnumerator ShrinkWidthOverTime()
    {
        float timer = 0f;
        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float newWidth = Mathf.Lerp(initialWidth, targetWidth, timer / shrinkDuration);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            yield return null; // Wait for the next frame
        }
        // Ensure the width is exactly the targetWidth at the end
        rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
    }
}