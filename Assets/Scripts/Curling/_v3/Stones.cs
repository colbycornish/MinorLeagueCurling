using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class Stones : MonoBehaviour
    {
        // base skin used for each team
        [Header("Game Objects")]
        // public GameObject stonePrefab_TeamHome;
        // public GameObject stonePrefab_TeamAway;

        public List<CurlingStone> stonesTeamHome = new List<CurlingStone>();
        public List<CurlingStone> stonesTeamAway = new List<CurlingStone>();

        [Header("Locations")]
        public List<Transform> stonesSpawnLocationsTeamHome = new List<Transform>();
        public List<Transform> stonesSpawnLocationsTeamAway = new List<Transform>();
        public Transform launchPoint;

        [Header("Current Stone")]
        // This is one of the most important bits, and will be referenced by 
        // the stone throw controller, sweeping controller, etc.
        public CurlingStone currentStone;
        public CurlingStone currentStoneId;
        // public CurlingStone nextStone;

        private int stonesSpawned = 0;
        private int stonesThrown = 0;
        // private int stonesInPlay = 0;



        /// <summary>
        /// Setup the initial values
        /// </summary>

        public void Setup(
            CurlingCourseData courseData,
            CurlingTeam teamHome,
            CurlingTeam teamAway
        )
        {
            Debug.Log("Setting up Stones...");
            // Declare the launch point
            SetLaunchPoint(courseData.launchPoint);

            // Setup Home Team Stones
            List<GameObject> teamHomeStones = SetupTeamStones(
                team: teamHome,
                stoneSpawnLocations: courseData.stonesSpawnLocationsTeamHome
            );
            Debug.Log($"[Stones] Team Home Stones Count: {teamHomeStones.Count}");
            this.stonesTeamHome = teamHomeStones.ConvertAll(stone => stone.GetComponent<CurlingStone>());

            // Setup Away Team Stones
            List<GameObject> teamAwayStones = SetupTeamStones(
                team: teamAway,
                stoneSpawnLocations: courseData.stonesSpawnLocationsTeamAway
            );
            Debug.Log($"[Stones] Team Away Stones Count: {teamAwayStones.Count}");
            this.stonesTeamAway = teamAwayStones.ConvertAll(stone => stone.GetComponent<CurlingStone>());

            // scoreBug.UpdateStoneAvailability(
            //     numHomeTeamStonesAvailable: 5,
            //     numAwayTeamStonesAvailable: 5
            // );

        }

        public void SetLaunchPoint(Transform location)
        {
            launchPoint = location;
        }

        public List<GameObject> SetupTeamStones(
            CurlingTeam team,
            List<Transform> stoneSpawnLocations
        )
        {
            int numStonesToCreate = stoneSpawnLocations.Count;

            List<GameObject> instantiatedStones = new List<GameObject>();
            for (int i = 0; i < numStonesToCreate; i++)
            {
                // find the spawn points for each team's stones
                Transform spawnPointA = stoneSpawnLocations[i];
                GameObject instantiatedStone = InstatiateStone(
                    stonePrefab: team.defaultStone, 
                    location: spawnPointA, 
                    teamId: team.teamId, 
                    stoneIndex: i
                );
                instantiatedStones.Add(instantiatedStone);
            }

            return instantiatedStones;
        }

         /// Setup a single stone on the ice
        public GameObject InstatiateStone(
            GameObject stonePrefab,
            Transform location,
            string teamId,
            int stoneIndex
        )
        {
            // Instantiate Team A stones
            GameObject stoneInstance = Instantiate(
                stonePrefab, 
                location.position, 
                Quaternion.identity
            );
            CurlingStone curlingStone = stoneInstance.GetComponent<CurlingStone>();

            // toDo: change id to a string
            curlingStone.teamId = teamId; // Team A
            curlingStone.stoneIndex = stoneIndex;
            curlingStone.rb = stoneInstance.GetComponent<Rigidbody>();
            curlingStone.visual = stoneInstance;

            return stoneInstance;

        }

        /// <summary>
        /// Prepare for Next Turn
        /// </summary>
        // TODO: Adjust to look for a selection from the canvas. 
        // These functions would then be a fallback state for if the selecting player
        // times out before selecting a stone.
        public List<CurlingStone> GetStonesForCurrentTeam(){
            if (CurlingManagersV3.CurlingManager._instance.turnManager.IsItTheHomeTeamsTurn()){
                return stonesTeamHome;
            }
            else {
                return stonesTeamAway;
            }
        }



        // Needs to account for the initial state, with a null starting stone.
        public void UpdateCurrentStone(CurlingStone selectedStone)
        {
            currentStone = selectedStone;
        }


        public void PlaceCurrentStoneInLaunchPosition()
        {

            if (currentStone != null && launchPoint != null)
            {
                currentStone.isThrown = false;
                currentStone.isInPlay = true;
                currentStone.transform.position = launchPoint.position;
                currentStone.rb.linearVelocity = Vector3.zero;
                currentStone.rb.angularVelocity = Vector3.zero;
            }
            else
            {
                Debug.LogWarning("Cannot place stone in launch position: stone or launchPoint is null.");
            }
        }





        // public void PrepareNextStone()
        // {
        //     // This method is called when a stone has been thrown, and prepares the next stone for the current team.
        //     if (stonesThrown < stonesSpawned)
        //     {
        //         CurlingStone nextStone = GetNextStone();
        //         if (nextStone != null)
        //         {
        //             nextStone.isThrown = false;
        //             nextStone.isInPlay = true;
        //             nextStone.transform.position = launchPoint.position; // Reset position to launch point
        //             nextStone.rb.linearVelocity = Vector3.zero; // Reset velocity
        //             nextStone.rb.angularVelocity = Vector3.zero; // Reset angular velocity
        //             currentStone = nextStone;
        //         }
        //     }
        //     stonesInPlay++;
        //     stonesThrown++;
        // }


        // Returns the next stone to be thrown based on the current team and stone index.
        private CurlingStone GetNextStone()
        {

            if (stonesThrown < stonesSpawned)
            {
                int currentTeam = stonesThrown % 2; // 0 for Team A, 1 for Team B
                int stoneIndex = stonesThrown / 2; // Each team has 5 stones

                if (currentTeam == 0 && stoneIndex < stonesTeamHome.Count)
                {
                    Debug.Log("Current Stone Retrieved -> Team A");
                    return stonesTeamHome[stoneIndex];
                }
                else if (currentTeam == 1 && stoneIndex < stonesTeamAway.Count)
                {
                    Debug.Log("Current Stone Retrieved -> Team B");
                    return stonesTeamAway[stoneIndex];
                }
            }
            return null;
        }
        
        /// <summary>
        /// Check if the current stone is moving
        /// </summary>

        public bool IsCurrentStoneMoving()
        {
            
            CurlingStone cs = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone;
            Rigidbody rb = cs.rb;
            float threshold = 0.3f;

            // Debug.Log($"[Stone Velocity] {rb.linearVelocity.magnitude}");
            if (rb.linearVelocity == Vector3.zero)
            {
                Debug.Log("Velocity is zero (direct comparison)");
                return false;
                // Do something when velocity is zero
            }

            // Method 2: Checking the magnitude
            if (rb.linearVelocity.magnitude < threshold)
            {
                Debug.Log("Velocity is near zero (magnitude)");
                return false;
                // Do something when velocity is near zero
            }

            // Method 3: Checking the square of the magnitude (slightly faster than magnitude)
            if (rb.linearVelocity.sqrMagnitude < threshold * threshold)
            {
                Debug.Log("Velocity is near zero (squared magnitude)");
                return false;
                // Do something when velocity is near zero
            }

            // Method 4: Using IsSleeping() (for more reliable check if object is at rest)
            if (rb.IsSleeping())
            {
                Debug.Log("Rigidbody is sleeping (at rest)");
                return false;
                // Do something when the rigidbody is sleeping
            }

            return true;
        }

        public bool IsCurrentStoneMovingForward()
        {
            CurlingStone cs = CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone;
            Rigidbody rb = cs.rb;
            Vector3 forwardVelocity = Vector3.Project(rb.linearVelocity, transform.forward);
            float threshold = 0.3f;

            // Check if the forward velocity is close to zero
            if (Mathf.Approximately(forwardVelocity.magnitude, 0f) || forwardVelocity.magnitude < threshold)
            {
                // Object is not moving forward (or very slowly)
                Debug.Log("Object is not moving forward");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reset various values
        /// </summary>
        
        /// clear the stones
        public void ClearExistingStones()
        {
            foreach (CurlingStone stone in FindObjectsByType<CurlingStone>(FindObjectsSortMode.None))
            {
                Destroy(stone.gameObject);
            }
        }

        public void Reset()
        {
            // stonePrefab_TeamHome = null;
            // stonePrefab_TeamAway = null;
            stonesTeamHome.Clear();
            stonesTeamAway.Clear();
            stonesSpawnLocationsTeamHome.Clear();
            stonesSpawnLocationsTeamAway.Clear();
            ClearExistingStones();
        }
    }   
}