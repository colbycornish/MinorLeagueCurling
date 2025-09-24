using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CharacterInfoDisplay : MonoBehaviour
{
    // public Sprite icon;
    [Header("Basic Info")]
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPosition;
    [SerializeField] private TextMeshProUGUI textDescription;

    [Header("Stats")]
    [SerializeField] private CharacterStatItem statItem1;
    [SerializeField] private CharacterStatItem statItem2;
    [SerializeField] private CharacterStatItem statItem3;

    public void UpdateUI()
    {

    }

    public void UpdateInfo(
        string name = null,
        string position = null,
        string description = null,
        Character character = null
    )
    {
        if (character != null)
        {
            UpdateName(character.fullName);
            UpdateAbility(character.description);
            CurlingPlayer curlingPlayerData = character.curlingPlayerData;
            CurlingPlayerStats characterStats = curlingPlayerData.stats;
            // if (characterStats != null)
            // {
            statItem1.UpdateStat(stat: characterStats.speed);
            statItem2.UpdateStat(stat: characterStats.strength);
            statItem3.UpdateStat(stat: characterStats.cooldownRate);
                // UpdateStats(
                //     stat1: characterStats.speed,
                //     stat2: characterStats.strength,
                //     stat3: characterStats.cooldownRate
                // );
            // }
        }
        else
        {
            if (name != null) { UpdateName(name); }
            if (position != null) { UpdatePosition(position); }
            if (description != null) { UpdateAbility(description); }
        }
    }

    public void UpdateName(string text)
    {
        if (textName != null)
        {
            textName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{text}";
        }
    }

    public void UpdatePosition(string text)
    {
        if (textPosition != null)
        {
            textPosition.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        }
    }

    public void UpdateAbility(string text)
    {
        if (textDescription != null)
        {
            textDescription.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        }
    }

    public void UpdateStats(
        CurlingPlayerStat stat1,
        CurlingPlayerStat stat2,
        CurlingPlayerStat stat3
    )
    {
        // statItem1.UpdateStat(
        //     // name: stat1.title,
        //     statValue: stat1.baseValue,
        //     statValueMin: stat1.min,
        //     statValueMax: stat1.max
        // );

        // statItem2.Init(
        //     // name: stat2.title,
        //     statValue: stat2.baseValue,
        //     statValueMin: stat2.min,
        //     statValueMax: stat2.max
        // );

        // statItem3.Init(
        //     // name: stat3.title,
        //     statValue: stat3.baseValue,
        //     statValueMin: stat3.min,
        //     statValueMax: stat3.max
        // );
    }
}
