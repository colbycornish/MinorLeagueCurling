using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ListOfBroomSquareItems : MonoBehaviour
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

    public string GetSelectedChildCharacterId()
    {
        int currentIndex = 0;
        foreach (Transform child in listArea.transform)
        {
            BroomItem controller = child.GetComponent<BroomItem>();
            if (currentIndex == selectedIndex)
            {
                return controller.broomId;
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
            BroomItem controller = child.GetComponent<BroomItem>();
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
            BroomItem controller = child.GetComponent<BroomItem>();
            if (higlighedIds != null && higlighedIds.Contains(controller.broomId))
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
    /// <param name="characterPrefabs"></param>
    public void BuildList(
        GameObject[] broomPrefabs
    )
    {
        foreach (Transform child in listArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (GameObject broomPrefab in broomPrefabs)
        {
            CurlingBroom c = broomPrefab.GetComponent<CurlingBroom>();

            GameObject listItem = Instantiate(listItemPrefab, listArea.transform);
            BroomItem controller = listItem.GetComponent<BroomItem>();
            listItem.transform.SetParent(this.transform);

            RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetBroomRenderTextureById(
                broomId: c.id,
                getFace: true,
                getBody: false
            );

            controller.Init(
                name: c.title,
                broomId: c.id,
                renderTexture: renderTexture,
                OnSelect: (string broomId) => { OnSelection(broomId); }
            );
            // controller.UpdateUI();
        }
        itemCount = broomPrefabs.Length - 1;
        UpdateSelectionDisplay();
    }

    public void OnSelection(string broomId)
    {
        if (OnSelect != null)
        {
            OnSelect?.Invoke(broomId);
            
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
