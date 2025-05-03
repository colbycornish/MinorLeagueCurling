#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorwayTeleport))]
public class DoorwayTeleportEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DoorwayTeleport teleport = (DoorwayTeleport)target;

        teleport.promptUI = (GameObject)EditorGUILayout.ObjectField("Prompt UI", teleport.promptUI, typeof(GameObject), true);
        teleport.sceneDatabase = (SceneDatabase)EditorGUILayout.ObjectField("Scene Database", teleport.sceneDatabase, typeof(SceneDatabase), false);
        teleport.spawnDatabase = (SpawnPointDatabase)EditorGUILayout.ObjectField("Spawn Database", teleport.spawnDatabase, typeof(SpawnPointDatabase), false);

        // Scene selection dropdown
        if (teleport.sceneDatabase != null && teleport.sceneDatabase.sceneNames.Count > 0)
        {
            int sceneIndex = Mathf.Max(0, teleport.sceneDatabase.sceneNames.IndexOf(teleport.targetScene));
            sceneIndex = EditorGUILayout.Popup("Target Scene", sceneIndex, teleport.sceneDatabase.sceneNames.ToArray());
            teleport.targetScene = teleport.sceneDatabase.sceneNames[sceneIndex];
        }
        else
        {
            teleport.targetScene = EditorGUILayout.TextField("Target Scene", teleport.targetScene);
            if (teleport.sceneDatabase == null)
                EditorGUILayout.HelpBox("Assign a SceneDatabase to enable scene selection dropdown.", MessageType.Info);
            else if (teleport.sceneDatabase.sceneNames.Count == 0)
                EditorGUILayout.HelpBox("No scene names found in the database. Add scene names.", MessageType.Warning);
        }

        // Spawn point selection dropdown
        if (teleport.spawnDatabase != null && teleport.spawnDatabase.spawnPointIDs.Count > 0)
        {
            int selectedIndex = Mathf.Max(0, teleport.spawnDatabase.spawnPointIDs.IndexOf(teleport.targetSpawnID));
            selectedIndex = EditorGUILayout.Popup("Target Spawn ID", selectedIndex, teleport.spawnDatabase.spawnPointIDs.ToArray());
            teleport.targetSpawnID = teleport.spawnDatabase.spawnPointIDs[selectedIndex];
        }
        else
        {
            teleport.targetSpawnID = EditorGUILayout.TextField("Target Spawn ID", teleport.targetSpawnID);
            if (teleport.spawnDatabase == null)
                EditorGUILayout.HelpBox("Assign a SpawnPointDatabase to enable spawn selection dropdown.", MessageType.Info);
            else if (teleport.spawnDatabase.spawnPointIDs.Count == 0)
                EditorGUILayout.HelpBox("No spawn IDs found in the database. Add some spawn IDs.", MessageType.Warning);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(teleport);
        }
    }
}
#endif