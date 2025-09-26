using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


public class CanvasDisplayAreaController : MonoBehaviour
{
    // public Sprite icon;
    [Header("Game Objects")]
    [SerializeField] public GameObject characterCanvasDisplayPrefab;
    [SerializeField] public GameObject broomCanvasDisplayPrefab;
    [SerializeField] public GameObject stoneCanvasDisplayPrefab;
    [SerializeField] public List<GameObject> characterObjectsToLoad;
    [SerializeField] public List<GameObject> broomObjectsToLoad;
    [SerializeField] public List<GameObject> stoneObjectsToLoad;
    [SerializeField] public List<CanvasDisplaySingleCharacterController> characterItems;
    [SerializeField] public List<CanvasDisplaySingleBroomController> broomItems;
    [SerializeField] public List<CanvasDisplaySingleStoneController> stoneItems;

    [Header("Render Areas")]
    [SerializeField] public GameObject characterArea;
    [SerializeField] public GameObject broomArea;
    [SerializeField] public GameObject stoneArea;

    [Header("Spacing")]
    [SerializeField] public int spacingX;

    // [Header("Functional Indicators")]
    // public event Action OnCharactersLoaded;



    public void Start()
    {

        // broomArea
        LoadCharacters();
        LoadBrooms();

        // AdjustAreaZPosition(
        //     area: characterArea,
        //     zAdjustment: 0.0f
        // );

        // AdjustAreaZPosition(
        //     area: broomArea,
        //     zAdjustment: -20.0f
        // );

        // AdjustAreaZPosition(
        //     area: stoneArea,
        //     zAdjustment: -40.0f
        // );
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
            GameObject instance = Instantiate(characterCanvasDisplayPrefab, this.characterArea.transform);
            CanvasDisplaySingleCharacterController controller = instance.GetComponent<CanvasDisplaySingleCharacterController>();

            obj.SetLayerRecursively("UI-ObjectRenderer");

            controller.Init(
                characterPrefabInstance: obj,
                characterId: c.id
            );
            instance.SetLayerRecursively("UI-ObjectRenderer");

            if (controller != null)
            {
                AddCharacterItem(controller);
            }

            instance.transform.SetParent(this.characterArea.transform);
        }

        LayoutIntoGrid(area: characterArea);
    }

    public void AddCharacterItem(CanvasDisplaySingleCharacterController item){
        characterItems.Add(item);
    }

    public void RemoveCharacterItem(CanvasDisplaySingleCharacterController item){
        characterItems.Remove(item);
    }

    public void ClearCharacterItems(){
        characterItems = new List<CanvasDisplaySingleCharacterController>();
    }


    public CanvasDisplaySingleCharacterController FindCharacterById(string characterId)
    {
        foreach (CanvasDisplaySingleCharacterController item in characterItems)
        {
            if (item != null && item.id == characterId){
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
            if (getBody) return item.GetRenderTextureFull();
            else return item.GetRenderTextureFace();
        }
        return null;
    }

    public RenderTexture GetRandomCharacterRenderTexture(bool getFace = true, bool getBody = false)
    {
        if (characterItems.Count == 0) return null;
        int randomIndex = UnityEngine.Random.Range(0, characterItems.Count);
        CanvasDisplaySingleCharacterController item = characterItems[randomIndex];
        if (item != null)
        {
            if (getBody) return item.GetRenderTextureFull();
            else return item.GetRenderTextureFace();
        }
        return null;
    }


    /// <summary>
    /// Broom List
    /// </summary>

    public void LoadBrooms()
    {
        foreach (GameObject obj in broomObjectsToLoad)
        {
            CurlingBroom c = obj.GetComponent<CurlingBroom>();
            GameObject instance = Instantiate(broomCanvasDisplayPrefab, this.broomArea.transform);
            CanvasDisplaySingleBroomController controller = instance.GetComponent<CanvasDisplaySingleBroomController>();

            obj.SetLayerRecursively("UI-ObjectRenderer");

            controller.Init(
                broomPrefabInstance: obj,
                broomId: c.id
            );
            instance.SetLayerRecursively("UI-ObjectRenderer");

            if (controller != null)
            {
                AddBroomItem(controller);
            }

            instance.transform.SetParent(this.broomArea.transform);
        }

        LayoutIntoGrid(area: broomArea);
        // AdjustAreaZPosition(
        //     area: broomArea,
        //     zAdjustment: -20.0f
        // );
    }

    public void AddBroomItem(CanvasDisplaySingleBroomController item){
        broomItems.Add(item);
    }

    public void RemoveBroomItem(CanvasDisplaySingleBroomController item){
        broomItems.Remove(item);
    }

    public void ClearBroomItems(){
        broomItems = new List<CanvasDisplaySingleBroomController>();
    }


    public CanvasDisplaySingleBroomController FindBroomById(string broomId)
    {
        foreach (CanvasDisplaySingleBroomController item in broomItems)
        {
            if (item != null && item.id == broomId){
                return item;
            }
        }
        Debug.LogWarning($"Broom with ID: {broomId} not found.");
        return null;
    }

    public RenderTexture GetBroomRenderTextureById(
        string broomId,
        bool getFace = true,
        bool getBody = false
    )
    {
        CanvasDisplaySingleBroomController item = FindBroomById(broomId);
        if (item != null)
        {
            if (getBody) return item.GetRenderTextureFull();
            else return item.GetRenderTextureHead();
        }
        return null;
    }


    /// <summary>
    /// stones
    /// </summary>

     public void LoadStones()
    {
        foreach (GameObject obj in stoneObjectsToLoad)
        {
            CurlingStone c = obj.GetComponent<CurlingStone>();
            GameObject instance = Instantiate(stoneCanvasDisplayPrefab, this.stoneArea.transform);
            CanvasDisplaySingleStoneController controller = instance.GetComponent<CanvasDisplaySingleStoneController>();

            obj.SetLayerRecursively("UI-ObjectRenderer");

            controller.Init(
                stonePrefabInstance: obj,
                stoneId: c.id
            );
            instance.SetLayerRecursively("UI-ObjectRenderer");

            if (controller != null)
            {
                AddStoneItem(controller);
            }

            instance.transform.SetParent(this.stoneArea.transform);
        }

        LayoutIntoGrid(area: stoneArea);
        // AdjustAreaZPosition(
        //     area: broomArea,
        //     zAdjustment: -20.0f
        // );
    }

    public void AddStoneItem(CanvasDisplaySingleStoneController item){
        stoneItems.Add(item);
    }

    public void RemoveStoneItem(CanvasDisplaySingleStoneController item){
        stoneItems.Remove(item);
    }

    public void ClearStoneItems(){
        stoneItems = new List<CanvasDisplaySingleStoneController>();
    }


    public CanvasDisplaySingleStoneController FindStoneById(string stoneId)
    {
        foreach (CanvasDisplaySingleStoneController item in stoneItems)
        {
            if (item != null && item.id == stoneId){
                return item;
            }
        }
        Debug.LogWarning($"Stone with ID: {stoneId} not found.");
        return null;
    }

    public RenderTexture GetStoneRenderTextureById(
        string stoneId,
        bool getFace = true,
        bool getBody = false
    )
    {
        CanvasDisplaySingleStoneController item = FindStoneById(stoneId);
        if (item != null)
        {
            if (getBody) return item.GetRenderTextureFull();
            else return item.GetRenderTextureHead();
        }
        return null;
    } 
    

    // public RenderTexture GetRandomCharacterRenderTexture(bool getFace = true, bool getBody = false)
    // {
    //     if (characterItems.Count == 0) return null;
    //     int randomIndex = UnityEngine.Random.Range(0, characterItems.Count);
    //     CanvasDisplaySingleCharacterController item = characterItems[randomIndex];
    //     if (item != null)
    //     {
    //         if (getBody) return item.GetRenderTextureFull();
    //         else return item.GetRenderTextureFace();
    //     }
    //     return null;
    // }


    
    

    /// <summary>
    /// Utility
    /// </summary>

    public void LayoutIntoGrid(
        GameObject area
    )
    {
        int index = 0;
        int columns = 50;
        Vector2 scale = new Vector2(5, 5);
        bool subChildren = false;

        if (subChildren)
        {

        }
        else
        {
            foreach (Transform item in area.transform)
            {
                int row = index / columns;
                int column = index % columns;

                Vector3 position = new Vector3(scale.x * column, 0, row * -scale.y);
                item.transform.localPosition = position;
                index++;
            }
        }
    }

    public void AdjustAreaZPosition(
        GameObject area,
        float zAdjustment = 0.0f
    ){
        // Get the current position
        Vector3 currentPosition = area.transform.position;

        // Create a new Vector3 with the desired Z position, keeping X and Y the same
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + zAdjustment);

        // Assign the new position back to the transform
        area.transform.position = newPosition;
    }
}
