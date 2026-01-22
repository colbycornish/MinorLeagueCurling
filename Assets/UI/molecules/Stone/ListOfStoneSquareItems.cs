using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ListOfStoneSquareItems : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private GameObject listArea;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField][HideInInspector] private int selectedIndex = 0;
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
        if (selectedIndex < itemCount)
        {
            selectedIndex++;
            UpdateSelectionDisplay();
        }
    }

    public void HighlightPrev()
    {
        if (selectedIndex > 0)
        {
            selectedIndex--;
            UpdateSelectionDisplay();
        }
    }

    public string GetSelectedChildStoneId()
    {
        int currentIndex = 0;
        foreach (Transform child in listArea.transform)
        {
            StoneSquareItem controller = child.GetComponent<StoneSquareItem>();
            if (currentIndex == selectedIndex)
            {
                return controller.stoneId;
            }
            currentIndex++;
        }
        return null;
    }

    public void UpdateSelectionDisplay()
    {
        int currentIndex = 0;
        foreach (Transform child in listArea.transform)
        {
            StoneSquareItem controller = child.GetComponent<StoneSquareItem>();
            if (currentIndex == selectedIndex)
            {
                controller.SetSelected(true);
            }
            else
            {
                controller.SetSelected(false);
            }
            currentIndex++;
        }
    }

    public void UpdateHighlightedDisplay(
        List<string> higlighedIds = null
    )
    {
        foreach (Transform child in listArea.transform)
        {
            StoneSquareItem controller = child.GetComponent<StoneSquareItem>();
            if (higlighedIds != null && higlighedIds.Contains(controller.stoneId))
            {
                controller.SetHighlighted(true);
            }
            else {
                controller.SetHighlighted(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void BuildList(
        CurlingStone[] stonePrefabs
    )
    {
        foreach (Transform child in listArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (CurlingStone stone in stonePrefabs)
        {
            // CurlingStone stone = stonePrefab.GetComponent<CurlingStone>();

            GameObject listItem = Instantiate(listItemPrefab, listArea.transform);
            StoneSquareItem controller = listItem.GetComponent<StoneSquareItem>();
            listItem.transform.SetParent(this.transform);

            // RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetStoneRenderTextureById(
            //     stoneId: stone.id,
            //     getFace: true,
            //     getBody: false
            // );

            controller.Init(
                title: stone.title,
                stoneId: stone.id,
                renderTexture: stone.avatarImage, //renderTexture,
                OnSelect: (string stoneId) => { OnSelection(stoneId); }
            );
            // controller.UpdateUI();
        }
        itemCount = stonePrefabs.Length - 1;
        UpdateSelectionDisplay();
    }

    public void OnSelection(string stoneId)
    {
        if (OnSelect != null)
        {
            OnSelect?.Invoke(stoneId);
            
        }
    }

    public void ClearList()
    {
        foreach (Transform child in listArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }


}
