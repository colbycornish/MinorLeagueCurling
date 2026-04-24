using System;
using System.Collections.Generic;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class Stones
    {
        
        // [SerializeField]
        // private List<CurlingStone> _StonesTeamHome = new List<CurlingStone>();
        // public List<CurlingStone> StonesTeamHome => _StonesTeamHome;

        // [SerializeField]
        // private List<CurlingStone> _StonesTeamAway = new List<CurlingStone>();
        // public List<CurlingStone> StonesTeamAway => _StonesTeamAway;

        // [SerializeField]
        // private CurlingStone _CurrentStone;
        // public CurlingStone CurrentStone => _CurrentStone;
        

        // [Header("Game Objects")]
        public List<CurlingStone> stonesTeamHome = new List<CurlingStone>();
        public List<CurlingStone> stonesTeamAway = new List<CurlingStone>();

        // [Header("Current Stone")]
        public CurlingStone currentStone;

    }
}