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
            characterObject: CurlingPreGameSetupManager._instance.selectedLeftSweeper
        );

        UpdateDisplayItem(
            displayItem: throwerItem,
            characterObject: CurlingPreGameSetupManager._instance.selectedThrower
        );

        UpdateDisplayItem(
            displayItem: rightSweeperItem,
            characterObject: CurlingPreGameSetupManager._instance.selectedRightSweeper
        );
    }

    public void UpdateDisplayItem(
        CharacterFullDisplayItem displayItem,
        GameObject characterObject = null
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
