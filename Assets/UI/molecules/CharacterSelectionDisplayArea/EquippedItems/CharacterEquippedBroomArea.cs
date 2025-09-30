using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class CharacterEquippedBroomArea : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] public EquippedBroomDisplayItem broomItem;
    [SerializeField] private GameObject editIndicator;
    
    public void Start()
    {

        
    }

    public void UpdateBroomInfo(
        CurlingBroom broom,
        RenderTexture renderTexture = null

    ){
        if (broom != null){
            broomItem.UpdateInfo(
                broom: broom,
                name: broom.title,
                desc: broom.description,
                image: renderTexture
            );
        }
        else {
            broomItem.ClearInfo();
        }
    }

    public void SetEmpty(bool status = false){
        broomItem.SetEmpty(status);
    }


    public void SetHighlighted(bool status = false){
        broomItem.SetHighlighted(status);
        
    }

    public void SetSelected(bool status = false){
        broomItem.SetSelected(status);
    }

    


    
}