using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages inputs for the Curling game.
/// It's unclear if inputs should be handled here, or at a lower level.
/// 
/// TODO: Research Input handling in Unity and decide if this is the right place.
/// </summary>


public class CurlingInputManagerV2 : MonoBehaviour
{
    public CurlingGameManagerV2 gameManager;
    private CurlingStoneThrowControllerV2 currentThrower;

    private void Update()
    {

        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;


        if (currentPhase == CurlingMatchPhase.RoundSplash &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.TeamSplash);
        }
        else if (currentPhase == CurlingMatchPhase.TeamSplash &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
        }
        else if (currentPhase == CurlingMatchPhase.StoneSelectionConfirm &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.AimControls);
        }

        else if (currentPhase == CurlingMatchPhase.AimControls &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PowerMeter);
        }
        else if (currentPhase == CurlingMatchPhase.PowerMeter &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingControls);
            //currentThrower?.ThrowStone(new Vector3(0, 0, 10));
        }
        else if (currentPhase == CurlingMatchPhase.CurlingControls &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            //currentThrower?.ThrowStone(new Vector3(0, 0, 10));
        }

        else if (currentPhase == CurlingMatchPhase.PostThrowResult &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.ObstaclePlacement);
        }
        else if (currentPhase == CurlingMatchPhase.ObstaclePlacement &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.FinalResults);
        }
        else if (currentPhase == CurlingMatchPhase.FinalResults &&
            Input.GetMouseButtonDown(0)
        )
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
        }
    }

    public void SetCurrentThrower(CurlingStoneThrowControllerV2 thrower)
    {
        currentThrower = thrower;
    }
}
