using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemInteraction))]
public class ItemInteractionEditor : Editor
{
    private SerializedProperty notificationTextProp;
    private SerializedProperty interactKeyProp;
    private SerializedProperty itemDatabaseProp;
    private SerializedProperty selectedItemIdProp;

    private void OnEnable()
    {
        notificationTextProp = serializedObject.FindProperty("notficationText");
        interactKeyProp = serializedObject.FindProperty("interactKey");
        itemDatabaseProp = serializedObject.FindProperty("itemDatabase");
        selectedItemIdProp = serializedObject.FindProperty("selectedItemId");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(notificationTextProp);
        EditorGUILayout.PropertyField(interactKeyProp);
        EditorGUILayout.PropertyField(itemDatabaseProp);

        var itemInteraction = (ItemInteraction)target;

        if (itemInteraction.itemDatabase != null && itemInteraction.itemDatabase.items != null)
        {
            var items = itemInteraction.itemDatabase.items;
            string[] options = new string[items.Count];
            int currentIndex = 0;

            for (int i = 0; i < items.Count; i++)
            {
                options[i] = items[i].name;
                if (items[i].id == selectedItemIdProp.stringValue)
                {
                    currentIndex = i;
                }
            }

            int selectedIndex = EditorGUILayout.Popup("Select Item", currentIndex, options);
            selectedItemIdProp.stringValue = items[selectedIndex].id;
        }
        else
        {
            EditorGUILayout.HelpBox("Assign a valid item database to select an item.", MessageType.Info);
        }

        serializedObject.ApplyModifiedProperties();
    }
}