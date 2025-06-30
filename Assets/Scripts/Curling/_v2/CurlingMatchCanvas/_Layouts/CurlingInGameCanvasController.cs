
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Change to Throw Canvas Controller
public class CurlingInGameCanvasController : MonoBehaviour
{
    // General
    public GameObject content;
    public GameObject shadow;

    // Top Content
    public GameObject teamDisplay001;
    public GameObject teamDisplay002;
    
    // Center Content
    // public GameObject contentAim;
    public GameObject contentCurling;
    public GameObject exhaustionBarLeft;
    public GameObject exhaustionBarRight;
    public GameObject powerMeter;
    public GameObject contentResultText;
    public GameObject contentResultSubtitle;
    
    // Bottom Content
    public GameObject controlView;
    public GameObject controlAim;
    public GameObject controlCurve;
    public GameObject controlConfirm;

    public void Start(){
        shadow.SetActive(false);
        LoadExistingSettings();
    }

    public void Init(){
        LoadExistingSettings();
    }


    public void LoadExistingSettings(){
    }



    // Team Display Functions
    public void EnableTeamDisplays() {
        // Enable the aim display UI
        teamDisplay001.SetActive(true);
        teamDisplay002.SetActive(true);
        Debug.Log("Aim Display Enabled");
    }

    public void DisableTeamDisplays() {
        // Enable the aim display UI
        teamDisplay001.SetActive(false);
        teamDisplay002.SetActive(false);
        Debug.Log("Aim Display Disabled");
    }

    public void SetTeamDisplays() {
        // Enable the aim display UI
        teamDisplay001.GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "BLUE TEAM";
        teamDisplay002.GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "GREEN TEAM";
        Debug.Log("Aim Display Enabled");
    }


    // Aim Display
    public void EnableAimDisplay() {
        // Enable the aim display UI
        // contentAim.SetActive(true);
        controlView.SetActive(true);
        controlAim.SetActive(true);
        controlCurve.SetActive(true);
        controlConfirm.SetActive(true);
        Debug.Log("Aim Display Enabled");
    }

    public void DisableAimDisplay() {
        // Enable the aim display UI
        controlView.SetActive(false);
        controlAim.SetActive(false);
        controlCurve.SetActive(false);
        controlConfirm.SetActive(false);
        Debug.Log("Aim Display Enabled");
    }
    
    // power meter
    public void EnablePowerMeter() {
        // Enable the power meter UI
        powerMeter.SetActive(true);
        controlConfirm.SetActive(true);
        Debug.Log("Power Meter Enabled");
    }
    public void DisablePowerMeter() {
        // Disable the power meter UI
        powerMeter.SetActive(false);
        controlConfirm.SetActive(false);
        Debug.Log("Power Meter Disabled");
    }

    // exhaustion meters
    public void EnableExhaustionBars() {
        // Enable the left exhaustion bar UI
        exhaustionBarLeft.SetActive(true);
        exhaustionBarRight.SetActive(true);
        Debug.Log("Exhaustion Bar Left Enabled");
    }
    public void DisableExhaustionBars() {
        // Disable the left exhaustion bar UI
        exhaustionBarLeft.SetActive(false);
        exhaustionBarRight.SetActive(false);
        Debug.Log("Exhaustion Bar Left Disabled");
    }

    // Result Text
    public void EnableResultText() {
        // Enable the result text UI
        contentResultText.SetActive(true);
        contentResultSubtitle.SetActive(true);
        Debug.Log("Result Text Enabled");
    }
    public void DisableResultText() {
        // Disable the result text UI
        contentResultText.SetActive(false);
        contentResultSubtitle.SetActive(false);
        Debug.Log("Result Text Disabled");
    }
    public void SetResultText(
        string titleText,
        string subtitleText
    ) {
        // contentResultText.GetComponent<TextMeshProUGUI>().text = titleText;
        contentResultText.GetComponent<TMPro.TextMeshProUGUI>().text = titleText;
        contentResultSubtitle.GetComponent<TMPro.TextMeshProUGUI>().text = subtitleText;
        // Enable the result subtitle UI
        Debug.Log("Result Title/Subtitle Set: " + titleText + " - " + subtitleText);
    }

}
