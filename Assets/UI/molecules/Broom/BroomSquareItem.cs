using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;


public class BroomSquareItem : MonoBehaviour
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
    [SerializeField] public string broomId;
    [SerializeField] private string broomName;

    [Header("Functions")]
    [SerializeField] public Action<string> OnSelect;
    


    public void Init(
        string name,
        string broomId,
        RenderTexture renderTexture,
        bool isDefault = true,
        bool isHighlighted = false,
        bool isDisabled = false,
        Action<string> OnSelect = null
    )
    {
        this.broomName = name;
        this.broomId = broomId;
        // Initialize the item state
        this.isDefault = isDefault;
        this.isHighlighted = isHighlighted;
        this.isDisabled = isDisabled;
        this.isSelected = false;
        this.OnSelect = OnSelect;
        itemDefault.GetComponent<BroomSquareItemState>().UpdateText(name);
        itemDefault.GetComponent<BroomSquareItemState>().UpdateFace(renderTexture);

        itemHighlighted.GetComponent<BroomSquareItemState>().UpdateText(name);
        itemHighlighted.GetComponent<BroomSquareItemState>().UpdateFace(renderTexture);

        itemDisabled.GetComponent<BroomSquareItemState>().UpdateText(name);
        itemDisabled.GetComponent<BroomSquareItemState>().UpdateFace(renderTexture);
    }

    public void UpdateUI()
    {

    }

    public void SetSelected(bool val)
    {
        isSelected = val;
        itemSelected.SetActive(val);
    }

    public void SetHighlighted(bool val)
    {
        itemDisabled.SetActive(false);
        isDisabled = false;

        itemDefault.SetActive(!val);
        itemHighlighted.SetActive(val);
    }

    public void SetDisabled(bool val)
    {
        itemDisabled.SetActive(val);
        isDisabled = val;

        itemDefault.SetActive(!val);
        
        itemHighlighted.SetActive(false);
        isHighlighted = false;
    }

    public void OnSelected()
    {
        OnSelect?.Invoke(broomId);
    }
}
