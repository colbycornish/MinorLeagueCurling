using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class SelectedDisplayArea : MonoBehaviour
{
    // public Sprite icon;
    [Header("Game Objects")]
    [SerializeField] public CharacterFullDisplayItem leftSweeperItem;
    [SerializeField] public CharacterFullDisplayItem throwerItem;
    [SerializeField] public CharacterFullDisplayItem rightSweeperItem;
    [SerializeField] public int currentIndex = 0;
    // Time in seconds to complete shrinkage


    void Start()
    {
         
    }


    public void UpdateSelectionDisplays()
    {
        UpdateDisplayItem(
            displayItem: leftSweeperItem,
            characterObject: CurlingPreGameSetupManager._instance.selectedLeftSweeper,
            broomObject: CurlingPreGameSetupManager._instance.selectedLeftSweeperBroom
        );

        UpdateDisplayItem(
            displayItem: throwerItem,
            characterObject: CurlingPreGameSetupManager._instance.selectedThrower,
            stoneObject1: CurlingPreGameSetupManager._instance.selectedStone1,
            stoneObject2: CurlingPreGameSetupManager._instance.selectedStone2,
            stoneObject3: CurlingPreGameSetupManager._instance.selectedStone3,
            stoneObject4: CurlingPreGameSetupManager._instance.selectedStone4,
            stoneObject5: CurlingPreGameSetupManager._instance.selectedStone5
        );

        UpdateDisplayItem(
            displayItem: rightSweeperItem,
            characterObject: CurlingPreGameSetupManager._instance.selectedRightSweeper,
            broomObject: CurlingPreGameSetupManager._instance.selectedRightSweeperBroom
        );
    }

    public void UpdateDisplayItem(
        CharacterFullDisplayItem displayItem,
        GameObject characterObject = null,
        GameObject broomObject = null,
        GameObject stoneObject1 = null,
        GameObject stoneObject2 = null,
        GameObject stoneObject3 = null,
        GameObject stoneObject4 = null,
        GameObject stoneObject5 = null
    )
    {
        if (characterObject != null)
        {
            Character c = characterObject.GetComponent<Character>();
            if (c != null)
            {
                RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetCharacterRenderTextureById(
                    characterId: c.id,
                    getFace: false,
                    getBody: true
                );
                displayItem.UpdateRenderTexture(
                    renderTexture: renderTexture
                );
                displayItem.UpdateInfo(
                    character: c
                );
            }
        }
        else
        {
            RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetRandomCharacterRenderTexture(
                getFace: false,
                getBody: true
            );
            displayItem.UpdateRenderTexture(
                renderTexture: renderTexture
            );

            displayItem.SetAsUnSelected();
        }

        if (broomObject != null)
        {
            UpdateDisplayItemBroom(
                displayItem: displayItem,
                broomObject: broomObject
            );
        }
        // else {
        //     displayItem.ClearEquipmentInfo();
        // }

        if (stoneObject1 != null)
        {
            UpdateDisplayItemStone(displayItem: displayItem, stoneObject: stoneObject1, index: 0);
        }
        if (stoneObject2 != null)
        {
            UpdateDisplayItemStone(displayItem: displayItem, stoneObject: stoneObject2, index: 1);
        }
        if (stoneObject3 != null)
        {
            UpdateDisplayItemStone(displayItem: displayItem, stoneObject: stoneObject3, index: 2);
        }
        if (stoneObject4 != null)
        {
            UpdateDisplayItemStone(displayItem: displayItem, stoneObject: stoneObject4, index: 3);
        }
        if (stoneObject5 != null)
        {
            UpdateDisplayItemStone(displayItem: displayItem, stoneObject: stoneObject5, index: 4);
        }

        
    }

    public void UpdateDisplayItemBroom(
        CharacterFullDisplayItem displayItem,
        GameObject broomObject = null
    ){
        if (broomObject != null)
        {
            CurlingBroom b = broomObject.GetComponent<CurlingBroom>();
            if (b != null)
            {
                RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetBroomRenderTextureById(
                    broomId: b.id,
                    getFace: true,
                    getBody: false
                );

                displayItem.UpdateEquipmentInfo(
                    broom: b,
                    renderTexture: renderTexture
                );
            }
        }
        // else {
        //     displayItem.ClearEquipmentInfo();
        // }
    }

    public void UpdateDisplayItemStone(
        CharacterFullDisplayItem displayItem,
        GameObject stoneObject = null,
        int index = 0
    ){
        if (stoneObject != null)
        {
            CurlingStone s = stoneObject.GetComponent<CurlingStone>();
            if (s != null)
            {
                RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetStoneRenderTextureById(
                    stoneId: s.id,
                    getFace: true,
                    getBody: false
                );
                
                displayItem.UpdateEquipmentInfo(
                    stone: s,
                    stoneIndex: index,
                    renderTexture: renderTexture
                );
            }
        }
        // else {
        //     displayItem.ClearEquipmentInfo(index);
        // }
    }

    




    // public void ChangeSelection(int modifyIndex)
    // {
    //     int newIndex = currentIndex + modifyIndex;
    //     if (newIndex < 0 || newIndex > 2) return;
    //     else
    //     {
    //         currentIndex = newIndex;
    //         switch (currentIndex)
    //         {
    //             case 0:
    //                 leftSweeperItem.SetHighlighted(status: true);
    //                 StartCoroutine(leftSweeperItem.Expand());
    //                 StartCoroutine(throwerItem.Shrink());
    //                 throwerItem.SetHighlighted(status: false);
    //                 break;
    //             case 1:
    //                 if (modifyIndex > 0)
    //                 {
    //                     StartCoroutine(leftSweeperItem.Shrink());
    //                     leftSweeperItem.SetHighlighted(status: false);
    //                 }
    //                 StartCoroutine(throwerItem.Expand());
    //                 throwerItem.SetHighlighted(status: true);;
    //                 if (modifyIndex < 0)
    //                 {
    //                     StartCoroutine(rightSweeperItem.Shrink());
    //                     rightSweeperItem.SetHighlighted(status: false);
    //                 }
    //                 break;
    //             case 2:
    //                 StartCoroutine(throwerItem.Shrink());
    //                 throwerItem.SetHighlighted(status: false);
    //                 StartCoroutine(rightSweeperItem.Expand());
    //                 rightSweeperItem.SetHighlighted(status: true);
    //                 break;
    //             default:
    //                 break;
    //         }
    //     }
    // }
}
