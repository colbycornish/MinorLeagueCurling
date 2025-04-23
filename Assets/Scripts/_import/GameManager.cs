
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 playerSpawnPosition;
    public string playerSpawnID;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Teleport the player to a new scene at a specified spawn position.
    /// </summary>
    // public void TeleportToScene(string sceneName, Vector3 spawnPosition)
    // {
    //     playerSpawnPosition = spawnPosition;
    //     StartCoroutine(LoadSceneWithFade(sceneName));
    // }
    

    public void TeleportToScene(string sceneName, string spawnID)
    {
        playerSpawnID = spawnID;
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Find the player before loading
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            player.SetActive(false);  // Disable the player BEFORE loading

        yield return FadeManager.Instance.FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return FadeManager.Instance.FadeIn();

        // Scene has loaded — find spawn point
        GameObject spawnPoint = FindSpawnPointByID(playerSpawnID);
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.SetActive(true);  // Enable the player AFTER positioning
        }
    }

    private GameObject FindSpawnPointByID(string id)
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (var sp in spawnPoints)
        {
            if (sp.spawnID == id)
                return sp.gameObject;
        }
        Debug.LogWarning("Spawn point with ID '" + id + "' not found.");
        return null;
    }
}
