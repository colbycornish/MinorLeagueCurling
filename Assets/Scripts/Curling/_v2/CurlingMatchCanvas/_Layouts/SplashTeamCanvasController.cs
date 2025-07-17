

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashTeamCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private string teamName_1 = "BLUE TEAM";
    private string teamName_2 = "RED TEAM";
    public GameObject content;
    public GameObject shadow;

    public GameObject teamNameText_1;
    public GameObject teamNameText_2;
    public GameObject teamOrder_1;
    public GameObject teamOrder_2;

    public void Start()
    {

    }

    private void OnEnable()
    {
        UpdateTeamNames();
        // UpdateOrder();
        ShowSplashTeam();
    }

    public void OnDisable()
    {
        // This method is called when the canvas is disabled
        HideSplashTeam();
    }


    public void UpdateTeamNames()
    {
        if (CurlingGameManagerV2.Instance.playerManager.teams.Count < 2)
        {
            Debug.LogError("[TeamDisplay] Not enough teams available. Cannot update team names.");
            return;
        }
        CurlingTeam teamData_01 = CurlingGameManagerV2.Instance.playerManager.teams[0];
        CurlingTeam teamData_02 = CurlingGameManagerV2.Instance.playerManager.teams[1];

        string teamName_1 = teamData_01.teamName;
        string teamName_2 = teamData_02.teamName;

        if (teamName_1 != null)
        {
            teamNameText_1.GetComponent<TMPro.TextMeshProUGUI>().text = teamName_1;
        }
        
        if (teamName_2 != null)
        {
            teamNameText_2.GetComponent<TMPro.TextMeshProUGUI>().text = teamName_2;
        }
        
    }

    public void LoadExistingSettings()
    {
    }

    
    public void ShowSplashTeam()
    {
        // Display the splash team on the canvas
        content.SetActive(true);
        shadow.SetActive(true);
        Debug.Log("Splash Team Displayed: " + teamName_1 + " vs " + teamName_2);
    }
    public void HideSplashTeam()
    {
        // Hide the splash team on the canvas
        content.SetActive(false);
        shadow.SetActive(false);
        Debug.Log("Splash Team Hidden");
    }


}
