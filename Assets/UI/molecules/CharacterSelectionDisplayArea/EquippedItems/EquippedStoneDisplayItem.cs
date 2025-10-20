using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedStoneDisplayItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private GameObject defaultState;
    [SerializeField] private GameObject highlightedState;
    [SerializeField] private GameObject selectedState;
    [SerializeField] private string stoneId = "";
    [SerializeField] private int stoneIndex = 0;

    [Header("Image")]
    [SerializeField] private GameObject imageObject;
    [SerializeField] private RawImage renderTexture;

    [Header("Settings")]
    [SerializeField] public bool isEmpty = true;
    [SerializeField] public bool isHighlighted = false;
    
    public void Start()
    {

        
    }

    public void Init(int index){
        stoneIndex = index;
        isEmpty = true;
        isHighlighted = false;
        defaultState.SetActive(true);
        highlightedState.SetActive(false);
        selectedState.SetActive(false);
    }

    public void UpdateInfo(
        CurlingStone stone,
        Texture image
    ){
        if (stone != null){
            // SetName(name: stone.title);
            // SetDescription(desc: stone.description);
            SetImage(image: image);
        }
        else{
            // SetName(name: "??????????");
            // SetDescription(desc: "???????");
            SetImage(image: null);
        }
    }

    public void SetImage(Texture image){
        if (image != null){
            imageObject.SetActive(true);
            renderTexture.texture = image;
            return;
        }
        else {
            imageObject.SetActive(false);
            renderTexture.texture = null;
        }
    }


    // public void SetEmpty(bool status = false){
    //     isEmpty = status;
    //     empty.SetActive(status);
    //     if (status == true){
    //         SetHighlighted(!status);
    //     }
    // }

    public void SetDefault(bool status = false){
        defaultState.SetActive(status);
        SetHighlighted(false);
        SetSelected(false);
    }

    public void SetHighlighted(bool status = false){
        isHighlighted = status;
        highlightedState.SetActive(status);
        
    }

    public void SetSelected(bool status = false){
        isHighlighted = status;
        highlightedState.SetActive(status);
    }
    


    
}