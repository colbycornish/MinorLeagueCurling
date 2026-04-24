using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToggleIndicatorAndColor : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private TextMeshProUGUI buttonText; // Or TextMeshProUGUI buttonText; for TMP
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.white;

    private bool isIndicatorActive = false;

    void OnDisable()
    {
        // Ensure the indicator is hidden and text color is reset when the button is disabled
        if (indicator != null) indicator.SetActive(false);
        if (buttonText != null) buttonText.color = inactiveColor;
        isIndicatorActive = false;
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        // gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);
        if (isIndicatorActive == false)
        {
            ToggleState();
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (isIndicatorActive == true)
        {
            ToggleState();
        }
        // gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 255);
    }

    // Call this function from the button's OnClick event
    public void ToggleState()
    {
        isIndicatorActive = !isIndicatorActive; // Toggle the state
        // Show/hide the indicator
        if (indicator != null) indicator.SetActive(isIndicatorActive);

        // Change the button text color
        if (isIndicatorActive)
        {
            if (buttonText != null) buttonText.color = activeColor;
        }
        else
        {
            if (buttonText != null) buttonText.color = inactiveColor;
        }
    }
}

// public class ButtonTextColorChangeScript : MonoBehaviour, ISelectHandler, IDeselectHandler {

//     void ISelectHandler.OnSelect(BaseEventData eventData)
//     {
//         gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);

//     }

//     public void OnDeselect(BaseEventData eventData)
//     {
//         gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 255);

//     }
// }