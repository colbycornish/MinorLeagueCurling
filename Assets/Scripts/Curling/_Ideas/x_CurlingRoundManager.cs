// using UnityEngine;
// using CurlingGameDataNamespace;

// namespace CurlingGameDataNamespace {
//     public class CurlingRoundManager : MonoBehaviour
//     {
//         // Start() and Update() methods deleted - we don't need them right now

//         public static CurlingGameManager Instance;
       
//         //
//         public CurlingMatchCanvasController curlingMatchCanvasController;
//         public StoneThrowController stoneThrowController;
//         /// <summary>
//         /// defines markers for the current turn
//         /// </summary>
//         /// 
        
//         private string currentTurnTeamName = "";
//         private string currentTurnPhase = "";
//         private string startingTeamName = "";
        
//         private int turnCount = 0;
//         private int roundCount = 1;
//         private int maxRounds = 3;

//         private bool hasGameEnded = false;
//         private bool hasGameStarted = false;
//         private bool hasGamePaused = false;
        
//         /// <summary>
//         /// Array of current stone placements (logged after throw)
//         /// </summary>


//         void SetTeams () {

//         }

//         void SaveStonePositions () {

//         }

//         void AdvanceTurn () {
//             turnCount =+ 1;
//         }

//         public void StartGame(){
//             hasGameStarted = true;
//         }

//         public void EndGame(){
//             hasGameEnded = true;
//         }

//         public bool getHasGameStarted(){
//             return hasGameStarted;
//         }

//         public bool getHasGameEnded(){
//             return hasGameEnded;
//         }

//         void Reset() {
//             turnCount = 0;
//             roundCount = 1;
//             hasGameEnded = false;
//             hasGameStarted = false;
//         }
        


//     }
// }