

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.Tools;

public class KeyIconController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    [Header("Game Objects")]
    public List<GameObject> icons;

    [Header("Settings")]
    public int activeIndex = 0;
    public bool shouldAnimate = false;
    public float repeatRate = 2.0f; // Time in seconds between repetitions
    // public bool shouldAnimate = false;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start(){
        UpdateVisuals();
        InvokeRepeating("UpdateItem", 0f, repeatRate);
    }

    public void Init(){
        UpdateVisuals();
    }

    void UpdateItem()
    {
        int nextIndex = activeIndex + 1;
        if (nextIndex >= icons.Count){
            nextIndex = 0;
        }
        activeIndex = nextIndex;
        UpdateVisuals();
        // Your code to update the item goes here
    }
    

    public void UpdateVisuals(){
        int i = 0;
        foreach(GameObject icon in icons){
            if (i == activeIndex){
                icon.SetActive(true);
            }
            else {
                icon.SetActive(false);
            }
            i++;
        }
    }

}
