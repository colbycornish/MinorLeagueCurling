

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamDisplayStoneIcon : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public bool isOutOfPlay = false;
    public bool isThrown = false;
    public bool isUsed = false;
    public bool isDefault = true;

    public GameObject stoneIconParent;


    public void Start()
    {
        Reset();
    }



    public void UpdateStoneIcon()
    {

    }

    public void Reset()
    {
        isOutOfPlay = false;
        isThrown = false;
        isUsed = false;
        isDefault = true;
    }

}
