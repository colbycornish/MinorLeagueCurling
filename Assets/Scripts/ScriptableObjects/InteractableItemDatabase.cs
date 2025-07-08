using UnityEngine;
using System.Collections.Generic;
using System;


/// <summary>
/// ScriptableObject that holds a list of valid spawn point IDs.
/// </summary>
[CreateAssetMenu(fileName = "InteractableItemDatabase", menuName = "Game/Interactable Item Database")]
public class InteractableItemDatabase : ScriptableObject
{
    [Tooltip("List of valid interactable item IDs used across all scenes.")]
    public List<ItemData> items = new List<ItemData>();
}