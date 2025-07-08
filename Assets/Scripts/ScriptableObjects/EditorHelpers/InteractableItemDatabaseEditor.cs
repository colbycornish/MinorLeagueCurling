using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractableItemDatabase))]
public class InteractableItemDatabaseEditor : Editor
{
    private SerializedProperty itemsProp;
    private bool showItems = true;

    private void OnEnable()
    {
        itemsProp = serializedObject.FindProperty("items"); 
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Item Database", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Refresh Items from JSON (Open Importer)"))
        {
            InteractableItemDatabaseImporter.ShowWindow();
        }

        EditorGUILayout.Space();
        showItems = EditorGUILayout.Foldout(showItems, $"Items ({itemsProp.arraySize})", true);
        if (showItems)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < itemsProp.arraySize; i++)
            {
                var item = itemsProp.GetArrayElementAtIndex(i);
                SerializedProperty id = item.FindPropertyRelative("id");
                SerializedProperty version = item.FindPropertyRelative("version");
                SerializedProperty name = item.FindPropertyRelative("name");
                SerializedProperty description = item.FindPropertyRelative("description");
                SerializedProperty type = item.FindPropertyRelative("type");
                SerializedProperty rarity = item.FindPropertyRelative("rarity");
                SerializedProperty iconPath = item.FindPropertyRelative("iconPath");
                SerializedProperty modelPath = item.FindPropertyRelative("modelPath");

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Item {i + 1}", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(id);
                EditorGUILayout.PropertyField(version);
                EditorGUILayout.PropertyField(name);
                EditorGUILayout.PropertyField(description);
                EditorGUILayout.PropertyField(type);
                EditorGUILayout.PropertyField(rarity);
                EditorGUILayout.PropertyField(iconPath);
                EditorGUILayout.PropertyField(modelPath);
                EditorGUILayout.EndVertical();
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}