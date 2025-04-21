using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FadeOutThenIn(System.Action onFadeMidpoint)
    {
        StartCoroutine(FadeSequence(onFadeMidpoint));
    }

    private IEnumerator FadeSequence(System.Action onFadeMidpoint)
    {
        yield return StartCoroutine(Fade(0, 1));  // Fade to black
        onFadeMidpoint?.Invoke();                 // Do teleport during black
        yield return StartCoroutine(Fade(1, 0));  // Fade back in
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = endAlpha;
    }
}