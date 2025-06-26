

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

    public GameObject stoneIcon;
    public GameObject closeIcon;
    public GameObject checkIcon;


    public void Start()
    {
        Reset();
    }



    public void UpdateStoneIcon()
    {

    }

    public void ApplyClose()
    {
        stoneIcon.SetActive(true);
        closeIcon.SetActive(true);
        checkIcon.SetActive(false);
    }

    public void ApplyCheck()
    {
        stoneIcon.SetActive(true);
        closeIcon.SetActive(false);
        checkIcon.SetActive(true);
    }

    public void Reset()
    {
        ApplyDefault();
        isOutOfPlay = false;
        isThrown = false;
        isUsed = false;
        isDefault = true;
    }

    public void ApplyDefault()
    {
        stoneIcon.SetActive(true);
        closeIcon.SetActive(false);
        checkIcon.SetActive(false);
    }

}
