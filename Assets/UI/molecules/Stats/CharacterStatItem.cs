using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterStatItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI textStatTitle;
    [SerializeField] StatProgressBar statBar;
    [SerializeField] private Sprite iconImage;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject barFill;

    [Header("Values")]
    [SerializeField] private int statValue;
    [SerializeField] private int statValueMax;
    [SerializeField] private int statValueMin;

    [Header("Settings")]
    [SerializeField] private bool useIcon;
    [SerializeField] private bool useTextTitle;


    void Start()
    {
        

    }

    public void Init(
        string name = "Stat",
        int statValue = 50,
        int statValueMin = 0,
        int statValueMax = 100,
        Sprite iconImage = null,
        bool useIcon = false
    )
    {
        this.statValue = statValue;
        this.statValueMin = statValueMin;
        this.statValueMax = statValueMax;
        this.iconImage = iconImage;
        this.useIcon = useIcon;

        if (iconImage != null && useIcon)
        {
            icon.GetComponent<Image>().sprite = iconImage;
            icon.SetActive(true);
            // barFill.GetComponent<RectTransform>().offsetMin = new Vector2(30, 0);
        }
        else
        {
            icon.SetActive(false);
            // barFill.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        }



        UpdateTitle(name);
        // UpdateStat();
    }

    public void UpdateTitle(string name)
    {
        if (useTextTitle == true)
        {
            textStatTitle.GetComponent<TMPro.TextMeshProUGUI>().text = $"{name}:";
        }
    }

    public void UpdateStat(
        CurlingPlayerStat stat
    )
    {
        statBar.UpdateValues(
            statValue: stat.baseValue,
            minValue: stat.min,
            maxValue: stat.max
        );
    }


    public void UpdateUI()
    {

    }
}