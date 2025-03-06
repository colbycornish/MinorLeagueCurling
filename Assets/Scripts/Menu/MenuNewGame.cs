using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNewGame : MonoBehaviour
{


    public void LoadGame(){
        // Opens up the 'start new game' experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }


    public void LaunchNewGame(){
        // Opens up the 'start new game' experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }


    public void LaunchContinueGame(){
        // Opens up the 'ContinueGame' experience
        // TODO: Create launcher script for a continue game experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }

}
