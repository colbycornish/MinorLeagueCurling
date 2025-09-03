using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class SectionTeamSelection : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] public CharacterSelectionDisplayArea characterSelectionDisplayArea;
    [SerializeField] public SelectedDisplayArea selectedDisplayArea;
    // Time in seconds to complete shrinkage


    void Start()
    {
        StartCoroutine(selectedDisplayArea.leftSweeperItem.Expand());
    }

    //
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            selectedDisplayArea.ChangeSelection(-1);
            characterSelectionDisplayArea.ChangeSelection(-1);
            
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            selectedDisplayArea.ChangeSelection(1);
            characterSelectionDisplayArea.ChangeSelection(1);
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
    //                 StartCoroutine(leftSweeperItem.Expand());
    //                 StartCoroutine(throwerItem.Shrink());
    //                 // StartCoroutine(rightSweeperItem.Shrink());
    //                 break;
    //             case 1:
    //                 if (modifyIndex > 0){
    //                     StartCoroutine(leftSweeperItem.Shrink());
    //                 }
    //                 StartCoroutine(throwerItem.Expand());
    //                 if (modifyIndex < 0)
    //                 {
    //                     StartCoroutine(rightSweeperItem.Shrink());
    //                 }
    //                 break;
    //             case 2:
    //                 // StartCoroutine(leftSweeperItem.Shrink());
    //                 StartCoroutine(throwerItem.Shrink());
    //                 StartCoroutine(rightSweeperItem.Expand());
    //                 break;
    //             default:
    //                 break;
    //         }
    //     }
    // }

   
}
