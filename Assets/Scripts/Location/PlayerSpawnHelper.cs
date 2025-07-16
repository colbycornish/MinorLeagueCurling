
using UnityEngine;

/// <summary>
/// Helper script that positions the player at the correct spawn point when the scene loads.
/// </summary>
public class PlayerSpawnHelper : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = GameManager._instance.playerSpawnPosition;
        }
    }
}
