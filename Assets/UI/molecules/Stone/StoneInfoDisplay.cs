using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class StoneInfoDisplay : MonoBehaviour
{
    // public Sprite icon;
    [Header("Basic Info")]
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textDescription;

    

    public void UpdateUI()
    {

    }

    public void UpdateInfo(
        string title = null,
        string description = null,
        CurlingStone stone = null
    )
    {
        if (stone != null)
        {
            UpdateName(stone.title);
            UpdateAbility(stone.description);
            
        }
        else
        {
            if (title != null) { UpdateName(title); }
            if (description != null) { UpdateAbility(description); }
        }
    }

    public void UpdateName(string text)
    {
        if (textTitle != null)
        {
            textTitle.GetComponent<TMPro.TextMeshProUGUI>().text = $"{text}";
        }
    }


    public void UpdateAbility(string text)
    {
        if (textDescription != null)
        {
            textDescription.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        }
    }
}
