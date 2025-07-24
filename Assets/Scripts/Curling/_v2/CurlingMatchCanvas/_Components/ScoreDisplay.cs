

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public GameObject scoreText;
    public GameObject parentScoreObj;

    public void Start()
    {
        ErrorCheck();
    }

    private void OnEnable()
    {
        if (CurlingGameManagerV2.Instance != null)
        {
            UpdateScore();
        }
    }

    private void OnDisable()
    {
        // if (CurlingGameManagerV2.Instance == null) return;
        // CurlingGameManagerV2.Instance.playerManager.OnTeamDataChanged -= HandleDataUpdate;
    }


    public void UpdateScore()
    {
        CurlingGameManagerV2.Instance.endManager.CalculateScore();
        CurlingGameScore score = CurlingGameManagerV2.Instance.gameData.score;
        
        int teamHomeScore = score.teamHomeScore;
        int teamAwayScore = score.teamAwayScore;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = $"{teamHomeScore}-{teamAwayScore}";
    
    }

    public void ErrorCheck()
    {


    }
}
