using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CharacterSelectionDisplayArea : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] public CharacterInfoDisplayItem leftSweeperItem;
    [SerializeField] public CharacterInfoDisplayItem throwerItem;
    [SerializeField] public CharacterInfoDisplayItem rightSweeperItem;
    [SerializeField] public int currentIndex = 0;
    // Time in seconds to complete shrinkage


    void Start()
    {
         
    }

    //
    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     ChangeSelection(-1);
            
        // }
        // if (Input.GetKeyDown(KeyCode.RightShift))
        // {
        //     ChangeSelection(1);
            
        // }
    }

    public void ChangeSelection(int modifyIndex)
    {
        int newIndex = currentIndex + modifyIndex;
        if (newIndex < 0 || newIndex > 2) return;
        else
        {
            currentIndex = newIndex;
            switch (currentIndex)
            {
                case 0:
                    StartCoroutine(leftSweeperItem.Expand());
                    StartCoroutine(throwerItem.Shrink());
                    // StartCoroutine(rightSweeperItem.Shrink());
                    break;
                case 1:
                    if (modifyIndex > 0){
                        StartCoroutine(leftSweeperItem.Shrink());
                    }
                    StartCoroutine(throwerItem.Expand());
                    if (modifyIndex < 0)
                    {
                        StartCoroutine(rightSweeperItem.Shrink());
                    }
                    break;
                case 2:
                    // StartCoroutine(leftSweeperItem.Shrink());
                    StartCoroutine(throwerItem.Shrink());
                    StartCoroutine(rightSweeperItem.Expand());
                    break;
                default:
                    break;
            }
        }
    }

   
}
