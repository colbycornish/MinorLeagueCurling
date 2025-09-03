using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterStatItem : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private TextMeshProUGUI textStatTitle;
    [SerializeField] private int statValue;
    [SerializeField] private int statValueMax;
    [SerializeField] private int statValueMin;
    [SerializeField] private bool useIcon;
    [SerializeField] private Sprite iconImage;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject barFill;

    public void UpdateText(string name)
    {
        textStatTitle.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}:";
    }


    public void UpdateUI()
    {

    }
}