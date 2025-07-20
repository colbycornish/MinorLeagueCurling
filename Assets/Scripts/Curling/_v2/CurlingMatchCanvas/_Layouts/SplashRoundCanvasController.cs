

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashRoundCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public GameObject content;
    public GameObject shadow;
    public GameObject turnDisplayText;


    public void Start()
    {
        // LoadExistingSettings();
    }

    public void OnEnable() {
        // This method is called when the canvas is enabled
        // Debug.Log("Splash Round Canvas Enabled");
        UpdateTurnDisplay();
        ShowSplashRound();
    }
    public void OnDisable() {
        // This method is called when the canvas is disabled
        // Debug.Log("Splash Round Canvas Disabled");
        HideSplashRound();
    }
    
    public void UpdateTurnDisplay()
    {
        
        int turnCount = CurlingGameManagerV2.Instance.turnCount;
        string turnDisplay = "Turn " + (turnCount + 1);

        turnDisplayText.GetComponent<TMPro.TextMeshProUGUI>().text = turnDisplay;
        
    }

    
    public void ShowSplashRound()
    {
        // Display the splash round on the canvas
        content.SetActive(true);
        shadow.SetActive(true);
        Debug.Log("Splash Round Displayed");
    }
    public void HideSplashRound() {
        // Hide the splash round from the canvas
        content.SetActive(false);
        shadow.SetActive(false);
        Debug.Log("Splash Round Hidden");
    }

}
