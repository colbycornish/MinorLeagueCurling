using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedBroomDisplayItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private EquippedBroomDisplayImageItem imageItem;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDesc;

    [Header("Settings")]
    [SerializeField] public bool hasInfo = false;
    [SerializeField] public bool isHighlighted = false;

    
    public void Start()
    {

        
    }

    public void Init(){
        
    }

    public void SetInfo(){
        
    }

    public void ClearInfo(){
        
    }

    public void UpdateInfo(
        CurlingBroom broom,
        string name,
        string desc,
        Texture image
    ){
        SetName(name);
        SetDescription(desc);
        SetImage(image);
    }

    public void SetName(string name){
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}";
    }

    public void SetDescription(string desc){
        textDesc.GetComponent<TMPro.TextMeshProUGUI>().text = $"{desc}";
    }

    public void SetImage(Texture image){
        imageItem.SetImage(image);
    }

    public void SetEmpty(bool status = false){
        imageItem.SetEmpty(status: status);
    }

    public void SetSelected(bool status = false){
        imageItem.SetSelected(status: status);
    }

    public void SetHighlighted(bool status = false){
        imageItem.SetHighlighted(status: status);
    }
    


    
}