using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CharacterFullStatBox : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPosition;
    [SerializeField] private TextMeshProUGUI textDescription;
    
    [SerializeField] private CharacterStatItem stat1;
    [SerializeField] private CharacterStatItem stat2;
    [SerializeField] private CharacterStatItem stat3;


    public void CharacterFullStatBoxInit(
        string name,
        string position,
        string description
    )
    {
        textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}:";
        textPosition.GetComponent<TMPro.TextMeshProUGUI>().text = position;
        textDescription.GetComponent<TMPro.TextMeshProUGUI>().text = description;
        
    }

    public void UpdateUI()
    {

    }
}
