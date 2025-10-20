

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.Tools;

public class PowerMeterSingleBar : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public Image backgroundColor;
    

    public void SetHexColor(string hexCode){
        Color newColor;
        ColorUtility.TryParseHtmlString($"#{hexCode}", out newColor);
        SetColor(newColor);
    }

    public void SetColor(Color newColor){
        backgroundColor.color = newColor;
    }
}
