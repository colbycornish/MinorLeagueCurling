using System.Collections.Generic;
using UnityEngine;

namespace UICanvasManager.v3
{
    public class CurlingGamePostThrowResultState : ICanvasState
    {
        private GameObject mainMenuCanvas;
        public GameObject canvasGoodThrow;
        public GameObject canvasBadThrow;
        public GameObject canvasOutOfBounds;
        public GameObject canvasSmashingMachine;
        public GameObject canvasBlockCity;
        public GameObject canvasExplosiveThrow;
        
        /************************************************************************************************************************/

        // public void Awake(){
        //     DisableAllCanvases();
        // }

        public override void OnEnter()
        {
            Debug.Log("PostThrowResultState OnEnter: Disabling all canvases and showing Post-Throw Result canvas");
            DisableAllCanvases();
            gameObject.SetActive(true); // Show the canvas
            SelectPostThrowReaction();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        

        public override void OnExit()
        {
            Debug.Log("PostThrowResultState OnExit: Disabling all canvases and hiding Post-Throw Result canvas");
            DisableAllCanvases();
            gameObject.SetActive(false); // Hide the canvas
            // Remove listeners
        }

        private void DisableAllCanvases()
        {
            Debug.Log("Disabling all post-throw result canvases...");
             if (canvasGoodThrow != null) canvasGoodThrow.SetActive(false);
            if (canvasGoodThrow != null) canvasGoodThrow.SetActive(false);
            if (canvasBadThrow != null) canvasBadThrow.SetActive(false);
            if (canvasOutOfBounds != null) canvasOutOfBounds.SetActive(false);
            if (canvasSmashingMachine != null) canvasSmashingMachine.SetActive(false);
            if (canvasBlockCity != null) canvasBlockCity.SetActive(false);
            if (canvasExplosiveThrow != null) canvasExplosiveThrow.SetActive(false);
        }


        private void SelectPostThrowReaction()
        {
            List<GameObject> postThrowCanvases = new List<GameObject>();
            if (canvasGoodThrow != null) postThrowCanvases.Add(canvasGoodThrow);
            if (canvasBadThrow != null) postThrowCanvases.Add(canvasBadThrow);
            if (canvasOutOfBounds != null) postThrowCanvases.Add(canvasOutOfBounds);
            if (canvasSmashingMachine != null) postThrowCanvases.Add(canvasSmashingMachine);
            if (canvasBlockCity != null) postThrowCanvases.Add(canvasBlockCity);
            if (canvasExplosiveThrow != null) postThrowCanvases.Add(canvasExplosiveThrow);

            foreach (GameObject canvas in postThrowCanvases)
            {
                canvas.SetActive(false);
            }

            Debug.Log("Selecting random post-throw reaction canvas...");
            int choice = Random.Range(0, postThrowCanvases.Count);
            Debug.Log($"Selected canvas index: {choice}, name: {postThrowCanvases[choice].name}");
            postThrowCanvases[choice].SetActive(true);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGamePostThrowResultDisplay;

    }
}