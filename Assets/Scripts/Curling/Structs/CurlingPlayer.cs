using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public struct CurlingPlayer
{
    [Header("Basic Data")]
    public string name;
    public string characterId;
    public Image avatarImage;
    public bool isThrower;
    public bool isSweeper;
    public bool isSweeperLeft;
    public bool isSweeperRight;

    [Header("Stats")]
    public CurlingPlayerStats stats;

    [Header("Model")]
    public GameObject character;
    public Rigidbody rb; // Rigidbody to apply force / detect motion
    public GameObject visual; // Optional: mesh or model 


    public void BuildRandomStats()
    {
        stats = new CurlingPlayerStats
        {
            exhaustionRate = new CurlingPlayerStat
            {
                baseValue = Random.Range(1, 100),
                min = 100,
                max = 1,
                current = Random.Range(1, 100)
            },
            cooldownRate = new CurlingPlayerStat
            {
                baseValue = Random.Range(1, 100),
                min = 100,
                max = 1,
                current = Random.Range(1, 100)
            },
            strength = new CurlingPlayerStat
            {
                baseValue = Random.Range(1, 100),
                min = 100,
                max = 1,
                current = Random.Range(1, 100)
            },
            stamina = new CurlingPlayerStat
            {
                baseValue = Random.Range(1, 100),
                min = 100,
                max = 1,
                current = Random.Range(1, 100)
            },
            speed = new CurlingPlayerStat
            {
                baseValue = Random.Range(1, 100),
                min = 100,
                max = 1,
                current = Random.Range(1, 100)
            },
            isSweeping = false,
            isExhausted = false,
            hasAdditionalBenefits = false
        };
    }
}
