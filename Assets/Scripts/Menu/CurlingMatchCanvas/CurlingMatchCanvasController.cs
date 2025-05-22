using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurlingMatchCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private SplashRoundCanvasController splashRoundCanvasController;
    private SplashTeamCanvasController splashTeamCanvasController;
    private StoneSelectionCanvasController stoneSelectionCanvasController;
    private CurlingGameCanvasController curlingGameCanvasController;

    public GameObject splashRoundCanvas;
    public GameObject splashTeamCanvas;
    public GameObject stoneSelectionCanvas;
    public GameObject curlingGameCanvas;

    private string activeCanvas = "GAME";

    public void Start(){
        
        splashRoundCanvas.SetActive(false);
        splashTeamCanvas.SetActive(false);
        stoneSelectionCanvas.SetActive(false);
        curlingGameCanvas.SetActive(true);
    }

    void Update()
    {
        // Check for user input to switch between canvases
        if (Input.GetKeyDown(KeyCode.N))
        {
            switch (activeCanvas){
                case "SPLASH_ROUND":
                    activeCanvas = "SPLASH_TEAM";
                    break;
                case "SPLASH_TEAM":
                    activeCanvas = "STONE_SELECTION";
                    break;
                case "STONE_SELECTION":
                    activeCanvas = "GAME";
                    break;
                case "GAME":
                    activeCanvas = "SPLASH_ROUND";
                    break;
                default:
                    Debug.Log("Invalid canvas name");
                    break;
            }
            ActiveCanvas();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            switch (activeCanvas){
                case "SPLASH_ROUND":
                    activeCanvas = "GAME";
                    break;
                case "SPLASH_TEAM":
                    activeCanvas = "SPLASH_ROUND";
                    break;
                case "STONE_SELECTION":
                    activeCanvas = "SPLASH_TEAM";
                    break;
                case "GAME":
                    activeCanvas = "STONE_SELECTION";
                    break;
                default:
                    Debug.Log("Invalid canvas name");
                    break;
            }
            ActiveCanvas();
        }
        

    }


    public void ActiveCanvas(){
        // Set the active canvas based on the current active canvas
        switch (activeCanvas)
        {
            case "SPLASH_ROUND":
                Debug.Log("Display Round Selection Canvas");
                splashRoundCanvas.SetActive(true);
                splashTeamCanvas.SetActive(false);
                stoneSelectionCanvas.SetActive(false);
                curlingGameCanvas.SetActive(false);
                break;
            case "SPLASH_TEAM":
                Debug.Log("Display Team Selection Canvas");
                splashRoundCanvas.SetActive(false);
                splashTeamCanvas.SetActive(true);
                stoneSelectionCanvas.SetActive(false);
                curlingGameCanvas.SetActive(false);
                break;
            case "STONE_SELECTION":
                Debug.Log("Display Stone Selection Canvas");
                splashRoundCanvas.SetActive(false);
                splashTeamCanvas.SetActive(false);
                stoneSelectionCanvas.SetActive(true);
                curlingGameCanvas.SetActive(false);
                break;
            case "GAME":
                Debug.Log("Display Game Canvas");
                splashRoundCanvas.SetActive(false);
                splashTeamCanvas.SetActive(false);
                stoneSelectionCanvas.SetActive(false);
                curlingGameCanvas.SetActive(true);
                break;
            default:
                Debug.Log("Invalid canvas name");
                splashRoundCanvas.SetActive(true);
                splashTeamCanvas.SetActive(false);
                stoneSelectionCanvas.SetActive(false);
                curlingGameCanvas.SetActive(false);
                break;

        }
        return;
    }



}
