using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeAndMove : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;

    public Vector2 targetPosition;
    public Vector2 startOffset = new Vector2(0, -100); // Example: start 100 units below target
    public float animationDuration = 1f;

    [Header("In: Direction")]
    public bool fromRight = false;
    public bool fromLeft = false;
    public bool fromTop = false;
    public bool fromBottom = false;

    [Header("Out: Direction")]
    public bool toRight = false;
    public bool toLeft = false;
    public bool toTop = false;
    public bool toBottom = false;

    void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // Set initial state
        canvasGroup.alpha = 0;
        rectTransform.anchoredPosition = targetPosition + startOffset;
    }

    public void StartFadeInFromDirection()
    {
        StartCoroutine(FadeInFromDirection(1f, targetPosition, animationDuration));
    }

    private IEnumerator FadeInFromDirection(float targetAlpha, Vector2 finalPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector2 initialPosition = rectTransform.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            canvasGroup.alpha = Mathf.Lerp(0f, targetAlpha, t);
            rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, finalPosition, t);

            yield return null;
        }

        // Ensure final state is set
        canvasGroup.alpha = targetAlpha;
        rectTransform.anchoredPosition = finalPosition;
    }
}