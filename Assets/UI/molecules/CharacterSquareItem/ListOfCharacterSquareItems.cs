using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ListOfCharacterSquareItems : MonoBehaviour
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
            CharacterSquareItem controller = child.GetComponent<CharacterSquareItem>();
            if (currentIndex == selectedIndex)
            {
                return controller.characterId;
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
            CharacterSquareItem controller = child.GetComponent<CharacterSquareItem>();
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
            CharacterSquareItem controller = child.GetComponent<CharacterSquareItem>();
            if (higlighedIds != null && higlighedIds.Contains(controller.characterId))
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
        GameObject[] characterPrefabs
    )
    {
        foreach (Transform child in listArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (GameObject characterPrefab in characterPrefabs)
        {
            Character c = characterPrefab.GetComponent<Character>();

            GameObject listItem = Instantiate(listItemPrefab, listArea.transform);
            CharacterSquareItem controller = listItem.GetComponent<CharacterSquareItem>();
            listItem.transform.SetParent(this.transform);

            RenderTexture renderTexture = CanvasManager._instance.canvasDisplayAreaController.GetCharacterRenderTextureById(
                characterId: c.id,
                getFace: true,
                getBody: false
            );

            controller.Init(
                name: c.fullName,
                characterId: c.id,
                renderTexture: renderTexture,
                OnSelect: (string characterId) => { OnSelection(characterId); }
            );
            // controller.UpdateUI();
        }
        itemCount = characterPrefabs.Length - 1;
        UpdateSelectionDisplay();
    }

    public void OnSelection(string characterId)
    {
        if (OnSelect != null)
        {
            OnSelect?.Invoke(characterId);
            
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
