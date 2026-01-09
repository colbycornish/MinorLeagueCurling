public enum CurlingGameScoreType
{
    /// Should only be used at the start
    Classic, // after all stones are thrown, only the closest player scores
    Brawl, // Stones will only be eligable to score if they hit something
    HouseParty, // points are only scored if the stone is within the target zone
    RollingTotal, // scores cumulate every turn
    WinnerTakeAll, // after all stones are thrown, only the closest player scores
    Skins, // Last thrower must score at least two points to win.
    HotShot // Trick shot points based on the situation
}
