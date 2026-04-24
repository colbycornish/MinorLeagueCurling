using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro

public class OptionButton : MonoBehaviour
{
    public TMP_Text optionText; // Reference to the Text UI element
    private string[] options = { 
        "Option A", 
        "Option B", 
        "Option C", 
        "Option D" 
    }; // Your list of options
    
    private int currentIndex = 0;

    void Start()
    {
        UpdateOptionDisplay();
    }

    public void NextOption()
    {
        currentIndex++;
        if (currentIndex >= options.Length)
        {
            currentIndex = 0; // Wrap around to the start
        }
        UpdateOptionDisplay();
    }

    public void PreviousOption()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = options.Length - 1; // Wrap around to the end
        }
        UpdateOptionDisplay();
    }

    void UpdateOptionDisplay()
    {
        if (optionText != null && options.Length > 0)
        {
            optionText.text = options[currentIndex];
        }
    }
}