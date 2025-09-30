using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class CharacterEquippedStoneArea : MonoBehaviour
{
    // public Sprite icon;
    [Header("Components")]
    [SerializeField] private EquippedStoneDisplayItem stoneItem;
    [SerializeField] private ListOfEquippedStoneDisplayItems listOfStoneItems;
    [SerializeField] private GameObject editIndicator;
    
    
    public void Start()
    {

        
    }


    public void SetEmpty(bool status = false, int index = 0){
        // stoneItem.SetEmpty(status: status, index: index);
    }

    public void UnSelectAll(){
        listOfStoneItems.SetSelectedIndex(index: -1);
    }


    public void SetHighlighted(bool status = false, int highlightedIndex = 0){
        listOfStoneItems.SetSelectedIndex(index: highlightedIndex);
    }

    public void SetSelected(bool status = false, int selectedIndex = 0){
        listOfStoneItems.SetSelectedIndex(index: selectedIndex);
    }

    public void UpdateStoneInfo(
        CurlingStone stone = null,
        int stoneIndex = 0,
        RenderTexture renderTexture = null
    ){
        if (stone != null){
            UpdateInfo(stone: stone, index: stoneIndex, renderTexture: renderTexture);
        }
    }

    public void UpdateAllStoneInfo(
        CurlingStone stone1 = null,
        CurlingStone stone2 = null,
        CurlingStone stone3 = null,
        CurlingStone stone4 = null,
        CurlingStone stone5 = null
    ){
        UpdateInfo(stone: stone1, index: 0);
        UpdateInfo(stone: stone2, index: 1);
        UpdateInfo(stone: stone3, index: 2);
        UpdateInfo(stone: stone4, index: 3);
        UpdateInfo(stone: stone5, index: 4);
    }

    public void UpdateInfo(
        CurlingStone stone,
        int index = 0,
        RenderTexture renderTexture = null
    ){
        listOfStoneItems.UpdateStoneInfo(stone: stone, index: index, renderTexture: renderTexture);
    }
    


    
}