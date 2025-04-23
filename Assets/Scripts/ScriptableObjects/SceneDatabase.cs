using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

/// <summary>
/// ScriptableObject that holds a list of valid scene names and supports auto-populating from Build Settings.
/// </summary>
[CreateAssetMenu(fileName = "SceneDatabase", menuName = "Game/Scene Database")]
public class SceneDatabase : ScriptableObject
{
    [Tooltip("List of scene names as defined in Build Settings.")]
    public List<string> sceneNames = new List<string>();

#if UNITY_EDITOR
    [ContextMenu("Populate from Build Settings")]
    public void PopulateFromBuildSettings()
    {
        sceneNames.Clear();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string path = scene.path;
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
                sceneNames.Add(sceneName);
            }
        }

        EditorUtility.SetDirty(this);
        Debug.Log("SceneDatabase populated from Build Settings.");
    }
#endif
}