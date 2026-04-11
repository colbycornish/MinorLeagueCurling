using System.Collections.Generic;
using UnityEngine;
using CurlingManagersV3;

namespace UICanvasManager.v3
{
    public class CurlingGamePostThrowResultState : ICanvasState
    {
        public GameObject canvasGoodThrow;
        public GameObject canvasBadThrow;
        public GameObject canvasOutOfBounds;

        // Unimplimented
        public GameObject canvasNiceTry;

    
        // Special Canvases for specific reactions
        public GameObject canvasSmashingMachine;
        public GameObject canvasBlockCity;
        public GameObject canvasExplosiveThrow;
        
        
        // Unimplimented
        
        public GameObject canvasBlockedNoNoNo;
        public GameObject canvasBlockedBlockParty;
        public GameObject canvasPenguinKiller;
        public GameObject canvasYouGotNailed;
        public GameObject canvasYetiSmackdown;
         
        
        
        /************************************************************************************************************************/

        // public void Awake(){
        //     DisableAllCanvases();
        // }

        public override void OnEnter()
        {
            gameObject.SetActive(true); // Show the canvas
            SelectPostThrowReaction();
        }

        

        public override void OnExit()
        {
            gameObject.SetActive(false); // Hide the canvas
        }

        private void DisableAllCanvases()
        {
            Debug.Log("Disabling all post-throw result canvases...");
            if (canvasGoodThrow != null) canvasGoodThrow.SetActive(false);
            if (canvasBadThrow != null) canvasBadThrow.SetActive(false);
            if (canvasOutOfBounds != null) canvasOutOfBounds.SetActive(false);
            if (canvasSmashingMachine != null) canvasSmashingMachine.SetActive(false);
            if (canvasBlockCity != null) canvasBlockCity.SetActive(false);
            if (canvasExplosiveThrow != null) canvasExplosiveThrow.SetActive(false);
        }


        private void SelectPostThrowReaction()
        {
            CurlingStone currentStone = CurlingManager._instance.Parameters.Stones.currentStone;

            if (currentStone == null)
            {
                Debug.LogWarning("Current stone is null. Cannot select post-throw reaction canvas.");
                return;
            }

            int numObstaclesHit = currentStone.Parameters.Status.NumObstaclesHitByStone;
            bool isScoringStone = currentStone.Parameters.Status.IsScoringStone;
            bool isInScoringZone = currentStone.Parameters.Status.IsInScoringZone;
            bool isInScoringZoneBullseye = currentStone.Parameters.Status.IsInScoringZoneBullseye;
            bool isInScoringZoneLevelOne = currentStone.Parameters.Status.IsInScoringZoneLevelOne;
            bool isInScoringZoneLevelTwo = currentStone.Parameters.Status.IsInScoringZoneLevelTwo;
            bool isInBlockingZone = currentStone.Parameters.Status.IsInBlockingZone;
            bool isOutOfBounds = currentStone.Parameters.Status.IsOutOfBounds;  

            // if the stone is in the scoring zone...
            if (isInScoringZone && isInScoringZoneBullseye){
                canvasGoodThrow.SetActive(true);
                return;
            }
            else if (isInScoringZone && isInScoringZoneLevelOne){
                if (numObstaclesHit >= 2) canvasSmashingMachine.SetActive(true);
                else canvasGoodThrow.SetActive(true);
            }
            else if (isInScoringZone && isInScoringZoneLevelTwo){
                if (numObstaclesHit >= 1) canvasExplosiveThrow.SetActive(true);
                else if (numObstaclesHit == 1) canvasBlockCity.SetActive(true);
                else canvasGoodThrow.SetActive(true);
                
            }
            else if (isInScoringZone){
                canvasGoodThrow.SetActive(true);
                return;
            }
            // if the stone is in the blocking zone...
            else if (isInBlockingZone)
            {
                canvasBlockCity.SetActive(true);
                return;
            }
            // if the stone is in the out of bounds...
            else if (isOutOfBounds)
            {
                canvasOutOfBounds.SetActive(true);
                return;
            }
            // Bad throw &/or unkown result
            else 
            {
                canvasBadThrow.SetActive(true);
                return;
            }
        }

        public void RandomizeCanvasShown()
        {
            // List<GameObject> postThrowCanvases = new List<GameObject>();
            // if (canvasGoodThrow != null) postThrowCanvases.Add(canvasGoodThrow);
            // if (canvasBadThrow != null) postThrowCanvases.Add(canvasBadThrow);
            // if (canvasOutOfBounds != null) postThrowCanvases.Add(canvasOutOfBounds);
            // if (canvasSmashingMachine != null) postThrowCanvases.Add(canvasSmashingMachine);
            // if (canvasBlockCity != null) postThrowCanvases.Add(canvasBlockCity);
            // if (canvasExplosiveThrow != null) postThrowCanvases.Add(canvasExplosiveThrow);

            // foreach (GameObject canvas in postThrowCanvases)
            // {
            //     canvas.SetActive(false);
            // }

            // Debug.Log("Selecting random post-throw reaction canvas...");
            // int choice = Random.Range(0, postThrowCanvases.Count);
            // Debug.Log($"Selected canvas index: {choice}, name: {postThrowCanvases[choice].name}");
            // postThrowCanvases[choice].SetActive(true);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGamePostThrowResultDisplay;

    }
}