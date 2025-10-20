using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;


public class StoneSquareItem : MonoBehaviour
{
    // public Sprite icon;
    [Header("State")]
    [SerializeField][HideInInspector] private bool isHighlighted;
    [SerializeField][HideInInspector] private bool isDisabled;
    [SerializeField][HideInInspector] private bool isDefault;
    [SerializeField][HideInInspector] private bool isSelected;

    [Header("Game Objects")]
    [SerializeField] private GameObject itemDefault;
    [SerializeField] private GameObject itemSelected;
    [SerializeField] private GameObject itemHighlighted;
    [SerializeField] private GameObject itemDisabled;

    [Header("Info")]
    [SerializeField] public string stoneId;
    [SerializeField] private string title;

    [Header("Functions")]
    [SerializeField] public Action<string> OnSelect;
    


    public void Init(
        string title,
        string stoneId,
        Texture renderTexture,
        bool isDefault = true,
        bool isHighlighted = false,
        bool isDisabled = false,
        Action<string> OnSelect = null
    )
    {
        this.title = title;
        this.stoneId = stoneId;
        // Initialize the item state
        this.isDefault = isDefault;
        this.isHighlighted = isHighlighted;
        this.isDisabled = isDisabled;
        this.isSelected = false;
        this.OnSelect = OnSelect;
        itemDefault.GetComponent<StoneSquareItemState>().UpdateText(title);
        itemDefault.GetComponent<StoneSquareItemState>().UpdateImage(renderTexture);

        itemHighlighted.GetComponent<StoneSquareItemState>().UpdateText(title);
        itemHighlighted.GetComponent<StoneSquareItemState>().UpdateImage(renderTexture);

        itemSelected.GetComponent<StoneSquareItemState>().UpdateText(title);
        // itemSelected.GetComponent<StoneSquareItemState>().UpdateImage(renderTexture);

        itemDisabled.GetComponent<StoneSquareItemState>().UpdateText(title);
        itemDisabled.GetComponent<StoneSquareItemState>().UpdateImage(renderTexture);
    }

    public void UpdateUI()
    {

    }

    public void SetSelected(bool val)
    {
        isSelected = val;
        itemDisabled.SetActive(false);
        itemDefault.SetActive(!val);
        itemSelected.SetActive(val);
    }

    public void SetHighlighted(bool val)
    {
        isHighlighted = val;
        itemHighlighted.SetActive(val);
    }

    public void SetDisabled(bool val)
    {
        isDisabled = val;
        itemDisabled.SetActive(val);
        itemDefault.SetActive(!val);
        itemSelected.SetActive(false);
    }

    public void OnSelected()
    {
        OnSelect?.Invoke(stoneId);
    }
}
