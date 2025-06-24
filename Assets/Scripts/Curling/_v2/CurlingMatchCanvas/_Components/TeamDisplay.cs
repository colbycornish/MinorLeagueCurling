

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamDisplay : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public bool controlsTeamOne = false;
    public bool controlsTeamTwo = false;


    public GameObject teamNameText;
    public GameObject parentStoneDisplay;

    public void Start()
    {
        errorCheck();
    }

    private void OnEnable()
    {
        if (CurlingGameManagerV2.Instance != null)
        {
            UpdateTeamName();
        }
    }

    // private void OnDisable()
    // {
        // if (CurlingGameManagerV2.Instance == null) return;
        // CurlingGameManagerV2.Instance.playerManager.OnTeamDataChanged -= HandleDataUpdate;
    // }


    public void UpdateTeamName()
    {
        int teamIndex = controlsTeamOne ? 0 : (controlsTeamTwo ? 1 : -1);
        if (teamIndex == -1)
        {
            Debug.LogError("[TeamDisplay] No team controls set. Cannot update team name.");
            return;
        }

        CurlingTeamData teamData = CurlingGameManagerV2.Instance.playerManager.teams[teamIndex];
        string teamName = teamData.teamName;
        if (teamName != null)
        {
            teamNameText.GetComponent<TMPro.TextMeshProUGUI>().text = teamName;
        }
    }

    public void UpdateStoneDisplay()
    {
        List<CurlingStone> stones = CurlingGameManagerV2.Instance.stoneManager.stonesTeamA;
        if (controlsTeamTwo)
        {
            stones = CurlingGameManagerV2.Instance.stoneManager.stonesTeamB;
        }

        int numStonesThrown = 0;
        foreach (CurlingStone stone in stones)
        {
            if (stone.isThrown)
            {
                numStonesThrown++;
            }
        }
    }


    public void errorCheck()
    {


    }
}
