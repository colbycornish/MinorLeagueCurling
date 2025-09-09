using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CharacterSquareItem : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField][HideInInspector] private bool isHighlighted;
    [SerializeField][HideInInspector] private bool isDisabled;
    [SerializeField][HideInInspector] private bool isDefault;
    [SerializeField][HideInInspector] private bool isSelected;
    [SerializeField] private GameObject itemDefault;
    [SerializeField] private GameObject itemSelected;
    [SerializeField] private GameObject itemHighlighted;
    [SerializeField] private GameObject itemDisabled;
    [SerializeField] private string characterId;
    [SerializeField] private string characterName;


    public void Init(
        string name,
        string characterId,
        RenderTexture renderTexture,
        bool isDefault = true,
        bool isHighlighted = false,
        bool isDisabled = false
    )
    {
        this.characterName = name;
        this.characterId = characterId;
        // Initialize the item state
        this.isDefault = isDefault;
        this.isHighlighted = isHighlighted;
        this.isDisabled = isDisabled;
        this.isSelected = false;
        itemDefault.GetComponent<CharacterSquareItemState>().UpdateText(name);
        itemDefault.GetComponent<CharacterSquareItemState>().UpdateFace(renderTexture);

        itemHighlighted.GetComponent<CharacterSquareItemState>().UpdateText(name);
        itemHighlighted.GetComponent<CharacterSquareItemState>().UpdateFace(renderTexture);

        itemDisabled.GetComponent<CharacterSquareItemState>().UpdateText(name);
        itemDisabled.GetComponent<CharacterSquareItemState>().UpdateFace(renderTexture);
    }

    public void UpdateUI()
    {

    }

    public void SetSelected(bool val)
    {
        itemSelected.SetActive(val);
    }

    public void SetHighlighted(bool val)
    {
        itemDisabled.SetActive(false);
        itemDefault.SetActive(!val);
        itemHighlighted.SetActive(val);
    }

    public void SetDisabled(bool val)
    {
        itemDisabled.SetActive(val);
        itemDefault.SetActive(false);
        itemHighlighted.SetActive(false);
    }
}
