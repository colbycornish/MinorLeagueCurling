using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class InteractableItemDatabaseImporter : EditorWindow
{
    private string jsonFolderPath = "Assets/_Database/Items"; // Folder with JSON files
    private InteractableItemDatabase database;

    [MenuItem("Tools/Import Item JSONs")]
    public static void ShowWindow()
    {
        GetWindow<InteractableItemDatabaseImporter>("Item JSON Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Item JSONs", EditorStyles.boldLabel);

        database = (InteractableItemDatabase)EditorGUILayout.ObjectField("Database", database, typeof(InteractableItemDatabase), false);
        jsonFolderPath = EditorGUILayout.TextField("JSON Folder Path", jsonFolderPath);

        if (database == null)
        {
            if (GUILayout.Button("Create New Item Database"))
            {
                string path = EditorUtility.SaveFilePanelInProject("Save Item Database", "NewItemDatabase", "asset", "Select save location for the item database");
                if (!string.IsNullOrEmpty(path))
                {
                    database = ScriptableObject.CreateInstance<InteractableItemDatabase>();
                    AssetDatabase.CreateAsset(database, path);
                    AssetDatabase.SaveAssets();
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = database;
                }
            }

            EditorGUILayout.HelpBox("No ItemDatabase assigned. Create or assign one above.", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("Import JSONs"))
        {
            LoadItemsFromJson();
        }
    }

    private void LoadItemsFromJson()
    {
        database.items.Clear();

        string[] files = Directory.GetFiles(jsonFolderPath, "*.json");
        foreach (string file in files)
        {
            string json = File.ReadAllText(file);
            ItemData item = JsonUtility.FromJson<ItemData>(json);
            database.items.Add(item);
        }

        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        Debug.Log($"Imported {database.items.Count} items from JSON.");
    }
}