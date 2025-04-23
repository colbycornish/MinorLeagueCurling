using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject that holds a list of valid spawn point IDs.
/// </summary>
[CreateAssetMenu(fileName = "SpawnPointDatabase", menuName = "Game/Spawn Point Database")]
public class SpawnPointDatabase : ScriptableObject
{
    [Tooltip("List of valid spawn point IDs used across all scenes.")]
    public List<string> spawnPointIDs = new List<string>();
}