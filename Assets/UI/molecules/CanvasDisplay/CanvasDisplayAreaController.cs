using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasDisplayAreaController : MonoBehaviour
{
    // public Sprite icon;
    [Header("Game Objects")]
    [SerializeField] public GameObject canvasDisplayPrefab;
    [SerializeField] public List<GameObject> characterObjectsToLoad;
    [SerializeField] public List<CanvasDisplaySingleCharacterController> characterItems;

    [Header("Spacing")]
    [SerializeField] public int spacingX;


    public void Start()
    {
        LoadCharacters();
    }

    public void CreateCharacterItems()
    {

    }

    public void UpdateUI()
    {

    }

    /// <summary>
    /// Character List
    /// </summary>

    public void LoadCharacters()
    {
        foreach (GameObject obj in characterObjectsToLoad)
        {
            Character c = obj.GetComponent<Character>();
            GameObject instance = Instantiate(canvasDisplayPrefab, this.transform);
            CanvasDisplaySingleCharacterController controller = instance.GetComponent<CanvasDisplaySingleCharacterController>();

            obj.SetLayerRecursively("UI-ObjectRenderer");

            controller.Init(
                characterPrefabInstance: obj,
                characterId: c.id
            );
            instance.SetLayerRecursively("UI-ObjectRenderer");

            if (controller != null)
            {
                AddItem(controller);
            }

            instance.transform.SetParent(this.transform);
        }

        LayoutIntoGrid();
    }


    public CanvasDisplaySingleCharacterController FindCharacterById(string characterId)
    {
        foreach (CanvasDisplaySingleCharacterController item in characterItems)
        {
            if (item != null && item.id == characterId)
            {
                Debug.Log($"Found character with ID: {characterId}");
                return item;
            }
        }
        Debug.LogWarning($"Character with ID: {characterId} not found.");
        return null;
    }

    public RenderTexture GetCharacterRenderTextureById(
        string characterId,
        bool getFace = true,
        bool getBody = false
    )
    {
        CanvasDisplaySingleCharacterController item = FindCharacterById(characterId);
        if (item != null)
        {
            if (getBody)
            {
                return item.GetRenderTextureFull();
            }
            else
            {
                return item.GetRenderTextureFace();
            }
        }
        return null;
    }

    public RenderTexture GetRandomCharacterRenderTexture(
        bool getFace = true,
        bool getBody = false
    )
    {
        if (characterItems.Count == 0) return null;

        int randomIndex = Random.Range(0, characterItems.Count);
        CanvasDisplaySingleCharacterController item = characterItems[randomIndex];
        if (item != null)
        {
            if (getBody)
            {
                return item.GetRenderTextureFull();
            }
            else
            {
                return item.GetRenderTextureFace();
            }
        }
        return null;
    }

    

    public void UnloadCharacters()
    {

    }

    public void AddItem(CanvasDisplaySingleCharacterController item)
    {
        characterItems.Add(item);
    }

    public void RemoveItem(CanvasDisplaySingleCharacterController item)
    {
        characterItems.Remove(item);
    }

    public void Clear()
    {
        characterItems = new List<CanvasDisplaySingleCharacterController>();
    }
    

    /// <summary>
    /// Utility
    /// </summary>

    public void LayoutIntoGrid()
    {
        int index = 0;
        int columns = 15;
        Vector2 scale = new Vector2(5, 5);
        bool subChildren = false;

        if (subChildren)
        {

        }
        else
        {
            foreach (Transform item in transform)
            {
                int row = index / columns;
                int column = index % columns;

                Vector3 position = new Vector3(scale.x * column, 0, row * -scale.y);
                item.transform.localPosition = position;
                index++;
            }
        }
    }
}
