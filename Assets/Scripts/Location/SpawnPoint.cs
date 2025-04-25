using UnityEngine;

/// <summary>
// Spawn points are locations where the player can spawn in the game world.
// This script is attached to spawn point objects in the scene.
/// </summary>

public class SpawnPoint : MonoBehaviour
{
    // The spawnID is used to identify the spawn point in the SpawnPointDatabase.
    // The spawnID should be unique for each spawn point in the scene.
    public string spawnID;
}