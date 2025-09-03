using UnityEngine;
using System.Collections;

public class UIScaler : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
    public float scaleDuration = 2f;

    void Start()
    {
        StartCoroutine(ScaleOverTime(targetScale, scaleDuration));
    }

    IEnumerator ScaleOverTime(Vector3 target, float duration)
    {
        Vector3 initialScale = transform.localScale;
        float timer = 0f;

        while (timer < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, target, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = target; // Ensure final scale is exact
    }
}