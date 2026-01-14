using System;
using System.Collections.Generic;
using UnityEngine;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class Teams
    {

        [SerializeField]
        private List<CurlingTeam> _Teams = new List<CurlingTeam>();
        public List<CurlingTeam> CurlingTeams => _Teams;
        // public List<CurlingTeam> teams = new List<CurlingTeam>();

        [SerializeField]
        // private CurlingTeam _TeamHome;
        // public CurlingTeam TeamHome => _TeamHome;
        public CurlingTeam teamHome;

        [SerializeField]
        // private CurlingTeam _TeamAway;
        // public CurlingTeam TeamAway => _TeamAway;
        public CurlingTeam teamAway;

        [SerializeField]
        public CurlingTeam currentTeam;

        // [Header("Team Members")]
        // private GameObject teamHomeThrower;
        // private GameObject teamHomeSweeperL;
        // private GameObject teamHomeSweeperR;
        // private GameObject teamAwayThrower;
        // private GameObject teamAwaySweeperL;
        // private GameObject teamAwaySweeperR;
        // public CurlingTeam currentTeam;
        
    }
}
