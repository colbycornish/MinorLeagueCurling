using UnityEngine;
using System.Collections.Generic;

public enum CurlingMatchPhase
{
    /// Should only be used at the start
    Loading,
    RoundSplash,
    TeamSplash,

    /// Curling loop begins here
    StoneSelection,
    StoneSelectionDetails,
    StoneSelectionConfirm,
    CurlingAimControlsPhase,
    CurlingPowerMeterPhase,
    CurlingStoneSweepingPhase,
    CurlingNoSweepZone,
    PostThrowResult,

    // Displayed if the settings are marked to allow player obstacles
    ObstacleSelection,
    ObstaclePlacement,

    // End Game
    FinalResults,

    // Exit the Curling Game and return to previous location
    ExitCurlingGame


}