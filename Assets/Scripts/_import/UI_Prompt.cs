
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI prompt that tells the player when they can interact with a doorway.
/// </summary>
public class UI_Prompt : MonoBehaviour
{
    public static UI_Prompt Instance;
    public GameObject promptUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowPrompt(bool show)
    {
        promptUI.SetActive(show);
    }
}
