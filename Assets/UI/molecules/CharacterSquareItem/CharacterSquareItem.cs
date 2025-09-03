using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CharacterSquareItem : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField][HideInInspector] private bool isHighlighted;
    [SerializeField][HideInInspector] private bool isDisabled;
    [SerializeField][HideInInspector] private bool isDefault;
    [SerializeField] private GameObject itemDefault;
    [SerializeField] private GameObject itemHighlighted;
    [SerializeField] private GameObject itemDisabled;


    public void CharacterSquareItemInit(
        string name,
        RawImage renderTexture,
        Image backgroundColor,
        bool isDefault = true,
        bool isHighlighted = false,
        bool isDisabled = false
    )
    {
        // Initialize the item state
        this.isDefault = isDefault;
        this.isHighlighted = isHighlighted;
        this.isDisabled = isDisabled;
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
}
