using UnityEngine;
using UnityEngine.UI;

public class AutoScrollToItem : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform contentTransform; // The parent of all items

    public float scrollSpeed = 0.5f; // Speed in normalized units per second
    private float targetNormalizedPositionX = 1.0f; // Target position (e.g., end of content)

    public float targetNormalizedPosition = 1.0f;
    public float smoothTime = 0.3f; // Approximate time to reach the target
    private float velocity = 0.0f; // Reference velocity for SmoothDamp, initialized to zero

    void Update()
    {
        float newPosition = Mathf.SmoothDamp(scrollRect.verticalNormalizedPosition, targetNormalizedPosition, ref velocity, smoothTime);
        scrollRect.verticalNormalizedPosition = newPosition;
    }

    // void Update()
    // {
    //     // Calculate the new normalized position using MoveTowards
    //     float newX = Mathf.MoveTowards(scrollRect.horizontalNormalizedPosition, targetNormalizedPositionX, scrollSpeed * Time.deltaTime);

    //     // Assign the new position to the ScrollRect
    //     scrollRect.horizontalNormalizedPosition = newX;

    //     // Optional: Loop the scrolling or stop when the target is reached
    //     if (Mathf.Approximately(scrollRect.horizontalNormalizedPosition, targetNormalizedPositionX))
    //     {
    //         // Example: Reset to the start or reverse direction
    //         // targetNormalizedPositionX = (targetNormalizedPositionX == 1.0f) ? 0.0f : 1.0f;
    //     }
    // }

    public void ScrollToItem(int itemIndex)
    {
        int itemCount = contentTransform.childCount;

        if (itemCount == 0 || itemIndex < 0 || itemIndex >= itemCount)
            return;

        // Calculate the normalized position for the target index
        // 1f - (float)itemIndex / (itemCount - 1) is for vertical scrolling, top (1) to bottom (0)
        float normalizedPosition = 1f - (float)itemIndex / (itemCount - 1);
        
        // Set the vertical position
        scrollRect.horizontalNormalizedPosition = normalizedPosition;
        // For horizontal, you would set scrollRect.horizontalNormalizedPosition
    }

    

    
}