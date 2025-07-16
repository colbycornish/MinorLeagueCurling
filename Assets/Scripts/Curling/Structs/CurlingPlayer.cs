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
}
