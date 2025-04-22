
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Handles fade in/out transitions between scenes using CanvasGroup.
/// </summary>
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

    /// <summary>
    /// Fades out to black.
    /// </summary>
    public IEnumerator FadeOut()
    {
        yield return Fade(0, 1);
    }

    /// <summary>
    /// Fades in from black.
    /// </summary>
    public IEnumerator FadeIn()
    {
        yield return Fade(1, 0);
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
