using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SectionCurlingInGameDisplay : MonoBehaviour
{
    [Header("ScoreBug")]
    [SerializeField] public ScorebugController scoreBug;

    [Header("Power Meter")]
    [SerializeField] public PowerMeterController powerMeter;


    [Header("Sweeper Bars")]
    [SerializeField] public SweeperExhaustionBarV2 leftSweeperExhaustionBar;
    [SerializeField] public SweeperExhaustionBarV2 rightSweeperExhaustionBar;
    

    void Start()
    {
        LoadData();
        
    }

    public void Update()
    {
        // Switch item selection right
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftSweeperExhaustionBar.IncreaseExhaustionLevel();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            rightSweeperExhaustionBar.IncreaseExhaustionLevel();
            
        }
        

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            powerMeter.IncreasePower();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            powerMeter.DecreasePower();
        }
    
    }

    public void LoadData()
    {
        /// leftSweeperBar
        leftSweeperExhaustionBar.SetExhaustionLevelsFromCharacter(
            exhaustionLevel: 0.0f, 
            maxExhaustionLevel: 1.0f, 
            minExhaustionLevel: 0.0f,
            exhaustionRate: 0.06f,
            recoveryRate: 0.04f,
            exhaustionThreshold: 0.85f
        );
        leftSweeperExhaustionBar.InitializeDisplay();
        
        /// rightSweeperBar
        rightSweeperExhaustionBar.SetExhaustionLevelsFromCharacter(
            exhaustionLevel: 0.0f, 
            maxExhaustionLevel: 1.0f, 
            minExhaustionLevel: 0.0f,
            exhaustionRate: 0.06f,
            recoveryRate: 0.04f,
            exhaustionThreshold: 0.85f
        );
        rightSweeperExhaustionBar.InitializeDisplay();

        /// Power
        powerMeter.UpdateCurrentPower(
            current: 0.8f
        );

        /// scoreBug
        scoreBug.UpdateTeamInfo(
            homeTeamName: "Blue Broom Brushers",
            awayTeamName: "Purple Stone Throwers"
        );
        scoreBug.SetTotalNumberOfStonesPerTeam(totalNumberOfStonesPerTeam: 5);
        scoreBug.UpdateScore(
            homeTeamScore: 0,
            awayTeamScore: 0
        );
        scoreBug.UpdateStoneAvailability(
            numHomeTeamStonesAvailable: 5,
            numAwayTeamStonesAvailable: 5
        );
    }

    public void UpdateStoneAvailability(){
        
    }

    public void UpdateUI()
    {
        // listOfCourses.UpdateSelectedDisplay(
            // selectedIds: CurlingPreGameSetupManager._instance.GetSelectedCourseIds()
        // );
    }

    ///
    /// Selection Functions
    /// 
    public void OnSelectStone(string stoneId)
    {
        // CurlingPreGameSetupManager._instance.OnSelectCourse(
            // courseId: courseId
        // );
        UpdateUI();
    }

}
