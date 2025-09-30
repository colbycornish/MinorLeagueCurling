using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ListOfEquippedStoneDisplayItems : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private GameObject listArea;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField][HideInInspector] private int highlightedIndex = 0;
    [SerializeField][HideInInspector] private int itemCount = 0;
    [SerializeField] public Action<string> OnSelect;

    public void Init(
        Action<string> OnSelect = null
    )
    {
        this.OnSelect = OnSelect;
    }

    public void UpdateUI()
    {

    }




    public void SetSelectedIndex(int index = 0){
        int currentIndex = 0;
        foreach(Transform item in listArea.transform){
            EquippedStoneDisplayItem controller = item.GetComponent<EquippedStoneDisplayItem>();
            if (currentIndex == index){
                controller.SetSelected(status: true);
            }
            else {
                controller.SetSelected(status: false);
            }
            currentIndex += 1;
        }

    }

    public void SetHighlightedIndex(int index = 0){
        int currentIndex = 0;
        foreach(Transform item in listArea.transform){
            EquippedStoneDisplayItem controller = item.GetComponent<EquippedStoneDisplayItem>();
            if (currentIndex == index){
                controller.SetHighlighted(status: true);
            }
            else {
                controller.SetHighlighted(status: false);
            }
            currentIndex += 1;
        }
        
    }

    public void UpdateStoneInfo(
        CurlingStone stone = null, 
        int index = 0,
        RenderTexture renderTexture = null
    ){
        int currentIndex = 0;
        foreach(Transform item in listArea.transform){
            EquippedStoneDisplayItem controller = item.GetComponent<EquippedStoneDisplayItem>();
            if (currentIndex == index){
                controller.UpdateInfo(
                    stone: stone,
                    image: renderTexture
                );
            }

            // if (currentIndex == index){
            //     controller.SetHighlighted(status: true);
            // }
            // else {
            //     controller.SetHighlighted(status: false);
            // }
            currentIndex += 1;
        }
    }

    public void HighlightNext()
    {
        if (highlightedIndex < itemCount)
        {
            highlightedIndex++;
            UpdateHighlightedDisplay();
        }
    }

    public void HighlightPrev()
    {
        if (highlightedIndex > 0)
        {
            highlightedIndex--;
            UpdateHighlightedDisplay(); 
        }
    }

    public void UpdateHighlightedDisplay()
    {
        // int currentIndex = 0;
        // foreach (Transform child in listArea.transform)
        // {
        //     EquippedStoneDisplayItem controller = child.GetComponent<EquippedStoneDisplayItem>();
        //     if (currentIndex == highlightedIndex)
        //     {
        //         controller.SetHighlighted(true);
        //     }
        //     else
        //     {
        //         controller.SetHighlighted(false);
        //     }
        //     currentIndex++;
        // }
    }

    public void UpdateSelectedDisplay(
        List<string> selectedIds = null
    )
    {
        // foreach (Transform child in listArea.transform)
        // {
        //     EquippedStoneDisplayItem controller = child.GetComponent<EquippedStoneDisplayItem>();
        //     if (selectedIds != null && selectedIds.Contains(controller.stoneId))
        //     {
        //         controller.SetSelected(true);
        //     }
        //     else
        //     {
        //         controller.SetSelected(false);
        //     }
        // }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coursePrefabs"></param>
    public void BuildList(
        GameObject[] stoneItemPrefabs
    )
    {
        // foreach (Transform child in listArea.transform)
        // {
        //     GameObject.Destroy(child.gameObject);
        // }

        // foreach (GameObject coursePrefab in coursePrefabs)
        // {
        //     EquippedStoneDisplayItem c = coursePrefab.GetComponent<EquippedStoneDisplayItem>();

        //     GameObject listItem = Instantiate(listItemPrefab, listArea.transform);
        //     EquippedStoneDisplayItem controller = listItem.GetComponent<EquippedStoneDisplayItem>();
        //     listItem.transform.SetParent(this.transform);

        //     // RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetCharacterRenderTextureById(
        //     //     characterId: c.id,
        //     //     getFace: true,
        //     //     getBody: false
        //     // );

        //     controller.Init(
        //         title: c.title,
        //         courseId: c.id,
        //         renderTexture: c.thumbnail,
        //         OnSelect: (string courseId) => { OnSelection(courseId); }
        //     );
        //     // controller.UpdateUI();
        // }
        // itemCount = coursePrefabs.Length - 1;
        // UpdateHighlightedDisplay();
    }

    public void OnSelection(string stoneId)
    {
        Debug.Log("loci: {stoneId}");
        if (OnSelect != null)
        {
            OnSelect?.Invoke(stoneId);
        }
    }


    /// <summary>
    /// Helper
    /// </summary>
    public void ClearList()
    {
        // foreach (Transform child in listArea.transform)
        // {
        //     GameObject.Destroy(child.gameObject);
        // }
    }
    

    public string GetHighlightedChildCourseId()
    {
        // int currentIndex = 0;
        // foreach (Transform child in listArea.transform)
        // {
        //     CourseItem controller = child.GetComponent<CourseItem>();
        //     if (currentIndex == highlightedIndex)
        //     {
        //         return controller.courseId;
        //     }
        //     currentIndex++;
        // }
        return null;
    }


}
