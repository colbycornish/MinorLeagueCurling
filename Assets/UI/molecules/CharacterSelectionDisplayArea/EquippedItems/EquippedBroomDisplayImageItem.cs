using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedBroomDisplayImageItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private EquippedBroomDisplayImageItemState empty;
    [SerializeField] private EquippedBroomDisplayImageItemState highlighted;
    [SerializeField] private EquippedBroomDisplayImageItemState selected;

    [Header("Settings")]
    [SerializeField] public bool hasInfo = false;
    [SerializeField] public bool isHighlighted = false;

    
    public void Start()
    {

        
    }

    public void Init(){
        
    }

    // public void SetName(string name){
    //     empty.UpdateName(name);
    //     highlighted.UpdateName(name);
    //     selected.UpdateName(name);
    // }

    // public void SetDescription(string desc){
    //     empty.UpdateDesc(desc);
    //     highlighted.UpdateDesc(desc);
    //     selected.UpdateDesc(desc);
    // }

    public void SetImage(Texture image){
        // empty.UpdateImage(image);
        highlighted.UpdateImage(image);
        selected.UpdateImage(image);
    }

    public void SetEmpty(bool status = false){
        empty.gameObject.SetActive(status);
        // highlighted.gameObject.SetActive(false);
    }

    public void SetHighlighted(bool status = false){
        // empty.gameObject.SetActive(false);
        highlighted.gameObject.SetActive(status);
    }

    public void SetSelected(bool status = false){
        // empty.gameObject.SetActive(false);
        // highlighted.gameObject.SetActive(true);
        selected.gameObject.SetActive(status);
    }


    


    
}

