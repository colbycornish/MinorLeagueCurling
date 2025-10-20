using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class CharacterEquipmentArea : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    // [SerializeField] public CharacterEquippedBroomArea broomArea;
    [SerializeField] public GameObject broomArea;
    [SerializeField] public CharacterEquippedStoneArea stoneArea;
    [SerializeField] public EquippedBroomDisplayItemV2 equippedBroomItem;

    [Header("Visual Display Settings")]
    [SerializeField] public bool useBrooms = false;
    [SerializeField] public bool useStones = false;

    [Header("Settings")]
    [SerializeField] public int selectedIndex = 0;
    [SerializeField] public bool isSelected = false;
    [SerializeField] public bool isHighlighted = false;
    
    
    public void Start()
    {

        
    }


    public void UpdateStoneInfo(
        CurlingStone stone = null,
        int stoneIndex = 0,
        RenderTexture renderTexture = null
    ){
        if (stone != null){
            stoneArea.UpdateStoneInfo(
                stone: stone, 
                stoneIndex: stoneIndex,
                renderTexture: renderTexture
            );
        }
    }

    public void UpdateBroomInfo(
        CurlingBroom broom = null,
        RenderTexture renderTexture = null
    ){
        if (broom != null){
            equippedBroomItem.UpdateInfo(
                broom: broom,
                name: broom.title,
                desc: broom.description,
                image: renderTexture
            );
        }
    }


    public void SetSelected(bool status = false, int selectedIndex = 0){
        if (useBrooms == true){
            SetSelectedBroomArea(status: status);
        } else if (useStones){
            SetSelectedStoneArea(status: status, selectedIndex: selectedIndex);
        }
    }

    private void SetSelectedBroomArea(bool status = false){
        equippedBroomItem.SetSelected(status: status);
        // broomArea.SetSelected(status: status);
    }

    private void SetSelectedStoneArea(bool status = false, int selectedIndex = 0){
        stoneArea.SetSelected(
            status: status,
            selectedIndex: selectedIndex
        );
    }


    public void SetHighlighted(bool status = false, int highlightedIndex = 0){
        if (useBrooms == true){
            SetHighlightedBroomArea(status: status);
        } else if (useStones){
            SetHighlightedStoneArea(highlightedIndex: highlightedIndex);
        }
    }

    private void SetHighlightedBroomArea(bool status = false){
        equippedBroomItem.SetHighlighted(status: status);
        // broomArea.SetHighlighted(status: status);
    }

    private void SetHighlightedStoneArea(bool status = false, int highlightedIndex = 0){
        stoneArea.SetHighlighted(
            status: status,
            highlightedIndex: highlightedIndex
        );
    }

    
    


    
}