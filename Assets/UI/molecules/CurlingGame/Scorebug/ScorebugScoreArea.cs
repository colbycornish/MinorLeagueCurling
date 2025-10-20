

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorebugScoreArea : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public TextMeshProUGUI scoreText;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Init(){
        
    }

    public void UpdateScore(
        int homeTeamScore = 0,
        int awayTeamScore = 0
    ){

        string htc = homeTeamScore.ToString();;
        string atc = awayTeamScore.ToString();;
        string text = $"{htc}-{atc}";
        if (text != null)
        {
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        }
    }


    public void ResetScore(){
       UpdateScore(
            homeTeamScore: 0,
            awayTeamScore: 0
        );
    }

}

