using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class CanvasDisplaySingleCharacterController : MonoBehaviour
{
    // public Sprite icon;
    [SerializeField] private string id;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject modelArea;
    [SerializeField] private Camera cameraFace;
    [SerializeField] private Camera cameraFull;
    [SerializeField][HideInInspector] private RenderTexture rtFace;
    [SerializeField][HideInInspector] private RenderTexture rtFull;

    public void Init(
        GameObject characterPrefabInstance,
        string characterId
    )
    {
        id = characterId;
        characterPrefab = characterPrefabInstance;
        SetupModel(characterPrefab);
        SetupRenderTextures();
    }

    public void UpdateUI()
    {

    }

    public void SetupModel(GameObject characterPrefabInstance)
    {
        if (model != null)
        {
            Destroy(model);
        }

        characterPrefabInstance.SetLayerRecursively("UI-Object");
        model = Instantiate(characterPrefabInstance, modelArea.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.Euler(0, 180, 0);
        model.transform.localScale = new Vector3(100, 100, 100);

    }

    public void SetupRenderTextures()
    {
        rtFace = new RenderTexture(256, 256, 24);
        cameraFace.targetTexture = rtFace;

        rtFull = new RenderTexture(256, 256, 24);
        cameraFull.targetTexture = rtFull;
    }

    public RenderTexture GetRenderTextureFace()
    {
        return cameraFace.targetTexture;
    }

    public RenderTexture GetRenderTextureFull()
    {

        return cameraFull.targetTexture;
    }
    

}


public static class GameObjectExtensions
{
    /// <summary>
    /// Sets the layer of the GameObject and all its children recursively.
    /// </summary>
    /// <param name="obj">The root GameObject.</param>
    /// <param name="newLayer">The new layer to assign (integer value).</param>
    public static void SetLayerRecursively(this GameObject obj, int newLayer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = newLayer; // Set the layer of the current GameObject

        // Iterate through all children and recursively call the method
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    /// <summary>
    /// Sets the layer of the GameObject and all its children recursively using a layer name.
    /// </summary>
    /// <param name="obj">The root GameObject.</param>
    /// <param name="layerName">The name of the new layer.</param>
    public static void SetLayerRecursively(this GameObject obj, string layerName)
    {
        int newLayer = LayerMask.NameToLayer(layerName);
        SetLayerRecursively(obj, newLayer);
    }
}