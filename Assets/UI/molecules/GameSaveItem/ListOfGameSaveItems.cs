using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ListOfGameSaveItems : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private GameObject listArea;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField] private GameObject listItemEmptyPrefab;
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
        int currentIndex = 0;
        foreach (Transform child in listArea.transform)
        {
            GameSaveItem controller = child.GetComponent<GameSaveItem>();
            if (currentIndex == highlightedIndex)
            {
                controller.SetHighlighted(true);
            }
            else
            {
                controller.SetHighlighted(false);
            }
            currentIndex++;
        }
    }

    public void UpdateSelectedDisplay(
        List<string> selectedIds = null
    )
    {
        foreach (Transform child in listArea.transform)
        {
            GameSaveItem controller = child.GetComponent<GameSaveItem>();
            if (selectedIds != null && selectedIds.Contains(controller.gameSaveId))
            {
                controller.SetSelected(true);
            }
            else
            {
                controller.SetSelected(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coursePrefabs"></param>
    public void BuildList(
        GameObject[] gameSavePrefabs
    )
    {
        foreach (Transform child in listArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (GameObject gameSavePrefab in gameSavePrefabs)
        {
            // GameSaveData c = gameSavePrefab.GetComponent<GameSaveData>();

            GameObject listItem = Instantiate(listItemPrefab, listArea.transform);
            GameSaveItem controller = listItem.GetComponent<GameSaveItem>();
            listItem.transform.SetParent(this.transform);

            // RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetCharacterRenderTextureById(
            //     characterId: c.id,
            //     getFace: true,
            //     getBody: false
            // );

            // controller.Init(
            //     title: c.title,
            //     courseId: c.id,
            //     renderTexture: c.thumbnail,
            //     OnSelect: (string courseId) => { OnSelection(courseId); }
            // );
            // controller.UpdateUI();
        }
        itemCount = gameSavePrefabs.Length - 1;
        UpdateHighlightedDisplay();
    }

    public void OnSelection(string courseId)
    {
        Debug.Log("loci: {courseId}");
        if (OnSelect != null)
        {
            OnSelect?.Invoke(courseId);

        }
    }


    /// <summary>
    /// Helper
    /// </summary>
    public void ClearList()
    {
        foreach (Transform child in listArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    

    public string GetHighlightedChildId()
    {
        int currentIndex = 0;
        foreach (Transform child in listArea.transform)
        {
            GameSaveItem controller = child.GetComponent<GameSaveItem>();
            if (currentIndex == highlightedIndex)
            {
                return controller.gameSaveId;
            }
            currentIndex++;
        }
        return null;
    }


}
