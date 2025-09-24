using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class DialogueItem : MonoBehaviour
{
    // Content
    [TextArea] public string dialogueText = "Quest text here";

    // Resources
    // [SerializeField] private Image itemIcon;
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemControlText;
    [SerializeField] private TextMeshProUGUI itemText;


    public void UpdateText(
        string name,
        string text,
        string controlKey
    )
    {
        itemName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}:";
        itemText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        itemControlText.GetComponent<TMPro.TextMeshProUGUI>().text = controlKey;
    }

    public void UpdateUI()
    {

    }
}