using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string id;
    public string version;
    public string firstName;
    public string lastName;
    public string fullName => $"{firstName} {lastName}";
    public string description;
    public string iconPath; // Path to the icon asset
    public string addressableModelPath; // Path to the model asset
    public CurlingPlayer curlingPlayerData;
    // public GameObject model;

    public void Start()
    {
        BuildRandomCurlingPlayerData();
    }

    public void BuildRandomCurlingPlayerData()
    {
        curlingPlayerData = new CurlingPlayer();
        curlingPlayerData.name = fullName;
        curlingPlayerData.characterId = id;
        curlingPlayerData.BuildRandomStats();
    }

}

