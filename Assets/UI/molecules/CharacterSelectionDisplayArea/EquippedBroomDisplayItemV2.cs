using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedBroomDisplayItemV2 : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] public GameObject selectedState;
    [SerializeField] public GameObject defaultState;

    // [SerializeField] private EquippedBroomDisplayImageItem imageItem;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDesc;

    [Header("Image Area")]
    [SerializeField] private GameObject backgroundImageArea;
    [SerializeField] private GameObject emptyImage;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private RawImage renderTexture;

    [Header("Settings")]
    [SerializeField] public bool hasInfo = false;
    [SerializeField] public bool isHighlighted = false;

    
    public void Start()
    {

        
    }

    public void UpdateInfo(
        CurlingBroom broom,
        string name,
        string desc,
        Texture image
    ){
        if (broom != null){
            SetName(name: broom.title);
            SetDescription(desc: broom.description);
            SetImage(image: image);
        }
        else{
            SetName(name: "??????????");
            SetDescription(desc: "???????");
            SetImage(image: null);
        }
    }

    public void SetName(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
    }

    public void SetDescription(string desc){
        textDesc.GetComponent<TMPro.TextMeshProUGUI>().text = $"{desc}";
    }

    public void SetImage(Texture image){
        if (image != null){
            emptyImage.SetActive(false);
            backgroundImageArea.SetActive(true);
            renderTexture.texture = image;
            return;
        }
        else {
            emptyImage.SetActive(true);
            backgroundImageArea.SetActive(false);
            renderTexture.texture = null;
        }
    }

    public void SetEmpty(bool status = false){
        SetName(name: "??????????");
        SetDescription(desc: "???????");
        SetImage(image: null);
    }

    public void SetSelected(bool status = false){
        selectedState.SetActive(status);
    }

    public void SetHighlighted(bool status = false){
        // imageItem.SetHighlighted(status: status);
    }

    public void SetDefault(bool status = false){
        selectedState.SetActive(false);
    }
    


    
}