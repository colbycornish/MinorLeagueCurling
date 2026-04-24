

public enum CurlingMatchPhase
{
    /// Should only be used at the start
    None,
    Loading,
    StartGameIntro,
    RoundSplash,
    TeamSplash,
    TurnSplash,

    /// Curling loop begins here
    StoneSelection,
    StoneSelectionDetails,
    StoneSelectionConfirm,
    AimingAndPowerPhase,
    CurlingAimControlsPhase,
    CurlingLaunchStonePhase,
    CurlingPowerMeterPhase,
    CurlingStoneSweepingPhase,
    CurlingNoSweepZone,
    PostThrowResult,
    ScoringPhase,

    // Displayed if the settings are marked to allow player obstacles
    ObstacleSelection,
    ObstaclePlacement,

    // End Game
    FinalResults,

    // Exit the Curling Game and return to previous location
    ExitCurlingGame
}