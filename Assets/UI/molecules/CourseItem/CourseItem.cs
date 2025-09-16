using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;


public class CourseItem : MonoBehaviour
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
    [SerializeField] public string courseId;
    [SerializeField] private string name;

    [Header("Functions")]
    [SerializeField] public Action<string> OnSelect;
    


    public void Init(
        string name,
        string courseId,
        Texture renderTexture,
        bool isDefault = true,
        bool isHighlighted = false,
        bool isDisabled = false,
        Action<string> OnSelect = null
    )
    {
        this.name = name;
        this.courseId = courseId;
        // Initialize the item state
        this.isDefault = isDefault;
        this.isHighlighted = isHighlighted;
        this.isDisabled = isDisabled;
        this.isSelected = false;
        this.OnSelect = OnSelect;
        itemDefault.GetComponent<CourseItemState>().UpdateText(name);
        itemDefault.GetComponent<CourseItemState>().UpdateImage(renderTexture);

        itemHighlighted.GetComponent<CourseItemState>().UpdateText(name);
        itemHighlighted.GetComponent<CourseItemState>().UpdateImage(renderTexture);

        itemSelected.GetComponent<CourseItemState>().UpdateText(name);
        itemSelected.GetComponent<CourseItemState>().UpdateImage(renderTexture);

        itemDisabled.GetComponent<CourseItemState>().UpdateText(name);
        itemDisabled.GetComponent<CourseItemState>().UpdateImage(renderTexture);
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
        OnSelect?.Invoke(courseId);
    }
}
