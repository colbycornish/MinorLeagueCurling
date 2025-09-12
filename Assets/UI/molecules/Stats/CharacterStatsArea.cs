using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterStatsArea : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public CharacterStatItem statItem1;
    [SerializeField] public CharacterStatItem statItem2;
    [SerializeField] public CharacterStatItem statItem3;

    [Header("Settings")]
    [SerializeField] private bool useSpeed;
    [SerializeField] private bool useStrength;
    [SerializeField] private bool useCooldown;

    public void UpdateStats(
        CurlingPlayerStat stat1,
        CurlingPlayerStat stat2,
        CurlingPlayerStat stat3
    )
    {
        statItem1.Init(
            name: stat1.title,
            statValue: stat1.baseValue,
            statValueMin: stat1.min,
            statValueMax: stat1.max
        );

        statItem2.Init(
            name: stat2.title,
            statValue: stat2.baseValue,
            statValueMin: stat2.min,
            statValueMax: stat2.max
        );

        statItem3.Init(
            name: stat3.title,
            statValue: stat3.baseValue,
            statValueMin: stat3.min,
            statValueMax: stat3.max
        );
    }

}