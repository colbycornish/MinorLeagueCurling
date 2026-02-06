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
        public CurlingTeam teamHome;
        
        // [SerializeField]
        // private CurlingTeam _TeamHome;
        // public CurlingTeam TeamHome
        // {
        //     get => _TeamHome;
        //     set => _TeamHome = value;
        // }

        [SerializeField]
        // private CurlingTeam _TeamAway;
        // public CurlingTeam TeamAway => _TeamAway;
        public CurlingTeam teamAway;

        [SerializeField]
        public CurlingTeam currentTeam;

        
    }
}
