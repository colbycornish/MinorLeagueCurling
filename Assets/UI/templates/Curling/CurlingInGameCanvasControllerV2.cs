using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingInGameCanvasControllerV2 : MonoBehaviour
{
    [Header("Sections")]
    [SerializeField] public GameObject sectionLoadingSection;
    [SerializeField] public GameObject sectionStoneSelection;
    [SerializeField] public GameObject sectionCurlingInGame;
    [SerializeField] public GameObject sectionPostThrowResultsDisplay;
    [SerializeField] public GameObject sectionFinalResultsDisplay;

    void Start()
    {
        SetActiveSelection();
    }

    private void OnEnable()
    {
        if (CurlingManagersV3.MatchPhaseManager._instance == null) return;
        CurlingManagersV3.MatchPhaseManager._instance.OnPhaseChanged += HandlePhase;
    }

    private void OnDisable()
    {
        if (CurlingManagersV3.MatchPhaseManager._instance == null) return;
        CurlingManagersV3.MatchPhaseManager._instance.OnPhaseChanged -= HandlePhase;
    }

    void HandlePhase(
        CurlingManagersV3.CurlingMatchPhase newPhase
    ){
        OnPhaseChanged();
    }

    void OnPhaseChanged(){
        CurlingManagersV3.CurlingMatchPhase currentPhase = CurlingManagersV3.MatchPhaseManager._instance.currentPhase;
        switch (currentPhase)
        {
            case CurlingManagersV3.CurlingMatchPhase.Loading:
                OpenLoadingSection();
                break;
            // case CurlingManagersV3.CurlingMatchPhase.RoundSplash:
            //     OpenRoundSplashSection();
            //     break;
            case CurlingManagersV3.CurlingMatchPhase.StoneSelection:
                OpenStoneSelection();
                break;
            case CurlingManagersV3.CurlingMatchPhase.CurlingAimControlsPhase:
                OpenCurlingInGameSection();
                break;
            case CurlingManagersV3.CurlingMatchPhase.PostThrowResult:
                OpenPostThrowResultsDisplay();
                break;
            case CurlingManagersV3.CurlingMatchPhase.FinalResults:
                OpenFinalResultsDisplay();
                break;
            default:
                // SetActiveSelection();
                break;
        }
    }

    public void OpenLoadingSection(){
        SetActiveSelection(openLoadingSection: true);
    }
    
    public void OpenStoneSelection(){
        SetActiveSelection(openStoneSelection: true);
    }

    public void OpenCurlingInGameSection(){
        SetActiveSelection(openCurlingInGame: true);
    }

    public void OpenPostThrowResultsDisplay(){
        SetActiveSelection(openPostThrowResultsDisplay: true);
    }

    public void OpenFinalResultsDisplay(){
        SetActiveSelection(openFinalResultsDisplay: true);
    }

    public void LaunchCurlingGame(){
        Debug.Log("Launching Curling Game...");
    }

    /// activate/Deactivate Sections
    private void SetActiveSelection(
        bool openLoadingSection = false,
        bool openStoneSelection = false, 
        bool openCurlingInGame = false, 
        bool openPostThrowResultsDisplay = false, 
        bool openFinalResultsDisplay = false
    ){
        sectionLoadingSection.SetActive(openLoadingSection);
        sectionStoneSelection.SetActive(openStoneSelection);
        sectionCurlingInGame.SetActive(openCurlingInGame);
        sectionPostThrowResultsDisplay.SetActive(openPostThrowResultsDisplay);
        sectionFinalResultsDisplay.SetActive(openFinalResultsDisplay);
    }


    /// DEMO FUNCTIONS
    
}
