using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class EquippedStoneDisplayItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private EquippedStoneDisplayItemState empty;
    [SerializeField] private EquippedStoneDisplayItemState highlighted;
    [SerializeField] private EquippedStoneDisplayItemState selected;
    [SerializeField] private string stoneId = "";

    [Header("Settings")]
    [SerializeField] public bool isEmpty = true;
    [SerializeField] public bool isHighlighted = false;
    
    public void Start()
    {

        
    }

    public void Init(){

    }

    public void UpdateInfo(CurlingStone stone){

    }

    public void SetEmpty(bool status = false){
        isEmpty = status;
        empty.gameObject.SetActive(status);
        if (status == true){
            SetHighlighted(!status);
        }
    }

    public void SetHighlighted(bool status = false){
        isHighlighted = status;
        highlighted.gameObject.SetActive(status);
    }

    public void SetSelected(bool status = false){
        isHighlighted = status;
        highlighted.gameObject.SetActive(status);
    }
    


    
}